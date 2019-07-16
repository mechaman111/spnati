using Desktop.Skinning;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop.Forms
{
	public partial class VariableMapper : SkinnedForm
	{
		public Dictionary<string, string> Map
		{
			get
			{
				Dictionary<string, string> map = new Dictionary<string, string>();
				foreach (DataGridViewRow row in gridVariables.Rows)
				{
					string variable = row.Cells[0].Value?.ToString();
					string replacement = row.Cells[1].Value?.ToString();
					if (string.IsNullOrEmpty(variable))
					{
						continue;
					}
					if (string.IsNullOrEmpty(replacement))
					{
						replacement = variable;
					}
					map[variable] = replacement;
				}
				return map;
			}
		}

		public VariableMapper()
		{
			InitializeComponent();
		}

		public VariableMapper(HashSet<string> variables) : this()
		{
			foreach (string variable in variables)
			{
				gridVariables.Rows.Add(new object[] { variable });
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
