using Desktop;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(LoaderRecord), 0)]
	public partial class Loader : Activity
	{
		public const bool ForceUncached = false;

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
			await LoadChunk("Backgrounds", 0, () => BackgroundDatabase.Load());
			await LoadChunk("Tags", 0, () => TagDatabase.Load());
			await LoadChunk("Variables", 1, () => VariableDatabase.Load());
			await LoadChunk("Default Dialogue", 1, () => DialogueDatabase.Load());
			await LoadChunk("Recipes", 1, () => RecipeProvider.Load());

			string lastCharacter = Config.GetString(Settings.AutoOpenCharacter);
			if (string.IsNullOrEmpty(lastCharacter))
			{
				lastCharacter = Config.GetString(Settings.LastCharacter);
			}

			List<string> failedCharacters = new List<string>();

			List<string> folders = Directory.EnumerateDirectories(Path.Combine(Config.GetString(Settings.GameDirectory), "opponents")).ToList();
			int count = folders.Count;
			int i = 0;
			string loadFilter = Config.GetString(Settings.LoadOnlyLastCharacter);
			HashSet<string> filter = new HashSet<string>();
			if (loadFilter.Length > 2)
			{
				foreach (string charToLoad in loadFilter.Split(','))
				{
					filter.Add(charToLoad);
				}
			}
			int loadCount = Config.GetInt(Settings.LoadOnlyLastCharacter);
			if (filter.Count > 0)
			{
				loadCount = 1;
			}
			else if (!string.IsNullOrEmpty(lastCharacter))
			{
				filter.Add(lastCharacter);
			}
			CharacterDatabase.UsePlaceholders = true;
			foreach (string key in folders)
			{
				int step = (int)(((float)i / count) * 99) + 1;
				string path = folders[i++];
				string folderName = Path.GetFileName(path);

				if (loadCount == 1 && !filter.Contains(folderName) && folderName != "reskins")
				{
					continue; //makes startup times way faster when you just need to check something really quick
				}
				if (loadCount > 1 && i >= loadCount)
				{
					continue;
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
						if (!File.Exists(Path.Combine(path, "behaviour.xml")))
						{
							return;
						}
						bool stale;
						CachedCharacter character = CharacterDatabase.LoadFromCache(path, out stale);
						stale = stale || ForceUncached;
						if (character == null || stale)
						{
							character = CharacterDatabase.CacheCharacter(folderName, character);
						}
						if (character != null)
						{
							character.FolderName = folderName;
							CharacterDatabase.Add(character);
							SpellChecker.Instance.AddWord(character.Label, false);
							for (int t = 0; t < character.Tags.Count; t++)
							{
								CharacterTag tag = character.Tags[t];
								tag.Tag = tag.Tag.ToLowerInvariant();
								character.Tags[t] = tag;

								Tag def = TagDatabase.GetTag(tag.Tag);
								if (def != null)
								{
									TagDatabase.CacheGroup(def, character);
								}

								if (!string.IsNullOrEmpty(tag.Tag))
								{
									TagDatabase.AddTag(tag.Tag);
								}								
							}
							TagDatabase.AddTag(character.DisplayName, false);
							for (int l = 0; l < character.Layers; l++)
							{
								Clothing layer = character.GetClothing(l);
								ClothingDatabase.AddClothing(layer);
							}
						}
						else
						{
							if (folderName != ".vs" && folderName != "human")
							{
								failedCharacters.Add(folderName);
								CharacterDatabase.FailedCharacters.Add(folderName);
							}
						}
					}
				});
			}

			CharacterDatabase.UsePlaceholders = false;

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

			//add the default skin
			Costume defaultCostume = new Costume() { Key = "default" };
			defaultCostume.Folders.Add(new StageSpecificValue(0, "default"));
			defaultCostume.Labels.Add(new StageSpecificValue(0, "Default Outfit"));
			CharacterDatabase.AddSkin(defaultCostume);

			progressBar.Visible = false;
			//display What's New form if this is a new version
			if (Config.GetString(Settings.LastVersionRun) != Config.Version)
			{
				new WhatsNew().ShowDialog();
			}

			if (failedCharacters.Count > 0)
			{
				Shell.Instance.SetStatus($"Failed to load {failedCharacters.Count} character(s). See errorlog.txt for more details.");
			}

			Shell.Instance.CloseWorkspace(Workspace);
			Shell.Instance.Maximize(false);
			if (!string.IsNullOrEmpty(lastCharacter))
			{
				if (failedCharacters.Contains(lastCharacter))
				{
					ShellLogic.RecoverCharacter(lastCharacter);
				}
				else
				{
					if (CharacterDatabase.Get(lastCharacter) != null)
					{
						Character last = CharacterDatabase.Load(lastCharacter);
						Shell.Instance.LaunchWorkspace(last);
					}
				}
			}

			Config.LoadRecentRecords<Character>();
			Config.LoadRecentRecords<Costume>();
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
