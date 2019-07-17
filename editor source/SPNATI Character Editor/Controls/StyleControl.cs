using Desktop;
using SPNATI_Character_Editor.IO;
using SPNATI_Character_Editor.Providers;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class StyleControl : UserControl
	{
		private Character _character;
		private CharacterStyleSheet _sheet;
		private HtmlElement _previewElement;
		private StyleRule _activeRule;

		public StyleControl()
		{
			InitializeComponent();
		}

		public void SetCharacter(Character character)
		{
			_character = character;
			CharacterStyleSheet sheet = _character.Styles;
			_sheet = sheet;
			if (_sheet != null)
			{
				PopulateSheet();
			}
			else
			{
				splitContainer1.Panel2.Visible = false;
			}
		}

		private void PopulateSheet()
		{
			splitContainer1.Panel2.Visible = true;
			txtAdvanced.Text = _sheet.FullText;
			if (_sheet.AdvancedMode)
			{
				splitContainer1.Visible = false;
			}
			else
			{
				string path = Path.GetFullPath(Path.Combine("Resources", "preview.html"));
				wbPreview.Navigate(path);
				_sheet.Rules.CollectionChanged += Rules_CollectionChanged;
				PopulateStyles();
			}
		}

		private void Rules_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
					lstStyles.Items.Add(e.NewItems[0]);
					lstStyles.SelectedIndex = e.NewStartingIndex;
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
					lstStyles.Items.Remove(e.OldItems[0]);
					break;
			}
		}

		private void PopulateStyles()
		{
			foreach (StyleRule rule in _sheet.Rules)
			{
				lstStyles.Items.Add(rule);
			}
		}

		public void Save()
		{
			if (_sheet == null) { return; }
			_character.StyleSheetName = _sheet.Name;
			_sheet.FullText = txtAdvanced.Text;
		}

		private void wbPreview_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			splitContainer1.Panel2.Visible = false;
			_previewElement = wbPreview.Document.GetElementById("preview");
		}

		private void tsAdd_Click(object sender, System.EventArgs e)
		{
			if (_sheet == null)
			{
				_character.StyleSheetName = "styles.css"; //this will force a default stylesheet to be created
				_sheet = _character.Styles;
				PopulateSheet();
				tmrCreate.Start();
			}
			else
			{
				StyleRule rule = new StyleRule() { ClassName = "style" };
				_sheet.Rules.Add(rule);
			}
		}

		private void tsRemove_Click(object sender, System.EventArgs e)
		{
			if (_activeRule != null)
			{
				_sheet.Rules.Remove(_activeRule);
			}
		}

		private void lstStyles_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_activeRule != null)
			{
				_activeRule.PropertyChanged -= _activeRule_PropertyChanged;
			}
			_activeRule = lstStyles.SelectedItem as StyleRule;
			UpdatePreview();
			if (_activeRule != null)
			{
				txtName.Text = _activeRule.ClassName;
				txtDescription.Text = _activeRule.Description;
				splitContainer1.Panel2.Visible = true;
				tableAttributes.Data = _activeRule;
				_activeRule.PropertyChanged += _activeRule_PropertyChanged;
			}
			else
			{
				splitContainer1.Panel2.Visible = false;
				tableAttributes.Data = null;
				txtName.Text = "";
				txtDescription.Text = "";
			}
		}

		private void _activeRule_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "ClassName")
			{
				lstStyles.RefreshListItems();
			}
			UpdatePreview();
		}

		private void UpdatePreview()
		{
			if (_previewElement != null && _activeRule != null)
			{
				txtSample.Text = $"This is the {{{_activeRule.ClassName}}}Preview Text{{!reset}} for the style.";
				List<string> styles = new List<string>();
				foreach (StyleAttribute attr in _activeRule.Attributes)
				{
					styles.Add($"{attr.Name}: {attr.Value}");
				}
				_previewElement.Style = string.Join(";", styles);
			}
		}

		private void txtDescription_TextChanged(object sender, System.EventArgs e)
		{
			if (_activeRule != null)
			{
				_activeRule.Description = txtDescription.Text;
			}
		}

		private void txtName_TextChanged(object sender, System.EventArgs e)
		{
			if (_activeRule != null)
			{
				_activeRule.ClassName = txtName.Text;
			}
		}

		private void txtName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string text = txtName.Text;
			text =  Regex.Replace(text, @"\W", "");
			txtName.Text = text;
		}

		private void cmdAddAttribute_Click(object sender, System.EventArgs e)
		{
			if (_activeRule == null) { return; }

			StyleAttributeRecord record = RecordLookup.DoLookup(typeof(StyleAttributeRecord), "", false, null, true, null) as StyleAttributeRecord;
			if (record != null)
			{
				StyleAttribute attribute = new StyleAttribute(record.Key, "");
				if (record.Key == "other")
				{
					attribute.Name = "";
				}
				_activeRule.Attributes.Add(attribute);
				tableAttributes.Data = null;
				tableAttributes.Data = _activeRule;
			}
		}

		private void tmrCreate_Tick(object sender, System.EventArgs e)
		{
			tmrCreate.Stop();
			lstStyles.SelectedIndex = 0;
		}
	}
}
