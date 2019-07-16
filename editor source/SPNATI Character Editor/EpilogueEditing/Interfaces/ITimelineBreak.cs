using System.Drawing;
using Desktop.Skinning;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public interface ITimelineBreak : ITimelineObject
	{
		float Time { get; set; }
		void DrawBackground(Graphics g, float pps, int height, bool selected);
		void Draw(Graphics g, float pps, int height, bool selected);
	}
}
