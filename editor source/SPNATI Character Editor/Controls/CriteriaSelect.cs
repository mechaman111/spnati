using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class CriteriaSelect : UserControl
	{
		public CriteriaSelect()
		{
			InitializeComponent();

			PopulateTree();
		}

		private void PopulateTree()
		{
			treeCriteria.Nodes.Clear();


		}
	}

	public class ReportCriteriaAttribute : Attribute
	{
		public string Path;
	}
}
