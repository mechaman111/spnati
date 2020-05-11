using Desktop;
using SPNATI_Character_Editor.Activities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class ValidationControl : UserControl
	{
		private Dictionary<Character, List<ValidationError>> _warnings = new Dictionary<Character, List<ValidationError>>();

		private Character _character;
		private CancellationTokenSource _cancelToken;
		public bool IsBusy { get; private set; }

		public ValidationControl()
		{
			InitializeComponent();
			PopulateFilters();
		}

		/// <summary>
		/// Performs validation for a single character, or all characters
		/// </summary>
		/// <param name="character">Character to validate. Pass null to validate all.</param>
		public void DoValidation(Character character)
		{
			if (character != null)
			{
				Validate(character);
			}
			else
			{
				ValidateAll();
			}
		}

		private void Validate(Character character)
		{
			pnlValid.Visible = false;
			List<ValidationError> warnings;
			bool valid = CharacterValidator.Validate(character, out warnings);
			if (valid)
			{
				pnlValid.Visible = true;
				pnlValid.BringToFront();
			}
			else
			{
				_warnings[character] = warnings;
				PopulateWarnings();
			}
		}

		private void PopulateWarnings()
		{
			cmdGoTo.Enabled = false;
			pnlWarnings.BringToFront();
			lstCharacters.Items.Clear();
			if (_warnings == null)
			{
				return;
			}
			foreach (Character c in _warnings.Keys)
			{
				lstCharacters.Items.Add(c);
			}
			lstCharacters.SelectedIndex = 0;
		}

		private async void ValidateAll()
		{
			IsBusy = true;

			pnlProgress.Visible = true;
			pnlProgress.BringToFront();
			progressBar.Value = 0;
			progressBar.Maximum = CharacterDatabase.Count;
			Enabled = false;
			cmdGoTo.Visible = false;

			int count = CharacterDatabase.Count;
			var progressUpdate = new Progress<Character>(next => {
				if (progressBar.Value < progressBar.Maximum)
				{
					progressBar.Value++;
				}
				lblProgress.Text = $"Validating {next}...";
			});

			_cancelToken = new CancellationTokenSource();
			CancellationToken token = _cancelToken.Token;

			try
			{
				_warnings = await ValidateAll(progressUpdate, token);
				PopulateWarnings();
			}
			finally
			{
				pnlProgress.Visible = false;
				Enabled = true;
				IsBusy = false;
			}
		}

		private Task<Dictionary<Character, List<ValidationError>>> ValidateAll(IProgress<Character> progress, CancellationToken cancelToken)
		{
			return Task.Run(() =>
			{
				try
				{
					Dictionary<Character, List<ValidationError>> allWarnings = new Dictionary<Character, List<ValidationError>>();
					foreach (Character c in CharacterDatabase.Characters)
					{
						if (c.FolderName == "human") { continue; }
						string status = Listing.Instance.GetCharacterStatus(c.FolderName);
						if (status == OpponentStatus.Incomplete || status == OpponentStatus.Offline || status == OpponentStatus.Duplicate)
							continue; //don't validate characters that aren't in the main opponents folder, since they're likely to have errors but aren't being actively worked on
						progress.Report(c);
						List<ValidationError> warnings;
						if (!CharacterValidator.Validate(c, out warnings))
						{
							allWarnings[c] = warnings;
						}
						cancelToken.ThrowIfCancellationRequested();
					}
					return allWarnings;
				}
				catch (OperationCanceledException)
				{
					return null;
				}
			}, cancelToken);
		}

		/// <summary>
		/// Cancels anything currently underway
		/// </summary>
		public void Cancel()
		{
			_cancelToken.Cancel();
		}

		/// <summary>
		/// Sets up the filters listbox
		/// </summary>
		private void PopulateFilters()
		{
			foreach (ValidationFilterLevel level in Enum.GetValues(typeof(ValidationFilterLevel)))
			{
				if (level == ValidationFilterLevel.None)
					continue;
				lstFilters.Items.Add(level);
				if (level != ValidationFilterLevel.Minor && level != ValidationFilterLevel.MissingTargets)
					lstFilters.SelectedItems.Add(level);
			}
		}

		/// <summary>
		/// Creates a validation filter level from the filters list box
		/// </summary>
		/// <returns></returns>
		private ValidationFilterLevel GetFilterLevel()
		{
			ValidationFilterLevel level = ValidationFilterLevel.None;
			foreach (object item in lstFilters.SelectedItems)
			{
				if (item is ValidationFilterLevel)
				{
					level |= (ValidationFilterLevel)item;
				}
			}

			return level;
		}

		private void lstCharacters_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			PopulateWarnings(lstCharacters.SelectedItem as Character);
		}

		private void PopulateWarnings(Character c)
		{
			_character = c;
			lstWarnings.Items.Clear();
			if (c == null)
			{
				return;
			}
			else
			{
				c.Behavior.BuildTriggers(c);
				ValidationFilterLevel filterLevel = GetFilterLevel();
				List<ValidationError> warnings;
				if (_warnings.TryGetValue(c, out warnings))
				{
					foreach (ValidationError error in warnings)
					{
						if (CharacterValidator.IsInFilter(filterLevel, error.Level))
						{
							lstWarnings.Items.Add(error);
						}
					}
				}
			}
		}

		private void lstFilters_SelectedIndexChanged(object sender, EventArgs e)
		{
			PopulateWarnings(lstCharacters.SelectedItem as Character);
		}

		private void lstWarnings_SelectedIndexChanged(object sender, EventArgs e)
		{
			ValidationError error = lstWarnings.SelectedItem as ValidationError;
			cmdGoTo.Enabled = (error != null && error.Context != null);
		}

		private void lstWarnings_DoubleClick(object sender, EventArgs e)
		{
			ValidationError error = lstWarnings.SelectedItem as ValidationError;
			GotoError(error);
		}

		private void cmdGoTo_Click(object sender, EventArgs e)
		{
			ValidationError error = lstWarnings.SelectedItem as ValidationError;
			GotoError(error);
		}

		private void GotoError(ValidationError error)
		{
			if (error == null || error.Context == null) { return; }

			switch (error.Context.ContextArea)
			{
				case ValidationContext.Area.Dialogue:
					Shell.Instance.Launch<Character, DialogueEditor>(_character, error.Context);
					break;
				case ValidationContext.Area.Epilogue:
					Shell.Instance.Launch<Character, EpilogueEditor>(_character, error.Context);
					break;
				case ValidationContext.Area.Collectible:
					Shell.Instance.Launch<Character, CollectibleEditor>(_character, error.Context);
					break;
			}			
		}

		private void cmdCopy_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			foreach (ValidationError error in lstWarnings.Items)
			{
				sb.AppendLine(error.ToString());
			}

			Clipboard.Clear();
			Clipboard.SetText(sb.ToString());
			Shell.Instance.SetStatus("Validation errors copied to the clipboard.");
		}

		private void cmdCopyAll_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			ValidationFilterLevel filterLevel = GetFilterLevel();
			foreach (KeyValuePair<Character, List<ValidationError>> kvp in _warnings)
			{
				Character c = kvp.Key;
				sb.AppendLine("***************************************************");
				sb.AppendLine("Warnings for: " + c);
				sb.AppendLine("***************************************************");
				foreach (ValidationError error in kvp.Value)
				{
					if (CharacterValidator.IsInFilter(filterLevel, error.Level))
					{
						sb.AppendLine(error.ToString());
					}
				}
				sb.AppendLine();
				sb.AppendLine();
				sb.AppendLine();
				sb.AppendLine();
			}

			Clipboard.Clear();
			Clipboard.SetText(sb.ToString());
			Shell.Instance.SetStatus("Validation errors copied to the clipboard.");
		}
	}
}
