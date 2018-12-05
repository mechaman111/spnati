using Desktop;
using SPNATI_Character_Editor.Activities;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public static class ShellLogic
	{
		public static void Initialize()
		{
			if (!DoInitialSetup())
			{
				Shell.Instance.Close();
				return;
			}
			CreateToolbar();

			Shell.Instance.LaunchWorkspace(new LoaderRecord());
			Shell.Instance.Maximize(true);
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

			//Validate
			menu = shell.AddToolbarSubmenu("Validate");
			shell.AddToolbarItem("All Characters", typeof(ValidationRecord), menu);

			//Tools
			menu = shell.AddToolbarSubmenu("Tools");
			shell.AddToolbarItem("Charts...", typeof(ChartRecord), menu);
			shell.AddToolbarItem("Marker Report...", typeof(MarkerReportRecord), menu);

			//Help
			shell.AddToolbarSeparator();
			menu = shell.AddToolbarSubmenu("Help");
			shell.AddToolbarItem("View Help", OpenHelp, menu, Keys.F1);
			shell.AddToolbarItem("About Character Editor...", OpenAbout, menu, Keys.None);
		}

		private static void OpenCharacterSelect()
		{
			IRecord record = RecordLookup.DoLookup(typeof(Character), "", true, CharacterDatabase.FilterHuman, null);
			if(record != null)
			{
				Shell.Instance.LaunchWorkspace(record as Character);
			}
		}

		private static void Save()
		{
			Cursor.Current = Cursors.WaitCursor;
			Shell.Instance.ActiveActivity?.Save();
			Shell.Instance.ActiveWorkspace?.SendMessage(WorkspaceMessages.Save);
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

		private static void OpenAbout()
		{
			About form = new About();
			form.ShowDialog();
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
			Config.Save();
		}
	}
}
