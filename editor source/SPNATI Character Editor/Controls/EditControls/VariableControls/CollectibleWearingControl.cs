using SPNATI_Character_Editor.DataStructures;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using SPNATI_Character_Editor.Providers;

namespace SPNATI_Character_Editor
{
	[SubVariable("collectible", "wearing")]
	public partial class CollectibleWearingControl : PlayerControlBase
	{
		public CollectibleWearingControl()
		{
			InitializeComponent();

			recField.RecordType = typeof(Collectible);
		}

		protected override void BindVariable(string variable)
		{
			string pattern = @"collectible\.([^~]+)\.wearing";
			Regex regex = new Regex(pattern);
			Match match = regex.Match(variable);
			if (match.Success)
			{
				string propName = match.Groups[1].Value?.ToString();
				if (!string.IsNullOrEmpty(propName) && propName != "*")
				{
					recField.RecordKey = propName;
					return;
				}
			}
			recField.RecordKey = null;
		}

		protected override void OnBoundData()
		{
			base.OnBoundData();
			radNotWearing.Checked = Expression.Value == "false";
			radWearing.Checked = Expression.Value != "false";
			OnAddedToRow();
		}

		protected override void OnTargetTypeChanged()
		{
			recField.RecordContext = GetTargetCharacter();
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Collectible (Wearing)");
		}

		protected override void AddHandlers()
		{
			base.AddHandlers();
			recField.RecordChanged += RecField_RecordChanged;
			radNotWearing.CheckedChanged += RadWearing_CheckedChanged;
			radWearing.CheckedChanged += RadWearing_CheckedChanged;
		}

		protected override void RemoveHandlers()
		{
			base.RemoveHandlers();
			recField.RecordChanged -= RecField_RecordChanged;
			radNotWearing.CheckedChanged -= RadWearing_CheckedChanged;
			radWearing.CheckedChanged -= RadWearing_CheckedChanged;
		}

		private void RecField_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			Save();
		}
		private void RadWearing_CheckedChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		protected override string GetVariable()
		{
			string key = recField.RecordKey;
			if (string.IsNullOrEmpty(key))
			{
				key = "*";
			}
			return $"collectible.{key}.wearing";
		}

		protected override void OnSave()
		{
			base.OnSave();
			Expression.Operator = "==";
			if (radNotWearing.Checked)
			{
				Expression.Value = "false";
			}
			else
			{
				Expression.Value = "true";
			}
		}
	}
}
