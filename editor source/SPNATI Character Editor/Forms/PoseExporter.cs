using Desktop.Skinning;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class PoseExporter : SkinnedForm
	{
		private Pose _pose;
		private int _stage;

		public PoseExporter()
		{
			InitializeComponent();
			preview.AutoPlayback = false;
		}

		public void SetPose(ISkin character, Pose pose, int stage)
		{
			preview.SetCharacter(character);
			_pose = pose;
			_stage = stage;
			lblName.Text = pose.Id;
			txtFile.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), pose.Id + ".gif");
		}

		private void cmdBrowse_Click(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtFile.Text))
			{
				saveFileDialog1.InitialDirectory = Path.GetDirectoryName(txtFile.Text);
				saveFileDialog1.FileName = Path.GetFileName(txtFile.Text);
			}
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				txtFile.Text = saveFileDialog1.FileName;
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cmdExport_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtFile.Text))
			{
				MessageBox.Show("Please specify a file name.");
			}
			preview.Width = (int)valWidth.Value;
			preview.Height = (int)valHeight.Value;
			PoseMapping pose = new PoseMapping(_pose.Id);
			pose.SetPose(-1, _pose);
			preview.SetImage(pose, _stage);

			float duration = preview.Pose.Sprites.Max(s =>
			{
				float start = s.Start;
				float end = s.Start;
				if (s.Keyframes.Count > 0)
				{
					end = s.Keyframes[s.Keyframes.Count - 1].Time + start;
				}
				return end;
			});

			try
			{
				float fps = (float)valFrameRate.Value;
				string file = txtFile.Text;
				using (FileStream stream = new FileStream(file, FileMode.Create))
				{
					float delay = 1000.0f / fps;
					delay /= 1000;
					delay *= 100;
					delay = (float)Math.Round(delay) * 10;
					using (GifWriter writer = new GifWriter(stream, (int)delay))
					{
						for (float time = 0; ; time += (1 / fps))
						{
							preview.SetTime(time);
							Image img = preview.GetImage();
							writer.WriteFrame(img);
							img.Dispose();
							if (time >= duration)
							{
								break;
							}
						}
					}
					MessageBox.Show($"Successfully exported to {file}.");
					Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
	}
}
