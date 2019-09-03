using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class StageGrid : UserControl, ISkinControl
	{
		private const int CellSize = 30;
		private const int HeaderPadding = 10;

		private Pen _border = new Pen(Color.DarkGray);

		private Character _character;
		private int _layerCount;
		private int _currentStage;
		private List<bool> _stages = new List<bool>();
		private Point _highlightedCell = new Point(-1, -1);
		private Case _case;
		private bool _populating;

		public event EventHandler CheckedChanged;
		public event EventHandler<int> LayerSelected;

		public HashSet<int> AllowedStages = new HashSet<int>();

		private int _headerHeight = 100;
		public int ColumnHeaderHeight
		{
			get { return _headerHeight; }
			set { _headerHeight = value; ResizeGrid(); }
		}

		public bool ShowSelectAll
		{
			get { return chkSelectAll.Visible; }
			set { chkSelectAll.Visible = value; }
		}

		public StageGrid()
		{
			InitializeComponent();
		}

		public bool GetChecked(int index)
		{
			return _stages[index];
		}

		public void OnUpdateSkin(Skin skin)
		{
			if (Parent != null)
			{
				lblTitle.BackColor = Parent.BackColor;
			}
			_border = skin.PrimaryColor.GetBorderPen(VisualState.Normal, false, Enabled);
		}

		private void ResizeGrid()
		{
			const int rows = 1;
			int cols = _layerCount;
			Width = (int)(cols * CellSize + _headerHeight * 1.67f);
			Height = rows * CellSize + _headerHeight;
			RecreateHandle();
		}

		public int GetPreviewStage()
		{
			return _currentStage;
		}

		public void SetPreviewStage(int currentStage)
		{
			_currentStage = currentStage;
			Invalidate();
		}

		public void SetData(Character character, Case workingCase, int currentStage)
		{
			_populating = true;
			_character = character;
			_case = workingCase;
			_currentStage = -1;
			chkSelectAll.Checked = false;
			_currentStage = currentStage;
			_stages.Clear();
			chkSelectAll.Enabled = (currentStage >= 0);
			_layerCount = character.Layers + Clothing.ExtraStages;
			for (int i = 0; i < _layerCount; i++)
			{
				_stages.Add(false);
			}
			if (workingCase != null)
			{
				foreach (int stage in workingCase.Stages)
				{
					_stages[stage] = true;
				}
			}

			UpdateCheckAllState();
			ResizeGrid();
			_populating = false;
		}

		private void panel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.Clear(Parent.BackColor);
			StringFormat sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };

			Skin skin = SkinManager.Instance.CurrentSkin;

			SolidBrush indicator = skin.SecondaryColor.GetBrush(VisualState.Normal, false, Enabled);

			if (_highlightedCell.X != -1)
			{
				using (SolidBrush pointBrush = new SolidBrush(Color.FromArgb(127, skin.SecondaryColor.Normal)))
				{
					g.FillRectangle(pointBrush, _highlightedCell.X * CellSize, _headerHeight + _highlightedCell.Y * CellSize, CellSize, CellSize);
				}
			}

			using (Brush headerBrush = new SolidBrush(Enabled ? skin.PrimaryForeColor : skin.Surface.DisabledForeColor))
			{
				g.DrawString("Stages", Skin.HeaderFont, headerBrush, -2, -3);
			}
			using (Brush disabledBrush = new SolidBrush(skin.Surface.DisabledForeColor))
			{
				using (Brush fontBrush = new SolidBrush(Enabled ? skin.Surface.ForeColor : skin.Surface.DisabledForeColor))
				{
					using (Brush disabledBack = new SolidBrush(skin.Background.Disabled))
					{
						if (ShowSelectAll)
						{
							g.DrawString("Select All", Skin.TextFont, fontBrush, chkSelectAll.Right + 2, chkSelectAll.Top);
						}

						g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

						Image check = Properties.Resources.Checkmark;
						Image disabledCheck = Properties.Resources.CheckmarkDisabled;
						for (int layer = 0; layer < _layerCount; layer++)
						{
							bool enabled = IsStageEnabled(layer);
							if (!enabled)
							{
								g.FillRectangle(disabledBack, layer * CellSize, _headerHeight, CellSize, CellSize);
							}

							if (layer == _currentStage)
							{
								g.FillRectangle(indicator, layer * CellSize, _headerHeight + CellSize - 5, CellSize, 5);
							}

							bool stageChecked = _stages[layer];
							if (stageChecked)
							{
								g.DrawImage(IsStageEnabled(layer) ? check : disabledCheck, layer * CellSize + CellSize / 2 - check.Width / 2, _headerHeight + CellSize / 2 - check.Height / 2, check.Width, check.Height);
							}
						}

						g.DrawLine(_border, 0, _headerHeight, 0, Height);
						g.DrawLine(_border, _headerHeight * 1.67f, 0, Width, 0);
						g.DrawLine(_border, 0, Height - 1, Width - _headerHeight * 1.67f, Height - 1);

						//Cells
						for (int i = 0; i < _layerCount; i++)
						{
							int x = CellSize * i;
							g.DrawLine(_border, x, _headerHeight, x, Height);
							g.DrawLine(_border, x, _headerHeight, x + _headerHeight * 1.67f, 0);
						}
						g.DrawLine(_border, Width - _headerHeight * 1.67f, _headerHeight, Width - _headerHeight * 1.67f, Height);
						g.DrawLine(_border, CellSize * _layerCount, _headerHeight, CellSize * _layerCount + _headerHeight * 1.67f, 0);
						g.DrawLine(_border, 0, _headerHeight, CellSize * _layerCount, _headerHeight);

						//Column labels
						for (int i = 0; i < _layerCount; i++)
						{
							string label = GetLayerName(i);

							int x = CellSize * i;
							g.TranslateTransform(x + 13, _headerHeight - 16); //fudging some numbers empirically to make it line up nice
							g.RotateTransform(-30);

							int width = (int)(Math.Sqrt((_headerHeight * _headerHeight) + (_headerHeight * 1.67f) * (_headerHeight * 1.67f)));
							g.DrawString(label, Font, IsStageEnabled(i) ? fontBrush : disabledBrush, new Rectangle(0, 0, width, CellSize), sf);

							g.ResetTransform();
						}
					}
				}
			}

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
		}

		private string GetLayerName(int layer)
		{
			StageName stage = _character.LayerToStageName(layer);
			return stage.DisplayName;
		}

		private void panel_MouseMove(object sender, MouseEventArgs e)
		{
			int x = e.X / CellSize;
			int y = (e.Y - _headerHeight) / CellSize;
			Point oldPt = _highlightedCell;
			if (x >= 0 && x < _layerCount && y == 0 && IsStageEnabled(x))
			{
				_highlightedCell.X = x;
				_highlightedCell.Y = y;

				Cursor = Cursors.Hand;
			}
			else
			{
				_highlightedCell.X = -1;
				_highlightedCell.Y = -1;
				Cursor = Cursors.Default;
			}
			if (_highlightedCell.X != oldPt.X || _highlightedCell.Y != oldPt.Y)
			{
				panel.Invalidate();
			}
		}

		private void panel_MouseLeave(object sender, System.EventArgs e)
		{
			if (_highlightedCell.X >= 0)
			{
				_highlightedCell.X = -1;
				_highlightedCell.Y = -1;
				Cursor = Cursors.Default;
				panel.Invalidate();
			}
		}

		public void ToggleStage(int stage)
		{
			_stages[stage] = !_stages[stage];
			_populating = true;
			CheckedChanged?.Invoke(this, EventArgs.Empty);
			UpdateCheckAllState();
			_populating = false;
		}

		private void panel_MouseDown(object sender, MouseEventArgs e)
		{
			int x = e.X / CellSize;
			int y = (e.Y - _headerHeight) / CellSize;
			if (e.X >= 18 && e.X <= 70 && e.Y >= 18 && e.Y <= 35 && ShowSelectAll)
			{
				chkSelectAll.Checked = !chkSelectAll.Checked;
			}
			else if (x >= 0 && x < _layerCount && y == 0)
			{
				if (!IsStageEnabled(x)) { return; }
				if (e.Button == MouseButtons.Left)
				{
					ToggleStage(x);
				}
				else if (e.Button == MouseButtons.Right && _stages[x])
				{
					LayerSelected?.Invoke(this, x);
				}
				panel.Invalidate();
			}
		}

		/// <summary>
		/// Updates the Select All checkbox based on the individual stage checkboxes
		/// </summary>
		private void UpdateCheckAllState()
		{
			bool allChecked = true;
			bool noneChecked = true;
			for (int i = 0; i < _stages.Count; i++)
			{
				if (_currentStage != i && IsStageEnabled(i))
				{
					if (_stages[i])
					{
						noneChecked = false;
					}
					else
					{
						allChecked = false;
					}
				}
			}
			if (chkSelectAll.Enabled)
			{
				chkSelectAll.CheckState = allChecked ? CheckState.Checked : noneChecked ? CheckState.Unchecked : CheckState.Indeterminate;
			}
			else
			{
				chkSelectAll.Checked = false;
			}
		}

		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			if (_currentStage == -1 || _populating)
			{
				return;
			}
			bool newState = chkSelectAll.Checked;
			for (int i = 0; i < _stages.Count; i++)
			{
				if (i == _currentStage)
					continue;
				if (IsStageEnabled(i))
				{
					_stages[i] = newState;
				}
			}
			Invalidate(true);
			CheckedChanged?.Invoke(this, e);
		}

		private bool IsStageEnabled(int i)
		{
			if (_case == null) { return false; }
			return TriggerDatabase.UsedInStage(_case.Tag, _character, i) && (AllowedStages.Count == 0 || AllowedStages.Contains(i));
		}
	}
}
