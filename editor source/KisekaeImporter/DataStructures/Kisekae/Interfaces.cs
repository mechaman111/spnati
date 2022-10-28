namespace KisekaeImporter
{
	public interface IColorable
	{
		KisekaeColor Color1 { get; set; }
		KisekaeColor Color2 { get; set; }
		KisekaeColor Color3 { get; set; }
	}

	public interface IOpenable
	{
		void SetOpenState(int state);
	}

	public interface IPoseable
	{
		void Pose(IPoseable pose);
	}

	public interface IMoveable
	{
		void ShiftX(int offset);
	}
}
