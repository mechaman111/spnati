using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedPanel : Panel, ISkinControl, ISkinnedPanel
	{
		private SkinnedBackgroundType _type = SkinnedBackgroundType.Background;
		public SkinnedBackgroundType PanelType
		{
			get { return _type; }
			set
			{
				_type = value;
				Invalidate(true);
			}
		}

		private TabSide _tabSide = TabSide.None;
		public TabSide TabSide
		{
			get { return _tabSide; }
			set { _tabSide = value; Invalidate(); }
		}

		public SkinnedPanel()
		{
			DoubleBuffered = true;
		}

		public virtual void OnUpdateSkin(Skin skin)
		{
			BackColor = skin.GetBackColor(PanelType);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (_tabSide != TabSide.None)
			{
				ColorSet set = SkinManager.Instance.CurrentSkin.GetColorSet(PanelType);
				Pen borderPen = set.GetBorderPen(VisualState.Normal, false, Enabled);
				e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
			}

			base.OnPaint(e);
		}
	}

	public enum TabSide
	{
		None,
		Top,
		Left
	}

	public interface ISkinnedPanel
	{
		SkinnedBackgroundType PanelType { get; }
	}

	public static class SkinnedExtensions
	{
		/// <summary>
		/// Gets the back color of the nearest ISkinnedPanel ancestor
		/// </summary>
		/// <param name="ctl"></param>
		/// <returns></returns>
		public static Color GetSkinnedPanelBackColor(this ISkinControl skinCtl)
		{
			SkinManager.Instance.RegisterControl(skinCtl);
			Control ctl = skinCtl as Control;
			Control parent = ctl.Parent;
			while (parent != null)
			{
				ISkinnedPanel skinnedPanel = parent as ISkinnedPanel;
				if (skinnedPanel != null && skinnedPanel.PanelType != SkinnedBackgroundType.Transparent)
				{
					return SkinManager.Instance.CurrentSkin.GetBackColor(skinnedPanel.PanelType);
				}
				else if (parent is TabPage)
				{
					//TabPages get set by an associated SkinnedTabStrip, which isn't a direct ancestor
					return parent.BackColor;
				}
				parent = parent.Parent;
			}

			return ctl.BackColor;
		}

		public static Color GetSkinnedPanelForeColor(this ISkinControl skinCtl)
		{
			SkinManager.Instance.RegisterControl(skinCtl);
			Control ctl = skinCtl as Control;
			Control parent = ctl.Parent;
			while (parent != null)
			{
				ISkinnedPanel skinnedPanel = parent as ISkinnedPanel;
				if (skinnedPanel != null && skinnedPanel.PanelType != SkinnedBackgroundType.Transparent)
				{
					return SkinManager.Instance.CurrentSkin.GetForeColor(skinnedPanel.PanelType);
				}
				else if (parent is TabPage)
				{
					//TabPages get set by an associated SkinnedTabStrip, which isn't a direct ancestor
					return parent.ForeColor;
				}
				parent = parent.Parent;
			}

			return ctl.ForeColor;
		}
	}
}
