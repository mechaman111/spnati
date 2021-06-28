using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	[SubVariable("stage")]
	public partial class PlayerStageControl : PlayerControlBase
	{
		private static readonly KeyValuePair<string, string>[] Operators = {
			new KeyValuePair<string, string>(null, ""),
			new KeyValuePair<string, string>("==", "is at"),
			new KeyValuePair<string, string>("!=", "is not at"),
			new KeyValuePair<string, string>("<=", "is at or before"),
			new KeyValuePair<string, string>("<", "is before"),
			new KeyValuePair<string, string>(">=", "is at or after"),
			new KeyValuePair<string, string>(">", "is after"),
		};

		public PlayerStageControl()
		{
			InitializeComponent();
			cboOperator.DisplayMember = "Value";
			cboOperator.ValueMember = "Key";
			cboOperator.DataSource = Operators;
		}

		protected override string GetVariable()
		{
			return "stage";
		}

		protected override void OnBoundData()
		{
			base.OnBoundData();
			cboOperator.SelectedValue = Expression.Operator ?? "==";

			int stage;
			if (int.TryParse(Expression.Value, out stage) && stage >= 0 && stage < cboStage.Items.Count)
			{
				cboStage.SelectedIndex = stage;
			}
			else
			{
				cboStage.SelectedIndex = -1;
			}
			OnAddedToRow();
		}

		protected override void OnTargetTypeChanged()
		{
			UpdateStage();
		}

		private void UpdateStage()
		{
			Character character = GetTargetCharacter();
			int stage = cboStage.SelectedIndex;
			List<object> data = new List<object>();

			if (character == null || character.Layers == 0)
			{
				//If the character is not valid, still allow something but there's no way to give a useful name to it
				for (int i = 0; i < 8 + Clothing.ExtraStages; i++)
				{
					data.Add(i);
				}
				cboStage.DataSource = data;
			}
			else
			{
				IWardrobe skin = character;
				for (int i = 0; i < character.Layers + Clothing.ExtraStages; i++)
				{
					data.Add(character.LayerToStageName(i, false, skin));
				}
				cboStage.DataSource = data;
			}
			if (stage >= 0 && cboStage.Items.Count > stage)
			{
				cboStage.SelectedIndex = stage;
				cboStage.Text = cboStage.Items[stage].ToString();
			}
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Stage");
		}

		protected override void AddHandlers()
		{
			base.AddHandlers();
			cboOperator.SelectedIndexChanged += CboOperator_ValueChanged;
			cboStage.SelectedIndexChanged += CboOperator_ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			base.AddHandlers();
			cboStage.SelectedIndexChanged -= CboOperator_ValueChanged;
			cboOperator.SelectedIndexChanged -= CboOperator_ValueChanged;
		}

		private void CboOperator_ValueChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			base.OnSave();
			Expression.Operator = cboOperator.SelectedValue?.ToString() ?? cboOperator.Text;
			Expression.Value = cboStage.SelectedIndex >= 0 ? cboStage.SelectedIndex.ToString() : "";
		}
	}
}
