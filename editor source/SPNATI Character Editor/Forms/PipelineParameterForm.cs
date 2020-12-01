using Desktop.Skinning;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class PipelineParametersForm : SkinnedForm
	{
		public PipelineParametersForm()
		{
			InitializeComponent();
		}

		public void SetData(List<string> parameters)
		{
			grid.Rows.Clear();
			if (parameters != null)
			{
				foreach (string p in parameters)
				{
					grid.Rows.Add(new object[] { p });
				}
			}
		}

		public List<string> Parameters
		{
			get
			{
				List<string> list = new List<string>();
				foreach (DataGridViewRow row in grid.Rows)
				{
					string value = row.Cells[nameof(ColParam)].Value?.ToString();
					if (string.IsNullOrEmpty(value))
					{
						continue;
					}
					list.Add(value);
				}
				if (list.Count == 0)
				{
					return null;
				}
				return list;
			}
		}

		private void grid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			string index = e.RowIndex.ToString();
			using (StringFormat sf = new StringFormat()
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			})
			{
				Rectangle bounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
				Skin skin = SkinManager.Instance.CurrentSkin;
				using (SolidBrush br = new SolidBrush(skin.Surface.ForeColor))
				{
					e.Graphics.DrawString(index, Font, br, bounds, sf);
				}
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
