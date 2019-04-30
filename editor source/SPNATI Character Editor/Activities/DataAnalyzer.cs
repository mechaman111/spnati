using Desktop;
using SPNATI_Character_Editor.Analyzers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(AnalyzerRecord), 0)]
	public partial class DataAnalyzer : Activity
	{
		public DataAnalyzer()
		{
			InitializeComponent();

			ColOperator.ValueType = typeof(string);

			radAnd.Checked = true;
			ShowMainScreen();
		}

		private void cmdLoad_Click(object sender, System.EventArgs e)
		{
			pnlStart.Visible = false;
			pnlLoad.Visible = true;
			LoadData();
		}

		private async void LoadData()
		{
			int count = 0;
			float total = CharacterDatabase.Count;
			foreach (Character character in CharacterDatabase.Characters)
			{
				await LoadChunk((int)(100 * (count / total)), () => LoadCharacter(character));
				count++;
			}
			ShowMainScreen();
		}

		private void LoadCharacter(Character character)
		{
			character.PrepareForEdit();
		}

		private Task LoadChunk(int progress, Action action)
		{
			progressBar.Value = Math.Min(progressBar.Maximum, progress);
			return Task.Run(action);
		}

		private void ShowMainScreen()
		{
			pnlLoad.Visible = false;
			pnlEdit.Visible = true;

			PopulateTree();
		}

		private void PopulateTree()
		{
			Type interfaceType = typeof(IDataAnalyzer);
			Assembly assembly = GetType().Assembly;
			Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();
			Dictionary<string, TreeNode> staticNodes = new Dictionary<string, TreeNode>();

			foreach (Type type in assembly.GetTypes())
			{
				if (interfaceType.IsAssignableFrom(type) && type != interfaceType && !type.IsAbstract)
				{
					IDataAnalyzer analyzer = Activator.CreateInstance(type) as IDataAnalyzer;
					TreeNode node = new TreeNode(analyzer.Name);
					node.Tag = analyzer;
					nodes[analyzer.Key] = node;
					staticNodes[analyzer.Key] = node;
				}
			}

			foreach (KeyValuePair<string, TreeNode> kvp in staticNodes)
			{
				IDataAnalyzer analyzer = kvp.Value.Tag as IDataAnalyzer;
				if (string.IsNullOrEmpty(analyzer.ParentKey))
				{
					tree.Nodes.Add(kvp.Value);
				}
				else
				{
					string[] chain = analyzer.ParentKey.Split('>');
					TreeNode previous = null;
					foreach (string key in chain)
					{
						TreeNode ancestor;
						if (!nodes.TryGetValue(key, out ancestor))
						{
							ancestor = new TreeNode(key);
							if (previous == null)
							{
								tree.Nodes.Add(ancestor);
							}
							else
							{
								previous.Nodes.Add(ancestor);
							}
							nodes[key] = ancestor;
						}
						previous = ancestor;
					}

					if (previous != null)
					{
						previous.Nodes.Add(kvp.Value);
					}
				}
			}

			tree.Sort();
		}

		private void radAnd_CheckedChanged(object sender, EventArgs e)
		{
			if (radAnd.Checked)
			{
				txtCustomExpression.Text = "AND";
				txtCustomExpression.Enabled = false;
			}
		}

		private void radOr_CheckedChanged(object sender, EventArgs e)
		{
			if (radOr.Checked)
			{
				txtCustomExpression.Text = "OR";
				txtCustomExpression.Enabled = false;
			}
		}

		private void radCustom_CheckedChanged(object sender, EventArgs e)
		{
			if (radCustom.Checked)
			{
				txtCustomExpression.Enabled = true;
			}
		}

		private void gridCriteria_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex == ColDelete.Index)
			{
				Image img = Properties.Resources.Delete;
				e.Paint(e.CellBounds, DataGridViewPaintParts.All);
				var w = img.Width;
				var h = img.Height;
				var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
				var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

				e.Graphics.DrawImage(img, new Rectangle(x, y, w, h));
				e.Handled = true;
			}
		}

		private void cmdAdd_Click(object sender, EventArgs e)
		{
			AddSelectedCriteria();
		}

		private void AddSelectedCriteria()
		{
			TreeNode selected = tree.SelectedNode;
			if (selected == null)
			{
				return;
			}
			IDataAnalyzer analyzer = selected.Tag as IDataAnalyzer;
			if (analyzer == null) { return; }
			DataGridViewRow row = gridCriteria.Rows[gridCriteria.Rows.Add()];
			row.Tag = analyzer;
			row.Cells[nameof(ColIndex)].Value = gridCriteria.Rows.Count;
			row.Cells[nameof(ColData)].Value = analyzer.FullName;

			DataGridViewComboBoxCell combo = row.Cells[nameof(ColOperator)] as DataGridViewComboBoxCell;
			combo.Items.Clear();
			if (analyzer != null)
			{
				Type type = analyzer.GetValueType();
				if (type == typeof(int))
				{
					combo.Items.Add("==");
					combo.Items.Add("!=");
					combo.Items.Add(">");
					combo.Items.Add(">=");
					combo.Items.Add("<");
					combo.Items.Add("<=");
					combo.Items.Add("In range");
					combo.Items.Add("Not in range");
				}
				else if (type == typeof(string))
				{
					combo.Items.Add("==");
					combo.Items.Add("!=");
					combo.Items.Add("Contains");
					combo.Items.Add("Does not contain");
				}
				else if (type == typeof(bool))
				{
					combo.Items.Add("==");
					combo.Items.Add("!=");
				}
			}

			RunReport();
		}

		private void tree_DoubleClick(object sender, EventArgs e)
		{
			AddSelectedCriteria();
		}

		private void RunReport()
		{
			lblError.Visible = false;
			AnalyzerReportCriteria report = new AnalyzerReportCriteria();
			report.Expression = txtCustomExpression.Text;
			foreach (DataGridViewRow row in gridCriteria.Rows)
			{
				DataCriterion criterion = new DataCriterion();
				IDataAnalyzer analyzer = row.Tag as IDataAnalyzer;
				criterion.Analyzer = analyzer;
				string op = row.Cells[nameof(ColOperator)].Value?.ToString();
				string value = row.Cells[nameof(ColValue)].Value?.ToString();
				criterion.Operator = op;
				criterion.Value = value;
				report.Criteria.Add(criterion);
			}
			lstResults.Items.Clear();
			grpResults.Text = "Results";
			if (report.Criteria.Count == 0)
			{
				return;
			}

			try
			{
				lstResults.Sorted = false;
				foreach (Character c in CharacterDatabase.Characters)
				{
					if (c.FolderName == "human") { continue; }
					if (report.MeetsCriteria(c))
					{
						lstResults.Items.Add(c);
					}
				}
				lstResults.Sorted = true;
				grpResults.Text = $"Results: {lstResults.Items.Count}";
			}
			catch (Exception ex)
			{
				lblError.Visible = true;
				lblError.Text = $"Failed to run report: {ex.Message}";
			}
		}

		private void gridCriteria_Validated(object sender, EventArgs e)
		{
			RunReport();
		}

		private void txtCustomExpression_Validated(object sender, EventArgs e)
		{
			RunReport();
		}

		private void gridCriteria_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex < 0 || e.ColumnIndex >= gridCriteria.Columns.Count || e.RowIndex == gridCriteria.NewRowIndex)
			{
				return;
			}
			DataGridViewColumn col = gridCriteria.Columns[e.ColumnIndex];
			if (col == ColDelete)
			{
				gridCriteria.Rows.RemoveAt(e.RowIndex);
				for (int i = e.RowIndex; i < gridCriteria.Rows.Count; i++)
				{
					DataGridViewCell cell = gridCriteria.Rows[i].Cells[nameof(ColIndex)];
					cell.Value = (i + 1);
				}
				RunReport();
			}
		}

		private void gridCriteria_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			RunReport();
		}

		private void gridCriteria_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (gridCriteria.SelectedCells.Count == 0) { return; }
			DataGridViewCell cell = gridCriteria.SelectedCells[0];
			if (cell.ColumnIndex == ColValue.Index)
			{
				IDataAnalyzer analyzer = cell.OwningRow.Tag as IDataAnalyzer;
				if (analyzer != null)
				{
					string[] values = analyzer.GetValues();
					if (values != null)
					{
						TextBox box = e.Control as TextBox;
						AutoCompleteStringCollection col = new AutoCompleteStringCollection();
						col.AddRange(values);
						box.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
						box.AutoCompleteSource = AutoCompleteSource.CustomSource;
						box.AutoCompleteCustomSource = col;
					}
				}
			}
		}

		private void tree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode node = tree.SelectedNode as TreeNode;
			if (node == null) { return; }
			if (node.Tag as IDataAnalyzer == null)
			{
				cmdAdd.Enabled = false;
			}
			else
			{
				cmdAdd.Enabled = true;
			}
		}

		private void lstResults_DoubleClick(object sender, EventArgs e)
		{
			IRecord rec = lstResults.SelectedItem as IRecord;
			if (rec != null)
			{
				Shell.Instance.LaunchWorkspace(rec.GetType(), rec);
			}
		}
	}

	public class AnalyzerRecord : BasicRecord
	{
		public AnalyzerRecord()
		{
			Name = "Analyzer";
			Key = "Analyzer";
		}
	}

	public class AnalyzerProvider : BasicProvider<AnalyzerRecord>
	{
	}
}
