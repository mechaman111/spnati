using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop;
using System.IO;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(CharacterLoaderRecord), 0)]
	public partial class CharacterLoader : Activity
	{
		private bool _loading;

		public CharacterLoader()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			Shell.Instance.Maximize(true);
		}
		protected override void OnFirstActivate()
		{
			LoadData();
		}
		public override bool CanDeactivate(DeactivateArgs args)
		{
			return !_loading;
		}
		public override bool CanQuit(CloseArgs args)
		{
			return !_loading;
		}

		private async void LoadData()
		{
			_loading = true;
			progressBar.Maximum = CharacterDatabase.UnloadedCount;
			progressBar.Visible = true;
			int count = 0;
			IEnumerator<Character> characters = CharacterDatabase.LoadAllIncrementally();
			while (characters.MoveNext())
			{
				Character c = characters.Current;
				await LoadChunk(c.Name, count++, () => { });
			}
			progressBar.Visible = false;
			_loading = false;
			Shell.Instance.CloseWorkspace(Workspace);
			Shell.Instance.Maximize(false);			
		}

		private Task LoadChunk(string caption, int progress, Action action)
		{
			lblProgress.Text = $"Loading {caption}...";
			progressBar.Value = Math.Min(progressBar.Maximum, progress);
			return Task.Run(action);
		}
	}

	public class CharacterLoaderRecord : BasicRecord
	{
	}
}
