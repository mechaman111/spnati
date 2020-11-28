using Desktop;
using SPNATI_Character_Editor.Activities;
using System;

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

		public override bool AllowAutoStart(Type activityType)
		{
			if ((activityType == typeof(PoseListEditor) || activityType == typeof(TemplateEditor)) && !Config.ShowLegacyPoseTabs)
			{
				return false;
			}
			return base.AllowAutoStart(activityType);
		}
	}
}
