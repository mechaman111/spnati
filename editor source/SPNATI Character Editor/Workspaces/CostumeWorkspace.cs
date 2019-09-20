using Desktop;

namespace SPNATI_Character_Editor.Workspaces
{
	[Workspace(typeof(Costume))]
	public class CostumeWorkspace : Workspace
	{
		protected override void OnInitialize()
		{
			Costume costume = Record as Costume;
			Character c = costume.Character;
			if (c is CachedCharacter)
			{
				CharacterDatabase.Load(c.FolderName);
			}
			base.OnInitialize();
		}
	}
}
