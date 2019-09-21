using Desktop.Skinning;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class MarkerSetup : SkinnedForm
	{
		public MarkerSetup()
		{
			InitializeComponent();
		}

		public void SetData(Character character, List<string> markers)
		{
			lstItems.SelectableItems = character.Markers.Value.Values.ToList().ConvertAll(m => m.Name).ToArray();
			lstItems.SelectedItems = markers.ToArray();
		}

		public List<string> Markers
		{
			get { return lstItems.SelectedItems.ToList(); }
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
