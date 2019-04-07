using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// In-game (non-epilogue) sprite
	/// </summary>
	public class Sprite : Directive
	{
		[XmlAttribute("parent")]
		public string ParentId;

		public override string ToString()
		{
			return $"Sprite: {Id}";
		}
	}
}