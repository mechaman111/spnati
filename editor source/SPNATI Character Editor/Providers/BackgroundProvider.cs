using SPNATI_Character_Editor.Providers;

namespace SPNATI_Character_Editor
{
	public class BackgroundProvider : DefinitionProvider<BackgroundTag>
	{
		public override void ApplyDefaults(BackgroundTag definition)
		{
		}

		public override string GetLookupCaption()
		{
			return "Choose a Property";
		}
	}

	public class BackgroundTagValueProvider : DefinitionProvider<BackgroundTagValue>
	{
		public override void ApplyDefaults(BackgroundTagValue definition)
		{
		}

		public override string GetLookupCaption()
		{
			return "Choose a Tag";
		}
	}
}
