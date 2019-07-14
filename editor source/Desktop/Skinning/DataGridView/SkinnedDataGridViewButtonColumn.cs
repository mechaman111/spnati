using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedDataGridViewButtonColumn : DataGridViewButtonColumn
	{
		private SkinnedDataGridViewButtonCell _cellTemplate;

		public bool Flat
		{
			get { return _cellTemplate.Flat; }
			set
			{
				_cellTemplate.Flat = value;
				if (DataGridView != null)
				{
					DataGridViewRowCollection rows = DataGridView.Rows;
					for (int i = 0; i < rows.Count; i++)
					{
						DataGridViewRow row = rows.SharedRow(i);
						SkinnedDataGridViewButtonCell cell = row.Cells[Index] as SkinnedDataGridViewButtonCell;
						if (cell != null)
						{
							cell.Flat = value;
						}
					}
					DataGridView.InvalidateColumn(Index);
				}
			}
		}

		public SkinnedFieldType FieldType
		{
			get { return _cellTemplate.FieldType; }
			set { _cellTemplate.FieldType = value; }
		}

		public override object Clone()
		{
			SkinnedDataGridViewButtonColumn copy = base.Clone() as SkinnedDataGridViewButtonColumn;
			if (copy != null)
			{
				copy.Flat = Flat;
				copy.FieldType = FieldType;
			}
			return copy;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public SkinnedDataGridViewButtonColumn()
		{
			CellTemplate = new SkinnedDataGridViewButtonCell();
		}

		public override DataGridViewCell CellTemplate
		{
			get { return base.CellTemplate; }
			set
			{
				base.CellTemplate = value;
				_cellTemplate = value as SkinnedDataGridViewButtonCell;
			}
		}
	}
}
