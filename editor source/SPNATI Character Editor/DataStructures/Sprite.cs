using Desktop.CommonControls.PropertyControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// In-game (non-epilogue) sprite
	/// </summary>
	public class Sprite : Directive
	{
		/// <summary>
		/// personal notes
		/// x,y,width,height are all % of baseHeight
		/// ex. true width = width * display.height / baseHeight
		/// 
		/// width and height are basically required, since otherwise it doesn't scale with display
		/// 
		/// keyframes need a starting frame - can't just rely on the previous state
		/// 
		/// x is centered (i.e. x = container.width * 0.5 + x * display.height / baseHeight
		/// </summary>

		public override string ToString()
		{
			return $"Sprite: {Id}";
		}
	}
}