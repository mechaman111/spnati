using Desktop;
using Desktop.CommonControls;
using Desktop.Skinning;
using SPNATI_Character_Editor.Providers;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SPNATI_Character_Editor
{
	public partial class FilterControl : PropertyEditControl
	{
		private TargetCondition _filter;
		private bool _collapsed;
		private SolidBrush _indicatorBrush = new SolidBrush(Color.Black);
		private bool _countAvailable = false;

		public FilterControl()
		{
			InitializeComponent();

			tableAdvanced.RowAdded += TableAdvanced_RowAdded;
			tableAdvanced.RowRemoved += TableAdvanced_RowAdded;
			recCharacter.RecordChanged += RecCharacter_RecordChanged;
			recWho.RecordType = typeof(FilterType);
			recCharacter.RecordType = typeof(Character);
			valFrom.Text = "";
			valTo.Text = "";
			recWho.RecordChanged += UpdateRecord;
		}

		public override void OnInitialAdd()
		{
			ToggleCollapsed(false);
		}

		public override void ApplyMacro(List<string> values)
		{
			if (values.Count >= 4)
			{
				string count = values[0];
				string tag = values[1];
				string gender = values[2];
				string status = values[3];
				if (string.IsNullOrEmpty(gender))
				{
					_filter.Gender = null;
				}
				else
				{
					_filter.Gender = gender;
				}
				_filter.FilterTag = tag;
				SetCount(count);

				_filter.Status = status;

				if (values.Count > 4)
				{
					_filter.Role = values[4];
					_filter.Variable = values[5];
					_filter.Character = values[6];
					_filter.Stage = values[7];
				}
				if (values.Count > 16)
				{
					_filter.Hand = values[8];
					_filter.Layers = values[9];
					_filter.StartingLayers = values[10];
					_filter.SaidMarker = values[11];
					_filter.NotSaidMarker = values[12];
					_filter.SayingMarker = values[13];
					_filter.Saying = values[14];
					_filter.TimeInStage = values[15];
					_filter.ConsecutiveLosses = values[16];
				}
			}

			ToggleCollapsed(!_filter.HasAdvancedConditions);
			tableAdvanced.Data = null;
			tableAdvanced.Data = _filter;
		}

		public override void BuildMacro(List<string> values)
		{
			string count = GetCount() ?? "";
			string tag = _filter.FilterTag ?? "";
			string gender = _filter.Gender ?? "";
			values.Add(count);
			values.Add(tag);
			values.Add(gender);
			values.Add(_filter.Status);
			values.Add(_filter.Role);
			values.Add(_filter.Variable);
			values.Add(_filter.Character);
			values.Add(_filter.Stage);
			values.Add(_filter.Hand);
			values.Add(_filter.Layers);
			values.Add(_filter.StartingLayers);
			values.Add(_filter.SaidMarker);
			values.Add(_filter.NotSaidMarker);
			values.Add(_filter.SayingMarker);
			values.Add(_filter.Saying);
			values.Add(_filter.TimeInStage);
			values.Add(_filter.ConsecutiveLosses);
		}

		protected override void OnSetInitialFocus()
		{
			if (string.IsNullOrEmpty(_filter.Role) && (!string.IsNullOrEmpty(_filter.Gender) || !string.IsNullOrEmpty(_filter.Status)))
			{
				valFrom.Focus();
			}
			else if (recCharacter.Visible)
			{
				recCharacter.Focus();
			}
			else
			{
				base.OnSetInitialFocus();
			}
		}

		protected override void OnBoundData()
		{
			tableAdvanced.Data = null;
			recWho.RecordContext = Data;
			_filter = GetValue() as TargetCondition;
			string role = _filter.Role;
			if (string.IsNullOrEmpty(role))
			{
				if (!string.IsNullOrEmpty(_filter.Character))
				{
					recWho.RecordKey = _filter.Character;
				}
				else
				{
					recWho.RecordKey = "any";
				}
			}
			else
			{
				recWho.RecordKey = role;
				if (role == "self")
				{
					recCharacter.RecordKey = (Context as Character)?.Key;
				}
			}
			if (!string.IsNullOrEmpty(_filter.Character))
			{
				recCharacter.RecordKey = _filter.Character;
			}
			if (string.IsNullOrEmpty(_filter.Gender))
			{
				_filter.Gender = null;
			}

			SetCount(_filter.Count);
			txtVariable.Text = _filter.Variable;

			tableAdvanced.Context = Data;
			tableAdvanced.SecondaryContext = Context;
			tableAdvanced.Data = _filter;
		}

		public override void OnAddedToRow()
		{
			OnRequireHeight(GetHeight());
			ToggleCollapsed(!_filter.HasAdvancedConditions);
		}

		private void TableAdvanced_RowAdded(object sender, EventArgs e)
		{
			OnRequireHeight(GetHeight());
		}

		private int GetHeight()
		{
			return tableAdvanced.Top + tableAdvanced.GetTotalHeight() + 1;
		}

		protected override void OnRequireHeight(int height)
		{
			base.OnRequireHeight(height);
		}

		protected override void AddHandlers()
		{
			recWho.RecordChanged += RecordDataChanged;
			recCharacter.RecordChanged += CharacterChanged;
			valFrom.TextChanged += DataValueChanged;
			valTo.TextChanged += DataValueChanged;
			txtVariable.TextChanged += DataValueChanged;
		}

		protected override void RemoveHandlers()
		{
			recWho.RecordChanged -= RecordDataChanged;
			recCharacter.RecordChanged -= CharacterChanged;
			valFrom.TextChanged -= DataValueChanged;
			valTo.TextChanged -= DataValueChanged;
			txtVariable.TextChanged -= DataValueChanged;
		}

		private void UpdateRecord(object sender, RecordEventArgs e)
		{
			FilterType type = recWho.Record as FilterType;
			if (type == null)
			{
				pnlRange.Visible = false;
				_countAvailable = false;
				pnlCharacter.Visible = false;
				pnlVariable.Visible = false;
			}
			else
			{
				pnlCharacter.Visible = type.CanSpecifyCharacter;
				_countAvailable = pnlRange.Visible = pnlVariable.Visible = type.CanSpecifyRange;
			}
			string key = type?.Key ?? "";
			switch (key)
			{
				case "self":
					grpContainer.PanelType = SkinnedBackgroundType.Group3;
					break;
				case "target":
					grpContainer.PanelType = SkinnedBackgroundType.Group4;
					break;
				case "other":
				case "opp":
					grpContainer.PanelType = SkinnedBackgroundType.Group2;
					break;
				default:
					grpContainer.PanelType = SkinnedBackgroundType.Group1;
					break;
			}
			tableAdvanced.HeaderType = grpContainer.PanelType;
		}

		private void DataValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void RecCharacter_RecordChanged(object sender, RecordEventArgs e)
		{
			pnlRange.Visible = pnlVariable.Visible = _countAvailable && string.IsNullOrEmpty(recCharacter.RecordKey);
		}

		private void CharacterChanged(object sender, RecordEventArgs e)
		{
			Save();
			tableAdvanced.UpdateProperty("Character");
		}

		private void RecordDataChanged(object sender, RecordEventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			FilterType type = recWho.Record as FilterType;
			if (type == null)
			{
				return;
			}
			_filter.Role = DetermineRole(type);
			if (type.CanSpecifyRange && pnlRange.Visible)
			{
				_filter.Count = GetCount();
				_filter.Variable = txtVariable.Text;

			}
			else
			{
				_filter.Count = "";
				_filter.Variable = null;
			}
			if (type.CanSpecifyCharacter)
			{
				_filter.Character = recCharacter.RecordKey;
			}
			else if (type.IsCharacter)
			{
				_filter.Character = type.Key;
			}
			else
			{
				_filter.Character = null;
			}
		}

		/// <summary>
		/// Gets the appropriate role for a filter type
		/// </summary>
		/// <param name="type"></param>
		private string DetermineRole(FilterType type)
		{
			switch (type.Key)
			{
				case "self":
				case "target":
				case "opp":
				case "other":
					return type.Key;
				default:
					return null;
			}
		}

		private void SetCount(string range)
		{
			if (range == null)
			{
				valFrom.Text = "";
				valTo.Text = "";
				return;
			}
			string[] pieces = range.Split('-');
			int from;
			int to;
			if (int.TryParse(pieces[0], out from))
			{
				valFrom.Value = Math.Max(valFrom.Minimum, Math.Min(valFrom.Maximum, from));
				valFrom.Text = valFrom.Value.ToString();
			}
			else
			{
				valFrom.Text = "";
			}
			if (pieces.Length > 1)
			{
				if (int.TryParse(pieces[1], out to))
				{
					valTo.Value = Math.Max(valTo.Minimum, Math.Min(valTo.Maximum, to));
					valTo.Text = valTo.Value.ToString();
				}
				else
				{
					valTo.Text = "";
				}
			}
			else
			{
				valTo.Value = valFrom.Value;
				valTo.Text = valTo.Value.ToString();
			}
		}

		private string GetCount()
		{
			int from = (int)valFrom.Value;
			int to = (int)valTo.Value;
			if (valFrom.Text == "")
			{
				from = -1;
			}
			if (valTo.Text == "")
			{
				to = -1;
			}
			return GUIHelper.ToRange(from, to);
		}

		private void cmdExpand_Click(object sender, EventArgs e)
		{
			ToggleCollapsed(!_collapsed);
		}

		/// <summary>
		/// Displays or hides the advanced property table
		/// </summary>
		/// <param name="collapsed"></param>
		private void ToggleCollapsed(bool collapsed)
		{
			_collapsed = collapsed;
			if (_collapsed)
			{
				cmdExpand.Image = Properties.Resources.ChevronDown;
				OnRequireHeight(25);

			}
			else
			{
				cmdExpand.Image = Properties.Resources.ChevronUp;
				OnRequireHeight(GetHeight());
			}
		}

		public override void EditSubProperty(string property)
		{
			tableAdvanced.AddProperty(property);
		}
	}

	public class FilterAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(FilterControl); }
		}
	}
}
