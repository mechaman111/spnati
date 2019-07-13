using Desktop.Skinning;
using SPNATI_Character_Editor.EpilogueEditor;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class CreateSequenceForm : SkinnedForm
	{
		public LiveSprite Sprite;
		private ISkin _character;

		public List<string> Frames
		{
			get
			{
				List<string> list = new List<string>();
				foreach (Frame frame in lstFrames.Items)
				{
					list.Add(frame.Src);
				}
				return list;
			}
		}

		public float Duration
		{
			get { return (float)valTime.Value; }
		}

		public string SequenceName
		{
			get
			{
				string name = txtName.Text;
				if (string.IsNullOrEmpty(name))
				{
					if (lstFrames.Items.Count > 0)
					{
						name = Path.GetFileNameWithoutExtension(lstFrames.Items[0]?.ToString());
					}
					else
					{
						name = "sequence";
					}
				}
				return name;
			}
		}

		public CreateSequenceForm(ISkin character, LiveSprite sprite) : this()
		{
			_character = character;
			Sprite = sprite;
			if (sprite != null)
			{
				radConvert.Text = $"Convert {Sprite.Id} Into Sequence";
				radConvert.Checked = true;
			}
			else
			{
				radCreate.Checked = true;
				radConvert.Enabled = false;
			}
		}

		public CreateSequenceForm()
		{
			InitializeComponent();
		}

		private void AddFrame(string src)
		{
			lstFrames.Items.Add(new Frame(src));
		}

		private class Frame
		{
			public string Src;

			public Frame(string src)
			{
				Src = src;
			}

			public override string ToString()
			{
				return Src;
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			if (radCreate.Checked)
			{
				Sprite = null;
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void lstFrames_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Frame frame = lstFrames.SelectedItem as Frame;
			if (frame != null)
			{
				picPreview.Image = LiveImageCache.Get(frame.Src);
			}
			else
			{
				picPreview.Image = null;
			}
		}

		private void tsAdd_Click(object sender, System.EventArgs e)
		{
			if (openDialog.ShowDialog(_character, "") == DialogResult.OK)
			{
				CreateFrames(openDialog.FileName);
			}
		}

		private void CreateFrames(string filename)
		{
			string name = Path.GetFileNameWithoutExtension(filename);
			string ext = Path.GetExtension(filename);
			if (char.IsDigit(name[name.Length - 1]))
			{
				int start;
				for (start = name.Length - 1; start >= 0 && char.IsDigit(name[start]); start--) { }

				string suffix = name.Substring(start + 1, name.Length - start - 1);
				int suffixNo;
				int.TryParse(suffix, out suffixNo);
				int folderLength = filename.Length - Path.GetFileName(filename).Length;
				string main = filename.Substring(0, folderLength + name.Length - (name.Length - start - 1));
				for (; ; suffixNo++)
				{
					string path = main + suffixNo + ext;
					if (File.Exists(Path.Combine(Config.SpnatiDirectory, "opponents", path)))
					{
						AddFrame(path);
					}
					else break;
				}
			}
			else
			{
				AddFrame(filename);
			}
			lstFrames.SelectedIndex = lstFrames.Items.Count - 1;
		}

		private void radCreate_CheckedChanged(object sender, System.EventArgs e)
		{
			lstFrames.Items.Clear();
			if (txtName.Text == "")
			{
				txtName.Text = "New Sequence";
			}
			txtName.Enabled = true;
		}

		private void radConvert_CheckedChanged(object sender, System.EventArgs e)
		{
			lstFrames.Items.Clear();
			txtName.Enabled = false;
			txtName.Text = Sprite.Id;
			if (Sprite.Keyframes.Count == 1)
			{
				CreateFrames((Sprite.Keyframes[0] as LiveSpriteKeyframe).Src);
			}
			else
			{
				foreach (LiveSpriteKeyframe kf in Sprite.Keyframes)
				{
					AddFrame(kf.Src);
				}
				if (Sprite.Keyframes.Count > 1)
				{
					valTime.Value = (decimal)(Sprite.Keyframes[1].Time - Sprite.Keyframes[0].Time);
				}
			}
			lstFrames.SelectedIndex = 0;
		}

		private void tsRemove_Click(object sender, System.EventArgs e)
		{
			Frame frame = lstFrames.SelectedItem as Frame;
			if (frame != null)
			{
				lstFrames.Items.Remove(frame);
			}
		}
	}
}
