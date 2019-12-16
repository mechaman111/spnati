using Desktop.Skinning;
using SPNATI_Character_Editor.Activities;
using SPNATI_Character_Editor.DataStructures;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class TrophyForm : SkinnedForm
	{
		private Character _character;
		private bool _abort;

		public string Id { get; private set; }
		public string Value { get; private set; }

		public TrophyForm()
		{
			InitializeComponent();
		}

		public TrophyForm(Character character, string text, string id, string value) : this()
		{
			_character = character;
			recId.RecordType = typeof(Collectible);
			recId.RecordContext = character;
			lblText.Text = text;
			recId.RecordKey = id;
			if (string.IsNullOrEmpty(id))
			{
				recId.DoSearch();
				id = recId.RecordKey;
				if (string.IsNullOrEmpty(id))
				{
					_abort = true;
				}
			}
			UpdatePreview();

			Collectible collectible = character.Collectibles.Get(id);
			if (!string.IsNullOrEmpty(value))
			{
				bool hasCounter = (collectible == null || collectible.Counter > 0);
				radDecrement.Enabled = radIncrement.Enabled = radSet.Enabled = hasCounter;
				if (hasCounter)
				{
					if (collectible != null)
					{
						valIncrement.Maximum = valDecrement.Maximum = valSet.Maximum = collectible.Counter;
					}

					NumericUpDown numericCtl = null;
					int amount;
					if (value != null && value.StartsWith("+"))
					{
						radIncrement.Checked = true;
						numericCtl = valIncrement;
						value = value.Substring(1);
					}
					else if (value != null && value.StartsWith("-"))
					{
						radDecrement.Checked = true;
						numericCtl = valDecrement;
						value = value.Substring(1);
					}
					else
					{
						radSet.Checked = true;
						numericCtl = valSet;
					}
					if (int.TryParse(value, out amount))
					{
						numericCtl.Value = Math.Max(numericCtl.Minimum, Math.Min(numericCtl.Maximum, amount));
					}
					else
					{
						radUnlock.Checked = true;
					}
				}
				else
				{
					radUnlock.Checked = true;
					radDecrement.Enabled = radIncrement.Enabled = radSet.Enabled = false;
				}
			}
		}

		private void TrophyForm_Shown(object sender, EventArgs e)
		{
			if (_abort)
			{
				DialogResult = DialogResult.Cancel;
				Close();
			}
		}

		private void UpdatePreview()
		{
			Collectible collectible = _character.Collectibles.Get(recId.RecordKey);
			if (collectible != null)
			{
				Bitmap preview = CollectibleEditor.GetImage(collectible.Thumbnail);
				picPreview.Image = preview;
			}
			else
			{
				picPreview.Image = Properties.Resources.Achievement;
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Id = recId.RecordKey;
			Value = null;
			if (!string.IsNullOrEmpty(Id))
			{
				if (radIncrement.Checked)
				{
					Value = $"+{valIncrement.Value}";
				}
				else if (radDecrement.Checked)
				{
					Value = $"+{valDecrement.Value}";
				}
				else if (radSet.Checked)
				{
					Value = $"{valSet.Value}";
				}
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void radDecrement_CheckedChanged(object sender, EventArgs e)
		{
			if (radDecrement.Checked)
			{
				valSet.Enabled = false;
				valDecrement.Enabled = true;
				valIncrement.Enabled = false;
			}
		}

		private void radIncrement_CheckedChanged(object sender, EventArgs e)
		{
			if (radIncrement.Checked)
			{
				valSet.Enabled = false;
				valDecrement.Enabled = false;
				valIncrement.Enabled = true;
			}
		}

		private void radSet_CheckedChanged(object sender, EventArgs e)
		{
			if (radSet.Checked)
			{
				valSet.Enabled = true;
				valDecrement.Enabled = false;
				valIncrement.Enabled = false;
			}
		}

		private void radUnlock_CheckedChanged(object sender, EventArgs e)
		{
			if (radUnlock.Checked)
			{
				valSet.Enabled = false;
				valDecrement.Enabled = false;
				valIncrement.Enabled = false;
			}
		}

		private void recId_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			UpdatePreview();

			Collectible collectible = _character.Collectibles.Get(recId.RecordKey);
			bool hasCounter = (collectible == null || collectible.Counter > 0);
			radDecrement.Enabled = radIncrement.Enabled = radSet.Enabled = hasCounter;
		}
	}
}
