using Desktop;
using KisekaeImporter;
using KisekaeImporter.ImageImport;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 205)]
	public partial class TemplateEditor : Activity
	{
		private ImageLibrary _imageLibrary;
		private Character _character;
		private string _lastTemplateFile;
		private PoseTemplate _lastTemplate;

		public TemplateEditor()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get
			{
				return "Template";
			}
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			_imageLibrary = ImageLibrary.Get(_character);
		}

		protected override void OnActivate()
		{
			gridLayers.Rows.Clear();
			for (int layer = 0; layer < _character.Layers + Clothing.ExtraStages; layer++)
			{
				gridLayers.Rows.Add();
			}
			RestoreLabels();
			if (_lastTemplate != null && gridLayers.Rows.Count == _lastTemplate.Stages.Count)
			{
				LoadTemplate(_lastTemplate);
			}
		}

		/// <summary>
		/// Wrapper around a file dialog to force the file selection to be within the character's folder
		/// </summary>
		/// <param name="dialog">Dialog to open</param>
		/// <returns></returns>
		private DialogResult ChooseFileInDirectory(FileDialog dialog, ref string file)
		{
			string dir = Config.GetRootDirectory(_character);
			dialog.InitialDirectory = dir;
			dialog.FileName = Path.GetFileName(file);
			DialogResult result = DialogResult.OK;
			bool invalid;
			do
			{
				invalid = false;
				result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					if (Path.GetDirectoryName(dialog.FileName) != dir)
					{
						MessageBox.Show("Images need to be in the character's folder.");
						invalid = true;
					}
				}
			}
			while (invalid);
			if (result == DialogResult.OK)
				file = dialog.FileName;
			return result;
		}

		private void cmdLoadTemplate_Click(object sender, EventArgs e)
		{
			if (ChooseFileInDirectory(openFileDialog1, ref _lastTemplateFile) == DialogResult.OK)
			{
				PoseTemplate template = PoseTemplate.LoadFromFile(openFileDialog1.FileName);
				if (template == null)
				{
					MessageBox.Show("Failed to read " + openFileDialog1.FileName + " as a pose template.");
				}
				LoadTemplate(template);
			}
		}

		/// <summary>
		/// Loads a template into the template tab
		/// </summary>
		/// <param name="template"></param>
		private void LoadTemplate(PoseTemplate template)
		{
			_lastTemplate = template;
			txtBaseCode.Text = template.BaseCode.Serialize();
			gridLayers.Rows.Clear();
			gridEmotions.Rows.Clear();
			foreach (StageTemplate stageCode in template.Stages)
			{
				DataGridViewRow row = gridLayers.Rows[gridLayers.Rows.Add()];
				row.Cells[1].Value = stageCode.Code;
				row.Cells[2].Value = stageCode.ExtraBlush;
				row.Cells[3].Value = stageCode.ExtraAnger;
				row.Cells[4].Value = stageCode.ExtraJuice;
			}
			foreach (var emotion in template.Emotions)
			{
				DataGridViewRow row = gridEmotions.Rows[gridEmotions.Rows.Add()];
				row.Cells[0].Value = emotion.Key;
				row.Cells[1].Value = emotion.Crop.Left;
				row.Cells[2].Value = emotion.Crop.Top;
				row.Cells[3].Value = emotion.Crop.Right;
				row.Cells[4].Value = emotion.Crop.Bottom;
				row.Cells[5].Value = emotion.Code;
			}
			RestoreLabels();
		}

		private void cmdSaveTemplate_Click(object sender, EventArgs e)
		{
			if (ChooseFileInDirectory(saveFileDialog1, ref _lastTemplateFile) == DialogResult.OK)
			{
				PoseTemplate template = CreateTemplate();
				template?.SaveToFile(saveFileDialog1.FileName);
			}
		}

		private void RestoreLabels()
		{
			for (int layer = 0; layer < _character.Layers + Clothing.ExtraStages && layer < gridLayers.Rows.Count; layer++)
			{
				StageName name = _character.LayerToStageName(layer);
				string label = name?.DisplayName ?? "Unknown";
				DataGridViewRow row = gridLayers.Rows[layer];
				row.Cells[0].Value = label;
			}
		}

		/// <summary>
		/// Creates a pose template from the template tab fields
		/// </summary>
		/// <returns></returns>
		private PoseTemplate CreateTemplate()
		{
			PoseTemplate template = new PoseTemplate();
			template.BaseCode = new KisekaeCode(txtBaseCode.Text);
			if (string.IsNullOrWhiteSpace(txtBaseCode.Text))
			{
				MessageBox.Show("You must supply a base code.", "Generating Template");
				return null;
			}
			foreach (DataGridViewRow row in gridLayers.Rows)
			{
				StageTemplate stageTemplate = GetStageTemplateFromRow(row);
				if (stageTemplate == null)
					continue;
				template.Stages.Add(stageTemplate);
			}
			foreach (DataGridViewRow row in gridEmotions.Rows)
			{
				Emotion emotion = GetEmotionFromRow(row);
				if (emotion == null)
					continue;
				template.Emotions.Add(emotion);
			}
			return template;
		}

		private int GetIntValue(object value)
		{
			if (value == null)
				return 0;
			int result;
			int.TryParse(value.ToString(), out result);
			return result;
		}

		private StageTemplate GetStageTemplateFromRow(DataGridViewRow row)
		{
			if (row == null)
				return null;
			string key = row.Cells[0].Value?.ToString();
			string value = row.Cells[1].Value?.ToString();
			if (string.IsNullOrEmpty(key))
				return null;
			int blush = GetIntValue(row.Cells[2].Value);
			int anger = GetIntValue(row.Cells[3].Value);
			int juice = GetIntValue(row.Cells[4].Value);
			StageTemplate stageTemplate = new StageTemplate();
			stageTemplate.ExtraBlush = blush;
			stageTemplate.ExtraAnger = anger;
			stageTemplate.ExtraJuice = juice;
			stageTemplate.Code = value;
			return stageTemplate;
		}

		private Emotion GetEmotionFromRow(DataGridViewRow row)
		{
			if (row == null)
				return null;
			string key = row.Cells[0].Value?.ToString();
			string value = row.Cells[5].Value?.ToString();
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
				return null;
			string left = row.Cells[1].Value?.ToString() ?? "0";
			string top = row.Cells[2].Value?.ToString() ?? "0";
			string right = row.Cells[3].Value?.ToString() ?? "600";
			string bottom = row.Cells[4].Value?.ToString() ?? "1400";
			Emotion emotion = new Emotion(key, value, left, top, right, bottom);
			return emotion;
		}

		private void cmdGenerate_Click(object sender, EventArgs e)
		{
			PoseTemplate template = CreateTemplate();
			if (template == null)
			{
				return;
			}
			PoseList list = template.GeneratePoseList();
			Shell.Instance.Launch<Character, PoseListEditor>(_character, list);
		}

		private async void cmdPreviewPose_Click(object sender, EventArgs e)
		{
			KisekaeCode baseCode = new KisekaeCode(txtBaseCode.Text, true);
			DataGridViewRow stageRow = null;
			DataGridViewRow emotionRow = null;
			if (gridLayers.Rows.Count == 0 || gridEmotions.Rows.Count == 0)
				return;
			if (gridLayers.SelectedCells.Count > 0)
				stageRow = gridLayers.SelectedCells[0].OwningRow;
			else stageRow = gridLayers.Rows[0];

			if (gridEmotions.SelectedCells.Count > 0)
				emotionRow = gridEmotions.SelectedCells[0].OwningRow;
			else emotionRow = gridEmotions.Rows[0];

			StageTemplate stage = GetStageTemplateFromRow(stageRow);
			Emotion emotion = GetEmotionFromRow(emotionRow);
			if (stage == null)
			{
				MessageBox.Show("No clothing data has been defined yet.", "Preview");
				return;
			}
			if (emotion == null)
			{
				MessageBox.Show("No pose has been defined yet.", "Preview");
				return;
			}

			Cursor.Current = Cursors.WaitCursor;
			Enabled = false;
			KisekaeCode code = PoseTemplate.CreatePose(baseCode, stage, emotion);
			Rect cropInfo = new Rect(0, 0, 600, 1400);
			Image img = await CharacterGenerator.GetCroppedImage(code, cropInfo, _character);
			Enabled = true;
			if (img != null)
			{
				CharacterImage preview = _imageLibrary.Replace(ImageLibrary.PreviewImage, img);
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, preview);
			}
			else
			{
				MessageBox.Show("Preview generation failed. Is Kisekae running?");
			}
			Cursor.Current = Cursors.Default;
		}
	}
}
