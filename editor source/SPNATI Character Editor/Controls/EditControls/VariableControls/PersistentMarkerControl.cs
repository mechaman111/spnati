using Desktop;
using Desktop.CommonControls;
using System;

namespace SPNATI_Character_Editor
{
	[SubVariable("persistent")]
	[SubVariable("marker")]
	public partial class PersistentMarkerControl : PlayerControlBase
	{
		public bool Persistent;

		public PersistentMarkerControl()
		{
			InitializeComponent();
			cboOperator.DataSource = ExpressionTest.Operators;

			recField.RecordType = typeof(Marker);
		}

		public override void OnAddedToRow()
		{
			string label = "Marker";
			if (Persistent)
			{
				label += " (Persistent)";
			}
			OnChangeLabel(label);
		}

		private bool FilterPrivateMarkers(IRecord record)
		{
			Marker marker = record as Marker;
			return marker.Scope == MarkerScope.Public;
		}

		protected override void OnBoundData()
		{
			base.OnBoundData();
			cboOperator.Text = Expression.Operator;
			txtValue.Text = Expression.Value;
			OnAddedToRow();
		}

		protected override void BindVariable(string variable)
		{
			string[] pieces = variable.Split('.'); ;
			string varName = pieces[0];
			Persistent = varName == "persistent";
			string propName = "";
			if (pieces.Length > 1)
			{
				propName = pieces[1];
			}
			if (!string.IsNullOrEmpty(propName) && propName != "*")
			{
				recField.RecordKey = propName;
			}
		}

		protected override void OnTargetTypeChanged()
		{
			recField.RecordContext = GetTargetCharacter();
			TargetId targetType = TargetType;
			if (targetType?.Key == "self")
			{
				recField.RecordFilter = null;
			}
			else
			{
				recField.RecordFilter = FilterPrivateMarkers;
			}
		}
		
		protected override void RemoveHandlers()
		{
			base.RemoveHandlers();
			recField.RecordChanged -= RecordChanged;
			cboOperator.SelectedIndexChanged -= ValueChanged;
			txtValue.TextChanged -= ValueChanged;
		}

		protected override void AddHandlers()
		{
			base.AddHandlers();
			recField.RecordChanged += RecordChanged;
			cboOperator.SelectedIndexChanged += ValueChanged;
			txtValue.TextChanged += ValueChanged;
		}

		protected override void OnClear()
		{
			RemoveHandlers();
			recField.RecordKey = null;
			cboOperator.SelectedIndex = 0;
			txtValue.Text = "";
			AddHandlers();
			Save();
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
			string key = recField.RecordKey;
			if (string.IsNullOrEmpty(key))
			{
				key = "*";
			}

			if (Persistent)
			{
				return $"persistent.{key}";
			}
			else
			{
				return $"marker.{key}";
			}
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void RecordChanged(object sender, RecordEventArgs record)
		{
			Save();
		}
	}
}
