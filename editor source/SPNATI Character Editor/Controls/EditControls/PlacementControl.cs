using System;

namespace SPNATI_Character_Editor
{
	[SubVariable("place", AllowRHSVariables = true)]
	[SubVariable("revplace", AllowRHSVariables = true)]
	[SubVariable("lead", AllowRHSVariables = true)]
	[SubVariable("biggestlead", AllowRHSVariables = true)]
	[SubVariable("trail", AllowRHSVariables = true)]
	public class PlacementControl : PlayerControlBase
	{
		private Desktop.Skinning.SkinnedComboBox cboPlace;
		private Desktop.Skinning.SkinnedTextBox txtValue;
		private Desktop.Skinning.SkinnedComboBox cboOperator;

		private static readonly PlacementVariable[] _variables =
		{
			new PlacementVariable("place", "Place (1=first, 5=last)"),
			new PlacementVariable("revplace", "Reverse Place (1=last, 5=first)"),
			new PlacementVariable("lead", "Layers in lead"),
			new PlacementVariable("trail", "Layers behind"),
			new PlacementVariable("biggestlead", "Largest lead"),
		};

		public PlacementControl()
		{
			InitializeComponent();
			cboOperator.DataSource = ExpressionTest.Operators;
			
			cboPlace.DataSource = _variables;
			Bindings.Add("AlsoPlaying");
		}

		private void InitializeComponent()
		{
			this.cboPlace = new Desktop.Skinning.SkinnedComboBox();
			this.txtValue = new Desktop.Skinning.SkinnedTextBox();
			this.cboOperator = new Desktop.Skinning.SkinnedComboBox();
			this.SuspendLayout();
			// 
			// cboPlace
			// 
			this.cboPlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboPlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboPlace.BackColor = System.Drawing.Color.White;
			this.cboPlace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPlace.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboPlace.FormattingEnabled = true;
			this.cboPlace.Location = new System.Drawing.Point(161, 0);
			this.cboPlace.Name = "cboPlace";
			this.cboPlace.SelectedIndex = -1;
			this.cboPlace.SelectedItem = null;
			this.cboPlace.Size = new System.Drawing.Size(100, 21);
			this.cboPlace.Sorted = false;
			this.cboPlace.TabIndex = 11;
			// 
			// txtValue
			// 
			this.txtValue.BackColor = System.Drawing.Color.White;
			this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtValue.ForeColor = System.Drawing.Color.Black;
			this.txtValue.Location = new System.Drawing.Point(324, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(100, 20);
			this.txtValue.TabIndex = 13;
			// 
			// cboOperator
			// 
			this.cboOperator.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboOperator.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboOperator.BackColor = System.Drawing.Color.White;
			this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOperator.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboOperator.FormattingEnabled = true;
			this.cboOperator.Location = new System.Drawing.Point(264, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.SelectedIndex = -1;
			this.cboOperator.SelectedItem = null;
			this.cboOperator.Size = new System.Drawing.Size(56, 21);
			this.cboOperator.Sorted = false;
			this.cboOperator.TabIndex = 12;
			// 
			// PlacementControl
			// 
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.cboPlace);
			this.Name = "PlacementControl";
			this.Size = new System.Drawing.Size(503, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		protected override void AddHandlers()
		{
			base.AddHandlers();
			cboOperator.SelectedIndexChanged += ValueChanged;
			cboPlace.SelectedIndexChanged += ValueChanged;
			txtValue.TextChanged += ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			base.RemoveHandlers();
			cboOperator.SelectedIndexChanged -= ValueChanged;
			cboPlace.SelectedIndexChanged -= ValueChanged;
			txtValue.TextChanged -= ValueChanged;
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void OnBoundData()
		{
			base.OnBoundData();
			cboOperator.Text = Expression.Operator ?? "==";
			txtValue.Text = Expression.Value;
			OnAddedToRow();
		}

		protected override void BindVariable(string variable)
		{
			for (int i = 0; i < _variables.Length; i++)
			{
				if (_variables[i].Variable == variable)
				{
					cboPlace.SelectedIndex = i;
					break;
				}
			}
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Place Check");
		}

		protected override void OnSave()
		{
			base.OnSave();
			string op = cboOperator.SelectedItem?.ToString();
			if (string.IsNullOrEmpty(op))
			{
				op = "==";
			}
			Expression.Operator = op;

			string value = txtValue.Text;
			if (string.IsNullOrEmpty(value))
			{
				value = null;
			}
			Expression.Value = value;
		}

		protected override string GetVariable()
		{
			string v = "";
			PlacementVariable variable = cboPlace.SelectedItem as PlacementVariable;
			if (variable != null)
			{
				v = variable.Variable;
			}
			return v;
		}

		private class PlacementVariable
		{
			public string Variable;
			public string DisplayName;

			public PlacementVariable(string variable, string name)
			{
				Variable = variable;
				DisplayName = name;
			}

			public override string ToString()
			{
				return DisplayName;
			}
		}
	}
}
