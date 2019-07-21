using Desktop.Skinning;
using KisekaeImporter.ImageImport;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class CropCopier : SkinnedForm
	{
		public CropCopier(PoseList list, ImageMetadata selected)
		{
			InitializeComponent();

			foreach (ImageMetadata data in list.Poses)
			{
				lstFrom.Items.Add(data);
				lstTo.Items.Add(data);
			}
			lstFrom.SelectedItem = selected;
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			ImageMetadata source = lstFrom.SelectedItem as ImageMetadata;
			foreach (ImageMetadata dest in lstTo.CheckedItems)
			{
				dest.CropInfo = source.CropInfo;
			}
			Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
