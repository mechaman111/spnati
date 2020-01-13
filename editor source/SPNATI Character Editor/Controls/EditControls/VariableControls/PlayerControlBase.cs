using Desktop;

namespace SPNATI_Character_Editor
{
	public partial class PlayerControlBase : SubVariableControl
	{
		public PlayerControlBase()
		{
			InitializeComponent();
			recType.RecordType = typeof(TargetId);
			Bindings.Add("AlsoPlaying");
			Bindings.Add("Target");
		}

		public TargetId TargetType
		{
			get { return recType.Record as TargetId; }
		}

		public ExpressionTest Expression { get; private set; }

		protected override void OnBindingUpdated(string property)
		{
			if (property == "AlsoPlaying" && (recType.RecordKey == "_" || recType.RecordKey == null))
			{
				string targetType;
				string variable;
				ExtractExpressionPieces(out targetType, out variable);
				recType.RecordKey = targetType;
			}
			else if (property == "Target" && recType.RecordKey == "target")
			{
				OnTargetTypeChanged();
			}
		}

		protected override void AddHandlers()
		{
			recType.RecordChanged += RecType_RecordChanged;
		}

		protected override void RemoveHandlers()
		{
			recType.RecordChanged -= RecType_RecordChanged;
		}

		private void RecType_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			OnTargetTypeChanged();
			Save();
		}

		protected virtual void OnTargetTypeChanged() { }

		protected Character GetTargetCharacter()
		{
			string targetType = recType.RecordKey;
			if (targetType == "self")
			{
				return Context as Character;
			}
			else if (targetType == "target")
			{
				Case dataCase = Data as Case;
				string target = dataCase.GetTarget();
				if (!string.IsNullOrEmpty(target))
				{
					return CharacterDatabase.Get(target);
				}
			}
			return CharacterDatabase.GetById(targetType);
		}

		private bool FilterTarget(IRecord record)
		{
			if (record.Key != "target") { return true; }
			Case workingCase = Data as Case;
			if (workingCase == null)
			{
				return true;
			}
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(workingCase.Tag);
			return trigger.HasTarget;

		}

		protected override void OnBoundData()
		{
			recType.RecordContext = Data;
			Expression = GetValue() as ExpressionTest;
			string variable;
			string targetType;
			ExtractExpressionPieces(out targetType, out variable);

			recType.RecordFilter = FilterTarget;
			recType.RecordKey = targetType;
			OnTargetTypeChanged();
			BindVariable(variable);
		}

		protected virtual void BindVariable(string variable)
		{
		}

		protected override void OnSave()
		{
			string targetType = recType.RecordKey;
			if (string.IsNullOrEmpty(targetType))
			{
				targetType = "_";
			}
			string v = GetVariable();
			Expression.Expression = $"~{targetType}.{v}~";
		}

		protected virtual string GetVariable()
		{
			return "";
		}
	}
}
