using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class MarkerOptions : UserControl, IDialogueDropDownControl
	{
		public int RowIndex { get; private set; }

		public SkinnedBackgroundType PanelType
		{
			get { return SkinnedBackgroundType.Background; }
		}

		private DialogueLine _line;

		public event EventHandler DataUpdated
		{
			add { }
			remove { }
		}

		public MarkerOptions()
		{
			InitializeComponent();
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void OnUpdateSkin(Skin skin)
		{
			BackColor = skin.Background.Normal;
			foreach (Control child in Controls)
			{
				SkinManager.Instance.ReskinControl(child, skin);
			}
			Invalidate(true);
		}

		public void SetData(int rowIndex, DialogueLine line, Character character)
		{
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
			RowIndex = rowIndex;
			_line = line;

			List<MarkerOperation> markers = new List<MarkerOperation>();
			if (!string.IsNullOrEmpty(line.Marker))
			{
				//move the marker into the list so there aren't two places to edit
				MarkerOperation marker = new MarkerOperation(line.Marker);
				markers.Add(marker);
			}
			markers.AddRange(_line.Markers);
			gridMarkers.SetMarkers(markers, character);
		}

		public DialogueLine GetLine()
		{
			List<MarkerOperation> markers = gridMarkers.GetMarkers();
			if (markers.Count > 0)
			{
				//move the first marker into the legacy attribute
				MarkerOperation marker = markers[0];
				markers.RemoveAt(0);
				_line.Marker = marker.ToString();
			}
			else
			{
				_line.Marker = null;
			}
			_line.Markers = markers;

			return _line;
		}
	}
}
