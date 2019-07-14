using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedDataGridView : DataGridView, ISkinControl
	{
		const int HeaderPadding = 5;

		public SkinnedDataGridView()
		{
			OnUpdateSkin(new Skin());
			DoubleBuffered = true;
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}

		public void OnUpdateSkin(Skin skin)
		{
			if (skin != null)
			{
				if (Parent != null && Parent.BackColor != System.Drawing.Color.Transparent)
				{
					BackgroundColor = Parent.BackColor;
				}
				else
				{
					BackgroundColor = skin.Background.Normal;
				}
				
				BorderStyle = BorderStyle.None;
				GridColor = skin.PrimaryColor.Border;
				EnableHeadersVisualStyles = false;

				ColumnHeadersBorderStyle = RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

				DefaultCellStyle.BackColor = skin.FieldBackColor;
				Font = Skin.TextFont;
				ColumnHeadersDefaultCellStyle.BackColor = RowHeadersDefaultCellStyle.BackColor = skin.Surface.Normal;
				ColumnHeadersDefaultCellStyle.ForeColor = RowHeadersDefaultCellStyle.ForeColor = skin.Surface.ForeColor;
				ColumnHeadersDefaultCellStyle.Padding = new Padding(0, HeaderPadding, 0, HeaderPadding);

				foreach (DataGridViewColumn column in Columns)
				{
					column.DefaultCellStyle.BackColor = skin.FieldBackColor;
					column.DefaultCellStyle.ForeColor = skin.Surface.ForeColor;
				}

				foreach (DataGridViewRow row in Rows)
				{
					foreach (DataGridViewCell cell in row.Cells)
					{
						cell.Style.BackColor = skin.FieldBackColor;
						cell.Style.ForeColor = skin.Surface.ForeColor;
					}
				}

				Invalidate(true);
			}
		}
	}
}
