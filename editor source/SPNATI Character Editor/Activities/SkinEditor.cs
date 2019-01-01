using Desktop;
using System;
using System.Collections.Generic;
using System.IO;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Costume), 0)]
	public partial class SkinEditor : Activity
	{
		private bool _linkDataChanged = false;
		private Costume _costume;
		private ImageLibrary _imageLibrary;
		private bool _populatingImages;

		public SkinEditor()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "General"; }
		}

		protected override void OnInitialize()
		{
			_costume = Record as Costume;
			SubscribeWorkspace<bool>(WorkspaceMessages.Save, OnSaveWorkspace);
			_imageLibrary = ImageLibrary.Get(_costume);
		}

		private void OnSaveWorkspace(bool auto)
		{
			if (!auto)
			{
				Save();
				Serialization.ExportSkin(_costume);
				if (Serialization.ExportSkin(_costume))
				{
					Shell.Instance.SetStatus(string.Format("{0} exported successfully at {1}.", _costume, DateTime.Now.ToShortTimeString()));
				}
				else
				{
					Shell.Instance.SetStatus(string.Format("{0} failed to export.", _costume));
				}
			}
		}

		protected override void OnFirstActivate()
		{
			SkinLink link = _costume.Link;
			if (link != null)
			{
				txtName.Text = link.Name;
			}

			cboBaseStage.Items.Add("- None -");
			for (int i = 0; i < _costume.Layers + Clothing.ExtraStages; i++)
			{
				cboBaseStage.Items.Add(_costume.Character.LayerToStageName(i, _costume));
			}

			//if anyone tries to get fancy by linking to multiple folders instead of just the reskin and the base, sorry, but we're not handling it for now
			string baseFolder = $"opponents/{_costume.Character.FolderName}/";
			StageSpecificValue baseStage = _costume.Folders.Find(f => f.Value == baseFolder);
			if (baseStage != null)
			{
				cboBaseStage.SelectedIndex = baseStage.Stage + 1;
			}
			else
			{
				cboBaseStage.SelectedIndex = -1;
			}

			gridLabels.Set(_costume.Labels);

			PopulatePortraitDropdown();
			cboDefaultPic.SelectedItem = _imageLibrary.Find(Path.GetFileNameWithoutExtension(_costume.Link?.PreviewImage));
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
			images.AddRange(_imageLibrary.GetImages(-1));
			cboDefaultPic.DataSource = images;
			_populatingImages = false;
		}

		public override void Save()
		{
			if (_costume.Link != null)
			{
				if (txtName.Text != _costume.Link.Name)
				{
					_linkDataChanged = true;
				}
				if (_linkDataChanged)
				{
					_linkDataChanged = false;
					_costume.Link.Name = txtName.Text;
					Serialization.ExportCharacter(_costume.Character);
				}
			}

			_costume.Labels = gridLabels.Values;

			//Here's where any unexpected folders are thrown out
			string folder = _costume.Folder;
			_costume.Folders.Clear();
			_costume.Folders.Add(new StageSpecificValue(0, folder));
			int baseIndex = cboBaseStage.SelectedIndex - 1;
			if (baseIndex >= 0)
			{
				_costume.Folders.Add(new StageSpecificValue(baseIndex, $"opponents/{_costume.Character.FolderName}/"));
			}
		}

		private void cboDefaultPic_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_populatingImages)
				return;
			CharacterImage image = cboDefaultPic.SelectedItem as CharacterImage;
			if (image == null)
				return;
			if (_costume.Link != null)
			{
				_costume.Link.PreviewImage = image.Name + image.FileExtension; ;
				_linkDataChanged = true;
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, _imageLibrary.Find(_costume.Link.PreviewImage));
			}
		}
	}
}
