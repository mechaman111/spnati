using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class ValidationForm : Form
	{
		private Dictionary<Character, List<ValidationError>> _warnings;

		public ValidationForm()
		{
			InitializeComponent();

			PopulateFilters();
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
				if (level != ValidationFilterLevel.Minor)
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

		public void SetData(Dictionary<Character, List<ValidationError>> warnings)
		{
			_warnings = warnings;
			lstCharacters.Items.Clear();
			foreach (Character c in warnings.Keys)
			{
				lstCharacters.Items.Add(c);
			}
			lstCharacters.SelectedIndex = 0;
		}

		private void lstCharacters_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			PopulateWarnings(lstCharacters.SelectedItem as Character);
		}

		private void PopulateWarnings(Character c)
		{
			if (c == null)
			{
				txtWarnings.Text = "";
				return;
			}
			else
			{
				ValidationFilterLevel filterLevel = GetFilterLevel();
				List<ValidationError> warnings;
				List<string> text = new List<string>();
				if (_warnings.TryGetValue(c, out warnings))
				{
					foreach (ValidationError error in warnings)
					{
						if (CharacterValidator.IsInFilter(filterLevel, error.Level))
						{
							text.Add(error.Text);
						}
					}
					txtWarnings.Text = string.Join("\r\n", text);
				}
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void lstFilters_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			PopulateWarnings(lstCharacters.SelectedItem as Character);
		}
	}
}
