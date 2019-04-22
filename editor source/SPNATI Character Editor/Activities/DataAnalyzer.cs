using Desktop;
using System;
using System.Threading.Tasks;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(AnalyzerRecord), 0)]
	public partial class DataAnalyzer : Activity
	{
		public DataAnalyzer()
		{
			InitializeComponent();
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
			foreach(Character character in CharacterDatabase.Characters)
			{
				await LoadChunk((int)(100 * (count / total)), () => LoadCharacter(character));
				count++;
			}
			pnlLoad.Visible = false;
			pnlEdit.Visible = true;
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
