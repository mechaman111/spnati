using Desktop;
using Desktop.CommonControls;
using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public partial class OneShotControl : PropertyEditControl
	{
		private OneShotMode _mode;
		private int _id;

		public OneShotControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			OneShotAttribute attrib = parameters as OneShotAttribute;
			_mode = attrib.Mode;
		}

		protected override void OnBoundData()
		{
			int id = (int)GetValue();
			_id = id;

			//If this is being added, we must always want to play once, so assign an idea right away
			chkOneShot.Checked = true;
			Save();
		}

		protected override void AddHandlers()
		{
			chkOneShot.CheckedChanged += ChkOneShot_CheckedChanged;
		}

		protected override void RemoveHandlers()
		{
			chkOneShot.CheckedChanged -= ChkOneShot_CheckedChanged;
		}

		private void ChkOneShot_CheckedChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void OnClear()
		{
			RemoveHandlers();
			chkOneShot.Checked = false;
			AddHandlers();
			Save();
		}

		protected override void OnSave()
		{
			if (chkOneShot.Checked)
			{
				if (_id == 0)
				{
					Character character = Context as Character;
					if (character == null)
					{
						_id = 1;
					}
					else
					{
						switch (_mode)
						{
							case OneShotMode.Case:
								_id = ++character.Behavior.MaxCaseId;
								break;
							case OneShotMode.State:
								_id = ++character.Behavior.MaxStateId;
								break;
						}
					}
				}

				SetValue(_id);
			}
			else
			{
				SetValue(0);
			}
		}

		public override void BuildMacro(List<string> values)
		{
			values.Add(chkOneShot.Checked ? _id.ToString() : "");
		}

		public override void ApplyMacro(List<string> values)
		{
			if (values.Count > 0)
			{
				int id;
				if (int.TryParse(values[0], out id) && id > 0)
				{
					chkOneShot.Checked = true;
				}
				else
				{
					chkOneShot.Checked = false;
				}
			}
		}
	}

	public class OneShotAttribute : EditControlAttribute
	{
		public OneShotMode Mode;

		public override Type EditControlType
		{
			get { return typeof(OneShotControl); }
		}

		public OneShotAttribute(OneShotMode mode)
		{
			Mode = mode;
		}
	}

	public enum OneShotMode
	{
		Case,
		State
	}
}
