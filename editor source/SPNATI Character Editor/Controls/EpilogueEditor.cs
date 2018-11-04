using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class EpilogueEditor : UserControl
	{
		private Character _character;
		private Epilogue _ending;
		private int _screenIndex;
		private Screen _screen;
		private int _textIndex;
		private Font _font;
		private HtmlDocument _doc;
		private bool _populatingEnding;

		public EpilogueEditor()
		{
			InitializeComponent();
			_font = new Font("Trebuchet MS", 0.875f);

			//Arrow combo boxes
			DataGridViewComboBoxColumn col = gridText.Columns["ColArrow"] as DataGridViewComboBoxColumn;
			col.Items.Clear();
			col.Items.Add("none");
			col.Items.Add("left");
			col.Items.Add("right");
			col.Items.Add("up");
			col.Items.Add("down");

			Enabled = _character != null;
			EnableFields(false);
		}

		private void EnableFields(bool enabled)
		{
			groupScreen.Enabled = enabled;
			txtTitle.Enabled = enabled;
			cboGender.Enabled = enabled;
			cmdDeleteEnding.Enabled = enabled;
		}

		public void SetCharacter(Character character)
		{
			bool ready = !(_doc == null || _doc?.Url?.AbsoluteUri == "about:blank");
			if (!ready)
			{
				wb.Navigate(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "epilogue.html"));
			}
			_character = character;
			_ending = null;
			_screen = null;
			if (ready)
			{
				EnableForEdit();
			}
		}

		private void ClearFields()
		{
			gridText.Rows.Clear();
			txtTitle.Text = "";
			txtScreenImage.Text = "";
			lblScreen.Text = "";
			UpdateImage();
			UpdateTextBoxes();
		}

		private void PopulateEndingCombo()
		{
			_populatingEnding = true;
			//Endings combo
			cboEnding.Items.Clear();
			foreach (Epilogue ending in _character.Endings)
			{
				cboEnding.Items.Add(ending);
			}
			_populatingEnding = false;
		}

		private void cboEnding_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_populatingEnding)
				return;
			Epilogue epilogue = cboEnding.SelectedItem as Epilogue;
			LoadEnding(epilogue);
		}

		private void cmdAddEnding_Click(object sender, EventArgs e)
		{
			CreateNewEnding();
		}

		private void cmdDeleteEnding_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to delete this ending?", "Delete Ending", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				RemoveEnding();
			}
		}

		private void CreateNewEnding()
		{
			Epilogue ending = new Epilogue();
			_character.Endings.Add(ending);
			PopulateEndingCombo();
			cboEnding.SelectedItem = ending;
		}

		private void LoadEnding(Epilogue ending)
		{
			SaveEnding();
			_ending = ending;

			if (_ending == null)
			{
				cboGender.SelectedIndex = -1;
				txtTitle.Text = "";
				EnableFields(false);
			}
			else
			{
				cboGender.Text = ending.Gender;
				txtTitle.Text = ending.Title;
				EnableFields(true);
			}
			LoadScreen(0);
		}

		public void Save()
		{
			SaveEnding();
		}

		private void SaveEnding()
		{
			if (_ending == null)
				return;
			_ending.Title = txtTitle.Text;
			_ending.Gender = cboGender.Text;
			SaveScreen();

			//Strip out any empty screens
			for (int i = _ending.Screens.Count - 1; i >= 0; i--)
			{
				Screen screen = _ending.Screens[i];
				if (screen.IsEmpty)
				{
					_ending.Screens.RemoveAt(i);
				}
			}
		}

		private void RemoveEnding()
		{
			if (_character == null || _ending == null)
				return;
			_character.Endings.Remove(_ending);
			_ending = null;
			LoadEnding(null);
			PopulateEndingCombo();
			if (_character.Endings.Count > 0)
			{
				cboEnding.SelectedIndex = 0;
			}
		}

		private void LoadScreen(int index)
		{
			if (_ending == null)
				return;
			SaveScreen();
			if (index == _ending.Screens.Count)
			{
				//new screen
				Screen screen = new Screen();
				_ending.Screens.Add(screen);
			}
			else if (index >= _ending.Screens.Count)
				index = -1;

			_screenIndex = index;
			gridText.Rows.Clear();
			if (index == -1)
			{
				txtScreenImage.Text = "";
				lblScreen.Text = "";
				_screen = null;
			}
			else
			{
				_screen = _ending.Screens[index];
				txtScreenImage.Text = _screen.Image;
				lblScreen.Text = string.Format("Screen {0} of {1}", _screenIndex + 1, _ending.Screens.Count);
				PopulateTextGrid();
			}

			cmdPrevScreen.Enabled = (index > 0);
			cmdNextScreen.Text = (index == _ending.Screens.Count - 1 ? "Add" : "Next");
			_textIndex = 0;
			UpdateImage();
			UpdateTextBoxes();
		}

		private void SaveScreen()
		{
			if (_screen == null)
				return;
			_screen.Image = txtScreenImage.Text;
			_screen.Text.Clear();
			foreach (DataGridViewRow row in gridText.Rows)
			{
				string x = row.Cells["ColX"]?.Value?.ToString();
				string y = row.Cells["ColY"]?.Value?.ToString();
				string width = row.Cells["ColWidth"]?.Value?.ToString();
				string arrow = row.Cells["ColArrow"]?.Value?.ToString();
				string content = row.Cells["ColContent"]?.Value?.ToString();
				if (x == null || y == null || arrow == null || content == null)
					continue;
				EndingText text = new EndingText();
				text.X = x;
				text.Y = y;
				text.Width = width;
				text.Arrow = arrow;
				text.Content = content;
				_screen.Text.Add(text);
			}
		}

		private void PopulateTextGrid()
		{
			if (_screen == null)
				return;
			foreach (EndingText text in _screen.Text)
			{
				DataGridViewRow row = gridText.Rows[gridText.Rows.Add()];
				row.Cells["ColX"].Value = text.X;
				row.Cells["ColY"].Value = text.Y;
				row.Cells["ColWidth"].Value = text.Width;
				row.Cells["ColArrow"].Value = text.Arrow;
				row.Cells["ColContent"].Value = text.Content;
			}
		}

		private void txtScreenImage_Validated(object sender, System.EventArgs e)
		{
			UpdateImage();
		}

		private void UpdateImage()
		{
			if (_character != null)
			{
				string image = txtScreenImage.Text;
				string filename = Path.Combine(Config.GetRootDirectory(_character), image);
				HtmlElement background = _doc.GetElementById("epilogue-screen");
				if (File.Exists(filename))
				{
					background.Style = string.Format("background-image:url({0});", ("file:///" + filename).Replace("\\", "/"));
				}
				else
				{
					background.Style = "";
				}
			}
		}

		private void UpdateTextBoxes()
		{
			//Clear any existing textboxes
			HtmlElement background = _doc.GetElementById("epilogue-screen");
			background.InnerHtml = "";

			for (int i = 0; i <= _textIndex && i < gridText.Rows.Count; i++)
			{
				var elem = CreateTextBox(i);
				if (elem != null)
				{
					background.AppendChild(elem);
				}
			}
		}

		private HtmlElement CreateTextBox(int index)
		{
			DataGridViewRow row = gridText.Rows[index];
			string x = row.Cells["ColX"]?.Value?.ToString();
			string y = row.Cells["ColY"]?.Value?.ToString();
			string width = row.Cells["ColWidth"]?.Value?.ToString();
			string arrow = row.Cells["ColArrow"]?.Value?.ToString();
			string text = row.Cells["ColContent"]?.Value?.ToString();
			if (x == null || y == null || arrow == null || text == null)
				return null;

			if (width == null)
				width = "20%";

			if (x == "centered")
			{
				int w;
				if (width.Length > 1 && int.TryParse(width.Substring(0, width.Length - 1), out w))
				{
					x = (50 - (w / 2.0f)).ToString() + "%";
				}
			}

			StringBuilder sb = new StringBuilder();
			HtmlElement root = _doc.CreateElement("div");
			root.Style = string.Format("position:absolute;left:{0};top:{1};width:{2}", x, y, width);

			sb.Append("<div class='bordered dialogue-bubble-area modal-dialogue'>");
			sb.Append("<div class='dialogue-area'>");
			sb.Append(string.Format("<span class='dialogue-bubble arrow-{0}'>", arrow));
			sb.Append(text);
			sb.Append("</span></div></div>");
			root.InnerHtml = sb.ToString();
			return root;
		}

		private void cmdPrevScreen_Click(object sender, System.EventArgs e)
		{
			LoadScreen(_screenIndex - 1);
		}

		private void cmdNextScreen_Click(object sender, System.EventArgs e)
		{
			LoadScreen(_screenIndex + 1);
		}

		private void cmdInsertScreen_Click(object sender, EventArgs e)
		{
			if (_ending == null || _screenIndex == -1)
				return;
			Screen screen = new Screen();
			_ending.Screens.Insert(_screenIndex, screen);
			LoadScreen(_screenIndex);
		}

		private void cmdRemoveScreen_Click(object sender, EventArgs e)
		{
			if (_ending == null || _screen == null)
				return;
			_ending.Screens.RemoveAt(_screenIndex);
			if (_screenIndex == _ending.Screens.Count)
				LoadScreen(_ending.Screens.Count - 1);
			else LoadScreen(0);
		}

		private void cmdBrowseImage_Click(object sender, System.EventArgs e)
		{
			if (_character == null)
				return;
			string dir = Config.GetRootDirectory(_character);
			imageFileDialog.InitialDirectory = dir;
			DialogResult result = DialogResult.OK;
			bool invalid;
			do
			{
				invalid = false;
				result = imageFileDialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					if (Path.GetDirectoryName(imageFileDialog.FileName) != dir)
					{
						MessageBox.Show("Images need to come from the character's folder.");
						invalid = true;
					}
				}
			}
			while (invalid);

			if (result == DialogResult.OK)
			{
				string file = Path.GetFileName(imageFileDialog.FileName);
				txtScreenImage.Text = file;
				UpdateImage();
			}
		}

		private void gridText_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			_textIndex = e.RowIndex;
			UpdateTextBoxes();
		}

		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (wb.Document.Url.AbsoluteUri == "about:blank")
				return;
			_doc = wb.Document;
			//Insert CSS
			var element = _doc.CreateElement("link");
			element.SetAttribute("rel", "stylesheet");
			element.SetAttribute("type", "text/css");
			element.SetAttribute("href", "file:///" + Path.Combine(Config.GameDirectory, "css", "spni.css"));
			_doc.GetElementsByTagName("head")[0].AppendChild(element);

			if (_character != null)
			{
				EnableForEdit();
			}
		}

		private void EnableForEdit()
		{
			Enabled = true;
			PopulateEndingCombo();

			if (_character.Endings.Count > 0)
			{
				cboEnding.SelectedIndex = 0;
			}
			else
			{
				ClearFields();
			}
		}

		private void gridText_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewCell cell = gridText.Rows[e.RowIndex].Cells[e.ColumnIndex];
			if (cell.Value == null)
				return;

			string value = cell.Value.ToString();
			if (e.ColumnIndex == 0)
			{
				if (!value.EndsWith("%"))
				{
					if (value.ToLower().StartsWith("cen"))
					{
						cell.Value = "centered";
					}
					else
					{
						int intValue;
						if (int.TryParse(value, out intValue))
						{
							cell.Value = intValue + "%";
						}
						else cell.Value = 0;
					}
				}
			}
			else if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
			{
				if (!value.EndsWith("%"))
				{
					int intValue;
					if (int.TryParse(value, out intValue))
					{
						cell.Value = intValue + "%";
					}
					else cell.Value = 0;
				}
			}
		}

		private void cmdAdvancedConditions_Click(object sender, EventArgs e)
		{
			new AdvancedEpilogueConditions(_character, _ending).ShowDialog();
		}
	}
}
