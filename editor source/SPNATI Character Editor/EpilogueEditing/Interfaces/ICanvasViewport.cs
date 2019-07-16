using System;
using System.Drawing;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public interface ICanvasViewport
	{
		event EventHandler ViewportUpdated;
		void FitToViewport(int windowWidth, int windowHeight, ref Point offset, ref float zoom);
		bool AllowPan { get; }
	}
}
