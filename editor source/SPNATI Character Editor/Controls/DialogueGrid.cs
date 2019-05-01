using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class DialogueGrid : UserControl
	{
		private Case _selectedCase;
		private Stage _selectedStage;
		private Character _character;
		private ImageLibrary _imageLibrary;
		private bool _populatingCase;
		private int _selectedRow;

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
					gridDialogue.AllowUserToDeleteRows = false;
					gridDialogue.RowHeadersVisible = false;
					gridDialogue.EditMode = DataGridViewEditMode.EditProgrammatically;
					gridDialogue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					ColDelete.Visible = false;
				}
				else
				{
					gridDialogue.AllowUserToAddRows = true;
					gridDialogue.AllowUserToDeleteRows = true;
					gridDialogue.RowHeadersVisible = true;
					gridDialogue.EditMode = DataGridViewEditMode.EditOnEnter;
					gridDialogue.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
					ColDelete.Visible = true;
				}
			}
		}

		public DialogueGrid()
		{
			InitializeComponent();

			ColPerTarget.FalseValue = false;
			ColImage.ValueType = typeof(CharacterImage);

			_intellisense = new IntellisenseControl();
			_intellisense.InsertSnippet += _intellisense_InsertSnippet;
			Controls.Add(_intellisense);
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
			SetData(character, stage, c, stages, ImageLibrary.Get(character));
		}

		public void SetData(Character character, Stage stage, Case c, HashSet<int> selectedStages, ImageLibrary imageLibrary)
		{
			_character = character;
			_selectedStage = stage;
			_selectedCase = c;
			_imageLibrary = imageLibrary;
			_populatingCase = true;
			for (int i = 0; i < gridDialogue.Rows.Count; i++)
			{
				DataGridViewRow row = gridDialogue.Rows[i];
				row.Cells["ColImage"].Value = null;
			}

			UpdateAvailableImagesForCase(selectedStages, false);

			//Populate lines
			gridDialogue.Rows.Clear();
			List<DialogueLine> lines = (_selectedCase.Tag == Trigger.StartTrigger ? _character.StartingLines : _selectedCase.Lines);
			foreach (DialogueLine line in lines)
			{
				AddLineToDialogueGrid(line);
			}
			if (lines.Count > 0)
				SelectRow(0);
			_populatingCase = false;
		}

		public void Save()
		{
			List<DialogueLine> lines = (_selectedCase.Tag == Trigger.StartTrigger ? _character.StartingLines : _selectedCase.Lines);
			foreach (DialogueLine line in lines)
			{
				_character.RemoveMarkerReference(line.Marker);
			}
			lines.Clear();
			for (int i = 0; i < gridDialogue.Rows.Count; i++)
			{
				DialogueLine line = ReadLineFromDialogueGrid(i);
				if (line != null)
				{
					lines.Add(line);
					_character.CacheMarker(line.Marker);
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
			CharacterImage pose = row.Cells["ColImage"].Value as CharacterImage;
			string image = pose?.Name;
			string text = row.Cells["ColText"].Value?.ToString();
			string marker = row.Cells["ColMarker"].Value?.ToString();
			if (string.IsNullOrEmpty(marker))
			{
				marker = null;
			}
			string markerValue = row.Cells["ColMarkerValue"].Value?.ToString();
			bool perTarget = (bool)(row.Cells["ColPerTarget"].Value ?? false);
			string direction = row.Cells["ColDirection"].Value?.ToString();
			string location = row.Cells["ColLocation"].Value?.ToString();

			if (text == "~silent~")
			{
				text = "";
			}
			if (text == null && pose == null)
				return null;
			if (pose != null && !pose.IsGeneric)
			{
				image = DialogueLine.GetDefaultImage(image);
			}
			DialogueLine line = new DialogueLine(image, text);

			if (perTarget)
			{
				marker += "*";
			}

			if (string.IsNullOrEmpty(markerValue))
			{
				line.Marker = marker;
			}
			else if (markerValue == "+" || markerValue == "-")
			{
				line.Marker = $"{markerValue}{marker}";
			}
			else if (markerValue == "+1")
			{
				line.Marker = $"+{marker}";
			}
			else if (markerValue == "-1")
			{
				line.Marker = $"-{marker}";
			}
			else
			{
				line.Marker = $"{marker}={markerValue}";
			}

			line.Direction = direction;
			line.Location = location;
			if (pose != null)
			{
				line.IsGenericImage = pose.IsGeneric;
			}

			string ai = row.Cells["ColIntelligence"].Value?.ToString();
			string size = row.Cells["ColSize"].Value?.ToString();
			string label = row.Cells["ColLabel"].Value?.ToString();
			string gender = row.Cells["ColGender"].Value?.ToString();
			string weight = row.Cells[nameof(ColWeight)].Value?.ToString() ?? "0";
			float fval;
			if (float.TryParse(weight, NumberStyles.Number, CultureInfo.InvariantCulture, out fval))
			{
				line.Weight = fval;
			}
			else
			{
				line.Weight = 1;
			}

			line.Intelligence = ai;
			line.Size = size;
			line.Label = label;
			line.Gender = gender;

			Tuple<string, string> collectibleData = row.Cells[nameof(ColTrophy)].Tag as Tuple<string, string>;
			if (collectibleData != null)
			{
				line.CollectibleId = collectibleData.Item1;
				line.CollectibleValue = collectibleData.Item2;
			}

			return line;
		}

		/// <summary>
		/// Updates the Image column in the dialogue grid to contain only images that exist for all currently selected stages
		/// </summary>
		/// <param name="retainValue"></param>
		public void UpdateAvailableImagesForCase(HashSet<int> selectedStages, bool retainValue)
		{
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

			int stageId = _selectedStage == null ? 0 : _selectedStage.Id;
			DataGridViewComboBoxColumn col = gridDialogue.Columns["ColImage"] as DataGridViewComboBoxColumn;
			col.Items.Clear();
			List<CharacterImage> images = new List<CharacterImage>();
			if (_selectedStage == null)
			{
				images.AddRange(_imageLibrary.GetImages(0));
				if (Config.UsePrefixlessImages)
				{
					string prefix = Config.PrefixFilter;
					foreach (CharacterImage img in _imageLibrary.GetImages(-1))
					{
						string file = img.Name;
						if (string.IsNullOrEmpty(prefix) || !file.StartsWith(prefix))
						{
							images.Add(img);
						}
					}
				}
				foreach (var image in images)
				{
					col.Items.Add(image);
				}
			}
			else
			{
				images.AddRange(_imageLibrary.GetImages(stageId));
				if (Config.UsePrefixlessImages)
				{
					string prefix = Config.PrefixFilter;
					foreach (CharacterImage img in _imageLibrary.GetImages(-1))
					{
						string file = img.Name;
						if (string.IsNullOrEmpty(prefix) || !file.StartsWith(prefix))
						{
							images.Add(img);
						}
					}
				}

				foreach (var image in images)
				{
					string name = DialogueLine.GetDefaultImage(image.Name);
					//Filter out the ones that don't appear in every selected stage
					bool allExist = true;
					if (!image.IsGeneric)
					{
						bool custom = name.StartsWith("custom:");
						string nameWithoutStage = name;
						if (custom)
						{
							nameWithoutStage = DialogueLine.GetDefaultImage(image.Name.Substring(7));
						}
						foreach (int stage in selectedStages)
						{
							string key = stage + "-" + nameWithoutStage;
							if (custom)
							{
								key = "custom:" + key;
							}
							if (_imageLibrary.Find(key) == null)
							{
								allExist = false;
								break;
							}
						}

					}
					if (allExist)
						col.Items.Add(image);
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
						CharacterImage img = item as CharacterImage;
						CharacterImage oldImg = values[i] as CharacterImage;
						if ((oldImg == null && img.DefaultName == values[i]?.ToString()) || (oldImg != null && oldImg.DefaultName == img.DefaultName))
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
			SelectRow(e.RowIndex);
		}

		private void gridDialogue_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex == ColDelete.Index || e.ColumnIndex == ColTrophy.Index)
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
					DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)gridDialogue.Rows[e.RowIndex].Cells[e.ColumnIndex];
					foreach (object item in cell.Items)
					{
						if (((CharacterImage)item).DefaultName == e.Value.ToString())
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

			List<string> invalidVars = DialogueLine.GetInvalidVariables(_selectedCase.Tag, e.FormattedValue.ToString());
			if (invalidVars.Count > 0)
			{
				MessageBox.Show(string.Format("The following variables are invalid for this line: {0}", string.Join(",", invalidVars)));
				e.Cancel = true;
			}
		}

		private void gridDialogue_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
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
			row.Cells["ColDelete"].ToolTipText = "Delete line";
			row.Cells["ColTrophy"].ToolTipText = "Unlock collectible";
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
				gridDialogue.Rows.RemoveAt(e.RowIndex);
			}
			else if (col == ColTrophy)
			{
				ShowTrophyForm(e.RowIndex);
			}
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

		public bool ShowAdvancedColumns
		{
			set
			{
				gridDialogue.Columns["ColDirection"].Visible = gridDialogue.Columns["ColLocation"].Visible = value;
				ColGender.Visible = ColSize.Visible = ColIntelligence.Visible = ColLabel.Visible = ColWeight.Visible = value;
			}
		}

		/// <summary>
		/// Gets the image at a row
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public string GetImage(int index)
		{
			DataGridViewRow row = gridDialogue.Rows[index];
			string image = row.Cells["ColImage"].Value?.ToString();
			return image;
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
				var line = ReadLineFromDialogueGrid(i);
				if (line != null)
					lines.Add(line);
			}
			return lines;
		}

		public void Clear()
		{
			gridDialogue.Rows.Clear();
		}

		public void PasteLines(List<DialogueLine> lines)
		{
			foreach (var line in lines)
			{
				AddLineToDialogueGrid(line);
			}
		}

		/// <summary>
		/// Adds a row to the dialogue grid
		/// </summary>
		/// <param name="line">Line to populate the row with</param>
		private void AddLineToDialogueGrid(DialogueLine line)
		{
			string imageKey = line.Image;
			if (!line.IsGenericImage && _selectedCase.Stages.Count > 0)
			{
				int stage = _selectedCase.Stages[0];
				if (_selectedStage != null)
					stage = _selectedStage.Id;
				imageKey = DialogueLine.GetStageImage(stage, imageKey);
			}
			DataGridViewRow row = gridDialogue.Rows[gridDialogue.Rows.Add()];
			row.Tag = line;
			DataGridViewComboBoxCell imageCell = row.Cells["ColImage"] as DataGridViewComboBoxCell;
			SetImage(imageCell, imageKey);
			DataGridViewCell textCell = row.Cells["ColText"];
			textCell.Value = line.Text;

			DataGridViewCell markerCell = row.Cells["ColMarker"];
			DataGridViewCell markerValueCell = row.Cells["ColMarkerValue"];
			DataGridViewCheckBoxCell perTargetCell = row.Cells["ColPerTarget"] as DataGridViewCheckBoxCell;
			string marker, markerValue;
			bool perTarget;
			marker = Marker.ExtractPieces(line.Marker, out markerValue, out perTarget);
			markerCell.Value = marker;
			markerValueCell.Value = markerValue;
			perTargetCell.Value = perTarget;

			DataGridViewCell directionCell = row.Cells["ColDirection"];
			directionCell.Value = line.Direction;

			DataGridViewCell locationCell = row.Cells["ColLocation"];
			locationCell.Value = line.Location;

			row.Cells["ColIntelligence"].Value = line.Intelligence;
			row.Cells["ColSize"].Value = line.Size;
			row.Cells["ColGender"].Value = line.Gender;
			row.Cells["ColLabel"].Value = line.Label;

			row.Cells[nameof(ColWeight)].Value = line.Weight;
			row.Cells[nameof(ColTrophy)].Tag = new Tuple<string, string>(line.CollectibleId, line.CollectibleValue);
		}

		/// <summary>
		/// Sets the image cell of a dialogue row
		/// </summary>
		/// <param name="cell"></param>
		/// <param name="key"></param>
		private void SetImage(DataGridViewComboBoxCell cell, string key)
		{
			string defaultKey = DialogueLine.GetDefaultImage(key);
			foreach (var item in cell.Items)
			{
				CharacterImage image = item as CharacterImage;
				if (image != null && image.DefaultName == defaultKey)
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
					row.Cells[1].Selected = true;
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

		public void SelectTextInRow(int rowIndex, int startIndex, int length)
		{
			DataGridViewRow row = gridDialogue.Rows[rowIndex];
			gridDialogue.Select();
			gridDialogue.CurrentCell = row.Cells["ColText"];
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
			if (args.DoReplace && startIndex >= 0)
			{
				//Back up a space when replacing so the highlighted word gets replaced
				startIndex--;
			}
			for (int l = startLine; l < gridDialogue.Rows.Count; l++)
			{
				DataGridViewRow row = gridDialogue.Rows[l];
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
			if (column == ColText || column == ColMarkerValue)
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
	}
}
