using Desktop;
using Desktop.CommonControls;
using SPNATI_Character_Editor.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.StyleControls
{
	public partial class StyleAttributeEditControl : PropertyEditControl
	{
		private SubAttributeControl _subcontrol;

		public StyleAttributeEditControl()
		{
			InitializeComponent();
		}

		protected override void OnIndexChanged()
		{
			if (_subcontrol != null)
			{
				_subcontrol.Index = Index;
			}
		}

		protected override void OnBoundData()
		{
			StyleAttribute attribute = GetValue() as StyleAttribute;
			SwitchMode(attribute);
			if (_subcontrol != null)
			{
				return;
			}
			txtAttribute.Text = attribute.Name;
			txtValue.Text = attribute.Value;
		}

		public override void OnAddedToRow()
		{
			if (_subcontrol != null)
			{
				_subcontrol.OnAddedToRow();
				OnChangeLabel(_subcontrol.Attribute.Name);
			}
		}

		protected override void OnDestroy()
		{
			if (_subcontrol != null)
			{
				_subcontrol.Destroy();
			}
			base.OnDestroy();
		}

		protected override void AddHandlers()
		{
			if (_subcontrol != null)
			{
				_subcontrol.AddEventHandlers();
			}
			else
			{
				txtAttribute.TextChanged += Text_ValueChanged;
				txtValue.TextChanged += Text_ValueChanged;
			}
		}

		protected override void RemoveHandlers()
		{
			if (_subcontrol != null)
			{
				_subcontrol.AddEventHandlers();
			}
			else
			{
				txtAttribute.TextChanged -= Text_ValueChanged;
				txtValue.TextChanged -= Text_ValueChanged;
			}
		}

		private void DestroySubControl()
		{
			if (_subcontrol != null)
			{
				Controls.Remove(_subcontrol);
				_subcontrol.Destroy();
				foreach (Control ctl in Controls)
				{
					ctl.Visible = true;
				}
				_subcontrol = null;
			}
		}

		/// <summary>
		/// Switches to a more appropriate control specialized for a particular attribute
		/// </summary>
		private void SwitchMode(StyleAttribute attribute)
		{
			DestroySubControl();

			string name = attribute.Name;
			if (!string.IsNullOrEmpty(name))
			{
				StyleAttributeRecord record = Definitions.Instance.Get<StyleAttributeRecord>(name);
				if (record != null && record.ControlType != null)
				{
					_subcontrol = Activator.CreateInstance(record.ControlType) as SubAttributeControl;
					if (_subcontrol != null)
					{
						foreach (Control ctl in Controls)
						{
							ctl.Visible = false;
						}

						_subcontrol.ParentControl = this;
						_subcontrol.Attribute = attribute;
						_subcontrol.Margin = new Padding(0);
						_subcontrol.Left = 0;
						_subcontrol.Top = 0;
						_subcontrol.Dock = DockStyle.Fill;
						_subcontrol.SetData(Data, Property, Index, Context, SecondaryContext, UndoManager, PreviewData, ParentTable);
						_subcontrol.RemoveEventHandlers(); //these'll be added back when this control's AddHandlers gets called, so this is a method to prevent double handlers
						Controls.Add(_subcontrol);
					}
				}
			}
		}

		private void Text_ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void OnClear()
		{
			if (_subcontrol != null)
			{
				_subcontrol.Clear();
			}
			else
			{
				RemoveHandlers();
				txtAttribute.Text = "";
				txtValue.Text = "";
				AddHandlers();
				Save();
			}
		}

		protected override void OnSave()
		{
			if (_subcontrol != null)
			{
				_subcontrol.Save();
			}
			else
			{
				StyleAttribute attribute = GetValue() as StyleAttribute;
				attribute.Name = txtAttribute.Text;
				attribute.Value = txtValue.Text;
			}
		}
	}

	public class StyleAttributeEditControlAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(StyleAttributeEditControl); }
		}
	}

	public class SubAttributeControl : PropertyEditControl
	{
		public PropertyEditControl ParentControl;
		public StyleAttribute Attribute;

		public void AddEventHandlers()
		{
			AddHandlers();
		}

		public void RemoveEventHandlers()
		{
			RemoveHandlers();
		}

		public override bool IsUpdating
		{
			get
			{
				return base.IsUpdating || ParentControl.IsUpdating;
			}
		}

		protected StyleAttribute GetAttribute(string name)
		{
			IList list = GetList();
			if (list != null)
			{
				foreach (object item in list)
				{
					StyleAttribute attrib = item as StyleAttribute;
					if (attrib != null && attrib.Name == name)
					{
						return attrib;
					}
				}
			}
			return null;
		}

		public override void BuildMacro(List<string> values)
		{
			Save();
			StyleAttribute attribute = GetValue() as StyleAttribute;
			values.Add(attribute.Name);
			values.Add(attribute.Value);
		}

		protected string[] GetPieces()
		{
			string value = Attribute.Value;
			string[] pieces = value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			return pieces;
		}

		protected float PxToInt(string px)
		{
			if (px.EndsWith("px") || px.EndsWith("pt"))
			{
				px = px.Substring(0, px.Length - 2);
			}
			float value;
			float.TryParse(px, out value);
			return value;
		}

		protected float ToPts(string value)
		{
			float amount = PxToInt(value);
			if (value.EndsWith("px"))
			{
				amount *= 0.75f;
			}
			return amount;
		}

		protected string IntToPx(int value)
		{
			if (value == 0)
			{
				return "0";
			}
			else
			{
				return $"{value}px";
			}
		}
	}

	/// <summary>
	/// Maps a control to a particular CSS attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class SubAttributeAttribute : Attribute
	{
		public string Attribute { get; private set; }
		public string Description { get; private set; }

		public SubAttributeAttribute(string attribute, string description)
		{
			Attribute = attribute;
			Description = description;
		}
	}
}
