using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public sealed class SkinManager : IDisposable
	{
		private static SkinManager _instance;
		public static SkinManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new SkinManager();
				}
				return _instance;
			}
		}

		public List<Skin> AvailableSkins { get; } = new List<Skin>();
		public Skin CurrentSkin { get; private set; } = new Skin();

		public Pen FocusRectangle = new Pen(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };

		//Controls that are registered to get a color from an ISkinnedPanel ancestor
		//Because the right color could change anytime the control is moved into another hierarchy, we need to track all ancestors and run the event
		//whenever one of their parents changes
		private Dictionary<ISkinControl, HashSet<Control>> _hierarchyListeners = new Dictionary<ISkinControl, HashSet<Control>>();
		private Dictionary<ISkinControl, EventHandler> _hierarchyHandlers = new Dictionary<ISkinControl, EventHandler>();
		private HashSet<ISkinControl> _pendingUpdates = new HashSet<ISkinControl>();
		private Timer _timer = new Timer();

		private Dictionary<Color, SolidBrush> _colorBrushes = new Dictionary<Color, SolidBrush>();
		private Dictionary<Color, Pen> _colorPens = new Dictionary<Color, Pen>();

		private SkinManager()
		{
			_timer.Interval = 1;
			_timer.Tick += DispatchPendingUpdates;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				_timer.Dispose();
			}
		}

		public void LoadSkins(string path)
		{
			Skin defaultSkin = new Skin();

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			foreach (string file in Directory.EnumerateFiles(path, "*.skin"))
			{
				try
				{
					string json = File.ReadAllText(file);
					Skin skin = Json.Deserialize<Skin>(json);
					bool replaced = false;
					for (int i = 0; i < AvailableSkins.Count; i++)
					{
						if (AvailableSkins[i].Name == skin.Name)
						{
							AvailableSkins[i] = skin;
							replaced = true;
							break;
						}
					}
					if (!replaced)
					{
						AvailableSkins.Add(skin);
					}
					if (skin.Name == defaultSkin.Name)
					{
						defaultSkin = skin;
					}
				}
				catch { }
			}

			if (AvailableSkins.Count == 0)
			{
				AvailableSkins.Add(defaultSkin);
			}

			AvailableSkins.Sort();
			CurrentSkin = defaultSkin;
		}

		public void SetSkin(Skin skin)
		{
			CurrentSkin = skin;
			FocusRectangle.Color = skin.FocusRectangle;
			Shell.Instance.PostOffice.SendMessage(CoreDesktopMessages.SkinChanged, CurrentSkin);
		}

		public void DrawFocusRectangle(Graphics g, Rectangle clientRectangle)
		{
			g.DrawRectangle(FocusRectangle, clientRectangle.X + 2, clientRectangle.Y + 2, clientRectangle.Width - 5, clientRectangle.Height - 5);
		}

		public void ReskinControl(Control ctl, Skin skin)
		{
			UpdateSkin(ctl, skin);
			foreach (Control child in ctl.Controls)
			{
				UpdateSkin(child, skin);
			}
		}

		public static void UpdateSkin(Control ctl, Skin skin)
		{
			ISkinControl skinnedCtl = ctl as ISkinControl;
			ctl.Invalidate(true);
			if (skinnedCtl != null)
			{
				skinnedCtl.OnUpdateSkin(skin);
			}
			else if (ctl is ToolStrip)
			{
				ToolStrip ts = ctl as ToolStrip;
				ts.Font = Skin.TextFont;
				ts.Renderer = new SkinnedToolStripRenderer(skin, ts);

				foreach (ToolStripItem item in ts.Items)
				{
					ToolStripMenuItem mi = item as ToolStripMenuItem;
					if (mi != null)
					{
						ToolStripDropDown dropdown = mi.DropDown;
						if (dropdown != null)
						{
							Instance.ReskinControl(dropdown, skin);
						}
					}
				}
			}
			foreach (Control child in ctl.Controls)
			{
				UpdateSkin(child, skin);
			}
		}

		#region Hierarchical management
		public void RegisterControl(ISkinControl listener)
		{
			if (!_hierarchyListeners.ContainsKey(listener))
			{
				Control ctl = listener as Control;
				if (ctl != null)
				{
					ctl.HandleDestroyed += Ctl_HandleDestroyed;
				}
				ReplaceListeners(listener);
			}
		}

		private void Ctl_HandleDestroyed(object sender, EventArgs e)
		{
			UnregisterControl(sender as ISkinControl);
		}

		private void ReplaceListeners(ISkinControl listener)
		{
			HashSet<Control> set;
			EventHandler handler;
			UnregisterControl(listener);
			handler = new EventHandler((object sender, EventArgs e) =>
			{
				EnqueueHierarchyChangeEvent(listener);
			});
			_hierarchyHandlers[listener] = handler;
			set = new HashSet<Control>();
			_hierarchyListeners[listener] = set;
			Control ctl = listener as Control;
			while (ctl != null)
			{
				ctl.ParentChanged += handler;
				set.Add(ctl);
				ctl = ctl.Parent;
			}
		}

		public void UnregisterControl(ISkinControl listener)
		{
			HashSet<Control> set;
			EventHandler handler;
			if (_hierarchyHandlers.TryGetValue(listener, out handler))
			{
				if (_hierarchyListeners.TryGetValue(listener, out set))
				{
					foreach (Control c in set)
					{
						c.ParentChanged -= handler;
					}
					set.Clear();
				}
				_hierarchyHandlers.Remove(listener);
			}
		}

		private void EnqueueHierarchyChangeEvent(ISkinControl listener)
		{
			Control ctl = listener as Control;
			if (ctl != null && ctl.IsDisposed)
			{
				return;
			}
			_pendingUpdates.Add(listener);
			_timer.Start();
		}

		private void DispatchPendingUpdates(object sender, EventArgs e)
		{
			Skin skin = CurrentSkin;
			_timer.Stop();
			foreach (ISkinControl listener in _pendingUpdates)
			{
				Control ctl = listener as Control;
				if (ctl != null && ctl.IsDisposed)
				{
					continue;
				}
				listener.OnUpdateSkin(skin);
			}
			_pendingUpdates.Clear();
		}
		#endregion


		/// <summary>
		/// Gets and caches a brush with the given color
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public SolidBrush GetBrush(Color color)
		{
			SolidBrush brush;
			if (!_colorBrushes.TryGetValue(color, out brush))
			{
				brush = new SolidBrush(color);
				_colorBrushes[color] = brush;
			}
			return brush;
		}

		/// <summary>
		/// Gets and caches a pen with the given color
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public Pen GetPen(Color color)
		{
			Pen pen;
			if (!_colorPens.TryGetValue(color, out pen))
			{
				pen = new Pen(color);
				_colorPens[color] = pen;
			}
			return pen;
		}
	}
}
