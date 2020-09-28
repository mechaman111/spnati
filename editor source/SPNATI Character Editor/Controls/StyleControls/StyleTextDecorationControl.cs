using System;
using System.Drawing;

namespace SPNATI_Character_Editor.Controls.StyleControls
{
	[SubAttribute("text-decoration", "Underline, strikethrough, etc.")]
	public partial class StyleTextDecorationControl : SubAttributeControl
	{
		public StyleTextDecorationControl()
		{
			InitializeComponent();
			cboStyle.Items.AddRange(new string[] {
				"solid",
				"double",
				"dotted",
				"dashed",
				"wavy",
			});
		}

		protected override void OnBoundData()
		{
			string value = Attribute.Value?.ToLower()?.Trim() ?? "";
			int lineCount = 0;
			if (value.Contains("underline"))
			{
				chkUnder.Checked = true;
				lineCount++;
			}
			if (value.Contains("overline"))
			{
				chkOver.Checked = true;
				lineCount++;
			}
			if (value.Contains("line-through"))
			{
				chkStrike.Checked = true;
				lineCount++;
			}
			string[] pieces = value.Split(' ');
			int pieceCount = pieces.Length - lineCount;
			if (pieceCount > 2)
			{
				//color style
				try
				{
					Color color = ColorTranslator.FromHtml(pieces[pieces.Length - 2]);
					fldColor.Color = color;
				}
				catch { }
			}
			if (pieceCount > 1)
			{
				//color|style
				if (cboStyle.Items.Contains(pieces))
				{
					cboStyle.SelectedItem = pieces;
				}
				else
				{
					try
					{
						Color color = ColorTranslator.FromHtml(pieces[pieces.Length - 2]);
						fldColor.Color = color;
					}
					catch { }
				}
			}
			if (pieceCount <= 0)
			{
				fldColor.Color = Color.Black;
			}
		}

		protected override void RemoveHandlers()
		{
			chkOver.CheckedChanged -= ChkOver_CheckedChanged;
			chkUnder.CheckedChanged -= ChkOver_CheckedChanged;
			chkStrike.CheckedChanged -= ChkOver_CheckedChanged;
			fldColor.ColorChanged -= ChkOver_CheckedChanged;
			cboStyle.SelectedIndexChanged -= ChkOver_CheckedChanged;
		}

		protected override void AddHandlers()
		{
			chkOver.CheckedChanged += ChkOver_CheckedChanged;
			chkUnder.CheckedChanged += ChkOver_CheckedChanged;
			chkStrike.CheckedChanged += ChkOver_CheckedChanged;
			fldColor.ColorChanged += ChkOver_CheckedChanged;
			cboStyle.SelectedIndexChanged += ChkOver_CheckedChanged;
		}

		private void ChkOver_CheckedChanged(object sender, EventArgs e)
		{
			Save();
		}


		protected override void OnSave()
		{
			string line = "";
			if (chkUnder.Checked)
			{
				line = "underline";
			}
			if (chkOver.Checked)
			{
				line += " overline";
			}
			if (chkStrike.Checked)
			{
				line += " line-through";
			}
			line.Trim();
			//IE doesn't support shorthand, so leaving out for now since the preview won't show it
			//string color = ColorTranslator.ToHtml(fldColor.Color);
			//string style = cboStyle.SelectedItem?.ToString();
			//Attribute.Value = $"{line} {color} {style}";
			Attribute.Value = line;
		}
	}
}
