using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public class AccordionColumn
	{
		/// <summary>
		/// Column fill weight
		/// </summary>
		public float FillWeight { get; set; } = 0;

		/// <summary>
		/// Fixed column width
		/// </summary>
		public int Width { get; set; } = 100;

		/// <summary>
		/// Property to bind to this column
		/// </summary>
		public string PropertyName { get; set; }

		/// <summary>
		/// Header text
		/// </summary>
		public string Text { get; set; } = "";

		/// <summary>
		/// Column text alignment
		/// </summary>
		public HorizontalAlignment TextAlign { get; set; } = HorizontalAlignment.Left;

		public AccordionColumn(string text, string propertyName)
		{
			PropertyName = propertyName;
			Text = text;
		}
	}
}
