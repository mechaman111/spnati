namespace SPNATI_Character_Editor
{
	/// <summary>
	/// In-game (non-epilogue) sprite
	/// </summary>
	public class Sprite : Directive
	{
		public override string ToString()
		{
			return $"Sprite: {Id}";
		}
	}
}