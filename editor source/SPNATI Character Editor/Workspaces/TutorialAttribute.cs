using System;

namespace SPNATI_Character_Editor
{
	public class TutorialAttribute : Attribute
	{
		public string Url { get; private set; }

		public TutorialAttribute(string url)
		{
			Url = url;
		}
	}
}
