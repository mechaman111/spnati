using Desktop;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Spacer]
	[Activity(typeof(Character), 1000, DelayRun = true, Caption = "Configuration")]
	public partial class CharacterConfiguration : Activity
	{
		private Character _character;
		private CharacterEditorData _editorData;

		public override string Caption
		{
			get { return "Configuration"; }
		}

		public CharacterConfiguration()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			_editorData = CharacterDatabase.GetEditorData(_character);
		}

		protected override void OnFirstActivate()
		{
			foreach (string prefix in _editorData.IgnoredPrefixes)
			{
				gridPrefixes.Rows.Add(new object[] { prefix });
			}
		}

		public override void Save()
		{
			_editorData.IgnoredPrefixes.Clear();
			foreach (DataGridViewRow row in gridPrefixes.Rows)
			{
				string prefix = row.Cells[0].Value?.ToString();
				if (!string.IsNullOrEmpty(prefix))
				{
					_editorData.IgnoredPrefixes.Add(prefix);	
				}
			}
		}
	}
}
