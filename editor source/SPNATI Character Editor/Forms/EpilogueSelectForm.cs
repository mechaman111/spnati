using Desktop.Skinning;

namespace SPNATI_Character_Editor.Forms
{
	public partial class EpilogueSelectForm : SkinnedForm
	{
		public EpilogueSelectForm()
		{
			InitializeComponent();
		}

		public Epilogue Epilogue
		{
			set
			{
				string name = value.ToString();
				if (name == "New Ending")
				{
					name = "this ending";
				}
				skinnedLabel1.Text = skinnedLabel1.Text.Replace("{0}", name);
			}
		}

		public int Version
		{
			get
			{
				return radDirective.Checked ? 1 : 2;
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
