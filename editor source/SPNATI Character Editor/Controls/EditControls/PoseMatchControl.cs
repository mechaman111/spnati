using Desktop;
using Desktop.CommonControls;
using System;
using System.Linq;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	public partial class PoseMatchControl : PropertyEditControl
	{
		private bool _handlersAdded = false;

		public PoseMatchControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			cboPose.Text = GetValue()?.ToString() ?? "";
			PopulateCombo();
		}

		private void PopulateCombo()
		{
			bool handlersAdded = _handlersAdded;
			if (handlersAdded)
			{
				RemoveHandlers();
			}
			string current = cboPose.Text;
			cboPose.SelectedIndex = -1;
			TargetCondition condition = Data as TargetCondition;
			Character target = GetCharacter();
			cboPose.Items.Clear();
			if (target != null)
			{
				CharacterEditorData editorData = CharacterDatabase.GetEditorData(target);
				cboPose.Items.AddRange(target.PoseLibrary.Poses.Select(p => p.GetBasicName())
					.GroupBy(n => n)
					.Select(group => group.First())
					.Where(name =>
					{
						if (editorData == null)
						{
							return true;
						}
						foreach (string prefix in editorData.IgnoredPrefixes)
						{
							if (name.StartsWith(prefix))
							{
								return false;
							}
						}
						return true;
					}));
			}
			cboPose.Text = current;
			if (handlersAdded)
			{
				AddHandlers();
			}
		}

		private Character GetCharacter()
		{
			Character character = CharacterDatabase.Get(GetBindingValue("Character")?.ToString());
			if (character == null && Context is Character)
			{
				character = Context as Character;
			}
			if (character == null && SecondaryContext is Character)
			{
				character = SecondaryContext as Character;
			}
			return character;
		}

		protected override void AddHandlers()
		{
			_handlersAdded = true;
			cboPose.TextChanged += CboPose_TextChanged;
			TargetCondition data = Data as TargetCondition;
			if (data != null)
			{
				data.PropertyChanged += Data_PropertyChanged;
			}
		}

		protected override void RemoveHandlers()
		{
			_handlersAdded = false;
			cboPose.TextChanged -= CboPose_TextChanged;
			TargetCondition data = Data as TargetCondition;
			if (data != null)
			{
				data.PropertyChanged -= Data_PropertyChanged;
			}
		}

		private void Data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Character" || e.PropertyName == "Role")
			{
				PopulateCombo();
			}
		}

		private void CboPose_TextChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void OnClear()
		{
			cboPose.Text = "";
		}

		protected override void OnSave()
		{
			string pose = cboPose.Text;
			SetValue(pose);
		}
	}

	public class PoseMatchAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(PoseMatchControl); }
		}
	}
}
