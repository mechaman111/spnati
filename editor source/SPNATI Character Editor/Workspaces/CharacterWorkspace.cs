using System;
using Desktop;
using SPNATI_Character_Editor.Activities;
using SPNATI_Character_Editor.Services;

namespace SPNATI_Character_Editor.Workspaces
{
	[Workspace(typeof(Character))]
	public class CharacterWorkspace : Workspace
	{
		public const string SpellCheckerService = "SpellCheck";

		private Character _character;

		protected override void OnInitialize()
		{
			_character = Record as Character;
			SpellCheckerService spellChecker = new SpellCheckerService(_character);
			SetData(SpellCheckerService, spellChecker);

			Config.Set(Settings.LastCharacter, _character.FolderName);
		}

		public override bool AllowAutoStart(Type activityType)
		{
			if (activityType == typeof(Dashboard) && (!Config.EnableDashboard || _character.IsNew))
			{
				return false;
			}
			return base.AllowAutoStart(activityType);
		}
	}
}
