using Desktop;
using Desktop.CommonControls;
using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	public partial class PoseMetadataControl : PropertyEditControl
	{
		public PoseMetadataControl()
		{
			InitializeComponent();
		}

		private void cmdSet_Click(object sender, EventArgs e)
		{
			PoseSettingsForm form = new PoseSettingsForm();
			PoseEntry entry = Data as PoseEntry;
			Dictionary<string, string> extraData = entry.ExtraMetadata;
			form.SetData(extraData);
			form.UseManualCode = entry.SkipPreProcessing;
			if (form.ShowDialog() == DialogResult.OK)
			{
				entry.SkipPreProcessing = form.UseManualCode;
				entry.ExtraMetadata = form.GetData();
			}
		}
	}

	public class PoseMetadataAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(PoseMetadataControl); }
		}
	}
}
