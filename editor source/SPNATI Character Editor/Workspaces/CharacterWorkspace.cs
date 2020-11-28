using Desktop;
using SPNATI_Character_Editor.Activities;
using SPNATI_Character_Editor.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor.Workspaces
{
	[Workspace(typeof(Character))]
	public class CharacterWorkspace : Workspace
	{
		public const string SpellCheckerService = "SpellCheck";

		private static HashSet<Character> _sessionActivations = new HashSet<Character>();
		private Character _character;

		protected override void OnInitialize()
		{
			_character = Record as Character;
			_character.PrepareForEdit();

			SpellCheckerService spellChecker = new SpellCheckerService(_character);
			SetData(SpellCheckerService, spellChecker);

			Config.Set(Settings.LastCharacter, _character.FolderName);
		}

		protected override void OnActivate()
		{
			base.OnActivate();
			if (!_sessionActivations.Contains(_character))
			{
				_sessionActivations.Add(_character);
				if (Config.EnableDashboard && (Config.DevMode || Config.IncludesUserName(_character.Metadata.Writer)) && GlobalCache.HasChanges(_character.FolderName))
				{
					Shell.Instance.ShowToast("New Incoming Dialogue!", $"Some characters have had new lines written that target {_character}. Check out the Dashboard for a summary.");
				}
			}

			bool knownVersion = _character.Source != EditorSource.CharacterEditor;
			string version = _character.Version;
			for (int i = 0; i < Config.VersionHistory.Length; i++)
			{
				if (version == Config.VersionHistory[i])
				{
					knownVersion = true;
					break;
				}
			}
			if (!knownVersion)
			{
				Match match = Regex.Match(version, @"v(\d+)");
				if (match.Success)
				{
					string majorVersion = match.Groups[1].Value;
					int versionNumber;
					if (int.TryParse(majorVersion, out versionNumber) && versionNumber >= 5)
					{
						ShowBanner($"This character was saved in a later version of the editor ({_character.Version}). Some features may not work properly.", Desktop.Skinning.SkinnedHighlight.Bad);
					}
				}
			}

			string status = Listing.Instance.GetCharacterStatus(_character.FolderName);
			if (status == OpponentStatus.Duplicate)
			{
				ShowBanner("This character has been replaced by a newer version.", Desktop.Skinning.SkinnedHighlight.Bad);
			}
			else if (status == OpponentStatus.Event)
			{
				ShowBanner("This character is not part of the permanent roster and is only available during certain events.", Desktop.Skinning.SkinnedHighlight.Bad);
			}
			else if (status == OpponentStatus.Incomplete)
			{
				if (Config.WarnAboutIncompleteStatus)
				{
					ShowBanner("This character is incomplete, meaning they have likely been abandoned.", Desktop.Skinning.SkinnedHighlight.Bad);
				}
			}
			else if (status == OpponentStatus.Unlisted)
			{
				//auto-add to the listing
				Opponent opp = new Opponent(_character.FolderName, OpponentStatus.Testing);
				Listings.Test.Characters.Add(opp);
				Listing.Instance.Characters.Add(opp);
				Serialization.ExportListing(Listings.Test, "listing-test.xml");
			}
		}

		public override bool AllowAutoStart(Type activityType)
		{
			if (activityType == typeof(Dashboard) && (!Config.EnableDashboard || _character.IsNew))
			{
				return false;
			}
			if ((activityType == typeof(PoseListEditor) || activityType == typeof(TemplateEditor)) && !Config.ShowLegacyPoseTabs)
			{
				return false;
			}
			return base.AllowAutoStart(activityType);
		}

		public override IActivity GetDefaultActivity()
		{
			if (!Config.StartOnDashboard)
			{
				List<IActivity> list = Activities[WorkspacePane.Main];
				return list[1];
			}
			return base.GetDefaultActivity();
		}
	}
}
