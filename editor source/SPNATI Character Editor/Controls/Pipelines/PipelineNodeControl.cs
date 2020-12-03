using Desktop;
using Desktop.CommonControls;
using Desktop.Skinning;
using ImagePipeline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SPNATI_Character_Editor.Controls.Pipelines.NodeControls;

namespace SPNATI_Character_Editor.Controls.Pipelines
{
	public partial class PipelineNodeControl : SelectablePanel, ISkinControl
	{
		public event EventHandler PreviewInvalidated;
		public event EventHandler HeaderClicked;
		public event EventHandler<PortEventArgs> InputPortGrabbed;
		public event EventHandler<PortEventArgs> OutputPortGrabbed;
		public event EventHandler<PortEventArgs> InputPortReleased;
		public event EventHandler<PortEventArgs> InputHovered;
		public event EventHandler PreviewChanged;
		public event EventHandler Deleted;

		public VisualState MouseState { get; private set; }
		private Font _font = new Font("Arial", 8);
		private Font _portFont = new Font("Arial", 7);
		private StringFormat _headerFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };

		public const int BoxWidth = 175;
		private const int HeaderHeight = 20;
		private const int BoxPadding = 5;
		public const int PortRadius = BoxPadding;
		private const int PortPadding = 3;
		private const int PreviewPadding = 1;
		private const int PreviewSize = BoxWidth - BoxPadding * 2 - PreviewPadding * 2;
		private SolidBrush _backColor = new SolidBrush(Color.Gray);
		private SolidBrush _fontBrush = new SolidBrush(Color.Black);
		private SolidBrush _portBrush = new SolidBrush(Color.Black);
		private bool _focused;
		private bool _dragging;
		private Point _mouseOffset;
		private int _previewIndex = -1;
		private PictureBox _previewBox;

		private bool _hoverValid = true;
		private int _hoverInput = -1;
		private int _hoverOutput = -1;

		public Bitmap Preview;

		public PipelineNodeControl()
		{
			InitializeComponent();
			BackColor = Color.Transparent;
		}

		public bool IsMasterNode
		{
			get { return Node.Graph.MasterNode == Node; }
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (DesignMode) { return; }

			MouseLeave += Control_MouseLeave;
			MouseDown += Control_MouseDown;
			MouseUp += Control_MouseUp;
			MouseMove += Control_MouseMove;
			Enter += Control_Enter;
			Leave += Control_Leave;
		}

		private void Control_Leave(object sender, EventArgs e)
		{
			_focused = false;
			Invalidate();
		}

		private void Control_Enter(object sender, EventArgs e)
		{
			_focused = true;
			BringToFront();
			Invalidate();
		}

		public void Destroy()
		{
			_node.PropertyChanged -= _node_PropertyChanged;
			MouseLeave -= Control_MouseLeave;
			MouseDown -= Control_MouseDown;
			MouseMove -= Control_MouseMove;
			MouseUp -= Control_MouseUp;
			Enter -= Control_Enter;
			Leave -= Control_Leave;
		}

		private void Control_MouseLeave(object sender, EventArgs e)
		{
			_hoverOutput = -1;
			_hoverInput = -1;
			Invalidate();
		}

		private void Control_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (e.Y < HeaderHeight + BoxPadding)
				{
					_dragging = true;
					HeaderClicked?.Invoke(this, EventArgs.Empty);
					_mouseOffset = e.Location;
				}
				else
				{
					//see if a port was selected
					if (_hoverInput >= 0)
					{
						InputPortGrabbed?.Invoke(this, new PortEventArgs(_hoverInput, e.Location));
					}
					else if (_hoverOutput >= 0)
					{
						OutputPortGrabbed?.Invoke(this, new PortEventArgs(_hoverOutput, e.Location));
					}
				}

				Invalidate();
			}
		}

		public void ParentMouseUp(MouseEventArgs e, Point screenPosition)
		{
			Point client = PointToClient(screenPosition);
			MouseEventArgs args = new MouseEventArgs(e.Button, e.Clicks, client.X, client.Y, e.Delta);
			Control_MouseUp(this, args);
		}

		private void Control_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_dragging = false;

				if (_hoverInput >= 0)
				{
					InputPortReleased?.Invoke(this, new PortEventArgs(_hoverInput, e.Location));
				}
			}
			Invalidate();
		}

		public void ParentMouseMove(MouseEventArgs e, Point screenPosition)
		{
			Point client = PointToClient(screenPosition);
			MouseEventArgs args = new MouseEventArgs(e.Button, e.Clicks, client.X, client.Y, e.Delta);
			Control_MouseMove(this, args);
		}

		private void Control_MouseMove(object sender, MouseEventArgs e)
		{
			if (_dragging)
			{
				Point newOffset = new Point(e.Location.X - _mouseOffset.X, e.Location.Y - _mouseOffset.Y);
				Left += newOffset.X;
				Top += newOffset.Y;
				Panel parent = Parent as Panel;
				Node.X = Left + parent.HorizontalScroll.Value;
				Node.Y = Top + parent.VerticalScroll.Value;
			}
			else
			{
				int x = e.X;
				int y = e.Y;
				_hoverInput = -1;
				_hoverOutput = -1;
				_hoverValid = true;
				//see if a port is being hovered over
				if (Node.Definition.Inputs != null)
				{
					for (int i = 0; i < Node.Definition.Inputs.Length; i++)
					{
						Point pt = GetPortPosition(i, true);
						if (Math.Abs(pt.X - x) <= BoxPadding && Math.Abs(pt.Y - y) <= BoxPadding)
						{
							_hoverInput = i;
							PortEventArgs args = new PortEventArgs(i, pt);
							InputHovered?.Invoke(this, args);
							_hoverValid = args.IsValid;

							break;
						}
					}
				}
				if (_hoverInput == -1)
				{
					if (Node.Definition.Outputs != null)
					{
						for (int i = 0; i < Node.Definition.Outputs.Length; i++)
						{
							Point pt = GetPortPosition(i, false);
							if (Math.Abs(pt.X - x) <= BoxPadding && Math.Abs(pt.Y - y) <= BoxPadding)
							{
								_hoverOutput = i;
								break;
							}
						}
					}
				}
			}
			if (_dragging || _hoverInput >= 0 || _hoverOutput >= 0)
			{
				Invalidate();
			}
		}

		private PipelineNode _node;
		public PipelineNode Node
		{
			get { return _node; }
			set
			{
				_node = value;
				_node.PropertyChanged += _node_PropertyChanged;
				RenderControls();
			}
		}

		private void RenderControls()
		{
			Width = BoxWidth;
			int y = HeaderHeight;
			PortDefinition[] outputs = Node.Definition.Outputs;
			if (IsMasterNode)
			{
				_previewIndex = 0;
			}
			else if (outputs != null)
			{
				for (int i = 0; i < outputs.Length; i++)
				{
					PortDefinition port = outputs[i];
					if (port.Type == PortType.Bitmap)
					{
						_previewIndex = i;
						break;
					}
				}
			}
			PortDefinition[] inputs = Node.Definition.Inputs;
			int portCount = Math.Max(outputs?.Length ?? 0, inputs?.Length ?? 0);
			y += portCount * (BoxPadding * 2 + PortPadding) + BoxPadding + PortPadding;

			//Property fields
			NodeProperty[] properties = Node.Definition.Properties;
			if (properties != null)
			{
				int maxLabelWidth = 0;
				for (int i = 0; i < properties.Length; i++)
				{
					using (Graphics g = Graphics.FromHwnd(this.Handle))
					{
						string name = (properties[i].Name ?? "") + ":";
						maxLabelWidth = Math.Max(maxLabelWidth, (int)g.MeasureString(name, _font).Width);
					}
				}
				for (int i = 0; i < properties.Length; i++)
				{
					y = RenderPropertyControl(properties[i], i, y, maxLabelWidth);
				}
			}

			if (_previewIndex >= 0)
			{
				PictureBox box = new PictureBox();
				_previewBox = box;
				box.Left = BoxPadding + PreviewPadding;
				box.Top = y;
				box.Width = Width - BoxPadding * 2 - PreviewPadding * 2;
				box.Height = box.Width;
				y += box.Height + PreviewPadding;
				box.SizeMode = PictureBoxSizeMode.Zoom;
				Controls.Add(box);
			}

			y += BoxPadding;
			Height = y;
			InvalidatePreview();
		}

		private int RenderPropertyControl(NodeProperty property, int index, int top, int labelWidth)
		{
			Control ctl = null;
			switch (property.Type)
			{
				case NodePropertyType.Integer:
					if (property.DataType != null && property.DataType.IsEnum)
					{
						ctl = new NodeEnumControl();
					}
					else
					{
						ctl = new NodeNumberControl();
					}
					break;
				case NodePropertyType.Float:
					if (property.DataType == typeof(bool))
					{
						ctl = new NodeFloatControl();
					}
					else
					{
						ctl = new NodeSliderControl();
					}
					break;
				case NodePropertyType.ImageFile:
					ctl = new NodeFileControl();
					break;
				case NodePropertyType.CellReference:
					ctl = new NodeCellControl();
					break;
				case NodePropertyType.Point:
					ctl = new NodePointControl();
					break;
				case NodePropertyType.Boolean:
					ctl = new NodeBooleanControl();
					break;
				case NodePropertyType.Color:
					ctl = new NodeColorControl();
					break;
				case NodePropertyType.String:
					if (typeof(IRecord).IsAssignableFrom(property.DataType))
					{
						ctl = new NodeRecordControl();
					}
					else
					{
						ctl = new NodeTextControl();
					}
					break;
			}

			if (ctl != null)
			{
				SkinnedLabel label = new SkinnedLabel();
				label.Text = property.Name + ":";
				label.Left = BoxPadding * 2;
				label.Top = top + 3;
				label.Level = SkinnedLabelLevel.Label;
				int width = labelWidth;
				label.Width = width + 3;
				Controls.Add(label);
				ctl.Left = label.Right + BoxPadding;
				ctl.Width = BoxWidth - BoxPadding * 2 - ctl.Left;
				ctl.Top = top;
				Controls.Add(ctl);
				INodeControl nodeCtl = ctl as INodeControl;
				label.OnUpdateSkin(SkinManager.Instance.CurrentSkin);
				label.BackColor = Color.Transparent;
				nodeCtl.SetData(Node, index);
				return top + BoxPadding + ctl.Height;
			}
			return top;
		}

		private void InvalidatePreview()
		{
			PreviewInvalidated?.Invoke(this, EventArgs.Empty);
		}

		public void SetPreview(PipelineResult preview)
		{
			Unshield();
			if (_previewBox == null)
			{
				return;
			}
			Preview?.Dispose();
			Preview = null;
			if (preview != null)
			{
				foreach (object result in preview.Results)
				{
					Bitmap bmp = (result as DirectBitmap)?.Bitmap ?? (result as Bitmap);
					if (bmp != null)
					{
						//int previewWidth = PreviewSize;
						int width = bmp.Width;
						int height = (int)(width * ((float)bmp.Height / bmp.Width));
						Preview = new Bitmap(bmp, width, height);
						break;
					}
				}
			}
			_previewBox.Image = Preview;
			PreviewChanged?.Invoke(this, EventArgs.Empty);
			Invalidate();
		}

		private Shield _shield;
		public void Shield()
		{
			if (_shield != null || _previewBox == null) { return; }
			_shield = new Shield();
			_shield.Location = _previewBox.Location;
			_shield.Size = _previewBox.Size;
			Controls.Add(_shield);
			_shield.BringToFront();
		}

		public void Unshield()
		{
			if (_shield == null || _previewBox == null) { return; }
			Controls.Remove(_shield);
			_shield.Dispose();
			_shield = null;
		}

		public override void OnUpdateSkin(Skin skin)
		{
			base.OnUpdateSkin(skin);
			_backColor.Color = skin.Surface.Normal;
			_fontBrush.Color = skin.PrimaryLightColor.ForeColor;
			_portBrush.Color = skin.Surface.ForeColor;
		}

		private void panel_Paint(object sender, PaintEventArgs e)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			Graphics g = e.Graphics;

			g.FillRectangle(_backColor, BoxPadding, BoxPadding, Width - BoxPadding * 2, Height - BoxPadding * 2);
			ColorSet borderSet = skin.Surface;

			//title
			Rectangle headerRect = new Rectangle(BoxPadding, BoxPadding, Width - BoxPadding * 2, HeaderHeight);
			g.FillRectangle(skin.PrimaryLightColor.GetBrush(VisualState.Normal, false, true), headerRect);
			string title = Node.Definition.Name;
			g.DrawString(title, _font, _fontBrush, headerRect, _headerFormat);

			//border
			Pen borderPen = _focused ? Pens.White : borderSet.GetBorderPen(MouseState, false, Enabled);
			g.DrawRectangle(borderPen, BoxPadding, BoxPadding, Width - BoxPadding * 2, Height - BoxPadding * 2);
			if (_focused)
			{
				g.DrawRectangle(Pens.Black, BoxPadding - 1, BoxPadding - 1, Width - BoxPadding * 2 + 2, Height - BoxPadding * 2 + 2);
			}

			//ports
			PortDefinition[] inputs = Node.Definition.Inputs;
			DrawPorts(g, inputs, true);
			if (!IsMasterNode)
			{
				PortDefinition[] outputs = Node.Definition.Outputs;
				DrawPorts(g, outputs, false);
			}
		}

		public Point GetPortPosition(int index, bool input)
		{
			int size = BoxPadding * 2;
			int x = input ? ClientRectangle.X + BoxPadding : ClientRectangle.X + Width - size - 1 + BoxPadding;
			int y = ClientRectangle.Y + BoxPadding + HeaderHeight + PortPadding + BoxPadding + index * (size + PortPadding);
			return new Point(x, y);
		}

		private void DrawPorts(Graphics g, PortDefinition[] ports, bool input)
		{
			if (ports == null)
			{
				return;
			}

			for (int i = 0; i < ports.Length; i++)
			{
				if (input)
				{
					PortConnection connection = Node.Graph.GetInput(Node, i);
					DrawPort(g, i, input, connection != null);
				}
				else
				{
					List<PortConnection> outputs = Node.Graph.GetOutputs(Node, i);
					DrawPort(g, i, input, outputs != null && outputs.Count > 0);
				}
				
			}
		}

		private void DrawPort(Graphics g, int index, bool input, bool hasConnection)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			bool hovered = (input ? _hoverInput == index : _hoverOutput == index);
			bool invalid = hovered && !_hoverValid;
			ColorSet set = invalid ? skin.Critical : (hovered ? skin.SecondaryWidget : skin.PrimaryWidget);
			SolidBrush brush = set.GetBrush(VisualState.Normal, hovered, true);
			Pen pen = set.GetPen(VisualState.Normal, hovered, true);
			int size = BoxPadding * 2;

			Point pt = GetPortPosition(index, input);
			if (hasConnection)
			{
				g.FillEllipse(brush, pt.X - BoxPadding, pt.Y - BoxPadding, size, size);
			}
			else
			{
				g.FillEllipse(skin.Background.GetBrush(VisualState.Normal, false, true), pt.X - BoxPadding, pt.Y - BoxPadding, size, size);
				g.DrawEllipse(pen, pt.X - BoxPadding, pt.Y - BoxPadding, size, size);
			}

			//label
			PortDefinition port = input ? Node.Definition.Inputs[index] : Node.Definition.Outputs[index];
			string label = port.Name;
			Rectangle rect = input
				? new Rectangle(BoxPadding * 2, pt.Y - BoxPadding, BoxWidth / 2 - BoxPadding * 2, BoxPadding * 2)
				: new Rectangle(BoxWidth / 2, pt.Y - BoxPadding, BoxWidth / 2 - BoxPadding * 2, BoxPadding * 2);
			using (StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = input ? StringAlignment.Near : StringAlignment.Far })
			{
				g.DrawString(label, _portFont, _portBrush, rect, sf);
			}
		}

		public Size GetDimensions()
		{
			int height = BoxWidth;
			return new Size(BoxWidth, height);
		}

		private void _node_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Properties")
			{
				InvalidatePreview();
			}
		}

		private void PipelineNodeControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
				Deleted?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public class PortEventArgs : EventArgs
	{
		public int Index { get; set; }
		public Point MousePosition { get; set; }
		public bool IsValid { get; set; } = true;

		public PortEventArgs(int index, Point pt)
		{
			Index = index;
			MousePosition = pt;
		}
	}
}
