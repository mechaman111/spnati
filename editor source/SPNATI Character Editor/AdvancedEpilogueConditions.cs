using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class AdvancedEpilogueConditions : Form
	{
		private Character _character;
		private Epilogue _epilogue;

		public AdvancedEpilogueConditions(Character character, Epilogue epilogue)
		{
			InitializeComponent();
			_character = character;
			_epilogue = epilogue;
		}

		private void AdvancedEpilogueConditions_Load(object sender, EventArgs e)
		{
			GUIHelper.SetRange(valPlayerStartingLayers, valMaxPlayerStartingLayers, _epilogue.PlayerStartingLayers);

			selAllMarkers.SelectableItems
				= selNotMarkers.SelectableItems
				= selAnyMarkers.SelectableItems
				= _character.Markers.Values.ToList().ConvertAll(m => m.Name).ToArray();
			selAllMarkers.SelectedItems = _epilogue.AllMarkers?.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
			selNotMarkers.SelectedItems = _epilogue.NotMarkers?.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
			selAnyMarkers.SelectedItems = _epilogue.AnyMarkers?.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

			HashSet<string> otherMarkers = new HashSet<string>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				if (c != _character)
				{
					cboAlsoPlaying.Items.Add(c.DisplayName);
					foreach (Marker m in c.Markers.Values)
					{
						if (m.Scope == MarkerScope.Public) {
							otherMarkers.Add(m.Name);
						}
					}
				}
			}
			selAlsoPlayingAllMarkers.SelectableItems
				= selAlsoPlayingNotMarkers.SelectableItems
				= selAlsoPlayingAnyMarkers.SelectableItems
				= otherMarkers.ToArray();
			selAlsoPlayingAllMarkers.SelectedItems = _epilogue.AlsoPlayingAllMarkers?.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
			selAlsoPlayingNotMarkers.SelectedItems = _epilogue.AlsoPlayingNotMarkers?.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
			selAlsoPlayingAnyMarkers.SelectedItems = _epilogue.AlsoPlayingAnyMarkers?.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

			cboAlsoPlaying.Text = _epilogue.AlsoPlaying;
			valGalleryImage.Text = _epilogue.GalleryImage;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			_epilogue.AlsoPlaying = (string)cboAlsoPlaying.SelectedItem;
			_epilogue.PlayerStartingLayers = GUIHelper.ReadRange(valPlayerStartingLayers, valMaxPlayerStartingLayers);
			_epilogue.AllMarkers = selAllMarkers.SelectedItems.Length > 0 ? String.Join(" ", selAllMarkers.SelectedItems) : null;
			_epilogue.NotMarkers = selNotMarkers.SelectedItems.Length > 0 ? String.Join(" ", selNotMarkers.SelectedItems) : null;
			_epilogue.AnyMarkers = selAnyMarkers.SelectedItems.Length > 0 ? String.Join(" ", selAnyMarkers.SelectedItems) : null;
			_epilogue.AlsoPlayingAllMarkers = selAlsoPlayingAllMarkers.SelectedItems.Length > 0 ? String.Join(" ", selAlsoPlayingAllMarkers.SelectedItems) : null;
			_epilogue.AlsoPlayingNotMarkers = selAlsoPlayingNotMarkers.SelectedItems.Length > 0 ? String.Join(" ", selAlsoPlayingNotMarkers.SelectedItems) : null;
			_epilogue.AlsoPlayingAnyMarkers = selAlsoPlayingAnyMarkers.SelectedItems.Length > 0 ? String.Join(" ", selAlsoPlayingAnyMarkers.SelectedItems) : null;
			_epilogue.GalleryImage = valGalleryImage.Text;
		}

		private void markerControl_Enter(object sender, EventArgs e)
		{
			this.AcceptButton = ((Controls.SelectBox)sender).AddButton;
		}

		private void markerControl_Leave(object sender, EventArgs e)
		{
			this.AcceptButton = cmdOK;
		}

		private void valPlayerStartingLayers_ValueChanged(object sender, EventArgs e)
		{
			valMaxPlayerStartingLayers.Minimum = valPlayerStartingLayers.Value;

		}

		private void valMaxPlayerStartingLayers_ValueChanged(object sender, EventArgs e)
		{
			valPlayerStartingLayers.Maximum = valMaxPlayerStartingLayers.Value;
		}

		private void GalleryImageSelectBtn_Click(object sender, EventArgs e)
		{
			string dir = Config.GetRootDirectory(_character);
			galleryImageFileDialog.InitialDirectory = dir;
			DialogResult result = DialogResult.OK;
			bool invalid;
			do
			{
				invalid = false;
				result = galleryImageFileDialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					if (Path.GetDirectoryName(galleryImageFileDialog.FileName) != dir)
					{
						MessageBox.Show("Images need to come from the character's folder.");
						invalid = true;
					}
				}
			}
			while (invalid);

			if (result == DialogResult.OK)
			{
				string file = Path.GetFileName(galleryImageFileDialog.FileName);
				valGalleryImage.Text = file;
			}
		}
	}
}
