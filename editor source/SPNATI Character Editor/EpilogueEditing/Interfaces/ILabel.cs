using System;

namespace SPNATI_Character_Editor
{
	public interface ILabel
	{
		event EventHandler LabelChanged;
		string GetLabel();
	}
}
