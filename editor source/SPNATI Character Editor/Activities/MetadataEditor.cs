using Desktop;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 0)]
	public partial class MetadataEditor : Activity
	{
		private bool _populatingImages;
		private Character _character;
		private ImageLibrary _imageLibrary;

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
			_imageLibrary = ImageLibrary.Get(_character);
			Config.Set(Settings.LastCharacter, _character.FolderName);
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
				string portrait = _character.Metadata.Portrait.Replace("custom:", "@@@");
				portrait = Path.GetFileNameWithoutExtension(portrait);
				portrait = portrait.Replace("@@@", "custom:");
				cboDefaultPic.SelectedItem = _imageLibrary.Find(portrait);
			}
			gridAI.Data = _character.Intelligence;

			OpponentStatus status = Listing.Instance.GetCharacterStatus(_character.FolderName);
			lblIncomplete.Visible = (status == OpponentStatus.Incomplete);
			lblOffline.Visible = (status == OpponentStatus.Offline);
			lblTesting.Visible = (status == OpponentStatus.Testing);
			lblUnlisted.Visible = (status == OpponentStatus.Unlisted);
		}

		/// <summary>
		/// Populates the default portrait dropdown menu
		/// </summary>
		private void PopulatePortraitDropdown()
		{
			_populatingImages = true;
			List<CharacterImage> images = new List<CharacterImage>();
			images.Add(new CharacterImage(" ", null));
			images.AddRange(_imageLibrary.GetImages(0));
			if (Config.UsePrefixlessImages)
			{
				foreach (CharacterImage img in _imageLibrary.GetImages(-1))
				{
					if (!_imageLibrary.FilterImage(_character, img.Name))
					{
						images.Add(img);
					}
				}
			}
			cboDefaultPic.DataSource = images;
			_populatingImages = false;
		}

		protected override void OnActivate()
		{
			txtLabel.Text = _character.Label;
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, _imageLibrary.Find(_character.Metadata.Portrait));
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

		private void cboDefaultPic_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_populatingImages)
				return;
			CharacterImage image = cboDefaultPic.SelectedItem as CharacterImage;
			if (image == null)
				return;
			_character.Metadata.Portrait = image.Name + image.FileExtension;
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, _imageLibrary.Find(_character.Metadata.Portrait));
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
