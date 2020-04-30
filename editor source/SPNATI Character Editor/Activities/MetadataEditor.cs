using Desktop;
using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 0)]
	public partial class MetadataEditor : Activity
	{
		private bool _populatingImages;
		private Character _character;

		public MetadataEditor()
		{
			InitializeComponent();

			cboGender.Items.AddRange(new string[] { "female", "male" });
			cboSize.Items.AddRange(new string[] { "small", "medium", "large" });
			ColDifficulty.Items.AddRange(DialogueLine.AILevels);
		}

		public override string Caption
		{
			get { return "Metadata"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
		}

		protected override void OnFirstActivate()
		{
			txtFirstName.Text = _character.FirstName;
			txtLastName.Text = _character.LastName;
			cboSize.SelectedItem = _character.Size;
			cboGender.SelectedItem = _character.Gender;
			valRounds.Value = _character.Stamina;
			txtDescription.Text = _character.Metadata.Description;
			txtHeight.Text = _character.Metadata.Height;
			txtSource.Text = _character.Metadata.Source;
			txtWriter.Text = _character.Metadata.Writer;
			txtArtist.Text = _character.Metadata.Artist;
			PopulatePortraitDropdown();
			if (_character.Metadata.Portrait != null)
			{
				string portrait = _character.Metadata.Portrait;
				PoseMapping pose = _character.PoseLibrary.GetPose(portrait);
				cboDefaultPic.SelectedItem = pose;
			}
			gridAI.Data = _character.Intelligence;
		}

		/// <summary>
		/// Populates the default portrait dropdown menu
		/// </summary>
		private void PopulatePortraitDropdown()
		{
			_populatingImages = true;
			List<PoseMapping> poses = _character.PoseLibrary.GetPoses(0);
			cboDefaultPic.DisplayMember = "DisplayName";
			cboDefaultPic.DataSource = poses;
			_populatingImages = false;
		}

		protected override void OnActivate()
		{
			txtLabel.Text = _character.Label;

			PoseMapping image = _character.PoseLibrary.GetPose(_character.Metadata.Portrait);
			if (image == null)
				return;
			_character.Metadata.Portrait = image.Key.Replace("#-", "0-");
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, image, 0));
		}

		public override void Save()
		{
			_character.Label = txtLabel.Text;
			_character.FirstName = txtFirstName.Text;
			_character.LastName = txtLastName.Text;
			_character.Stamina = (int)valRounds.Value;
			_character.Gender = cboGender.SelectedItem.ToString();
			_character.Size = cboSize.SelectedItem.ToString();
			_character.Metadata.Description = txtDescription.Text;
			_character.Metadata.Height = txtHeight.Text;
			_character.Metadata.Source = txtSource.Text;
			_character.Metadata.Writer = txtWriter.Text;
			_character.Metadata.Artist = txtArtist.Text;
			gridAI.Save(ColAIStage);
		}

		private void cboDefaultPic_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_populatingImages)
				return;
			PoseMapping image = cboDefaultPic.SelectedItem as PoseMapping;
			if (image == null)
				return;
			_character.Metadata.Portrait = image.Key.Replace("#-", "0-");
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, image, 0));
		}

		private void cboGender_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string gender = cboGender.SelectedItem?.ToString();
			if (gender == "male")
			{
				lblSize.Text = "Crotch:";
			}
			else
			{
				lblSize.Text = "Chest:";
			}
		}
	}
}
