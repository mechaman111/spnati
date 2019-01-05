using Desktop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using SPNATI_Character_Editor.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(LoaderRecord), 0)]
	public partial class Loader : Activity
	{
		public Loader()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			LoadData();
		}

		private async void LoadData()
		{
			await LoadChunk("Triggers", 0, () => TriggerDatabase.Load());
			await LoadChunk("Tags", 0, () => TagDatabase.Load());
			await LoadChunk("Variables", 1, () => VariableDatabase.Load());
			await LoadChunk("Default Dialogue", 1, () => DialogueDatabase.Load());

			string lastCharacter = Config.GetString(Settings.LastCharacter);

			List<string> folders = Directory.EnumerateDirectories(Path.Combine(Config.GetString(Settings.GameDirectory), "opponents")).ToList();
			int count = folders.Count;
			int i = 0;
			foreach (string key in folders)
			{
				int step = (int)(((float)i / count) * 99) + 1;
				string path = folders[i++];
				string folderName = Path.GetFileName(path);

				if (Config.GetBoolean(Settings.LoadOnlyLastCharacter) && folderName != lastCharacter && folderName != "reskins")
				{
					continue; //makes startup times way faster when you just need to check something really quick
				}

				await LoadChunk(folderName, step, () =>
				{
					if (folderName == "reskins")
					{
						foreach (string skinFolder in Directory.EnumerateDirectories(path))
						{
							Costume reskin = Serialization.ImportSkin(skinFolder);
							if (reskin != null)
							{
								CharacterDatabase.AddSkin(reskin);
								reskin.Tags.ForEach(t =>
								{
									if (!string.IsNullOrEmpty(t.Name))
									{
										t.Name = t.Name.ToLowerInvariant();
										TagDatabase.AddTag(t.Name);
									}
								});
								TagDatabase.AddTag(reskin.Id);
							}
						}
					}
					else
					{
						Character character = Serialization.ImportCharacter(path);
						if (character != null)
						{
							CharacterDatabase.Add(character);
							for (int t = 0; t < character.Tags.Count; t++)
							{
								string tag = character.Tags[t].ToLowerInvariant();
								character.Tags[t] = tag;
								if (!string.IsNullOrEmpty(tag))
								{
									TagDatabase.AddTag(tag);
								}
							}
							TagDatabase.AddTag(character.DisplayName, false);
						}
					}
				});
			}
			//add a placeholder for the human
			Character human = new Character()
			{
				FolderName = "human",
				Label = "Human",
				FirstName = "Human",
			};
			human.Behavior.OnAfterDeserialize(human);
			CharacterDatabase.Add(human);
			CharacterDatabase.AddEditorData(human, new CharacterEditorData() { Owner = "human" });

			//link up skins
			foreach (Character c in CharacterDatabase.Characters)
			{
				c.Metadata.AlternateSkins.ForEach(set =>
				{
					foreach (SkinLink link in set.Skins)
					{
						Costume skin = CharacterDatabase.GetSkin(link.Folder);
						if (skin != null)
						{
							skin.Character = c;
							link.Costume = skin;
							skin.Link = link;
						}
					}
				});
			}

			progressBar.Visible = false;
			//display What's New form if this is a new version
			if (Config.GetString(Settings.LastVersionRun) != Config.Version)
			{
				new WhatsNew().ShowDialog();
			}

			Shell.Instance.CloseWorkspace(Workspace);
			Shell.Instance.Maximize(false);
			if (!string.IsNullOrEmpty(lastCharacter))
			{
				Shell.Instance.LaunchWorkspace(CharacterDatabase.Get(lastCharacter));
			}
		}

		private Task LoadChunk(string caption, int progress, Action action)
		{
			lblProgress.Text = $"Loading {caption}...";
			progressBar.Value = Math.Min(progressBar.Maximum, progress);
			return Task.Run(action);
		}
	}

	public class LoaderRecord : BasicRecord
	{
	}

	public class LoaderProvider : BasicProvider<LoaderRecord>
	{
	}
}
