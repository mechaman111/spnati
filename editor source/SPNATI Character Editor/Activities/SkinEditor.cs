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
		private bool _populatingImages;

		public SkinEditor()
		{
			InitializeComponent();

			cboStatus.Items.Add("");
			cboStatus.Items.Add("online");
			cboStatus.Items.Add("offline");
		}

		public override string Caption
		{
			get { return "General"; }
		}

		protected override void OnInitialize()
		{
			_costume = Record as Costume;
			SubscribeWorkspace<bool>(WorkspaceMessages.Save, OnSaveWorkspace);
		}

		private void OnSaveWorkspace(bool auto)
		{
			if (!auto)
			{
				Save();
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

		private void LinkCharacter()
		{
			Character character = RecordLookup.DoLookup(typeof(Character), "", false, _costume) as Character;
			if (character != null)
			{
				_costume.LinkCharacter(character);
			}
		}

		protected override void OnFirstActivate()
		{
			if (_costume.Character == null)
			{
				LinkCharacter();
			}

			SkinLink link = _costume.Link;
			if (link != null)
			{
				txtName.Text = link.Name;
				cboStatus.Text = link.Status;
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
			if (_costume.Link?.PreviewImage != null)
			{
				string portrait = _costume.Link.PreviewImage;
				PoseMapping pose = _costume.Character.PoseLibrary.GetPose(portrait);
				cboDefaultPic.SelectedItem = pose;
			}
		}

		/// <summary>
		/// Populates the default portrait dropdown menu
		/// </summary>
		private void PopulatePortraitDropdown()
		{
			_populatingImages = true;
			List<PoseMapping> poses = _costume.Character.PoseLibrary.GetPoses(0);
			cboDefaultPic.DisplayMember = "DisplayName";
			cboDefaultPic.DataSource = poses;
			_populatingImages = false;
		}

		public override void Save()
		{
			if (_costume.Link != null)
			{
				string status = cboStatus.Text;
				if (string.IsNullOrEmpty(status))
				{
					status = null;
				}
				if (txtName.Text != _costume.Link.Name || status != _costume.Link.Status || _costume.Link.IsDirty)
				{
					_linkDataChanged = true;
				}
				if (_linkDataChanged)
				{
					_linkDataChanged = false;
					_costume.Link.IsDirty = false;
					_costume.Link.Name = txtName.Text;
					_costume.Link.Status = status;
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

			PoseMapping image = cboDefaultPic.SelectedItem as PoseMapping;
			if (image == null)
				return;
			string newKey = image.Key.Replace("#-", "0-");
			if (_costume.Link.PreviewImage != newKey)
			{
				_costume.Link.PreviewImage = newKey;
				_costume.Link.IsDirty = true;
			}
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_costume, image, 0));
		}
	}
}
