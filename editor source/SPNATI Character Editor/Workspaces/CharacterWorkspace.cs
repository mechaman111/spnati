using Desktop;

namespace SPNATI_Character_Editor.Workspaces
{
	[Workspace(typeof(Character))]
	public class CharacterWorkspace : Workspace
	{
		private Character _character;

		protected override void OnInitialize()
		{
			_character = Record as Character;
			Config.Set(Settings.LastCharacter, _character.FolderName);
		}
	}
}
