using Desktop;
using SPNATI_Character_Editor.Activities;
using SPNATI_Character_Editor.Forms;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Threading;

namespace SPNATI_Character_Editor
{
	public static class ShellLogic
	{
		private static DispatcherTimer _backupTimer = new DispatcherTimer();

		public static void Initialize()
		{
			if (!DoInitialSetup())
			{
				Shell.Instance.Close();
				return;
			}
			BuildDefinitions();
			CreateToolbar();

			Shell.Instance.LaunchWorkspace(new LoaderRecord());
			Shell.Instance.Maximize(true);

			Shell.Instance.AutoTickFrequency = Config.AutoSaveInterval * 60000;
			Shell.Instance.AutoTick += Instance_AutoTick;

			_backupTimer.Tick += _backupTimer_Tick;
			_backupTimer.Interval = new TimeSpan(0, 5, 0);
			_backupTimer.Start();

			Config.LoadMacros<Case>("Case");
		}

		private static void _backupTimer_Tick(object sender, EventArgs e)
		{
			if (!Config.AutoBackupEnabled) { return; }
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			foreach (IWorkspace ws in Shell.Instance.Workspaces)
			{
				Character c = ws.Record as Character;
				if (c != null)
				{
					Serialization.BackupCharacter(c);
				}
			}
			Cursor.Current = cursor;
		}

		private static void Instance_AutoTick(object sender, System.EventArgs e)
		{
			//loop through all open characters and save only those whose author is the current user
			foreach (IWorkspace ws in Shell.Instance.Workspaces)
			{
				Character c = ws.Record as Character;
				if (c != null && !string.IsNullOrEmpty(c.Metadata?.Writer) && c.Metadata.Writer.Contains(Config.UserName))
				{
					Save(true, ws);
				}
			}
		}

		/// <summary>
		/// Builds definition data that isn't currently found in an xml file
		/// </summary>
		private static void BuildDefinitions()
		{
			BuildDirectiveTypes();
		}

		private static void BuildDirectiveTypes()
		{
			//TODO: should these go in an XML file like practically every other definition? Maybe, but the epilogue editor needs code updates to handle new directives either way

			DirectiveProvider provider = new DirectiveProvider();
			DirectiveDefinition def = provider.Create("sprite") as DirectiveDefinition;
			def.Description = "Adds a sprite to the scene.";
			foreach (string key in new string[] { "id", "src", "layer", "width", "height", "x", "y", "scalex", "scaley", "rotation", "alpha", "pivotx", "pivoty", "marker", "delay", "skewx", "skewy" })
			{
				def.AllowedProperties.Add(key);
			}

			def = provider.Create("text") as DirectiveDefinition;
			def.Description = "Displays a speech bubble.";
			foreach (string key in new string[] { "id", "x", "y", "text", "arrow", "width", "alignmentx", "alignmenty", "marker", "delay" })
			{
				def.AllowedProperties.Add(key);
			}

			def = provider.Create("clear") as DirectiveDefinition;
			def.Description = "Removes a speech bubble.";
			foreach (string key in new string[] { "id", "marker", "delay" })
			{
				def.AllowedProperties.Add(key);
			}

			def = provider.Create("clear-all") as DirectiveDefinition;
			foreach (string key in new string[] { "marker", "delay" })
			{
				def.AllowedProperties.Add(key);
			}
			def.Description = "Removes all speech bubbles.";

			def = provider.Create("move") as DirectiveDefinition;
			def.IsAnimatable = true;
			def.Description = "Moves/rotates/scales a sprite or emitter.";
			def.FilterPropertiesById = true;
			foreach (string key in new string[] { "id", "src", "x", "y", "scalex", "scaley", "rotation", "alpha", "rate", "time", "delay", "loop", "ease", "tween", "clamp", "iterations", "marker", "skewx", "skewy" })
			{
				def.AllowedProperties.Add(key);
			}
			foreach (string key in new string[] { "time", "src", "x", "y", "scalex", "scaley", "rotation", "alpha" })
			{
				def.RequiredAnimatedProperties.Add(key);
			}

			def = provider.Create("camera") as DirectiveDefinition;
			def.IsAnimatable = true;
			def.Description = "Pans or zooms the camera.";
			foreach (string key in new string[] { "x", "y", "zoom", "time", "delay", "loop", "ease", "tween", "clamp", "iterations", "marker" })
			{
				def.AllowedProperties.Add(key);
			}
			foreach (string key in new string[] { "time", "x", "y", "zoom" })
			{
				def.RequiredAnimatedProperties.Add(key);
			}

			def = provider.Create("fade") as DirectiveDefinition;
			def.Description = "Fades the overlay to a new color and opacity level.";
			def.IsAnimatable = true;
			foreach (string key in new string[] { "color", "alpha", "time", "delay", "loop", "ease", "tween", "clamp", "iterations", "marker" })
			{
				def.AllowedProperties.Add(key);
			}
			foreach (string key in new string[] { "time", "color", "alpha" })
			{
				def.RequiredAnimatedProperties.Add(key);
			}

			def = provider.Create("stop") as DirectiveDefinition;
			def.Description = "Stops an animation.";
			foreach (string key in new string[] { "id", "marker", "delay" })
			{
				def.AllowedProperties.Add(key);
			}

			def = provider.Create("wait") as DirectiveDefinition;
			def.Description = "Waits for animations to complete.";
			foreach (string key in new string[] { "marker" })
			{
				def.AllowedProperties.Add(key);
			}

			def = provider.Create("pause") as DirectiveDefinition;
			def.Description = "Waits for the user to click next.";
			foreach (string key in new string[] { "marker" })
			{
				def.AllowedProperties.Add(key);
			}

			def = provider.Create("remove") as DirectiveDefinition;
			def.Description = "Removes a sprite or emitter from the scene.";
			foreach (string key in new string[] { "id", "marker", "delay" })
			{
				def.AllowedProperties.Add(key);
			}

			def = provider.Create("emitter") as DirectiveDefinition;
			def.Description = "Adds an object emitter to the scene.";
			foreach (string key in new string[] { "id", "layer", "src", "rate", "angle", "width", "height", "x", "y", "rotation", "startScaleX", "startScaleY", "endScaleX", "delay", 
				"endScaleY", "speed", "accel", "forceX", "forceY", "startColor", "endColor", "startAlpha", "endAlpha", "startRotation", "endRotation", "lifetime", "ease", "ignoreRotation", "marker",
				"startSkewX", "startSkewY", "endSkewX", "endSkewY" })
			{
				def.AllowedProperties.Add(key);
			}

			def = provider.Create("emit") as DirectiveDefinition;
			def.Description = "Emits an object from an emitter.";
			foreach (string key in new string[] { "id", "count", "marker", "delay" })
			{
				def.AllowedProperties.Add(key);
			}
		}

		private static bool DoInitialSetup()
		{
			string appDir = Config.GetString(Settings.GameDirectory);
			if (!string.IsNullOrEmpty(appDir) && !SettingsSetup.VerifyApplicationDirectory(appDir))
			{
				Config.Set(Settings.GameDirectory, null);
			}
			if (string.IsNullOrEmpty(Config.GetString(Settings.GameDirectory)))
			{
				if (OpenSetup() == DialogResult.Cancel)
				{
					ErrorLog.LogError("Unable to launch because setup was cancelled.");
					return false;
				}
			}
			if (string.IsNullOrEmpty(Config.GetString(Settings.GameDirectory)))
			{
				//Not going to play along? Then we'll quit.
				ErrorLog.LogError("SPNATI directory not specified.");
				return false;
			}
			if (string.IsNullOrEmpty(Config.KisekaeDirectory))
			{
				KisekaeSetup setup = new KisekaeSetup();
				setup.ShowDialog();
			}
			return true;
		}

		/// <summary>
		/// Opens the initial setup form
		/// </summary>
		private static DialogResult OpenSetup()
		{
			SettingsSetup form = new SettingsSetup();
			return form.ShowDialog();
		}

		private static void CreateToolbar()
		{
			Shell shell = Shell.Instance;

			//File
			ToolStripMenuItem menu = shell.AddToolbarSubmenu("File");
			shell.AddToolbarItem("Save", Save, menu, Keys.Control | Keys.S);
			shell.AddToolbarSeparator(menu);
			shell.AddToolbarItem("Import .txt...", ImportCharacter, menu, Keys.Control | Keys.I);
			shell.AddToolbarItem("Export .txt for make_xml.py", ExportCharacter, menu, Keys.Control | Keys.E);
			shell.AddToolbarSeparator(menu);
			shell.AddToolbarItem("Setup...", Setup, menu, Keys.None);
			shell.AddToolbarSeparator(menu);
			shell.AddToolbarItem("Exit", Exit, menu, Keys.Alt | Keys.F4);

			//Edit
			menu = shell.AddToolbarSubmenu("Edit");
			shell.AddToolbarItem("Find", Find, menu, Keys.Control | Keys.F);
			shell.AddToolbarItem("Replace", Replace, menu, Keys.Control | Keys.H);
			shell.AddToolbarSeparator();

			//Characters
			shell.AddToolbarItem("Characters...", OpenCharacterSelect, Keys.None);
			shell.AddToolbarItem("Skins...", typeof(Costume));

			//Validate
			menu = shell.AddToolbarSubmenu("Validate");
			shell.AddToolbarItem("All Characters", typeof(ValidationRecord), menu);

			//Tools
			menu = shell.AddToolbarSubmenu("Tools");
			shell.AddToolbarItem("Charts...", typeof(ChartRecord), menu);
			shell.AddToolbarItem("Marker Report...", typeof(MarkerReportRecord), menu);
			shell.AddToolbarItem("Configure Game...", ConfigGame, menu, Keys.None);
			shell.AddToolbarItem("Manage Macros...", ManageCaseMacros, menu, Keys.None);
			shell.AddToolbarItem("Manage Dictionary...", typeof(DictionaryRecord), menu);
			shell.AddToolbarSeparator(menu);
			shell.AddToolbarItem("Data Recovery", OpenDataRecovery, menu, Keys.None);
			shell.AddToolbarItem("Fix Kisekae", ResetKisekae, menu, Keys.None);

			//Help
			shell.AddToolbarSeparator();
			menu = shell.AddToolbarSubmenu("Help");
			shell.AddToolbarItem("View Help", OpenHelp, menu, Keys.F1);
			shell.AddToolbarItem("Change Log", OpenChangeLog, menu, Keys.None);
			shell.AddToolbarItem("About Character Editor...", OpenAbout, menu, Keys.None);
		}

		private static void OpenCharacterSelect()
		{
			IRecord record = RecordLookup.DoLookup(typeof(Character), "", true, CharacterDatabase.FilterHuman, null);
			if (record != null)
			{
				Shell.Instance.LaunchWorkspace(record as Character);
			}
		}

		private static void Save()
		{
			Save(false, Shell.Instance.ActiveWorkspace);
		}

		private static void Save(bool auto, IWorkspace workspace)
		{
			Cursor.Current = Cursors.WaitCursor;
			Shell.Instance.ActiveActivity?.Save();
			workspace?.SendMessage(WorkspaceMessages.Save, auto);
			Cursor.Current = Cursors.Default;
		}

		private static void Exit()
		{
			Shell.Instance.Close();
		}

		private static void Find()
		{
			Shell.Instance.ActiveWorkspace.SendMessage(WorkspaceMessages.Find);
		}

		private static void Replace()
		{
			Shell.Instance.ActiveWorkspace.SendMessage(WorkspaceMessages.Replace);
		}

		private static void OpenHelp()
		{
			HelpForm form = new HelpForm();
			form.Show();
		}

		private static void OpenChangeLog()
		{
			new ChangeLogReview().ShowDialog();
		}

		private static void OpenAbout()
		{
			About form = new About();
			form.ShowDialog();
		}

		/// <summary>
		/// Cleans the kisekae #airversion folder after a bad import corrupted the importer
		/// </summary>
		private static void ResetKisekae()
		{
			if (MessageBox.Show("This will attempt fix Kisekae when imports are failing. Close kkl.exe before proceeding.", "Fix Kisekae", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
			{
				return;
			}
			string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "kkl", "#airversion");
			if (Directory.Exists(folder))
			{
				DirectoryInfo di = new DirectoryInfo(folder);
				foreach (FileInfo file in di.EnumerateFiles())
				{
					file.Delete();
				}
				foreach (DirectoryInfo dir in di.EnumerateDirectories())
				{
					dir.Delete(true);
				}
			}
			MessageBox.Show("Kisekae data cleaned up. You can restart kkl.exe.");
		}

		private static void OpenDataRecovery()
		{
			DataRecovery recovery = new DataRecovery();
			Character c = GetActiveCharacter();
			recovery.SetCharacter(c);
			if (recovery.ShowDialog() == DialogResult.OK)
			{
				IWorkspace ws = Shell.Instance.GetWorkspace(c);
				if (ws != null)
				{
					Shell.Instance.CloseWorkspace(ws, true);
				}
				Shell.Instance.LaunchWorkspace(recovery.RecoveredCharacter);
			}
		}

		private static void OpenDataRecovery(string name)
		{
			DataRecovery recovery = new DataRecovery();
			recovery.SetCharacter(name);
			if (recovery.ShowDialog() == DialogResult.OK)
			{
				Shell.Instance.LaunchWorkspace(recovery.RecoveredCharacter);
			}
		}

		private static Character GetActiveCharacter()
		{
			IWorkspace ws = Shell.Instance.ActiveWorkspace;
			if (ws != null && ws.Record is Character)
			{
				return ws.Record as Character;
			}
			return null;
		}

		private static void ImportCharacter()
		{
			Character current = GetActiveCharacter();
			if (current == null)
			{
				current = RecordLookup.DoLookup(typeof(Character), "") as Character;
				if (current == null)
				{
					return;
				}
			}
			string dir = Config.GetRootDirectory(current);
			string file = Shell.Instance.ShowOpenFileDialog(dir, "edit-dialogue.txt");
			if (!string.IsNullOrEmpty(file))
			{
				FlatFileSerializer.Import(file, current);
				Character c = current;
				CharacterDatabase.Set(c.FolderName, c);

				Shell.Instance.CloseWorkspace(Shell.Instance.ActiveWorkspace, true);
				Shell.Instance.LaunchWorkspace(current);
			}
		}

		private static void ExportCharacter()
		{
			Character current = GetActiveCharacter();
			if (current == null)
			{
				MessageBox.Show("No character is currently being edited. You can only export from a character's workspace.");
				return;
			}
			Save();
			if (FlatFileSerializer.ExportFlatFile(current))
			{
				Shell.Instance.SetStatus("Generated edit-dialogue.txt");
			}
		}

		private static void Setup()
		{
			SettingsSetup form = new SettingsSetup();
			form.ShowDialog();
		}

		public static void Teardown()
		{
			SpellChecker.Instance.SaveUserDictionary();
			Config.Save();
		}

		public static void RecoverCharacter(string name)
		{
			OpenDataRecovery(name);
		}

		private static void ConfigGame()
		{
			GameConfig form = new GameConfig();
			form.ShowDialog();
		}

		private static void ManageCaseMacros()
		{
			MacroManager form = new MacroManager();
			form.SetType(typeof(Case), "Case");
			form.ShowDialog();
			Shell.Instance.PostOffice.SendMessage(DesktopMessages.MacrosUpdated);
		}
	}
}
