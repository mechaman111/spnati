using SPNATI_Character_Editor.DataStructures;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using SPNATI_Character_Editor.Providers;

namespace SPNATI_Character_Editor
{
	[SubVariable("collectible", "counter")]
	public partial class CollectibleCountControl : PlayerControlBase
	{
		public CollectibleCountControl()
		{
			InitializeComponent();
			cboOperator.DataSource = ExpressionTest.Operators;
			recField.RecordType = typeof(Collectible);
		}

		protected override void BindVariable(string variable)
		{
			string pattern = @"collectible\.([^~]+)\.counter";
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
			try
			{
				cboOperator.SelectedItem = Expression.Operator ?? "==";
			}
			catch
			{
				cboOperator.SelectedItem = "==";
			}
			int count;
			int.TryParse(Expression.Value, out count);
			valCounter.Value = Math.Max(valCounter.Minimum, Math.Min(valCounter.Maximum, count));
			OnAddedToRow();
		}

		protected override void OnTargetTypeChanged()
		{
			recField.RecordContext = GetTargetCharacter();
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Collectible (Counter)");
		}

		protected override void AddHandlers()
		{
			base.AddHandlers();
			recField.RecordChanged += RecField_RecordChanged;
			cboOperator.SelectedIndexChanged += Field_ValueChanged;
			valCounter.ValueChanged += Field_ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			base.RemoveHandlers();
			recField.RecordChanged -= RecField_RecordChanged;
			cboOperator.SelectedIndexChanged -= Field_ValueChanged;
			valCounter.ValueChanged -= Field_ValueChanged;
		}

		private void RecField_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			Save();
		}
		private void Field_ValueChanged(object sender, System.EventArgs e)
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
			return $"collectible.{key}.counter";
		}

		protected override void OnSave()
		{
			base.OnSave();
			Expression.Operator = cboOperator.Text;
			Expression.Value = valCounter.Value.ToString();
		}
	}
}
