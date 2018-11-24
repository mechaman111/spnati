using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Main form
	/// </summary>
	public partial class CharacterEditor : Form
	{
		private Character _selectedCharacter = null;
		private Listing _listing = null;
		private Clothing _selectedLayer;
		private ImageLibrary _imageLibrary = new ImageLibrary();
		private Stage _selectedStage;
		private Case _selectedCase;
		private bool _populatingImages;
		private bool _populatingWardrobe;
		private bool _populatingTree;
		private bool _populatingCase;
		private TreeNode _startNode;
		private TreeFilterMode _filterMode = TreeFilterMode.All;
		private Queue<WardrobeChange> _wardrobeChanges = new Queue<WardrobeChange>();
		private TabPage _currentTab;

		private FindReplace _findForm;

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int GetScrollPos(IntPtr hWnd, int nBar);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

		private const int SB_HORZ = 0x0;
		private const int SB_VERT = 0x1;

		public CharacterEditor()
		{
			InitializeComponent();

			imageImporter.PreviewImage += ImageImporter_PreviewImage;
		}

		private void CharacterEditor_Load(object sender, EventArgs e)
		{
			findToolStripMenuItem.Enabled = false;
			replaceToolStripMenuItem.Enabled = false;
			grpCase.Enabled = false;
			tsbtnRemoveDialogue.Enabled = false;
			tsbtnSplit.Enabled = false;

			//Initial setup
			string appDir = Config.GameDirectory;
			if (!string.IsNullOrEmpty(appDir) && !SettingsSetup.VerifyApplicationDirectory(appDir))
			{
				Config.GameDirectory = null;
			}
			if (string.IsNullOrEmpty(Config.GameDirectory))
			{
				if (OpenSetup() == DialogResult.Cancel)
				{
					ErrorLog.LogError("Unable to launch because setup was cancelled.");
					this.Close();
					return;
				}
			}
			if (string.IsNullOrEmpty(Config.GameDirectory))
			{
				//Not going to play along? Then we'll quit.
				ErrorLog.LogError("SPNATI directory not specified.");
				this.Close();
				return;
			}

			TriggerDatabase.Load();
			PopulateTriggerMenu();
			DialogueDatabase.Load();

			_listing = Serialization.ImportListing();
			CharacterDatabase.Load();

			EnableControls(false);
			if (!string.IsNullOrEmpty(Config.LastCharacter))
			{
				LoadCharacter(Config.LastCharacter);
			}

			cboTreeFilter.SelectedIndex = 0;
			PopulateListingFields();

			SetupFindReplace();
		}

		/// <summary>
		/// Enables the form's controls or not
		/// </summary>
		/// <param name="enabled">Whether to enable things</param>
		/// <returns></returns>
		private void EnableControls(bool enabled)
		{
			saveToolStripMenuItem.Enabled = enabled;
			saveAsToolStripMenuItem.Enabled = enabled;
			viewToolStripMenuItem.Enabled = enabled;
			foreach (Control ctl in this.Controls)
			{
				if (ctl == menuStrip1)
					continue;
				ctl.Enabled = enabled;
			}
		}

		/// <summary>
		/// Prompts the user to export the current character
		/// </summary>
		/// <returns></returns>
		private bool PromptToSave()
		{
			if (_selectedCharacter == null)
				return true;
			DialogResult result = MessageBox.Show(string.Format("Do you wish to save {0} first?", _selectedCharacter), "Save changes", MessageBoxButtons.YesNoCancel);
			if (result == DialogResult.Yes)
			{
				return Export();
			}
			else if (result == DialogResult.No)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// "New" menu item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (!PromptToSave())
				return;
			NewCharacterPrompt prompt = new NewCharacterPrompt();
			if (prompt.ShowDialog() == DialogResult.OK)
			{
				CreateNewCharacter(prompt.FolderName);
			}
		}

		/// <summary>
		/// Creates a new character
		/// </summary>
		/// <param name="folder"></param>
		private void CreateNewCharacter(string folder)
		{
			Character c = new Character();
			c.FirstName = folder;
			c.Label = folder;
			c.FolderName = folder;
			if (Serialization.ExportCharacter(c))
			{
				SetStatus("New character created.");
				CharacterDatabase.Characters.Add(c);
				LoadCharacter(folder);
			}
		}

		/// <summary>
		/// Loads a character for editing
		/// </summary>
		/// <param name="folder">Character's folder name</param>
		private void LoadCharacter(string folder)
		{
			ImageCache.Clear();
			_selectedLayer = null;
			_selectedStage = null;
			_selectedCase = null;
			var lastCharacter = _selectedCharacter;
			Character c = CharacterDatabase.Get(folder);
			EnableControls(c != null);
			if (c != null)
			{
				c.PrepareForEdit();
				Config.LastCharacter = c.FolderName;
				_selectedCharacter = c;
				_imageLibrary.Load(c);

				CreateStageCheckboxes();
				DisplayPortrait();
				PopulatePortraitDropdown();
				PopulateCharacterFields();
				if (lastCharacter != null)
					epilogueEditor.SetCharacter(c);
				if (tabControl.SelectedTab == tabImages)
					imageImporter.SetCharacter(c);
				if (treeDialogue.Nodes.Count > 0)
					treeDialogue.SelectedNode = treeDialogue.Nodes[0];
				OpponentStatus status = _listing.GetCharacterStatus(c.FolderName);
				lblIncomplete.Visible = (status == OpponentStatus.Incomplete);
				lblOffline.Visible = (status == OpponentStatus.Offline);
				lblTesting.Visible = (status == OpponentStatus.Testing);
				lblUnlisted.Visible = (status == OpponentStatus.Unlisted);
				cmdAddToListing.Enabled = (status == OpponentStatus.Unlisted);
			}
			else
			{
				lblIncomplete.Visible = lblOffline.Visible = lblTesting.Visible = lblUnlisted.Visible = cmdAddToListing.Enabled = false;
			}
		}

		private void setupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenSetup();
		}

		/// <summary>
		/// Opens the initial setup form
		/// </summary>
		private DialogResult OpenSetup()
		{
			SettingsSetup form = new SettingsSetup();
			return form.ShowDialog();
		}

		/// <summary>
		/// Updates the status bar text
		/// </summary>
		/// <param name="text">Text to display</param>
		private void SetStatus(string text)
		{
			lblStatus.Text = text;
		}

		/// <summary>
		/// Raised when the application is closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmEditor_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!PromptToSave())
			{
				e.Cancel = true;
				return;
			}
			Config.Save();
		}

		/// <summary>
		/// Open menu item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			OpenCharacter();
		}

		private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			Export();
		}

		private void saveAsToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (_selectedCharacter == null)
				return;
			FileNameSelect select = new FileNameSelect();
			select.FolderName = _selectedCharacter.FolderName;
			if (select.ShowDialog() == DialogResult.OK)
			{
				string oldDir = Path.Combine(Config.GetRootDirectory(_selectedCharacter));
				_selectedCharacter.FolderName = select.FolderName;
				Config.LastCharacter = _selectedCharacter.FolderName;
				Export();

				//Copy all non-xml files over. The xml files were already generated by the Export
				string newDir = Config.GetRootDirectory(_selectedCharacter);
				foreach (string file in Directory.EnumerateFiles(oldDir))
				{
					if (Path.GetExtension(file) == ".xml")
						continue;
					File.Copy(file, Path.Combine(newDir, Path.GetFileName(file)));
				}

				LoadCharacter(_selectedCharacter.FolderName); //Quick and dirty way to switch context
			}
		}

		/// <summary>
		/// Opens a character for editing
		/// </summary>
		/// <returns></returns>
		private bool OpenCharacter()
		{
			if (!PromptToSave())
				return false;
			LoadCharacterPrompt prompt = new LoadCharacterPrompt();
			if (_selectedCharacter != null)
				prompt.FolderName = _selectedCharacter.FolderName;
			if (prompt.ShowDialog() == DialogResult.OK)
			{
				LoadCharacter(prompt.FolderName);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Populates fields that list cross-character data (names, tags, etc.)
		/// </summary>
		private void PopulateListingFields()
		{
			List<string> items = new List<string>();
			items.Add("");
			foreach (var character in CharacterDatabase.Characters)
			{
				items.Add(character.FolderName);
			}
			items.Sort();
			cboAlsoPlaying.DataSource = items;
			cboAlsoPlaying.BindingContext = new BindingContext();
			cboTreeTarget.DataSource = items;
			cboTreeTarget.BindingContext = new BindingContext();

			List<object> filters = new List<object>();
			List<string> tags = new List<string>();
			foreach (var tag in TagDatabase.Tags)
			{
				filters.Add(tag);
				tags.Add(tag.Value);
			}
			filters.Add("");
			filters.Sort((i1, i2) => { return i1.ToString().CompareTo(i2.ToString()); });
			tags.Add("");
			tags.Sort();
			cboLineFilter.DataSource = filters;
			cboLineFilter.BindingContext = new BindingContext();

			DataGridViewComboBoxColumn gridCol = gridFilters.Columns["ColTagFilter"] as DataGridViewComboBoxColumn;
			if (gridCol != null)
			{
				foreach (var tag in tags)
				{
					gridCol.Items.Add(tag);
				}
			}
		}

		/// <summary>
		/// Populates the target field with loaded characters of the appropriate gender for the current case
		/// </summary>
		private void PopulateTargetField()
		{
			cboLineTarget.Items.Clear();
			List<string> items = new List<string>();
			items.Add("");

			Trigger trigger = TriggerDatabase.GetTrigger(_selectedCase?.Tag);
			string gender = trigger?.Gender;
			bool useGender = !string.IsNullOrEmpty(gender);
			string size = trigger?.Size;
			bool useSize = !string.IsNullOrEmpty(size);

			foreach (var character in CharacterDatabase.Characters)
			{
				if ((!useGender || gender == character.Gender) && (!useSize || size == character.Size))
				{
					items.Add(character.FolderName);
				}
			}
			items.Add("human");
			items.Sort();
			cboLineTarget.Items.Clear();
			foreach (string item in items)
			{
				cboLineTarget.Items.Add(item);
			}
		}

		/// <summary>
		/// Populates the default portrait dropdown menu
		/// </summary>
		private void PopulatePortraitDropdown()
		{
			_populatingImages = true;
			List<CharacterImage> images = new List<CharacterImage>();
			images.Add(new CharacterImage(" ", null));
			images.AddRange(_imageLibrary.GetImages(0));
			images.AddRange(_imageLibrary.GetImages(-1));
			cboDefaultPic.DataSource = images;
			_populatingImages = false;
		}

		/// <summary>
		/// Populates the screens with the current character's info
		/// </summary>
		private void PopulateCharacterFields()
		{
			txtLabel.Text = _selectedCharacter.Label;
			LoadLabels();
			txtFirstName.Text = _selectedCharacter.FirstName;
			txtLastName.Text = _selectedCharacter.LastName;
			cboSize.SelectedItem = _selectedCharacter.Size;
			cboGender.SelectedItem = _selectedCharacter.Gender;
			valRounds.Value = _selectedCharacter.Stamina;
			txtDescription.Text = _selectedCharacter.Metadata.Description;
			txtHeight.Text = _selectedCharacter.Metadata.Height;
			txtSource.Text = _selectedCharacter.Metadata.Source;
			txtWriter.Text = _selectedCharacter.Metadata.Writer;
			txtArtist.Text = _selectedCharacter.Metadata.Artist;
			if ((decimal)_selectedCharacter.Metadata.Scale >= valScale.Minimum 
				&& (decimal)_selectedCharacter.Metadata.Scale <= valScale.Maximum)
			{
				valScale.Value = (decimal)_selectedCharacter.Metadata.Scale;
			}
			else
			{
				valScale.Value = 100.0m;
			}
			cboDefaultPic.SelectedItem = _imageLibrary.Find(Path.GetFileNameWithoutExtension(_selectedCharacter.Metadata.Portrait));
			LoadTags();
			LoadIntelligence();
			RegenerateWardrobeList(false);
			GenerateDialogueTree(true);
		}

		/// <summary>
		/// Populates the Tags grid with the character's tags
		/// </summary>
		private void LoadTags()
		{
			gridTags.Rows.Clear();
			foreach (string tag in _selectedCharacter.Tags)
			{
				DataGridViewRow row = gridTags.Rows[gridTags.Rows.Add()];
				row.Cells[0].Value = tag;
			}
		}

		/// <summary>
		/// Populates the intelligence grid
		/// </summary>
		private void LoadIntelligence()
		{
			gridAI.Rows.Clear();
			foreach (StageSpecificValue i in _selectedCharacter.Intelligence)
			{
				DataGridViewRow row = gridAI.Rows[gridAI.Rows.Add()];
				row.Cells["ColAIStage"].Value = i.Stage;
				row.Cells["ColDifficulty"].Value = i.Value;
			}
		}

		/// <summary>
		/// Populates the advanced labels grid
		/// </summary>
		private void LoadLabels()
		{
			gridLabels.Rows.Clear();
			foreach (StageSpecificValue i in _selectedCharacter.Labels)
			{
				DataGridViewRow row = gridLabels.Rows[gridLabels.Rows.Add()];
				row.Cells["ColLabelsStage"].Value = i.Stage;
				row.Cells["ColLabelsLabel"].Value = i.Value;
			}
		}

		private void PopulateTriggerMenu()
		{
			List<Trigger> triggers = TriggerDatabase.Triggers;
			triggers.Sort((a, b) => a.Group == b.Group ? a.GroupOrder - b.GroupOrder : a.Group - b.Group);
			int curGroup = -1;
			ContextMenuStrip curGroupMenu = null;

			foreach (Trigger t in triggers)
			{
				if (t.StartStage < 0) continue;
				if (t.Group != curGroup)
				{
					curGroup = t.Group;
					ToolStripMenuItem groupMenuItem = new ToolStripMenuItem();
					groupMenuItem.Text = TriggerDatabase.GetGroupName(curGroup);
					curGroupMenu = new ContextMenuStrip();
					curGroupMenu.ShowImageMargin = false;
					groupMenuItem.DropDown = curGroupMenu;
					triggerMenu.Items.Add(groupMenuItem);
				}
				curGroupMenu.Items.Add(new ToolStripMenuItem(t.Label, null, triggerMenuItem_Click, t.Tag));

			}
		}

		/// <summary>
		/// Saves the fields into the current Character object, but does NOT save to disk. That's Export()
		/// </summary>
		private void SaveCharacter()
		{
			if (_selectedCharacter == null)
				return;
			Cursor.Current = Cursors.WaitCursor;
			_selectedCharacter.FirstName = txtFirstName.Text;
			_selectedCharacter.LastName = txtLastName.Text;
			_selectedCharacter.Stamina = (int)valRounds.Value;
			_selectedCharacter.Gender = cboGender.SelectedItem.ToString();
			_selectedCharacter.Size = cboSize.SelectedItem.ToString();
			_selectedCharacter.Metadata.Description = txtDescription.Text;
			_selectedCharacter.Metadata.Height = txtHeight.Text;
			_selectedCharacter.Metadata.Source = txtSource.Text;
			_selectedCharacter.Metadata.Writer = txtWriter.Text;
			_selectedCharacter.Metadata.Artist = txtArtist.Text;
			_selectedCharacter.Metadata.Scale = (float)valScale.Value;
			SaveLayer();
			SaveTags();
			SaveLabels();
			SaveIntelligence();
			SaveCase(false);
			if (tabControl.SelectedTab == tabMarkers)
				markerGrid.Save();

			ApplyWardrobeChanges();

			epilogueEditor.Save();
			if (_selectedCharacter.Behavior.EnsureDefaults(_selectedCharacter))
			{
				SetStatus("Character was missing some required lines, so defaults were automatically pulled in.");
				GenerateDialogueTree(false);
			}
			Cursor.Current = Cursors.Default;
		}

		/// <summary>
		/// Saves the Tags grid into the current character
		/// </summary>
		private void SaveTags()
		{
			_selectedCharacter.Tags.Clear();
			for (int i = 0; i < gridTags.Rows.Count; i++)
			{
				DataGridViewRow row = gridTags.Rows[i];
				object value = row.Cells[0].Value;
				if (value == null)
					continue;
				string tag = value.ToString();
				_selectedCharacter.Tags.Add(tag);
			}
		}

		//Saves the Intelligence grid into the current character
		private void SaveIntelligence()
		{
			_selectedCharacter.Intelligence.Clear();
			for (int i = 0; i < gridAI.Rows.Count; i++)
			{
				DataGridViewRow row = gridAI.Rows[i];
				string level = row.Cells["ColDifficulty"].Value?.ToString();
				string stageString = row.Cells["ColAIStage"].Value?.ToString();
				if (string.IsNullOrEmpty(level))
					continue;
				stageString = stageString ?? string.Empty;
				int stage;
				if (int.TryParse(stageString, out stage))
				{
					_selectedCharacter.Intelligence.Add(new StageSpecificValue(stage, level));
				}
			}
		}

		//Saves the Labels grid into the current character
		private void SaveLabels()
		{
			_selectedCharacter.Labels.Clear();
			for (int i = 0; i < gridLabels.Rows.Count; i++)
			{
				DataGridViewRow row = gridLabels.Rows[i];
				string label = row.Cells["ColLabelsLabel"].Value?.ToString();
				string stageString = row.Cells["ColLabelsStage"].Value?.ToString();
				if (string.IsNullOrEmpty(label))
					continue;
				stageString = stageString ?? string.Empty;
				int stage;
				if (int.TryParse(stageString, out stage))
				{
					_selectedCharacter.Labels.Add(new StageSpecificValue(stage, label));
				}
			}
		}

		/// <summary>
		/// Exports the current character to disk (i.e. updates the meta.xml and behaviour.xml files)
		/// </summary>
		private bool Export()
		{
			if (_selectedCharacter == null)
				return true;
			SaveCharacter();
			if (Serialization.ExportCharacter(_selectedCharacter))
			{
				SetStatus(string.Format("{0} exported successfully.", _selectedCharacter));
				return true;
			}
			else
			{
				SetStatus(string.Format("{0} failed to export.", _selectedCharacter));
				return false;
			}
		}

		/// <summary>
		/// Exports the character to a text file for make_xml.py to read
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exporttxtFileForPythonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_selectedCharacter == null)
				return;
			SaveCharacter();
			if (FlatFileSerializer.ExportFlatFile(_selectedCharacter))
			{
				SetStatus("Generated edit-dialogue.txt");
			}
		}

		/// <summary>
		/// Imports a make_xml.py text file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void importtxtToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_selectedCharacter == null)
				return;
			string dir = Config.GetRootDirectory(_selectedCharacter);
			openFileDialog1.InitialDirectory = dir;
			openFileDialog1.FileName = "edit-dialogue.txt";
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				FlatFileSerializer.Import(openFileDialog1.FileName, _selectedCharacter);
				Character c = _selectedCharacter;
				_selectedCharacter = null;
				CharacterDatabase.Set(c.FolderName, c);
				LoadCharacter(c.FolderName);
			}
		}

		/// <summary>
		/// Opens the dialogue tester
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dialogueTesterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_selectedCharacter == null)
				return;
			SaveCharacter();
			_selectedCharacter.OnBeforeSerialize();
			GameSimulator sim = new GameSimulator();
			sim.SetCharacter(0, _selectedCharacter);
			sim.ShowDialog();
		}

		private void graphsToolStripItem_Click(object sender, EventArgs e)
		{
			ChartHost form = new ChartHost();
			form.ShowDialog();
		}

		/// <summary>
		/// Raised when changing tabs
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
		{
			if (_selectedCharacter == null || e.Action != TabControlAction.Selecting)
				return;
			if (tabControl.SelectedTab == tabDialogue)
			{
				if (_selectedCharacter.Layers < 2)
				{
					MessageBox.Show("You need to add at least two articles of clothing before adding dialogue.");
					e.Cancel = true;
				}
				ApplyWardrobeChanges();
			}
			else if (_currentTab == tabMarkers)
			{
				markerGrid.Save();
			}
		}

		/// <summary>
		/// Applies any pending wardrobe changes to the dialogue
		/// </summary>
		private void ApplyWardrobeChanges()
		{
			if (_wardrobeChanges.Count > 0)
			{
				_selectedCharacter.ApplyWardrobeChanges(_wardrobeChanges);
				_wardrobeChanges.Clear();
				CreateStageCheckboxes();
				GenerateDialogueTree(true);
			}
		}

		/// <summary>
		/// Raised after changing tabs
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			_currentTab = tabControl.SelectedTab;
			if (_selectedCharacter != null)
			{
				if (tabControl.SelectedTab == tabEndings)
				{
					epilogueEditor.SetCharacter(_selectedCharacter);
				}
				else if (tabControl.SelectedTab == tabImages)
				{
					imageImporter.SetCharacter(_selectedCharacter);
				}
				else if (tabControl.SelectedTab == tabMarkers)
				{
					markerGrid.SetCharacter(_selectedCharacter);
				}
			}
			if (tabControl.SelectedTab == tabDialogue)
			{
				findToolStripMenuItem.Enabled = true;
				replaceToolStripMenuItem.Enabled = true;
			}
			else
			{
				findToolStripMenuItem.Enabled = false;
				replaceToolStripMenuItem.Enabled = false;
			}
			_findForm.Hide();
		}


		/// <summary>
		/// Displays the character's portrait picture in the preview box
		/// </summary>
		private void DisplayPortrait()
		{
			CharacterImage image = _imageLibrary.Find(_selectedCharacter.Metadata.Portrait);
			DisplayImage(image);
		}

		/// <summary>
		/// Displays an image in the preview box
		/// </summary>
		/// <param name="image">Image to display</param>
		private void DisplayImage(CharacterImage image)
		{
			if (!Config.DisplayImages)
				return;
			if (image != null)
				picPortrait.Image = image.Image;
			else picPortrait.Image = null;
		}

		#region Wardrobe editor
		private void LoadLayer()
		{
			if (_selectedLayer == null)
				return;
			txtClothesProperName.Text = _selectedLayer.Name;
			txtClothesLowerCase.Text = _selectedLayer.Lowercase;
			cboClothesPosition.SelectedItem = _selectedLayer.Position;
			cboClothesType.SelectedItem = _selectedLayer.Type;
			ckbClothesPlural.Checked = _selectedLayer.Plural;
		}

		private void SaveLayer()
		{
			if (_selectedLayer == null)
				return;
			_selectedLayer.Name = txtClothesProperName.Text;
			_selectedLayer.Lowercase = txtClothesLowerCase.Text;
			_selectedLayer.Position = cboClothesPosition.SelectedItem.ToString();
			_selectedLayer.Type = cboClothesType.SelectedItem.ToString();
			_selectedLayer.Plural = ckbClothesPlural.Checked;
		}

		private void lstClothes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_populatingWardrobe)
				return;

			SaveLayer();
			_selectedLayer = lstClothes.SelectedItem as Clothing;
			LoadLayer();
		}

		private void cmdAddClothes_Click(object sender, System.EventArgs e)
		{
			Clothing layer = new Clothing();
			int index = _selectedCharacter.AddLayer(layer);
			lstClothes.Items.Add(layer);
			lstClothes.SelectedItem = layer;
			if (index >= 0)
			{
				_wardrobeChanges.Enqueue(new WardrobeChange(WardrobeChangeType.Add, index));
			}
			RegenerateWardrobeList(true);
		}

		private void cmdRemoveClothes_Click(object sender, System.EventArgs e)
		{
			if (_selectedLayer == null)
				return;
			int index = _selectedCharacter.RemoveLayer(_selectedLayer);
			lstClothes.Items.Remove(_selectedLayer);
			if (lstClothes.Items.Count > 0)
				lstClothes.SelectedIndex = 0;
			if (index >= 0)
			{
				_wardrobeChanges.Enqueue(new WardrobeChange(WardrobeChangeType.Remove, index));
			}

			RegenerateWardrobeList(true);
		}

		private void cmdClothesUp_Click(object sender, System.EventArgs e)
		{
			if (_selectedLayer == null)
				return;
			int index = _selectedCharacter.MoveUp(_selectedLayer);
			if (index >= 0)
			{
				_wardrobeChanges.Enqueue(new WardrobeChange(WardrobeChangeType.MoveUp, index));
			}
			RegenerateWardrobeList(true);
		}

		private void cmdClothesDown_Click(object sender, System.EventArgs e)
		{
			if (_selectedLayer == null)
				return;
			int index = _selectedCharacter.MoveDown(_selectedLayer);
			if (index >= 0)
			{
				_wardrobeChanges.Enqueue(new WardrobeChange(WardrobeChangeType.MoveDown, index));
			}
			RegenerateWardrobeList(true);
		}

		private void RegenerateWardrobeList(bool changingLayers)
		{
			wardrobeEditor.SetCharacter(_selectedCharacter);
			_populatingWardrobe = true;
			lstClothes.Items.Clear();
			for (int i = _selectedCharacter.Wardrobe.Count - 1; i >= 0; i--)
			{
				Clothing layer = _selectedCharacter.Wardrobe[i];
				lstClothes.Items.Add(layer);
			}
			_populatingWardrobe = false;
			lstClothes.SelectedItem = _selectedLayer;
			if (changingLayers)
			{
				//Apply change
				CreateStageCheckboxes();
			}
		}

		private void UpdateWardrobeList()
		{
			_populatingWardrobe = true;
			lstClothes.DisplayMember = "";
			lstClothes.DisplayMember = "Name";
			_populatingWardrobe = false;
		}

		private void txtClothesProperName_TextChanged(object sender, System.EventArgs e)
		{
			UpdateWardrobeList();
		}
		#endregion

		#region Metadata tab
		private void cboDefaultPic_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_selectedCharacter == null || _populatingImages)
				return;
			CharacterImage image = cboDefaultPic.SelectedItem as CharacterImage;
			if (image == null)
				return;
			_selectedCharacter.Metadata.Portrait = image.Name + image.FileExtension;
			DisplayImage(image);
		}
		#endregion


		#region Dialogue tab
		private void cboLineTarget_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_populatingCase)
				return;
			string key = cboLineTarget.SelectedItem.ToString();
			Character c = CharacterDatabase.Characters.Find(chr => chr.FolderName == key);
			PopulateStageCombo(cboTargetStage, c, true);
			PopulateStageCombo(cboTargetToStage, c, true);
			PopulateMarkerCombo(cboTargetMarker, c, false);
			PopulateMarkerCombo(cboTargetNotMarker, c, false);
		}

		private void cboAlsoPlaying_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_populatingCase)
				return;
			string key = cboAlsoPlaying.SelectedItem.ToString();
			Character c = CharacterDatabase.Characters.Find(chr => chr.FolderName == key);
			PopulateStageCombo(cboAlsoPlayingStage, c, false);
			PopulateStageCombo(cboAlsoPlayingMaxStage, c, false);
			PopulateMarkerCombo(cboAlsoPlayingMarker, c, false);
			PopulateMarkerCombo(cboAlsoPlayingNotMarker, c, false);
		}

		private enum TreeFilterMode
		{
			All = 0,
			NonTargeted = 1,
			Targeted = 2
		}

		private void cboTreeFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			TreeFilterMode mode = (TreeFilterMode)cboTreeFilter.SelectedIndex;
			if (mode != _filterMode)
			{
				_filterMode = mode;
				SaveCase(false);
				if (_filterMode == TreeFilterMode.Targeted)
					treeDialogue.ExpandAll();
			}
		}

		private void cboTreeTarget_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_filterMode != TreeFilterMode.Targeted)
				return; //no point in regenerating for filters that don't use the target
			SaveCase(false);
			treeDialogue.ExpandAll();
		}

		/// <summary>
		/// Updates a stage-specific dropdown to have display friendly options specific to the character being targeted
		/// </summary>
		/// <param name="box"></param>
		/// <param name="character"></param>
		/// <param name="useLookForward">If true, for removed/removing cases, only valid stages will be provided</param>
		private void PopulateStageCombo(ComboBox box, Character character, bool filterStages)
		{
			string oldText = box.Text;
			box.Items.Clear();
			box.Text = "";

			string tag = _selectedCase?.Tag;
			string filterType = null;
			bool removing = false;
			bool removed = false;
			bool lookForward = false;
			if (tag != null && filterStages)
			{
				removing = tag.Contains("removing_");
				removed = tag.Contains("removed_");
				lookForward = removing;
				if (removing || removed)
				{
					int index = tag.LastIndexOf('_');
					if (index >= 0 && index < tag.Length - 1)
					{
						filterType = tag.Substring(index + 1);
						if (filterType == "accessory")
							filterType = "extra";
					}
				}
			}

			if (character == null)
			{
				//If the character is not valid, still allow something but there's no way to give a useful name to it
				for (int i = 0; i < 8 + Clothing.ExtraStages; i++)
				{
					box.Items.Add(i);
				}
			}
			else
			{
				for (int i = 0; i < character.Layers + Clothing.ExtraStages; i++)
				{
					if (filterStages)
					{
						if (filterType != null)
						{
							//Filter out stages that will never be valid
							if (i >= 0 && i <= character.Layers)
							{
								int layer = removed ? i - 1 : i;
								if (layer < 0 || layer >= character.Layers)
									continue;

								Clothing clothing = character.Wardrobe[character.Layers - layer - 1];
								string realType = clothing.Type;
								if (filterType != realType.ToLower())
									continue;
							}
							else continue;
						}
					}
					box.Items.Add(character.LayerToStageName(i, lookForward));
				}
				if (!string.IsNullOrEmpty(oldText))
				{
					box.Text = oldText;
				}
			}
		}

		/// <summary>
		/// Sets the checked state for each stage for the current case
		/// </summary>
		private void CreateStageCheckboxes()
		{
			//Stage checkmarks
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (box != null)
				{
					box.CheckedChanged -= Check_CheckedChanged;
				}
			}
			flowStageChecks.Controls.Clear();
			int layers = _selectedCharacter.Layers + 3;
			for (int i = 0; i < layers; i++)
			{
				StageName stage = _selectedCharacter.LayerToStageName(i);
				CheckBox check = new CheckBox();
				check.CheckedChanged += Check_CheckedChanged;
				check.Tag = stage;
				check.Text = string.Format("{0} ({1})", stage.DisplayName, stage.Id);
				check.Width = 200;
				flowStageChecks.Controls.Add(check);
			}
		}

		/// <summary>
		/// Checks or unchecks all stages besides the current stage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			if (_populatingCase)
				return;
			int currentStage = _selectedStage == null ? 0 : _selectedStage.Id;
			bool newState = chkSelectAll.Checked;
			_populatingCase = true;
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				if (i == currentStage)
					continue;
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (box != null && box.Enabled)
				{
					box.Checked = newState;
				}
			}
			_populatingCase = false;
			gridDialogue.UpdateAvailableImagesForCase(GetSelectedStages(), true);
		}

		/// <summary>
		/// Raised when a Stage checkbox is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Check_CheckedChanged(object sender, EventArgs e)
		{
			if (_populatingCase)
				return;
			_populatingCase = true;
			UpdateCheckAllState();
			gridDialogue.UpdateAvailableImagesForCase(GetSelectedStages(), true);
			_populatingCase = false;
		}

		/// <summary>
		/// Updates a marker dropdown to contain only markers used in the given character's dialogue
		/// </summary>
		/// <param name="box"></param>
		/// <param name="character"></param>
		private void PopulateMarkerCombo(ComboBox box, Character character, bool allowPrivate)
		{
			string oldText = box.Text;
			box.Items.Clear();
			box.Text = "";
			if (character == null)
				return;

			foreach (var marker in character.Markers.Values)
			{
				if (allowPrivate || marker.Scope == MarkerScope.Public)
				{
					box.Items.Add(marker.Name);
				}
			}

			if (!string.IsNullOrEmpty(oldText))
			{
				box.Text = oldText;
			}
		}

		/// <summary>
		/// Generates the dialogue tree.
		/// </summary>
		/// <param name="initialGeneration">If true, the tree is completely built from scratch. If false, only the Case nodes are regenerated</param>
		public void GenerateDialogueTree(bool initialGeneration)
		{
			if (_selectedCharacter == null)
			{
				grpCase.Enabled = false;
				return;
			}
			if (_selectedCharacter.Layers < 2)
				return;
			_populatingTree = true;

			_selectedCharacter.Behavior.SortWorking();

			Dictionary<int, TreeNode> stageMap = new Dictionary<int, TreeNode>();
			Tuple<int, int> scrollPosition = null;
			if (initialGeneration)
			{
				treeDialogue.Nodes.Clear();
				_startNode = CreateNode(new DialogueWrapper(_selectedCharacter), null);
				_startNode.Text = "Starting Lines";

				//Create nodes for each stage
				int layers = _selectedCharacter.Layers + Clothing.ExtraStages;

				for (int i = 0; i < layers; i++)
				{
					TreeNode node = CreateNode(new DialogueWrapper(_selectedCharacter, new Stage(i)), null);
					stageMap[i] = node;
				}
			}
			else
			{
				//Save off the scroll position since it's going to get messed up regenerating the nodes
				scrollPosition = new Tuple<int, int>(GetScrollPos(treeDialogue.Handle, SB_HORZ), GetScrollPos(treeDialogue.Handle, SB_VERT));

				for (int i = 0; i < treeDialogue.Nodes.Count; i++)
				{
					TreeNode stageNode = treeDialogue.Nodes[i];
					stageNode.Nodes.Clear();
					DialogueWrapper wrapper = stageNode.Tag as DialogueWrapper;
					if (wrapper.NodeType == NodeType.Stage)
					{
						stageMap[wrapper.Stage.Id] = stageNode;
					}
				}
			}

			TreeNode selection = null;
			TreeNode selectionStageNode = null;
			string filterTarget = cboTreeTarget.SelectedItem?.ToString();
			foreach (Case c in _selectedCharacter.Behavior.WorkingCases)
			{
				//Exclude cases depending on filters. These are just excluded from the UI. This has no bearing on the actual underlying data
				switch (_filterMode)
				{
					case TreeFilterMode.NonTargeted:
						if (c.HasFilters)
							continue;
						break;
					case TreeFilterMode.Targeted:
						if (!c.HasFilters)
							continue;
						if (!string.IsNullOrEmpty(filterTarget))
						{
							if (c.Target != filterTarget && c.AlsoPlaying != filterTarget)
								continue;
						}
						break;
				}

				//Create a case node for each stage it occupies
				for (int i = 0; i < c.Stages.Count; i++)
				{
					int stage = c.Stages[i];
					TreeNode stageNode = stageMap[stage];

					if (selectionStageNode == null && _selectedStage != null && stage == _selectedStage.Id)
						selectionStageNode = stageNode;

					TreeNode caseNode = CreateNode(new DialogueWrapper(_selectedCharacter, c), stageNode);
					Tuple<int, string> key = new Tuple<int, string>(stage, c.Tag);
					if (_selectedStage != null && _selectedCase != null && stage == _selectedStage.Id && c == _selectedCase)
					{
						selection = caseNode;
					}
				}
			}
			if (selection == null && selectionStageNode != null)
				selection = selectionStageNode;

			treeDialogue.SelectedNode = selection;
			if (scrollPosition != null)
			{
				SetScrollPos(treeDialogue.Handle, SB_HORZ, scrollPosition.Item1, true);
				SetScrollPos(treeDialogue.Handle, SB_VERT, scrollPosition.Item2, true);
			}
			lblLinesOfDialogue.Text = _selectedCharacter.Behavior.UniqueLines.ToString();
			_populatingTree = false;
		}

		/// <summary>
		/// Creates a node in the dialogue tree
		/// </summary>
		/// <param name="wrapper"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		private TreeNode CreateNode(DialogueWrapper wrapper, TreeNode parent)
		{
			TreeNode node = new TreeNode();
			node.Text = wrapper.ToString();
			node.Tag = wrapper;

			if (wrapper.NodeType == NodeType.Case)
			{
				node.ContextMenuStrip = splitMenu;
				if (wrapper.Case.HasFilters)
				{
					//Highlight targeted dialogue
					node.ForeColor = System.Drawing.Color.Green;
				}
				else
				{
					//Highlight lines that are still using the default
					Tuple<string, string> template = DialogueDatabase.GetTemplate(wrapper.Case.Tag);
					if (template != null)
					{
						foreach (var line in wrapper.Case.Lines)
						{
							if (Path.GetFileNameWithoutExtension(line.Image) == template.Item1 && line.Text?.Trim() == template.Item2)
							{
								node.ForeColor = System.Drawing.Color.Blue;
								//Color parent too
								TreeNode ancestor = parent;
								while (ancestor != null)
								{
									ancestor.ForeColor = System.Drawing.Color.Blue;
									ancestor = ancestor.Parent;
								}
							}
						}
					}
				}
			}

			if (parent == null)
			{
				treeDialogue.Nodes.Add(node);
			}
			else
			{
				parent.Nodes.Add(node);
			}
			return node;
		}

		/// <summary>
		/// Helper class for tagging tree nodes to particular cases
		/// </summary>
		private class DialogueWrapper
		{
			public NodeType NodeType;

			public Character Character;

			public Stage Stage;
			public Case Case;
			public DialogueLine Line;

			public DialogueWrapper(Character character)
			{
				Character = character;
				NodeType = NodeType.Start;
			}

			public DialogueWrapper(Character character, Stage stage)
			{
				Character = character;
				NodeType = NodeType.Stage;
				Stage = stage;
			}

			public DialogueWrapper(Character character, Case stageCase)
			{
				Character = character;
				NodeType = NodeType.Case;
				Case = stageCase;
			}

			public DialogueWrapper(Character character, DialogueLine line)
			{
				Character = character;
				NodeType = NodeType.Line;
				Line = line;
			}

			public override string ToString()
			{
				switch (NodeType)
				{
					case NodeType.Start:
						return "Starting Lines";
					case NodeType.Stage:
						return string.Format("Stage: {0} ({1})", Character.LayerToStageName(Stage.Id), Stage.Id);
					case NodeType.Case:
						return string.Format("{0}", Case.ToString());
					case NodeType.Line:
						return Line.ToString();
					default:
						return "Unknown node";
				}
			}
		}

		private enum NodeType
		{
			Stage,
			Case,
			Line,
			Start
		}
		#endregion

		/// <summary>
		/// Raised when a new node is selected in the dialogue tree
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeDialogue_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (_populatingTree)
				return;
			TreeNode node = treeDialogue.SelectedNode;
			if (node == null)
			{
				grpCase.Enabled = false;
				return;
			}

			DialogueWrapper wrapper = node.Tag as DialogueWrapper;
			if (wrapper == null)
			{
				tsbtnRemoveDialogue.Enabled = false;
				tsbtnSplit.Enabled = false;
				return;
			}

			bool changing = _selectedCase != null;
			bool needRegeneration = SaveCase(true);

			TreeNode parent;
			DialogueWrapper parentWrapper;
			switch (wrapper.NodeType)
			{
				case NodeType.Start:
					_selectedStage = null;
					_selectedCase = new Case(Trigger.StartTrigger);
					tsbtnRemoveDialogue.Enabled = false;
					tsbtnSplit.Enabled = false;
					tabControlConditions.Enabled = false;
					break;
				case NodeType.Case:
					_selectedCase = wrapper.Case;
					parent = node.Parent;
					parentWrapper = parent.Tag as DialogueWrapper;
					_selectedStage = parentWrapper.Stage;
					tabControlConditions.Enabled = true;
					tsbtnRemoveDialogue.Enabled = true;
					tsbtnSplit.Enabled = true;
					break;
				case NodeType.Stage:
					_selectedStage = wrapper.Stage;
					_selectedCase = null;
					tsbtnRemoveDialogue.Enabled = false;
					tsbtnSplit.Enabled = false;
					break;
			}
			PopulateCase();
			if (changing && needRegeneration)
			{
				GenerateDialogueTree(false);
			}
		}

		/// <summary>
		/// Loads the newly selected case into the dialogue fields
		/// </summary>
		private void PopulateCase()
		{
			string minStage, maxStage;
			if (_selectedCase == null)
			{
				grpCase.Visible = false;
				return;
			}
			else
			{
				grpCase.Visible = true;
				grpCase.Enabled = true;
			}

			_populatingCase = true;
			Case stageCase = _selectedCase;
			PopulateStageCheckboxes();
			PopulateTargetField();

			int stageId = _selectedStage == null ? 0 : _selectedStage.Id;
			Trigger caseTrigger = TriggerDatabase.GetTrigger(stageCase.Tag);

			#region Case-wide settings
			//Tag combo box
			cboCaseTags.Items.Clear();
			if (_selectedStage != null)
			{
				Trigger selection = null;
				foreach (string tag in TriggerDatabase.GetTags())
				{
					if (TriggerDatabase.UsedInStage(tag, _selectedCharacter, stageId))
					{
						Trigger t = new Trigger(tag, TriggerDatabase.GetLabel(tag));
						if (tag == _selectedCase.Tag)
							selection = t;
						cboCaseTags.Items.Add(t);
					}
				}
				cboCaseTags.SelectedItem = selection;
				cboCaseTags.Enabled = true;
			}
			else
			{
				cboCaseTags.Enabled = false;
			}

			//Help text
			lblHelpText.Text = caseTrigger.HelpText;

			//Available variables
			List<string> vars = new List<string>();
			vars.Add("~player~");
			foreach (string variable in caseTrigger.AvailableVariables)
			{
				vars.Add("~" + variable + "~");
			}
			lblAvailableVars.Text = string.Format("Variables: {0}", string.Join(" ", vars));

			#endregion

			#region Target tab
			ClearConditionFields();
			if (caseTrigger.HasTarget)
			{
				((Control)tabTarget).Enabled = true;
				SetComboBox(cboLineTarget, _selectedCase.Target);
				SetComboBox(cboTargetHand, _selectedCase.TargetHand);
				SetComboBox(cboLineFilter, _selectedCase.Filter);
				Character target = CharacterDatabase.Characters.Find(c => c.FolderName == _selectedCase.Target);
				_selectedCase.SplitTargetStage(out minStage, out maxStage);
				PopulateStageCombo(cboTargetStage, target, true);
				SetStageComboBox(cboTargetStage, minStage);
				PopulateStageCombo(cboTargetToStage, target, true);
				SetStageComboBox(cboTargetToStage, maxStage);
				PopulateMarkerCombo(cboTargetMarker, target, false);
				cboTargetMarker.Text = _selectedCase.TargetSaidMarker;
				PopulateMarkerCombo(cboTargetNotMarker, target, false);
				cboTargetNotMarker.Text = _selectedCase.TargetNotSaidMarker;
				SetRange(valTimeInStage, valMaxTimeInStage, _selectedCase.TargetTimeInStage);
				SetRange(valLosses, valMaxLosses, _selectedCase.ConsecutiveLosses);
				valOwnLosses.Enabled = false;
				valMaxOwnLosses.Enabled = false;
			}
			else
			{
				((Control)tabTarget).Enabled = false;
				SetComboBox(cboLineTarget, "");
				cboTargetStage.Text = "";
				cboTargetToStage.Text = "";
				cboTargetMarker.Text = "";
				cboTargetNotMarker.Text = "";
				SetComboBox(cboTargetHand, "");
				SetComboBox(cboLineFilter, "");
				valOwnLosses.Enabled = true;
				valMaxOwnLosses.Enabled = true;
			}
			#endregion

			#region Also Playing tab
			SetComboBox(cboAlsoPlaying, _selectedCase.AlsoPlaying);
			SetComboBox(cboAlsoPlayingHand, _selectedCase.AlsoPlayingHand);
			Character other = CharacterDatabase.Characters.Find(c => c.FolderName == _selectedCase.AlsoPlaying);
			cboAlsoPlayingStage.Text = "";
			cboAlsoPlayingMaxStage.Text = "";
			SetRange(valAlsoTimeInStage, valMaxAlsoTimeInStage, _selectedCase.AlsoPlayingTimeInStage);

			_selectedCase.SplitAlsoPlayingStage(out minStage, out maxStage);
			PopulateStageCombo(cboAlsoPlayingStage, other, false);
			SetStageComboBox(cboAlsoPlayingStage, minStage);
			PopulateStageCombo(cboAlsoPlayingMaxStage, other, false);
			SetStageComboBox(cboAlsoPlayingMaxStage, maxStage);
			PopulateMarkerCombo(cboAlsoPlayingMarker, other, false);
			PopulateMarkerCombo(cboAlsoPlayingNotMarker, other, false);
			cboAlsoPlayingMarker.Text = _selectedCase.AlsoPlayingSaidMarker;
			cboAlsoPlayingNotMarker.Text = _selectedCase.AlsoPlayingNotSaidMarker;
			#endregion

			#region Self tab
			cboOwnHand.SelectedItem = _selectedCase.HasHand;
			SetRange(valOwnLosses, valMaxOwnLosses, _selectedCase.ConsecutiveLosses);
			SetRange(valOwnTimeInStage, valMaxOwnTimeInStage, _selectedCase.TimeInStage);
			PopulateMarkerCombo(cboMarker, _selectedCharacter, true);
			PopulateMarkerCombo(cboNotMarker, _selectedCharacter, true);
			cboMarker.Text = _selectedCase.SaidMarker;
			cboNotMarker.Text = _selectedCase.NotSaidMarker;
			#endregion

			#region Misc tab
			SetRange(cboTotalFemales, cboMaxTotalFemales, _selectedCase.TotalFemales);
			SetRange(cboTotalMales, cboMaxTotalMales, _selectedCase.TotalMales);
			SetRange(valGameRounds, valMaxGameRounds, _selectedCase.TotalRounds);
			SetRange(cboTotalPlaying, cboMaxTotalPlaying, _selectedCase.TotalPlaying);
			SetRange(cboTotalExposed, cboMaxTotalExposed, _selectedCase.TotalExposed);
			SetRange(cboTotalNaked, cboMaxTotalNaked, _selectedCase.TotalNaked);
			SetRange(cboTotalFinishing, cboMaxTotalFinishing, _selectedCase.TotalFinishing);
			SetRange(cboTotalFinished, cboMaxTotalFinished, _selectedCase.TotalFinished);
			SetNumericBox(valPriority, _selectedCase.CustomPriority);
			#endregion

			#region Tags tab
			LoadFilterConditions();
			#endregion

			#region Dialogue
			var stages = GetSelectedStages();
			gridDialogue.SetData(_selectedCharacter, _selectedStage, _selectedCase, stages, _imageLibrary);
			GetSelectedStages();
			#endregion

			_populatingCase = false;
			HighlightRow(0);
		}

		private HashSet<int> GetSelectedStages()
		{
			HashSet<int> selectedStages = new HashSet<int>();
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (box.Checked)
				{
					selectedStages.Add(i);
				}
			}
			return selectedStages;
		}

		private void ClearConditionFields()
		{
			foreach (TabPage page in tabControlConditions.TabPages)
			{
				foreach (Control ctl in page.Controls)
				{
					if (ctl is TextBox || ctl is NumericUpDown)
						ctl.Text = "";
					else if (ctl is ComboBox)
					{
						ComboBox box = ctl as ComboBox;
						box.SelectedIndex = -1;
						box.Text = "";
					}

				}
			}
		}

		/// <summary>
		/// Sets a range value into its boxes
		/// </summary>
		/// <param name="minBox"></param>
		/// <param name="maxBox"></param>
		/// <param name="value"></param>
		public static void SetRange(ComboBox minBox, ComboBox maxBox, string value)
		{
			if (value == null)
			{
				minBox.Text = "";
				maxBox.Text = "";
				return;
			}
			string[] pieces = value.Split('-');
			string min = pieces[0];
			string max = null;
			if (pieces.Length > 1)
			{
				max = pieces[1];
			}
			if (string.IsNullOrEmpty(min))
			{
				minBox.Text = "";
			}
			else
			{
				minBox.Text = min;
			}
			if (string.IsNullOrEmpty(max))
			{
				maxBox.Text = "";
			}
			else
			{
				maxBox.Text = max;
			}
		}

		/// <summary>
		/// Sets a range value into its boxes
		/// </summary>
		/// <param name="minBox"></param>
		/// <param name="maxBox"></param>
		/// <param name="value"></param>
		public static void SetRange(NumericUpDown minBox, NumericUpDown maxBox, string value)
		{
			if (value == null)
			{
				SetNumericBox(minBox, null);
				SetNumericBox(maxBox, null);
				return;
			}
			string[] pieces = value.Split('-');
			string min = pieces[0];
			string max = null;
			if (pieces.Length > 1)
			{
				max = pieces[1];
			}
			SetNumericBox(minBox, min);
			SetNumericBox(maxBox, max);
		}

		public static string ReadRange(ComboBox minBox, ComboBox maxBox)
		{
			string min = minBox.Text;
			if (string.IsNullOrEmpty(min))
				return null;
			string max = maxBox.Text;
			if (string.IsNullOrEmpty(max))
				return min;
			return min + "-" + max;
		}

		public static string ReadRange(NumericUpDown minBox, NumericUpDown maxBox)
		{
			string min = ReadNumericBox(minBox);
			if (string.IsNullOrEmpty(min))
				return null;
			string max = ReadNumericBox(maxBox);
			if (string.IsNullOrEmpty(max))
				return min;
			return min + "-" + max;
		}

		/// <summary>
		/// Sets a stage target box to the given stage
		/// </summary>
		/// <param name="box"></param>
		/// <param name="stage"></param>
		private void SetStageComboBox(ComboBox box, string stage)
		{
			for (int i = 0; i < box.Items.Count; i++)
			{
				StageName stageName = box.Items[i] as StageName;
				if (stageName != null && stageName.Id == stage)
				{
					box.SelectedIndex = i;
					return;
				}
			}

			box.Text = stage; //If couldn't set an object, just set the text
		}

		/// <summary>
		/// Attempts to set a combo box's value to the provided text
		/// </summary>
		/// <param name="box"></param>
		/// <param name="text"></param>
		private void SetComboBox(ComboBox box, string text)
		{
			box.Text = text;
		}

		/// <summary>
		/// Reads the value from a combo box
		/// </summary>
		/// <param name="box"></param>
		/// <returns></returns>
		private string ReadComboBox(ComboBox box)
		{
			if (box.SelectedItem is Trigger)
			{
				return ((Trigger)box.SelectedItem).Tag;
			}
			else if (box.SelectedItem is Tag)
			{
				return TagDatabase.StringToTag(box.Text);
			}
			string value = box.Text;
			if (value == "")
				return null;
			else return value;
		}

		private static void SetNumericBox(NumericUpDown box, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				box.Text = "";
			}
			else
			{
				int v;
				if (int.TryParse(value, out v) && v >= box.Minimum && v <= box.Maximum)
				{
					box.Value = v;
					box.Text = v.ToString();
				}
			}
		}

		static string ReadNumericBox(NumericUpDown box)
		{
			if (string.IsNullOrEmpty(box.Text))
				return null;
			return box.Value.ToString();
		}

		/// <summary>
		/// Reads the value from a stage dropdown
		/// </summary>
		/// <param name="box"></param>
		/// <returns></returns>
		private string ReadStageComboBox(ComboBox box)
		{
			StageName stage = box.SelectedItem as StageName;
			if (stage == null)
			{
				//Must be a generic stage
				return box.Text;
			}

			return stage.Id;
		}

		/// <summary>
		/// Populates the Filters grid
		/// </summary>
		private void LoadFilterConditions()
		{
			if (_selectedCase == null)
				return;
			gridFilters.Rows.Clear();
			DataGridViewComboBoxColumn col = gridFilters.Columns["ColTagFilter"] as DataGridViewComboBoxColumn;
			foreach (TargetCondition condition in _selectedCase.Conditions)
			{
				if (string.IsNullOrEmpty(condition.Filter))
					continue;
				DataGridViewRow row = gridFilters.Rows[gridFilters.Rows.Add()];
				if (!col.Items.Contains(condition.Filter))
				{
					col.Items.Add(condition.Filter);
				}
				try
				{
					row.Cells["ColTagFilter"].Value = condition.Filter;
					row.Cells["ColTagCount"].Value = condition.Count;
				}
				catch { }
			}
		}

		/// <summary>
		/// Saves filter conditions into the case
		/// </summary>
		private void SaveFilterConditions()
		{
			if (_selectedCase == null)
				return;
			_selectedCase.Conditions.Clear();
			for (int i = 0; i < gridFilters.Rows.Count; i++)
			{
				DataGridViewRow row = gridFilters.Rows[i];
				string filter = row.Cells["ColTagFilter"].Value?.ToString();
				string countValue = row.Cells["ColTagCount"].Value?.ToString();
				if (string.IsNullOrEmpty(filter) || string.IsNullOrEmpty(countValue))
					continue;
				TargetCondition condition = new TargetCondition(filter, countValue);
				_selectedCase.Conditions.Add(condition);
			}
		}

		/// <summary>
		/// Puts the data in the fields into the selected case object
		/// </summary>
		/// <param name="switchingCases">True when saving within the context of switching selected cases</param>
		/// <returns>True if cases were changed in such a way that the dialogue tree needs to be regenerated</returns>
		private bool SaveCase(bool switchingCases)
		{
			if (_selectedCase == null)
				return false;
			bool needRegeneration = false;
			var c = _selectedCase;
			if (c.Tag != Trigger.StartTrigger)
			{
				string newTag = ReadComboBox(cboCaseTags);
				if (newTag != c.Tag)
					needRegeneration = true;
				c.Tag = newTag;
				Trigger trigger = TriggerDatabase.GetTrigger(newTag);

				//Figure out the stages
				List<int> oldStages = new List<int>();
				oldStages.AddRange(c.Stages);
				c.Stages.Clear();
				for (int i = 0; i < flowStageChecks.Controls.Count; i++)
				{
					CheckBox box = flowStageChecks.Controls[i] as CheckBox;
					if (box.Checked && TriggerDatabase.UsedInStage(newTag, _selectedCharacter, i))
					{
						c.Stages.Add(i);
						if (!oldStages.Contains(i))
							needRegeneration = true;
					}
					else if (oldStages.Contains(i))
						needRegeneration = true;
				}
				
				#region Target tab
				if (trigger.HasTarget)
				{
					c.Target = ReadComboBox(cboLineTarget);
					c.SetTargetStage(ReadStageComboBox(cboTargetStage), ReadStageComboBox(cboTargetToStage));
					c.TargetHand = ReadComboBox(cboTargetHand);
					c.Filter = ReadComboBox(cboLineFilter);
					c.TargetTimeInStage = ReadRange(valTimeInStage, valMaxTimeInStage);
					c.ConsecutiveLosses = ReadRange(valLosses, valMaxLosses);
					c.TargetSaidMarker = ReadComboBox(cboTargetMarker);
					c.TargetNotSaidMarker = ReadComboBox(cboTargetNotMarker);
				}
				else
				{
					c.Target = null;
					c.TargetStage = null;
					c.TargetHand = null;
					c.Filter = null;
					c.TargetTimeInStage = null;
					c.TargetSaidMarker = null;
					c.TargetNotSaidMarker = null;
				}
				#endregion

				#region Also Playing Tab
				c.AlsoPlaying = ReadComboBox(cboAlsoPlaying);
				c.AlsoPlayingHand = ReadComboBox(cboAlsoPlayingHand);
				c.SetAlsoPlayingStage(ReadStageComboBox(cboAlsoPlayingStage), ReadStageComboBox(cboAlsoPlayingMaxStage));
				c.AlsoPlayingTimeInStage = ReadRange(valAlsoTimeInStage, valMaxAlsoTimeInStage);
				c.AlsoPlayingSaidMarker = ReadComboBox(cboAlsoPlayingMarker);
				c.AlsoPlayingNotSaidMarker = ReadComboBox(cboAlsoPlayingNotMarker);
				#endregion

				#region Self tab
				c.SaidMarker = ReadComboBox(cboMarker);
				c.NotSaidMarker = ReadComboBox(cboNotMarker);
				c.HasHand = ReadComboBox(cboOwnHand);
				c.TimeInStage = ReadRange(valOwnTimeInStage, valMaxOwnTimeInStage);
				if (!trigger.HasTarget)
				{
					c.ConsecutiveLosses = ReadRange(valOwnLosses, valMaxOwnLosses);
				}
				#endregion

				#region Misc tab
				c.TotalFemales = ReadRange(cboTotalFemales, cboMaxTotalFemales);
				c.TotalMales = ReadRange(cboTotalMales, cboMaxTotalMales);
				c.TotalRounds = ReadRange(valGameRounds, valMaxGameRounds);
				c.TotalPlaying = ReadRange(cboTotalPlaying, cboMaxTotalPlaying);
				c.TotalExposed = ReadRange(cboTotalExposed, cboMaxTotalExposed);
				c.TotalNaked = ReadRange(cboTotalNaked, cboMaxTotalNaked);
				c.TotalFinishing = ReadRange(cboTotalFinishing, cboMaxTotalFinishing);
				c.TotalFinished = ReadRange(cboTotalFinished, cboMaxTotalFinished);
				c.CustomPriority = ReadNumericBox(valPriority);
				#endregion

				#region Tags tab
				SaveFilterConditions();
				#endregion
			}

			//Lines
			gridDialogue.Save();
			
			if (!switchingCases)
				GenerateDialogueTree(false);

			return needRegeneration;
		}

		/// <summary>
		/// Updates the stage checkboxes to match the selected case's stages
		/// </summary>
		private void PopulateStageCheckboxes()
		{
			chkSelectAll.Enabled = (_selectedStage != null);
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (_selectedCase != null)
				{
					box.Enabled = TriggerDatabase.UsedInStage(_selectedCase.Tag, _selectedCharacter, i);
				}
				box.Checked = _selectedCase == null ? false : _selectedCase.Stages.Contains(i);
			}
			UpdateCheckAllState();
		}

		/// <summary>
		/// Updates the Select All checkbox based on the individual stage checkboxes
		/// </summary>
		private void UpdateCheckAllState()
		{
			bool allChecked = true;
			bool noneChecked = true;
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (_selectedStage != null && _selectedStage.Id != i && box.Enabled)
				{
					if (box.Checked)
						noneChecked = false;
					else allChecked = false;
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

		/// <summary>
		/// Adds a new case whose tag matches the currently selected case
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbtnAddDialogue_ButtonClick(object sender, EventArgs e)
		{
			if (_selectedCharacter == null || _selectedStage == null || _selectedCase == null)
			{
				tsbtnAddDialogue.ShowDropDown();
				return;
			}
			TreeNode node = treeDialogue.SelectedNode;
			if (node == null)
				return;
			DialogueWrapper wrapper = node.Tag as DialogueWrapper;
			if (wrapper == null)
				return;

			TreeNode stageNode = (wrapper.NodeType == NodeType.Stage ? node : node.Parent);
			Stage stage = ((DialogueWrapper)stageNode.Tag).Stage;
			SaveCase(true);
			string tag = _selectedCase.Tag;
			Case newCase = new Case(tag);
			newCase.Stages.Add(stage.Id);
			Tuple<string, string> template = DialogueDatabase.GetTemplate(tag);
			DialogueLine line = new DialogueLine(template.Item1, template.Item2);
			_selectedCharacter.Behavior.WorkingCases.Add(newCase);
			newCase.Lines.Add(line);
			_selectedCase = newCase;
			GenerateDialogueTree(false);
			PopulateCase();
		}

		private void triggerMenuItem_Click(object sender, System.EventArgs e)
		{
			if (_selectedCharacter == null)
				return;
			SaveCase(true);
			string tag = ((ToolStripMenuItem)sender).Name;
			Case newCase = new Case(tag);
			Trigger t = TriggerDatabase.GetTrigger(tag);
			for (int stage = 0; stage < flowStageChecks.Controls.Count; stage++)

			{
				if (TriggerDatabase.UsedInStage(tag, _selectedCharacter, stage))
				{
					newCase.Stages.Add(stage);
				}
			}
			Tuple<string, string> template = DialogueDatabase.GetTemplate(tag);
			DialogueLine line = new DialogueLine(template.Item1, template.Item2);
			_selectedCharacter.Behavior.WorkingCases.Add(newCase);
			newCase.Lines.Add(line);
			if (_selectedStage == null || !newCase.Stages.Contains(_selectedStage.Id))
			{
				foreach (TreeNode n in treeDialogue.Nodes)
				{
					DialogueWrapper w = n.Tag as DialogueWrapper;
					if (w.NodeType == NodeType.Stage && w.Stage.Id == newCase.Stages[0])
					{
						_selectedStage = w.Stage;
					}
				}
			}
			_selectedCase = newCase;
			GenerateDialogueTree(false);
			PopulateCase();
		}

		/// <summary>
		/// Performs a bulk replace from one case to another
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bulkReplaceToolToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_selectedCase == null)
				return;
			SaveCase(true);
			BulkReplaceTool replacer = new BulkReplaceTool();
			replacer.SourceTag = _selectedCase.Tag;
			if (replacer.ShowDialog() == DialogResult.OK)
			{
				_selectedCharacter.Behavior.BulkReplace(replacer.SourceTag, replacer.DestinationTags);
				GenerateDialogueTree(false);
				SetStatus("Dialogue replaced.");
			}
		}

		/// <summary>
		/// Duplicates the selected case
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdDupe_Click(object sender, EventArgs e)
		{
			if (_selectedCharacter == null || _selectedCase == null || _selectedStage == null)
				return;
			SaveCase(true);
			Case copy = _selectedCharacter.Behavior.DuplicateCase(_selectedCase);
			_selectedCase = copy;
			GenerateDialogueTree(false);
		}

		/// <summary>
		/// Displays the split context menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbtnSplit_Click(object sender, EventArgs e)
		{
			Control ctl = sender as Control;
			splitMenu.Show(sender as Control, 0, ctl.Height);
		}

		/// <summary>
		/// Split All context item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void splitAll_Click(object sender, EventArgs e)
		{
			SplitAllStages();
		}

		/// <summary>
		/// Split at point context item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void splitAtPoint_Click(object sender, EventArgs e)
		{
			SeparateStage();
		}

		/// <summary>
		/// Separate stage context item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void separateThisStageIntoANewCaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SeparateCaseFromStage();
		}

		/// <summary>
		/// Separates the selected case into individual copies for each applicable stage
		/// </summary>
		private void SplitAllStages()
		{
			if (_selectedCharacter == null || _selectedCase == null || _selectedStage == null)
				return;
			SaveCase(true);
			_selectedCharacter.Behavior.DivideCaseIntoSeparateStages(_selectedCase, _selectedStage.Id);
			GenerateDialogueTree(false);
			PopulateCase();
		}

		/// <summary>
		/// Separates the current stage into a new case with the same conditions and dialogue as the selected one
		/// </summary>
		private void SeparateCaseFromStage()
		{
			if (_selectedCharacter == null || _selectedCase == null || _selectedStage == null)
				return;
			SaveCase(true);
			_selectedCharacter.Behavior.SplitCaseStage(_selectedCase, _selectedStage.Id);
			GenerateDialogueTree(false);
			PopulateCase();
		}

		/// <summary>
		/// Separates the current stage and later ones from the current case, effectively splitting the case in two
		/// </summary>
		private void SeparateStage()
		{
			if (_selectedCharacter == null || _selectedCase == null || _selectedStage == null)
				return;
			SaveCase(true);
			_selectedCharacter.Behavior.SplitCaseAtStage(_selectedCase, _selectedStage.Id);
			GenerateDialogueTree(false);
			PopulateCase();
		}

		/// <summary>
		/// Keyboard shortcut handling for the dialogue tree
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeDialogue_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				DeleteSelectedCase();
			}
		}

		/// <summary>
		/// Removes a case
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbtnRemoveDialogue_Click(object sender, EventArgs e)
		{
			DeleteSelectedCase();
		}

		/// <summary>
		/// Removes a case from the tree
		/// </summary>
		private void DeleteSelectedCase()
		{
			if (_selectedCharacter == null || _selectedCase == null)
				return;

			if (MessageBox.Show("Are you sure you want to permanently delete this case from all applicable stages?", "Delete Case", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_selectedCharacter.Behavior.WorkingCases.Remove(_selectedCase);
				_selectedCase = null;
				GenerateDialogueTree(false);
				PopulateCase();
			}
		}

		/// <summary>
		/// Adds the character to listing.xml
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdAddToListing_Click(object sender, EventArgs e)
		{
			if (_listing.Characters.Exists(opp => opp.Name == _selectedCharacter.FolderName))
			{
				MessageBox.Show("This character is already in the listing.");
				return;
			}
			_selectedCharacter.Metadata.Enabled = true;
			_listing.Characters.Add(new Opponent(_selectedCharacter.FolderName, OpponentStatus.Testing));
			Serialization.ExportListing(_listing);
			Export();
			lblUnlisted.Visible = false;
			lblTesting.Visible = true;
			cmdAddToListing.Enabled = false;
		}
		
		private void gridDialogue_HighlightRow(object sender, int index)
		{
			HighlightRow(index);
		}

		/// <summary>
		/// Updates the preview image to display the selected line of dialogue's pose
		/// </summary>
		/// <param name="index"></param>
		private void HighlightRow(int index)
		{
			if (index == -1 || _populatingCase)
				return;
			string image = gridDialogue.GetImage(index);
			CharacterImage img = null;
			img = _imageLibrary.Find(image);
			if (img == null)
			{
				int stage = _selectedStage == null ? 0 : _selectedStage.Id;
				image = DialogueLine.GetDefaultImage(image);
				img = _imageLibrary.Find(stage + "-" + image);
			}
			DisplayImage(img);
		}

		private List<DialogueLine> _lineClipboard = new List<DialogueLine>();
		/// <summary>
		/// Copies the current case's lines to the clipboard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdCopyAll_Click(object sender, EventArgs e)
		{
			if (_selectedCase == null)
				return;
			_lineClipboard = gridDialogue.CopyLines();
			SetStatus(string.Format("Lines from {0} copied to the clipboard.", _selectedCase));
		}

		/// <summary>
		/// Pastes the lines in the clipboard into the selected case, either replacing or appending to the existing lines
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdPasteAll_Click(object sender, EventArgs e)
		{
			if (_selectedCase == null || _lineClipboard.Count == 0)
				return;

			if (!gridDialogue.IsEmpty)
			{
				DialogResult result = MessageBox.Show("Do you want to overwrite the existing lines?", "Paste Lines", MessageBoxButtons.YesNoCancel);
				if (result == DialogResult.Cancel)
					return;
				else if (result == DialogResult.Yes)
				{
					gridDialogue.Clear();
				}
			}
			gridDialogue.PasteLines(_lineClipboard);
		}

		/// <summary>
		/// Updates help text when changing a clothing type
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboClothesType_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = cboClothesType.Text;
			if (text == "important")
			{
				lblTypeHelp.Text = "Covers nudity (e.g. underwear)";
			}
			else if (text == "major")
			{
				lblTypeHelp.Text = "Covers a lot of skin, but no nudity (e.g. shirt)";
			}
			else if (text == "minor")
			{
				lblTypeHelp.Text = "Covers a little skin (e.g. jacket)";
			}
			else if (text == "extra")
			{
				lblTypeHelp.Text = "Covers nothing of importance (e.g. necklace)";
			}
		}

		/// <summary>
		/// Updates help text when selecting a clothing position
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboClothesPosition_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = cboClothesPosition.Text;
			if (text == "upper")
			{
				lblPositionHelp.Text = "Covers the chest area";
			}
			else if (text == "lower")
			{
				lblPositionHelp.Text = "Covers the crotch area";
			}
			else
			{
				lblPositionHelp.Text = "Covers any other area";
			}
		}

		#region Character validation
		/// <summary>
		/// Validates the current character
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void currentCharacterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_selectedCharacter == null)
				return;
			SaveCharacter();
			_selectedCharacter.Behavior.BuildStageTree(_selectedCharacter);
			List<ValidationError> warnings;
			bool valid = CharacterValidator.Validate(_selectedCharacter, _listing, out warnings);
			if (valid)
			{
				MessageBox.Show("Everything checks out!");
			}
			else
			{
				Dictionary<Character, List<ValidationError>> validationResults = new Dictionary<Character, List<ValidationError>>();
				validationResults[_selectedCharacter] = warnings;
				ValidationForm form = new ValidationForm();
				form.SetData(validationResults);
				form.ShowDialog();
			}
		}

		/// <summary>
		/// Validates all characters
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allCharactersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<Character, List<ValidationError>> allWarnings = new Dictionary<Character, List<ValidationError>>();
			if (_selectedCharacter == null)
				return;
			SaveCharacter();
			_selectedCharacter.Behavior.BuildStageTree(_selectedCharacter);

			ProgressForm progressForm = new ProgressForm();
			progressForm.Text = "Character Validation";
			progressForm.Show();

			int count = CharacterDatabase.Characters.Count;
			var progressUpdate = new Progress<int>(value => progressForm.SetProgress(string.Format("Validating {0} of {1}...", value, count), value, count));

			progressForm.Shown += async (s, args) =>
			{
				var cts = new CancellationTokenSource();
				progressForm.SetCancellationSource(cts);
				try
				{
					allWarnings = await ValidateAll(progressUpdate, cts.Token);
				}
				finally
				{
					progressForm.Close();
				}
				if (allWarnings != null)
				{
					if (allWarnings.Count == 0)
					{
						MessageBox.Show("No validation warnings found.");
					}
					else
					{
						ValidationForm form = new ValidationForm();
						form.SetData(allWarnings);
						form.ShowDialog();
					}
				}
			};
		}

		/// <summary>
		/// Goes through each character and validates them, updating the progress bar each time
		/// </summary>
		/// <param name="progress"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		private Task<Dictionary<Character, List<ValidationError>>> ValidateAll(IProgress<int> progress, CancellationToken ct)
		{
			return Task.Run(() =>
			{
				try
				{
					Dictionary<Character, List<ValidationError>> allWarnings = new Dictionary<Character, List<ValidationError>>();
					int current = 0;
					foreach (Character c in CharacterDatabase.Characters)
					{
						OpponentStatus status = _listing.GetCharacterStatus(c.FolderName);
						if (status == OpponentStatus.Incomplete || status == OpponentStatus.Offline)
							continue; //don't validate characters that aren't in the main opponents folder, since they're likely to have errors but aren't being actively worked on
						current++;
						progress.Report(current);
						List<ValidationError> warnings;
						if (!CharacterValidator.Validate(c, _listing, out warnings))
						{
							allWarnings[c] = warnings;
						}
						ct.ThrowIfCancellationRequested();
					}
					return allWarnings;
				}
				catch (OperationCanceledException)
				{
					return null;
				}
			}, ct);
		}
		#endregion

		/// <summary>
		/// About menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void aboutCharacterEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			About form = new About();
			form.ShowDialog();
		}

		private void howToGuideToolStripMenuItem_Click(object sender, EventArgs e)
		{
			HelpForm form = new HelpForm();
			form.Show();
		}

		/// <summary>
		/// Event handler for when the Image Importer is previewing an image
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="img"></param>
		private void ImageImporter_PreviewImage(object sender, System.Drawing.Image img)
		{
			picPortrait.Image = img;
		}

		#region Find/Replace
		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tabControl.SelectedTab != tabDialogue)
				return;
			_findForm.SetReplaceMode(false);
			_findForm.Show();
		}

		private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tabControl.SelectedTab != tabDialogue)
				return;
			_findForm.SetReplaceMode(true);
			_findForm.Show();
		}

		/// <summary>
		/// Hooks up event handlers for the Find/Replace form
		/// </summary>
		private void SetupFindReplace()
		{
			_findForm = new FindReplace();
			_findForm.Find += DoFindReplace;
			_findForm.Replace += DoFindReplace;
			_findForm.ReplaceAll += DoFindReplace;
			_findForm.RestoreFocus += _findForm_RestoreFocus;
		}

		private void DoFindReplace(object sender, FindArgs args)
		{
			if (_selectedCharacter == null || string.IsNullOrEmpty(args.FindText))
				return;

			List<Case> cases = _selectedCharacter.Behavior.WorkingCases;
			int startCaseIndex = 0;

			//Look at the current screen before doing cases in the data structure
			if (_selectedCase != null)
			{
				startCaseIndex = Math.Max(0, cases.IndexOf(_selectedCase));
				bool found = gridDialogue.FindReplace(args);
				if (found)
					return;
			}

			//Nothing found, deselect everything
			gridDialogue.ClearSelection();
			
			//Now look across all cases
			List<Case> otherCases = new List<Case>();
			for (int i = startCaseIndex + 1; i < cases.Count; i++)
			{
				otherCases.Add(cases[i]);
			}
			for (int i = 0; i < startCaseIndex; i++)
			{
				otherCases.Add(cases[i]);
			}
			for (int i = 0; i < otherCases.Count; i++)
			{
				Case c = otherCases[i];
				for (int l = 0; l < c.Lines.Count; l++)
				{
					string text = c.Lines[l].Text;
					if (!string.IsNullOrEmpty(text))
					{
						int index = gridDialogue.FindText(text, 0, args);
						if (index >= 0)
						{
							args.Success = true;

							if (args.DoReplace)
							{
								text = text.ReplaceAt(index, args.FindText, args.ReplaceText);
								args.ReplaceCount++;
								c.Lines[l].Text = text;
							}
							else
							{
								//Select the case
								SelectCase(c, _selectedStage != null ? _selectedStage.Id : -1);
								//Select the line
								gridDialogue.SelectTextInRow(l, index, args.FindText.Length);
							}

							if (!args.ReplaceAll)
								return;
						}
					}
				}
			}
		}

		/// <summary>
		/// Selects a case node in the dialogue tree
		/// </summary>
		/// <param name="c">Working case to select</param>
		/// <param name="stage">Preferable stage to select</param>
		private void SelectCase(Case c, int stage)
		{
			//Figure out which stage to select the node under
			TreeNode selectNode = null;
			if (c.Tag == Trigger.StartTrigger)
			{
				//TODO: Start lines aren't currently searched
			}
			else
			{
				int findStage = stage;
				while (!c.Stages.Contains(stage))
				{
					stage = (stage + 1) % (_selectedCharacter.Layers + Clothing.ExtraStages);
					if (stage == findStage)
						return; //Couldn't find in any stage, somehow
				}

				TreeNode stageNode = treeDialogue.Nodes[stage + 1];
				for (int i = 0; i < stageNode.Nodes.Count; i++)
				{
					TreeNode caseNode = stageNode.Nodes[i];
					Case nodeCase = ((DialogueWrapper)caseNode.Tag).Case;
					if (nodeCase == c)
					{
						selectNode = caseNode; //Found it
						break;
					}
				}
			}
			if (selectNode != null)
			{
				treeDialogue.SelectedNode = selectNode;
			}
		}

		private void gridDialogue_KeyDown(object sender, KeyEventArgs e)
		{
			if (_findForm.Visible && e.KeyCode == Keys.Enter)
			{
				//Redirect enter to the Find form
				_findForm.RepeatKeyPress();
				e.Handled = true;
			}
		}

		private void _findForm_RestoreFocus(object sender, EventArgs e)
		{
			gridDialogue.SetFocus();
		}
		#endregion

		private void markerReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveCase(false);
			MarkerReport form = new MarkerReport();
			form.ShowDialog();
		}

		private void banterWizardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveCase(false);
			_selectedCharacter.Behavior.BuildStageTree(_selectedCharacter);
			BanterWizard wizard = new BanterWizard();
			wizard.SetCharacter(_selectedCharacter, _imageLibrary);
			wizard.ShowDialog();
			if (wizard.Modified)
			{
				GenerateDialogueTree(false);	
			}
		}

		private void txtLabel_Validated(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in gridLabels.Rows)
			{
				if (row.Cells["ColLabelsStage"].Value?.ToString() == "0")
				{
					row.Cells["ColLabelsLabel"].Value = txtLabel.Text;
					return;
				}
			}
			gridLabels.Rows.Add(new string[] { "0", txtLabel.Text });
		}

		private void gridLabels_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			if (gridLabels.Rows[e.RowIndex].Cells["ColLabelsStage"].ToString() == "0") {
				txtLabel.Text = gridLabels.Rows[e.RowIndex].Cells["ColLabelsLabel"].ToString();
			}
		}

		private void tsbtnSplit_DropDownOpening(object sender, EventArgs e)
		{
			tssepBeforeRemove.Visible = false;
			tsmiRemove.Visible = false;
		}

		private void tsbtnSplit_DropDownClosed(object sender, EventArgs e)
		{
			tssepBeforeRemove.Visible = true;
			tsmiRemove.Visible = true;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ckbShowSpeechBubbleColumns_CheckedChanged(object sender, EventArgs e)
		{
			this.gridDialogue.ShowSpeechBubbleColumns = ckbShowBubbleColumns.Checked;
		}
	}
}
