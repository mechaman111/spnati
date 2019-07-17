using Desktop;
using Desktop.CommonControls.PropertyControls;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveBubbleKeyframe : LiveKeyframe
	{
		[Text(DisplayName = "Text", Key = "text", GroupOrder = 5, Description = "Speech bubble text", RowHeight = 52, Multiline = true)]
		public string Text
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public LiveBubbleKeyframe() : base()
		{
			TrackedProperties.Remove("X");
			TrackedProperties.Remove("Y");
			TrackedProperties.Add("Text");
		}

		public override bool FilterRecord(PropertyRecord record)
		{
			switch (record.Key)
			{
				case "x":
				case "y":
					return false;
				default:
					return base.FilterRecord(record);
			}
		}
	}
}
