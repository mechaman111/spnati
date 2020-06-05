using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class FindReplace : SkinnedForm
	{
		public event EventHandler<FindArgs> Find;
		public event EventHandler<FindArgs> Replace;
		public event EventHandler<FindArgs> ReplaceAll;
		public event EventHandler RestoreFocus;

		private bool _replaceMode;
		private Button _lastClick;

		private string _finishText = "Find reached the end of the dialogue.";
		public string FinishText
		{
			get { return _finishText; }
			set { _finishText = value; }
		}

		public string AdvancedLabel
		{
			get { return chkMarkers.Text; }
			set { chkMarkers.Text = value; }
		}

		public bool ShowAdvanced
		{
			get { return chkMarkers.Visible; }
			set { chkMarkers.Visible = value; }
		}

		public FindReplace()
		{
			InitializeComponent();
		}

		private void FindReplace_Shown(object sender, EventArgs e)
		{
			SetReplaceMode(_replaceMode);
		}

		public void SetReplaceMode(bool replace)
		{
			if (replace)
			{
				tabs.SelectedTab = tabReplace;
			}
			else
			{
				tabs.SelectedTab = tabFind;
			}
			SetMode(replace);
			txtFind.Focus();
			txtFind.SelectAll();
		}

		private void SetMode(bool replace)
		{
			_replaceMode = replace;
			cmdReplace.Visible = replace;
			cmdReplaceAll.Visible = replace;
			lblReplace.Visible = replace;
			txtReplace.Visible = replace;
			chkMarkers.Visible = !replace;
		}

		public void RepeatKeyPress()
		{
			if(_lastClick != null)
			{
				_lastClick.PerformClick();
			}
		}

		private void tabs_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetMode(tabs.SelectedTab == tabReplace);
		}

		private void cmdFind_Click(object sender, EventArgs e)
		{
			_lastClick = cmdFind;
			var args = new FindArgs(txtFind.Text)
			{
				MatchCase = chkMatchCase.Checked,
				WholeWords = chkWholeWords.Checked,
				SearchAdvanced = chkMarkers.Checked && !_replaceMode,
			};
			Find?.Invoke(this, args);
			if (!args.Success)
			{
				MessageBox.Show(FinishText);
			}
		}

		private void cmdReplace_Click(object sender, EventArgs e)
		{
			_lastClick = cmdReplace;
			var args = new FindArgs(txtFind.Text, txtReplace.Text)
			{
				MatchCase = chkMatchCase.Checked,
				WholeWords = chkWholeWords.Checked
			};
			Replace?.Invoke(this, args);
		}

		private void cmdReplaceAll_Click(object sender, EventArgs e)
		{
			_lastClick = cmdReplaceAll;
			var args = new FindArgs(txtFind.Text, txtReplace.Text)
			{
				ReplaceAll = true,
				MatchCase = chkMatchCase.Checked,
				WholeWords = chkWholeWords.Checked
			};
			ReplaceAll?.Invoke(this, args);
			MessageBox.Show(string.Format("Replaced {0} occurrence(s) of \"{1}.\"", args.ReplaceCount, args.FindText));
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			Hide();
		}

		private void FindReplace_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}

		private void OnRestoreFocus(object sender, MouseEventArgs e)
		{
			focusTimer.Enabled = true;
		}

		private void focusTimer_Tick(object sender, EventArgs e)
		{
			focusTimer.Enabled = false;
			RestoreFocus?.Invoke(sender, new EventArgs());
		}

		private void chkMarkers_CheckedChanged(object sender, EventArgs e)
		{
			if (chkMarkers.Checked)
			{
				tabs.TabPages.Remove(tabReplace);
			}
			else
			{
				tabs.TabPages.Add(tabReplace);
			}
		}
	}

	public class FindArgs : EventArgs
	{
		/// <summary>
		/// Text to find
		/// </summary>
		public string FindText;
		/// <summary>
		/// Replacement text
		/// </summary>
		public string ReplaceText;
		/// <summary>
		/// True to replace instead of just find
		/// </summary>
		public bool DoReplace;
		/// <summary>
		/// True to replace all instances
		/// </summary>
		public bool ReplaceAll;
		/// <summary>
		/// True to match case
		/// </summary>
		public bool MatchCase;
		/// <summary>
		/// True to find whole words only
		/// </summary>
		public bool WholeWords;
		/// <summary>
		/// True to search markers instead of text
		/// </summary>
		public bool SearchAdvanced;

		/// <summary>
		/// Event handler sets this to indicate whether the find/replace operation was successful
		/// </summary>
		public bool Success;

		/// <summary>
		/// Event handler sets this to indicate how many replacements were made
		/// </summary>
		public int ReplaceCount;

		public FindArgs(string find)
		{
			FindText = find;
			DoReplace = false;
		}

		public FindArgs(string find, string replace)
		{
			FindText = find;
			ReplaceText = replace;
			DoReplace = true;
		}
	}
}
