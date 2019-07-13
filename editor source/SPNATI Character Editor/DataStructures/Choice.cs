using Desktop.CommonControls.PropertyControls;
using System;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// User prompt choice
	/// </summary>
	public class Choice : ICloneable
	{
		[ComboBox(DisplayName = "Action", Key = "action", GroupOrder = 5, Description = "Action to perform", Options = new string[] { "jump", "marker" })]
		[XmlAttribute("action")]
		public string Action;

		[Text(DisplayName = "ID", GroupOrder = 10, Key = "id", Description = "Action ID")]
		[XmlAttribute("id")]
		public string Id;

		[Text(DisplayName = "Value", GroupOrder = 20, Key = "value", Description = "Action value")]
		[XmlAttribute("value")]
		public string Value;

		[Text(DisplayName = "Caption", Key = "text", GroupOrder = 0, Description = "Button caption", RowHeight = 26, Multiline = true)]
		[XmlText]
		public string Caption;

		/// <summary>
		/// Parent directive
		/// </summary>
		[XmlIgnore]
		public Directive Directive { get; set; }

		public override string ToString()
		{
			return Caption;
		}

		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}
