using Desktop.Skinning;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class DialogueGrid : UserControl, ISkinControl
	{
		private Case _selectedCase;
		private Stage _selectedStage;
		private Character _character;
		private bool _populatingCase;
		private int _selectedRow;
		private IDialogueDropDownControl _markerCtl;
		private ToolStripDropDown _markerDropDown;
		private IDialogueDropDownControl _lineCtl;
		private ToolStripDropDown _lineDropDown;
		private ToolStripDropDown _activeDropdown;
		private bool _modifyingLine;

		#region Events
		public new event EventHandler<KeyEventArgs> KeyDown;
		public event EventHandler<int> TextUpdated;
		public event EventHandler<int> HighlightRow;
		#endregion

		private TextBox _editBox;
		private IntellisenseControl _intellisense;

		private bool _readOnly;
		public bool ReadOnly
		{
			get { return _readOnly; }
			set
			{
				_readOnly = value;
				if (_readOnly)
				{
					gridDialogue.AllowUserToAddRows = false;
					gridDialogue.EditMode = DataGridViewEditMode.EditProgrammatically;
					ColDelete.Visible = false;
				}
				else
				{
					gridDialogue.AllowUserToAddRows = true;
					gridDialogue.EditMode = DataGridViewEditMode.EditOnEnter;
					ColDelete.Visible = true;
				}
			}
		}

		public DialogueGrid()
		{
			InitializeComponent();

			ColTrophy.Flat = ColDelete.Flat = ColMore.Flat = ColMarkerOptions.Flat = ColImageOptions.Flat = true;

			_markerCtl = new MarkerOptions();
			CreateDropdown(_markerCtl, out _markerDropDown);

			_lineCtl = new DialogueAdvancedControl();
			CreateDropdown(_lineCtl, out _lineDropDown);

			ColImage.ValueType = typeof(PoseMapping);

			_intellisense = new IntellisenseControl();
			_intellisense.InsertSnippet += _intellisense_InsertSnippet;
			Controls.Add(_intellisense);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			foreach (Control ctl in Controls)
			{
				ctl.MouseDown += Ctl_MouseDown;
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			if (!Visible)
			{
				HideDropdown();
			}
			base.OnVisibleChanged(e);
		}

		private void Ctl_MouseDown(object sender, MouseEventArgs e)
		{
			HideDropdown();
		}

		private void CreateDropdown(IDialogueDropDownControl ctl, out ToolStripDropDown dropdown)
		{
			Control hostCtl = ctl as Control;
			dropdown = new ToolStripDropDown();
			dropdown.Tag = ctl;
			ToolStripControlHost host = new ToolStripControlHost(hostCtl);
			host.Margin = new Padding(0);
			dropdown.Padding = new Padding(0);
			dropdown.Items.Add(host);
			dropdown.Closing += DropDownClosing;
			ctl.DataUpdated += Ctl_DataUpdated;
		}

		public void SetStage(Stage stage, HashSet<int> stages)
		{
			_selectedStage = stage;
			_modifyingLine = true;
			UpdateAvailableImagesForCase(stages, true);
			HighlightRow?.Invoke(this, GetHighlightedLineIndex());
			_modifyingLine = false;
		}

		public void SetData(Character character, Case c)
		{
			Stage stage = null;
			HashSet<int> stages = new HashSet<int>();
			foreach (int s in c.Stages)
			{
				if (stage == null)
				{
					stage = new Stage(s);
				}
				stages.Add(s);
			}
			SetData(character, stage, c, stages);
		}

		public void SetData(Character character, Stage stage, Case c, HashSet<int> selectedStages)
		{
			HideDropdown();
			_character = character;
			_selectedStage = stage;
			_selectedCase = c;
			_populatingCase = true;

			for (int i = 0; i < gridDialogue.Rows.Count; i++)
			{
				DataGridViewRow row = gridDialogue.Rows[i];
				DialogueLine line = row.Tag as DialogueLine;
				if (line != null)
				{
					line.PropertyChanged -= Line_PropertyChanged;
				}
				row.Tag = null;
				row.Cells["ColImage"].Value = null;
			}

			UpdateAvailableImagesForCase(selectedStages, false);

			//Populate lines
			gridDialogue.Rows.Clear();
			if (_selectedCase != null)
			{
				List<DialogueLine> lines = _selectedCase.Lines;
				foreach (DialogueLine line in lines)
				{
					AddLineToDialogueGrid(line, null);
				}
				if (lines.Count > 0)
					SelectRow(0);
			}
			_populatingCase = false;
		}

		public void Save()
		{
			HideDropdown();
			List<DialogueLine> lines = _selectedCase.Lines;
			if (_character != null)
			{
				foreach (DialogueLine line in lines)
				{
					_character.RemoveMarkerReference(line.Marker);
				}
			}
			for (int i = 0; i < gridDialogue.Rows.Count; i++)
			{
				DialogueLine line = ReadLineFromDialogueGrid(i);
				if (line != null)
				{
					if (!_selectedCase.Lines.Contains(line))
					{
						_selectedCase.Lines.Add(line);
						NotifyChange();
					}
					if (_character != null)
					{
						_character.CacheMarker(line.Marker);
						foreach (MarkerOperation op in line.Markers)
						{
							string name = op.Name;
							if (name.EndsWith("*"))
							{
								name = name.Substring(0, name.Length - 1);
							}
							_character.CacheMarker(name);
						}
					}
				}
			}
		}

		private void SelectRow(int index)
		{
			_selectedRow = index;
			HighlightRow?.Invoke(this, index);
		}

		/// <summary>
		/// Converts a row in the dialogue grid into a DialogueLine
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <returns></returns>
		private DialogueLine ReadLineFromDialogueGrid(int rowIndex)
		{
			DataGridViewRow row = gridDialogue.Rows[rowIndex];
			DialogueLine line = row.Tag as DialogueLine;
			bool newLine = false;
			if (line == null)
			{
				line = new DialogueLine();
				row.Tag = line;
				newLine = true;
			}
			PoseMapping pose = row.Cells["ColImage"].Value as PoseMapping;
			string text = row.Cells["ColText"].Value?.ToString();
			string marker = row.Cells["ColMarker"].Value?.ToString();
			if (string.IsNullOrWhiteSpace(marker))
			{
				line.Marker = null;
			}
			else
			{
				marker = marker.Trim();
				MarkerOperation newMarker = new MarkerOperation(marker);

				if (newMarker.Operator == null && newMarker.Value == null)
				{
					if (!string.IsNullOrEmpty(line.Marker))
					{
						MarkerOperation oldMarker = new MarkerOperation(line.Marker);
						newMarker.Operator = oldMarker.Operator;
						newMarker.Value = oldMarker.Value;
						if (oldMarker.Name.EndsWith("*"))
						{
							newMarker.Name += "*";
						}
					}
				}
				line.Marker = newMarker.ToString();
			}

			if (text == "~silent~")
			{
				text = "";
			}
			if (text == null && pose == null)
				return null;

			line.Pose = pose;
			line.Text = text ?? "";
			if (row.Cells[nameof(ColOnce)].Value != null && (bool)row.Cells[nameof(ColOnce)].Value == true)
			{
				if (line.OneShotId == 0)
				{
					line.OneShotId = ++_character.Behavior.MaxStateId;
				}
			}
			else
			{
				line.OneShotId = 0;
			}

			Tuple<string, string> collectibleData = row.Cells[nameof(ColTrophy)].Tag as Tuple<string, string>;
			if (collectibleData != null)
			{
				line.CollectibleId = collectibleData.Item1;
				line.CollectibleValue = collectibleData.Item2;
			}

			if (newLine)
			{
				line.PropertyChanged += Line_PropertyChanged;
			}

			return line;
		}

		/// <summary>
		/// Updates the Image column in the dialogue grid to contain only images that exist for all currently selected stages
		/// </summary>
		/// <param name="retainValue"></param>
		public void UpdateAvailableImagesForCase(HashSet<int> selectedStages, bool retainValue)
		{
			if (_selectedCase == null) { return; }
			List<object> values = new List<object>();
			if (retainValue)
			{
				//save off values
				for (int i = 0; i < gridDialogue.Rows.Count; i++)
				{
					DataGridViewRow row = gridDialogue.Rows[i];
					values.Add(row.Cells["ColImage"].Value);
				}
			}

			bool hasStageImages = _selectedCase.Lines.Find(l => l.Images.Count > 0) != null;
			int stageId = _selectedStage == null ? 0 : _selectedStage.Id;
			SkinnedDataGridViewComboBoxColumn col = gridDialogue.Columns["ColImage"] as SkinnedDataGridViewComboBoxColumn;
			col.Items.Clear();
			HashSet<PoseMapping> images = new HashSet<PoseMapping>();
			if (_character != null)
			{
				if (_selectedStage == null)
				{
					images.AddRange(_character.PoseLibrary.GetPoses(0));
					foreach (var image in images)
					{
						col.Items.Add(image);
					}
				}
				else
				{
					if (hasStageImages)
					{
						foreach (int selectedStage in _selectedCase.Stages)
						{
							images.AddRange(_character.PoseLibrary.GetPoses(selectedStage));
						}
					}
					else
					{
						images.AddRange(_character.PoseLibrary.GetPoses(stageId));
					}
					
					foreach (PoseMapping image in images)
					{
						bool isGeneric = image.IsGeneric;
						bool allExist = true;
						if (!isGeneric && !hasStageImages)
						{
							foreach (int stage in selectedStages)
							{
								if (!image.ContainsStage(stage))
								{
									allExist = false;
									break;
								}
							}
						}
						if (allExist)
						{
							col.Items.Add(image);
						}
					}
				}
			}

			foreach (DataGridViewRow row in gridDialogue.Rows)
			{
				SkinnedDataGridViewComboBoxCell cellCol = row.Cells[ColImage.Index] as SkinnedDataGridViewComboBoxCell;
				if (cellCol != null)
				{
					object old = cellCol.Value;
					cellCol.Items.Clear();
					foreach (object item in col.Items)
					{
						cellCol.Items.Add(item);
					}
					cellCol.Value = old;
				}
			}
			col.DisplayMember = "DefaultName";

			if (retainValue)
			{
				//restore values
				for (int i = 0; i < gridDialogue.Rows.Count; i++)
				{
					DataGridViewRow row = gridDialogue.Rows[i];

					//Make sure the value is still valid
					bool found = false;
					foreach (var item in col.Items)
					{
						PoseMapping img = item as PoseMapping;
						PoseMapping oldImg = values[i] as PoseMapping;
						if (oldImg == img)
						{
							row.Cells["ColImage"].Value = item;
							found = true;
							break;
						}
					}
					if (!found)
					{
						row.Cells["ColImage"].Value = null;
					}
				}
			}
		}

		private void gridDialogue_KeyDown(object sender, KeyEventArgs e)
		{
			if (gridDialogue.SelectedRows.Count == 1 && !gridDialogue.SelectedRows[0].IsNewRow && e.KeyCode == Keys.Delete)
			{
				gridDialogue.Rows.RemoveAt(gridDialogue.SelectedRows[0].Index);
				e.Handled = true;
			}
			else
			{
				KeyDown?.Invoke(this, e);
			}
		}

		private void gridDialogue_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (_modifyingLine) { return; }
			SelectRow(e.RowIndex);
		}

		private void gridDialogue_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex == ColDelete.Index || e.ColumnIndex == ColTrophy.Index || e.ColumnIndex == ColMarkerOptions.Index || e.ColumnIndex == ColMore.Index
				|| e.ColumnIndex == ColImageOptions.Index || e.ColumnIndex == ColOnce.Index)
			{
				Image img = Properties.Resources.Delete;
				if (e.ColumnIndex == ColTrophy.Index)
				{
					img = Properties.Resources.TrophyUnfilled;
					if (e.RowIndex >= 0)
					{
						DataGridViewRow row = gridDialogue.Rows[e.RowIndex];
						DataGridViewCell cell = row.Cells[e.ColumnIndex];
						Tuple<string, string> data = cell.Tag as Tuple<string, string>;
						if (data != null && !string.IsNullOrEmpty(data.Item1))
						{
							img = Properties.Resources.TrophyFilled;
						}
					}
				}
				else if (e.ColumnIndex == ColMarkerOptions.Index)
				{
					img = Properties.Resources.BookmarkUnfilled;
					if (e.RowIndex >= 0)
					{
						DataGridViewRow row = gridDialogue.Rows[e.RowIndex];
						DialogueLine line = row.Tag as DialogueLine;
						if (line != null && line.HasAdvancedMarker)
						{
							img = Properties.Resources.BookmarkFilled;
						}
					}
				}
				else if (e.ColumnIndex == ColMore.Index)
				{
					img = Properties.Resources.Ellipsis;
					if (e.RowIndex >= 0)
					{
						DataGridViewRow row = gridDialogue.Rows[e.RowIndex];
						DialogueLine line = row.Tag as DialogueLine;
						if (line != null && line.HasAdvancedData)
						{
							img = Properties.Resources.EllipsisFilled;
						}
					}
				}
				else if (e.ColumnIndex == ColOnce.Index)
				{
					if (e.RowIndex == -1)
					{
						img = Properties.Resources.PlayOnce;
					}
					else
					{
						return;
					}
				}
				else if (e.ColumnIndex == ColImageOptions.Index)
				{
					img = Properties.Resources.ChevronDown;
				}
				e.Paint(e.CellBounds, DataGridViewPaintParts.All);
				var w = img.Width;
				var h = img.Height;
				var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
				var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

				e.Graphics.DrawImage(img, new Rectangle(x, y, w, h));
				e.Handled = true;
			}
		}

		private void gridDialogue_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
		{
			if (_selectedCase == null)
				return;
			if (e.ColumnIndex == 0)
			{
				if (e.Value != null)
				{
					SkinnedDataGridViewComboBoxCell cell = (SkinnedDataGridViewComboBoxCell)gridDialogue.Rows[e.RowIndex].Cells[e.ColumnIndex];
					foreach (object item in cell.Items)
					{
						if (((PoseMapping)item).DisplayName == e.Value.ToString())
						{
							e.Value = item;
							e.ParsingApplied = true;
							return;
						}
					}
				}
			}
			else if (e.ColumnIndex == 1)
			{
				//Validate variables in text
				string text = e.Value?.ToString();
				if (string.IsNullOrEmpty(text))
					return;
				Regex varRegex = new Regex(@"~\w*~", RegexOptions.IgnoreCase);
				e.Value = varRegex.Replace(text, (match) =>
				{
					string var = match.Value;
					if (var == "~clothes~")
					{
						var = "~clothing~";
					}
					return var;
				});
				e.ParsingApplied = true;
			}
		}

		private void gridDialogue_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (_selectedCase == null || e.FormattedValue == null || _populatingCase || !Config.UseIntellisense)
				return;

			//List<string> invalidVars = DialogueLine.GetInvalidVariables(_selectedCase, e.FormattedValue.ToString());
			//if (invalidVars.Count > 0)
			//{
			//	MessageBox.Show(string.Format("The following variables are invalid for this line: {0}", string.Join(",", invalidVars)));
			//	e.Cancel = true;
			//}
		}

		private void gridDialogue_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (_modifyingLine) { return; }
			if (e.ColumnIndex == 0)
			{
				SelectRow(e.RowIndex);
			}
			else if (e.ColumnIndex == ColText.Index)
			{
				TextUpdated?.Invoke(this, e.RowIndex);
			}
		}

		private void gridDialogue_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			if (gridDialogue.IsCurrentCellDirty)
			{
				gridDialogue.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}
		}

		private void gridDialogue_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			DataGridViewRow row = gridDialogue.Rows[e.RowIndex];
			DialogueLine line = new DialogueLine();
			line.PropertyChanged += Line_PropertyChanged;
			row.Tag = line;
			row.Cells["ColDelete"].ToolTipText = "Delete line";
			row.Cells["ColTrophy"].ToolTipText = "Unlock collectible";
			row.Cells[nameof(ColMore)].ToolTipText = "More options";
			row.Cells[nameof(ColMarkerOptions)].ToolTipText = "Advanced marker options";
			row.Cells[nameof(ColImageOptions)].ToolTipText = "Stage-specific images";
		}

		private void gridDialogue_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex < 0 || e.ColumnIndex >= gridDialogue.Columns.Count || e.RowIndex == gridDialogue.NewRowIndex || ReadOnly)
			{
				return;
			}
			DataGridViewColumn col = gridDialogue.Columns[e.ColumnIndex];
			if (col == ColDelete)
			{
				int rowIndex = e.RowIndex;
				DeleteLine(rowIndex);
			}
			else if (col == ColTrophy)
			{
				ShowTrophyForm(e.RowIndex);
			}
			else if (col == ColMarkerOptions)
			{
				ShowDropdown(e.RowIndex, e.ColumnIndex, _markerCtl, _markerDropDown);
			}
			else if (col == ColMore)
			{
				ShowDropdown(e.RowIndex, e.ColumnIndex, _lineCtl, _lineDropDown);
			}
			else if (col == ColImageOptions)
			{
				if (_character != null)
				{
					DialogueLine line = gridDialogue.Rows[e.RowIndex].Tag as DialogueLine;
					StageImageSelection form = new StageImageSelection(_character, line, _selectedCase);
					form.ShowDialog();
				}
			}
		}

		/// <summary>
		/// Clears all lines
		/// </summary>
		public void ClearLines()
		{
			for (int i = gridDialogue.Rows.Count - 1; i >= 0; i--)
			{
				DeleteLine(i);
			}
		}

		/// <summary>
		/// Deletes a line
		/// </summary>
		/// <param name="rowIndex"></param>
		public void DeleteLine(int rowIndex)
		{
			DataGridViewRow row = gridDialogue.Rows[rowIndex];
			if (row != null && !row.IsNewRow)
			{
				DialogueLine line = row.Tag as DialogueLine;
				_selectedCase.Lines.Remove(line);
				gridDialogue.Rows.RemoveAt(rowIndex);
				NotifyChange();
				line.PropertyChanged -= Line_PropertyChanged;
				row.Tag = null;
			}
		}

		private void ShowDropdown(int rowIndex, int colIndex, IDialogueDropDownControl ctl, ToolStripDropDown dropdown)
		{
			dropdown.AutoClose = false;
			DataGridViewRow row = gridDialogue.Rows[rowIndex];
			DialogueLine line = ReadLineFromDialogueGrid(rowIndex);
			if (line == null) { return; }
			ctl.SetData(rowIndex, line, _character);

			Point pt = gridDialogue.PointToScreen(gridDialogue.GetCellDisplayRectangle(colIndex, rowIndex, false).Location);
			Point screen = gridDialogue.PointToScreen(new Point(0, 0));
			dropdown.Show(this, new Point(pt.X - screen.X + ColMore.Width, pt.Y - screen.Y + row.Height));
			_activeDropdown = dropdown;
		}

		private void HideDropdown()
		{
			if (_activeDropdown != null)
			{
				_activeDropdown.Close();
			}
		}

		private void DropDownClosing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			_activeDropdown = null;
			Control senderCtl = sender as Control;
			IDialogueDropDownControl ctl = senderCtl.Tag as IDialogueDropDownControl;
			DataGridViewRow row = gridDialogue.Rows[ctl.RowIndex];
			AddLineToDialogueGrid(ctl.GetLine(), row);
		}

		private void Ctl_DataUpdated(object sender, EventArgs e)
		{
			IDialogueDropDownControl ctl = sender as IDialogueDropDownControl;
			DataGridViewRow row = gridDialogue.Rows[ctl.RowIndex];
			AddLineToDialogueGrid(ctl.GetLine(), row);
			SelectRow(ctl.RowIndex);
		}

		public bool IsEmpty
		{
			get
			{
				return gridDialogue.Rows.Count == 0 || string.IsNullOrEmpty(gridDialogue.Rows[0].Cells["ColText"].Value?.ToString());
			}
		}

		public void SetFocus()
		{
			if (ActiveControl != gridDialogue && ActiveControl != gridDialogue.EditingControl)
				gridDialogue.Select();
			if (gridDialogue.EditingControl != null && ActiveControl != gridDialogue.EditingControl)
				gridDialogue.EditingControl.Select();
		}

		/// <summary>
		/// Gets the image at a row
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public PoseMapping GetImage(int index)
		{
			DataGridViewRow row = gridDialogue.Rows[index];
			PoseMapping image = row.Cells["ColImage"].Value as PoseMapping;
			return image;
		}

		public int GetHighlightedLineIndex()
		{
			if (gridDialogue.SelectedCells.Count > 0)
			{
				return gridDialogue.SelectedCells[0].RowIndex;
			}
			return 0;
		}

		public DialogueLine GetLine(int index)
		{
			DialogueLine line = ReadLineFromDialogueGrid(index);
			if (line == null)
			{
				return new DialogueLine();
			}
			return line;
		}

		public List<DialogueLine> CopyLines()
		{
			List<DialogueLine> lines = new List<DialogueLine>();
			for (int i = 0; i < gridDialogue.Rows.Count; i++)
			{
				DialogueLine line = ReadLineFromDialogueGrid(i);
				if (line != null)
					lines.Add(line.Copy());
			}
			return lines;
		}

		public void Clear()
		{
			gridDialogue.Rows.Clear();
		}

		public void PasteLines(List<DialogueLine> lines)
		{
			foreach (DialogueLine line in lines)
			{
				DialogueLine copy = line.Copy();
				_selectedCase.Lines.Add(copy);
				AddLineToDialogueGrid(copy, null);
			}
		}

		/// <summary>
		/// Adds a row to the dialogue grid
		/// </summary>
		/// <param name="line">Line to populate the row with</param>
		private void AddLineToDialogueGrid(DialogueLine line, DataGridViewRow row)
		{
			_modifyingLine = true;
			PoseMapping pose = line.Pose;
			if (row == null)
			{
				row = gridDialogue.Rows[gridDialogue.Rows.Add()];
			}
			if (row.Tag != line)
			{
				line.PropertyChanged += Line_PropertyChanged;
				row.Tag = line;
			}
			SkinnedDataGridViewComboBoxCell imageCell = row.Cells["ColImage"] as SkinnedDataGridViewComboBoxCell;
			SetImage(imageCell, pose);
			DataGridViewCell textCell = row.Cells["ColText"];
			textCell.Value = line.Text;

			DataGridViewCell markerCell = row.Cells["ColMarker"];
			string marker, markerValue;
			bool perTarget;
			string op;
			marker = Marker.ExtractPieces(line.Marker, out markerValue, out perTarget, out op);
			markerCell.Value = marker;
			
			row.Cells[nameof(ColTrophy)].Tag = new Tuple<string, string>(line.CollectibleId, line.CollectibleValue);
			row.Cells[nameof(ColMarkerOptions)].ToolTipText = GetMarkerTooltip(line);
			row.Cells[nameof(ColOnce)].Value = (line.OneShotId > 0 ? true : false);
			_modifyingLine = false;

			gridDialogue.InvalidateRow(row.Index);
		}

		private void Line_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			NotifyChange();
		}

		private void NotifyChange()
		{
			_selectedCase?.NotifyPropertyChanged(nameof(_selectedCase.Lines));
			_character?.Behavior?.NotifyPropertyChanged("");
		}

		private string GetMarkerTooltip(DialogueLine line)
		{
			if (!line.HasAdvancedMarker)
			{
				return "Advanced marker options";
			}
			else
			{
				List<string> markers = new List<string>();
				if (!string.IsNullOrEmpty(line.Marker))
				{
					markers.Add(line.Marker);
				}
				foreach (MarkerOperation op in line.Markers)
				{
					markers.Add(op.ToString());
				}
				return string.Join("\n", markers);
			}
		}

		/// <summary>
		/// Sets the image cell of a dialogue row
		/// </summary>
		/// <param name="cell"></param>
		/// <param name="pose"></param>
		private void SetImage(SkinnedDataGridViewComboBoxCell cell, PoseMapping pose)
		{
			foreach (var item in cell.Items)
			{
				PoseMapping image = item as PoseMapping;
				if (image == pose)
				{
					cell.Value = item;
					return;
				}
			}
		}

		public void SelectLine(DialogueLine line)
		{
			foreach (DataGridViewRow row in gridDialogue.Rows)
			{
				DialogueLine loadedLine = row.Tag as DialogueLine;
				if (loadedLine == null)
				{
					continue;
				}
				if (loadedLine.Text == line.Text && loadedLine.Marker == line.Marker)
				{
					row.Cells[2].Selected = true;
					return;
				}
			}
		}

		#region Find/replace
		/// <summary>
		/// Gets the text selection start of a grid's TextBox cell, if there is one
		/// </summary>
		/// <param name="grid">Grid to look at</param>
		/// <returns>Selection start index, or -1 if there is none</returns>
		private int GetSelectionStart(DataGridView grid)
		{
			if (grid.CurrentCell == null)
				return -1;
			if (grid.EditingControl != null && grid.EditingControl is TextBox)
			{
				TextBox box = (TextBox)grid.EditingControl;
				return box.SelectionStart;
			}
			return -1;
		}

		private void ReplaceText(string replacement)
		{
			if (gridDialogue.EditingControl != null && gridDialogue.EditingControl is TextBox)
			{
				TextBox box = (TextBox)gridDialogue.EditingControl;
				int start = box.SelectionStart;
				box.SelectedText = replacement;
				box.SelectionStart = start;
				box.SelectionLength = replacement.Length;
			}
		}

		/// <summary>
		/// Looks for a find match in a string of text
		/// </summary>
		/// <param name="text">Text to search</param>
		/// <param name="args">Search args</param>
		/// <returns>Index in the string where the match begins, or -1 if no match</returns>
		public int FindText(string text, int startIndex, FindArgs args)
		{
			if (string.IsNullOrEmpty(text) || startIndex >= text.Length)
				return -1;
			string pattern = Regex.Escape(args.FindText);
			if (args.WholeWords)
			{
				pattern = string.Format(@"\b{0}\b", pattern);
			}

			Regex regex = new Regex(pattern, !args.MatchCase ? RegexOptions.IgnoreCase : RegexOptions.None);
			text = text.Substring(startIndex); //only search from the startIndex on
			Match match = regex.Match(text);
			if (match.Success)
			{
				return match.Index + startIndex;
			}
			return -1;
		}

		public void SelectTextInRow(int rowIndex, int startIndex, int length, bool selectMarker)
		{
			DataGridViewRow row = gridDialogue.Rows[rowIndex];
			gridDialogue.Select();
			string colName = selectMarker ? nameof(ColMarker) : nameof(ColText);
			gridDialogue.CurrentCell = row.Cells[colName];
			gridDialogue.BeginEdit(false);
			SelectText(startIndex, length);
		}

		private void SelectText(int start, int length)
		{
			TextBox textbox = (TextBox)gridDialogue.EditingControl;
			if (textbox != null)
			{
				textbox.SelectionStart = start;
				textbox.SelectionLength = length;
			}
		}

		public void ClearSelection()
		{
			_selectedRow = 0;
			TextBox box = gridDialogue.EditingControl as TextBox;
			if (box != null)
			{
				box.SelectionStart = 0;
				box.SelectionLength = 0;
			}
		}

		public bool FindReplace(FindArgs args)
		{
			int startLine = _selectedRow;
			int startIndex = GetSelectionStart(gridDialogue);
			if (args.SearchAdvanced)
			{
				startLine++;
			}
			if (args.DoReplace && startIndex >= 0)
			{
				//Back up a space when replacing so the highlighted word gets replaced
				startIndex--;
			}
			for (int l = startLine; l < gridDialogue.Rows.Count; l++)
			{
				DataGridViewRow row = gridDialogue.Rows[l];
				if (args.SearchAdvanced)
				{
					string marker = row.Cells[nameof(ColMarker)].Value?.ToString();
					if (!string.IsNullOrEmpty(marker))
					{
						int index = FindText(marker, 0, args);
						if (index >= 0)
						{
							if (ActiveControl != gridDialogue.EditingControl || gridDialogue.CurrentCell != row.Cells[nameof(ColMarker)])
							{
								gridDialogue.Select();
								gridDialogue.CurrentCell = row.Cells[nameof(ColMarker)];
								gridDialogue.BeginEdit(false);
							}
							SelectText(index, args.FindText.Length);
							args.Success = true;
							return true;
						}
					}
				}
				else
				{
					string text = row.Cells["ColText"].Value?.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						int index = startIndex;
						do
						{
							index = FindText(text, index + 1, args);
							if (index >= 0)
							{
								//highlight it
								if (ActiveControl != gridDialogue.EditingControl || gridDialogue.CurrentCell != row.Cells["ColText"])
								{
									gridDialogue.Select();
									gridDialogue.CurrentCell = row.Cells["ColText"];
									gridDialogue.BeginEdit(false);
								}
								SelectText(index, args.FindText.Length);

								args.Success = true;

								if (args.DoReplace)
								{
									ReplaceText(args.ReplaceText);
									args.ReplaceCount++;
									text = row.Cells["ColText"].Value?.ToString();
									index += args.ReplaceText.Length - 1;
								}

								if (!args.ReplaceAll)
									return true;
							}
						}
						while (index >= 0);
					}
					startIndex = -1;
				}
			}
			return false;
		}
		#endregion

		#region Intellisense
		private void gridDialogue_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (_editBox != null)
			{
				_editBox = null;
			}

			if (gridDialogue.SelectedCells.Count == 0) { return; }

			DataGridViewColumn column = gridDialogue.SelectedCells[0].OwningColumn;
			if (column == ColText)
			{
				_editBox = e.Control as TextBox;
				_intellisense.SetContext(_editBox, _character, _selectedCase);
			}
		}

		private void _intellisense_InsertSnippet(object sender, InsertEventArgs e)
		{
			if (gridDialogue.EditingControl == null)
			{
				gridDialogue.BeginEdit(false);
			}

			_editBox.SelectionStart = e.InsertionStart;
			_editBox.SelectionLength = e.InsertionLength;
			_editBox.SelectedText = e.Text;
		}

		private void DialogueGrid_Leave(object sender, EventArgs e)
		{
			_intellisense.Reset();
		}
		#endregion

		private void ShowTrophyForm(int rowIndex)
		{
			if (_character == null) { return; }
			if (_character.Collectibles.Count == 0)
			{
				MessageBox.Show("You haven't created any collectibles yet. Go to the Collectibles tab to create a collectible before attaching it to a dialogue line.");
				return;
			}
			DataGridViewRow row = gridDialogue.Rows[rowIndex];
			DataGridViewCell cell = row.Cells[nameof(ColTrophy)];
			Tuple<string, string> data = cell.Tag as Tuple<string, string>;
			string text = row.Cells[nameof(ColText)].Value?.ToString();
			if (data == null)
			{
				data = new Tuple<string, string>(null, null);
			}
			TrophyForm form = new TrophyForm(_character, text, data.Item1, data.Item2);
			if (form.ShowDialog() == DialogResult.OK)
			{
				data = new Tuple<string, string>(form.Id, form.Value);
				cell.Tag = data;
			}
		}

		public void OnUpdateSkin(Skin skin)
		{
			BackColor = skin.GetBackColor(SkinnedBackgroundType.Surface);
			_markerCtl.OnUpdateSkin(skin);
			_lineCtl.OnUpdateSkin(skin);
		}

		private void applyToColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gridDialogue.CurrentCell == null)
			{
				return;
			}
			DataGridViewColumn col = gridDialogue.CurrentCell.OwningColumn;
			if (col == ColImageOptions || col == ColDelete)
			{
				return;
			}
			else if (col == ColMarker || col == ColMarkerOptions)
			{
				DialogueLine line = ReadLineFromDialogueGrid(gridDialogue.CurrentCell.RowIndex);
				for (int i = 0; i < gridDialogue.Rows.Count; i++)
				{
					DataGridViewRow row = gridDialogue.Rows[i];
					if (row.IsNewRow || i == gridDialogue.CurrentCell.RowIndex)
					{
						continue;
					}
					DialogueLine otherLine = gridDialogue.Rows[i].Tag as DialogueLine;
					if (otherLine != null)
					{
						otherLine.Marker = line.Marker;
						otherLine.Markers.Clear();
						foreach (MarkerOperation op in line.Markers)
						{
							otherLine.Markers.Add(op.Copy());
						}
						AddLineToDialogueGrid(otherLine, row);
					}
				}
			}
			else if (col == ColTrophy)
			{
				DialogueLine line = ReadLineFromDialogueGrid(gridDialogue.CurrentCell.RowIndex);
				for (int i = 0; i < gridDialogue.Rows.Count; i++)
				{
					DataGridViewRow row = gridDialogue.Rows[i];
					if (row.IsNewRow || i == gridDialogue.CurrentCell.RowIndex)
					{
						continue;
					}
					DialogueLine otherLine = gridDialogue.Rows[i].Tag as DialogueLine;
					if (otherLine != null)
					{
						otherLine.CollectibleId = line.CollectibleId;
						otherLine.CollectibleValue = line.CollectibleValue;
						AddLineToDialogueGrid(otherLine, row);
					}
				}
			}
			else if (col == ColMore)
			{
				DialogueLine line = ReadLineFromDialogueGrid(gridDialogue.CurrentCell.RowIndex);
				for (int i = 0; i < gridDialogue.Rows.Count; i++)
				{
					DataGridViewRow row = gridDialogue.Rows[i];
					if (row.IsNewRow || i == gridDialogue.CurrentCell.RowIndex)
					{
						continue;
					}
					DialogueLine otherLine = gridDialogue.Rows[i].Tag as DialogueLine;
					if (otherLine != null)
					{
						otherLine.Direction = line.Direction;
						otherLine.Gender = line.Gender;
						otherLine.Intelligence = line.Intelligence;
						otherLine.Location = line.Location;
						otherLine.Size = line.Size;
						otherLine.Label = line.Label;
						otherLine.Layer = line.Layer;
						if (line.OneShotId > 0)
						{
							otherLine.OneShotId = ++_character.Behavior.MaxStateId;
						}
						else
						{
							otherLine.OneShotId = 0;
						}
						otherLine.Weight = line.Weight;
						AddLineToDialogueGrid(otherLine, row);
					}
				}
			}
			else
			{
				for (int i = 0; i < gridDialogue.Rows.Count; i++)
				{
					DataGridViewRow row = gridDialogue.Rows[i];
					if (row.IsNewRow || i == gridDialogue.CurrentCell.RowIndex)
					{
						continue;
					}
					row.Cells[col.Index].Value = gridDialogue.CurrentCell.Value;
				}
			}
		}

		private void gridDialogue_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				try
				{
					gridDialogue.CurrentCell = gridDialogue.Rows[e.RowIndex].Cells[e.ColumnIndex];
				}
				catch
				{
					gridDialogue.CurrentCell = null;
				}
			}
		}

		private void mnuContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (gridDialogue.CurrentCell == null)
			{
				e.Cancel = true;
				return;
			}
			DataGridViewColumn col = gridDialogue.CurrentCell.OwningColumn;
			if (col == ColImageOptions || col == ColDelete)
			{
				e.Cancel = true;
				return;
			}
		}
	}
}
