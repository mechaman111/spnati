using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class StageImageGrid : UserControl, ISkinControl
	{
		private const int CellSize = 30;
		private const int HeaderPadding = 10;

		public int RowHeaderWidth { get; set; } = 110;

		private Pen _border = new Pen(Color.DarkGray);

		private Character _character;
		private int _layerCount;
		private int _currentStage;
		private int _currentRow;
		private Point _highlightedCell = new Point(-1, -1);
		private Case _case;
		private bool _populating;

		private List<PoseMapping> _allowedPoses = new List<PoseMapping>();
		private List<HashSet<int>> _images = new List<HashSet<int>>();
		private List<PoseMapping> _selectedPoses = new List<PoseMapping>();

		public event EventHandler<int> CheckedChanged;
		public event EventHandler<Tuple<PoseMapping, int>> Preview;

		private HashSet<int> _allowedStages = new HashSet<int>();
		private List<HashSet<int>> _rowStages = new List<HashSet<int>>();

		private int _headerHeight = 100;
		public int ColumnHeaderHeight
		{
			get { return _headerHeight; }
			set { _headerHeight = value; }
		}

		public StageImageGrid()
		{
			InitializeComponent();
			panel.Paint += panel_Paint;
		}

		public List<StageImage> GetStages()
		{
			List<StageImage> list = new List<StageImage>();
			for (int i = 0; i < _images.Count; i++)
			{
				StageImage img = new StageImage();
				img.Stages.AddRange(_images[i]);
				img.Pose = _selectedPoses[i];
				list.Add(img);
			}
			return list;
		}

		public bool GetChecked(int index, int row)
		{
			if (row < 0 || row >= _images.Count)
			{
				return false;
			}
			HashSet<int> img = _images[row];
			return img.Contains(index);
		}

		public void OnUpdateSkin(Skin skin)
		{
			_border = skin.PrimaryColor.GetBorderPen(VisualState.Normal, false, Enabled);
		}

		public void SetData(Character character, Case workingCase, DialogueLine line)
		{
			_character = character;
			_case = workingCase;
			_currentStage = -1;
			_currentRow = -1;
			_images.Clear();
			_selectedPoses.Clear();
			_layerCount = character.Layers + Clothing.ExtraStages;

			HashSet<PoseMapping> poses = new HashSet<PoseMapping>();
			HashSet<PoseMapping> unusedPoses = new HashSet<PoseMapping>();
			//limit poses to those available in at least one selected stage
			foreach (int stage in workingCase.Stages)
			{
				_allowedStages.Add(stage);
				foreach (PoseMapping pose in character.PoseLibrary.GetPoses(stage))
				{
					poses.Add(pose);
					unusedPoses.Add(pose);
				}
			}
			List<PoseMapping> list = poses.ToList();
			list.Sort();
			_allowedPoses = list;

			foreach (StageImage img in line.Images)
			{
				unusedPoses.Remove(img.Pose);
				AddRow(img);
			}
			foreach (PoseMapping unused in unusedPoses)
			{
				StageImage placeholder = new StageImage(-1, unused);
				AddRow(placeholder);
			}
			AddRow(null);
		}

		private void AddRow(StageImage img)
		{
			_populating = true;
			HashSet<int> stages = new HashSet<int>();
			if (img != null)
			{
				stages.AddRange(img.Stages);
				foreach (int stage in stages)
				{
					if (_currentRow == -1 || _currentStage == -1)
					{
						_currentRow = _images.Count;
						_currentStage = stage;
						Preview?.Invoke(this, new Tuple<PoseMapping, int>(img.Pose, stage));
					}
					stages.Add(stage);
				}
				_images.Add(stages);
				_selectedPoses.Add(img.Pose);
			}
			SkinnedComboBox combo = new SkinnedComboBox();
			combo.DropDownStyle = ComboBoxStyle.DropDownList;
			combo.Width = RowHeaderWidth - 1;
			combo.Height = CellSize - 1;
			combo.FieldType = SkinnedFieldType.Surface;
			combo.OnUpdateSkin(SkinManager.Instance.CurrentSkin);
			combo.Location = new Point(1, _headerHeight + (_images.Count - (img == null ? 0 : 1)) * CellSize + 1);
			combo.DataSource = _allowedPoses;
			if (img != null)
			{
				combo.Tag = stages;
			}
			combo.SelectedIndexChanged += Combo_SelectedIndexChanged;
			if (img != null)
			{
				combo.SelectedItem = img.Pose;
			}
			Controls.Add(combo);
			combo.BringToFront();
			_populating = false;
		}

		private void Combo_SelectedIndexChanged(object sender, EventArgs e)
		{
			SkinnedComboBox box = sender as SkinnedComboBox;
			PoseMapping pose = box.SelectedItem as PoseMapping;
			if (pose != null)
			{
				HashSet<int> img = box.Tag as HashSet<int>;
				if (img == null)
				{
					img = new HashSet<int>();
					box.Tag = img;
					_images.Add(img);
					_selectedPoses.Add(pose);
					if (!_populating)
					{
						AddRow(null); //new row
					}
				}
				int index = _images.IndexOf(img);
				if (_rowStages.Count <= index)
				{
					_rowStages.Add(new HashSet<int>());
				}
				_selectedPoses[index] = pose;
				HashSet<int> stages = _rowStages[index];
				bool previewed = false;
				foreach (int stage in _case.Stages)
				{
					PoseReference poseRef = pose.GetPose(stage);
					bool selected = GetChecked(stage, index);
					if (poseRef != null)
					{
						stages.Add(stage);
						if (!previewed && selected && !_populating)
						{
							_currentRow = index;
							_currentStage = stage;
							Preview?.Invoke(this, new Tuple<PoseMapping, int>(pose, stage));
							previewed = true;
						}
					}
					else
					{
						stages.Remove(stage);
						if (selected)
						{
							ToggleStage(stage, index); //unselect invalid stage
						}
					}
				}
				if (!previewed && !_populating)
				{
					Preview?.Invoke(this, new Tuple<PoseMapping, int>(pose, _case.Stages[0]));
				}
				panel.Invalidate();
			}
		}

		private void panel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.Clear(Parent.BackColor);
			StringFormat sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };

			Skin skin = SkinManager.Instance.CurrentSkin;

			SolidBrush indicator = skin.SecondaryColor.GetBrush(VisualState.Normal, false, Enabled);

			int xOffset = RowHeaderWidth;

			if (_highlightedCell.X != -1)
			{
				using (SolidBrush pointBrush = new SolidBrush(Color.FromArgb(127, skin.SecondaryColor.Normal)))
				{
					g.FillRectangle(pointBrush, xOffset + _highlightedCell.X * CellSize, _headerHeight + _highlightedCell.Y * CellSize, CellSize, CellSize);
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
						g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

						int rows = _images.Count + 1;

						Image check = Properties.Resources.Checkmark;
						Image disabledCheck = Properties.Resources.CheckmarkDisabled;
						for (int row = 0; row < _images.Count + 1; row++)
						{
							for (int layer = 0; layer < _layerCount; layer++)
							{
								bool enabled = IsStageEnabled(layer, row);
								if (!enabled)
								{
									g.FillRectangle(disabledBack, xOffset + layer * CellSize, _headerHeight + CellSize * row, CellSize, CellSize);
								}

								if (layer == _currentStage && row == _currentRow)
								{
									g.FillRectangle(indicator, xOffset + layer * CellSize, _headerHeight + CellSize - 5 + CellSize * row, CellSize, 5);
								}

								bool stageChecked = row < _images.Count && _images[row].Contains(layer);
								if (stageChecked)
								{
									g.DrawImage(enabled ? check : disabledCheck, xOffset + layer * CellSize + CellSize / 2 - check.Width / 2, _headerHeight + CellSize * row + CellSize / 2 - check.Height / 2, check.Width, check.Height);
								}
							}
						}

						g.DrawLine(_border, xOffset + _headerHeight * 1.67f, 0, xOffset + _headerHeight * 1.67f + CellSize * _layerCount, 0); //top edge
						g.DrawLine(_border, 0, _headerHeight, 0, _headerHeight + CellSize * rows); //left edge

						//Cells
						for (int j = 0; j < _images.Count + 1; j++)
						{
							for (int i = 0; i < _layerCount; i++)
							{
								int x = CellSize * i;
								g.DrawLine(_border, xOffset + x, _headerHeight, xOffset + x, _headerHeight + rows * CellSize); //left grid line
								g.DrawLine(_border, xOffset + x, _headerHeight, xOffset + x + _headerHeight * 1.67f, 0); //grid diagonal
							}
							g.DrawLine(_border, 0, _headerHeight + CellSize * j, xOffset + CellSize * _layerCount, _headerHeight + CellSize * j); //top grid line
						}
						g.DrawLine(_border, xOffset + CellSize * _layerCount, _headerHeight, xOffset + CellSize * _layerCount, _headerHeight + CellSize * rows); //right edge
						g.DrawLine(_border, xOffset + CellSize * _layerCount, _headerHeight, xOffset + CellSize * _layerCount + _headerHeight * 1.67f, 0); //right diagonal edge
						g.DrawLine(_border, 0, _headerHeight + CellSize * rows, xOffset + CellSize * _layerCount, _headerHeight + CellSize * rows); //bottom edge

						//Column labels
						for (int i = 0; i < _layerCount; i++)
						{
							string label = GetLayerName(i);

							int x = xOffset + CellSize * i;
							g.TranslateTransform(x + 13, _headerHeight - 16); //fudging some numbers empirically to make it line up nice
							g.RotateTransform(-30);

							int width = (int)(Math.Sqrt((_headerHeight * _headerHeight) + (_headerHeight * 1.67f) * (_headerHeight * 1.67f)));
							g.DrawString(label, Font, fontBrush, new Rectangle(0, 0, width, CellSize), sf);

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
			int x = (e.X - RowHeaderWidth) / CellSize;
			int y = (e.Y - _headerHeight) / CellSize;
			Point oldPt = _highlightedCell;
			if (x >= 0 && x < _layerCount && y >= 0 && y < _images.Count && IsStageEnabled(x, y))
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

		public void ToggleStage(int stage, int row)
		{
			if (row < 0 || row >= _images.Count) { return; }
			HashSet<int> stages = _images[row];
			if (stages.Contains(stage))
			{
				if (_currentRow == row && _currentStage == stage)
				{
					_currentStage = -1;
					_currentStage = -1;
				}
				stages.Remove(stage);
			}
			else
			{
				stages.Add(stage);
			}
			CheckedChanged?.Invoke(this, stage);
			if (!_populating)
			{
				PoseMapping pose = _selectedPoses[row];
				Preview?.Invoke(this, new Tuple<PoseMapping, int>(pose, stage));
			}
		}

		private void panel_MouseDown(object sender, MouseEventArgs e)
		{
			int x = (e.X - RowHeaderWidth) / CellSize;
			int y = (e.Y - _headerHeight) / CellSize;

			if (x >= 0 && x < _layerCount && y >= 0 && y < _images.Count)
			{
				if (!IsStageEnabled(x, y)) { return; }
				if (e.Button == MouseButtons.Left)
				{
					ToggleStage(x, y);
				}
				else if (e.Button == MouseButtons.Right)
				{
					_currentRow = y;
					_currentStage = x;
					PoseMapping pose = _selectedPoses[_currentRow];
					Preview?.Invoke(this, new Tuple<PoseMapping, int>(pose, _currentStage));
				}
				panel.Invalidate();
			}
		}

		private bool IsStageEnabled(int i, int row)
		{
			if (_case == null) { return false; }
			return TriggerDatabase.UsedInStage(_case.Tag, _character, i) && (_allowedStages.Count == 0 || _allowedStages.Contains(i)) &&
				(row < _rowStages.Count && _rowStages[row].Contains(i));
		}
	}
}
