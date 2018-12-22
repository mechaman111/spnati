using Desktop;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Costume), 0)]
	public partial class SkinEditor : Activity
	{
		private Costume _costume;

		public SkinEditor()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "General"; }
		}

		protected override void OnInitialize()
		{
			_costume = Record as Costume;
		}

		protected override void OnFirstActivate()
		{
			SkinLink link = _costume.Link;
			if (link != null)
			{
				txtName.Text = link.Name;
			}

			cboBaseStage.Items.Add("- None -");
			for (int i = 0; i < _costume.Layers + Clothing.ExtraStages; i++)
			{
				cboBaseStage.Items.Add(_costume.Character.LayerToStageName(i, _costume));
			}

			//if anyone tries to get fancy by linking to multiple folders instead of just the reskin and the base, sorry, but we're not handling it for now
			string baseFolder = $"opponents/{_costume.Character.FolderName}/";
			StageSpecificValue baseStage = _costume.Folders.Find(f => f.Value == baseFolder);
			if (baseStage != null)
			{
				cboBaseStage.SelectedIndex = baseStage.Stage + 1;
			}
			else
			{
				cboBaseStage.SelectedIndex = -1;
			}
		}

		public override void Save()
		{
			if (_costume.Link != null)
			{
				_costume.Link.Name = txtName.Text;
				Serialization.ExportCharacter(_costume.Character);
			}

			//Here's where any unexpected folders are thrown out
			string folder = _costume.Folder;
			_costume.Folders.Clear();
			_costume.Folders.Add(new StageSpecificValue(0, folder));
			int baseIndex = cboBaseStage.SelectedIndex - 1;
			if (baseIndex >= 0)
			{
				_costume.Folders.Add(new StageSpecificValue(baseIndex, $"opponents/{_costume.Character.FolderName}/"));
			}
		}
	}
}
