using Desktop;
using Desktop.Messaging;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class CharacterImageBox : UserControl
	{
		private CharacterImage _image;
		private Mailbox _mailbox;

		public CharacterImageBox()
		{
			InitializeComponent();

			if (Shell.Instance != null)
			{
				_mailbox = Shell.Instance.PostOffice.GetMailbox();
				_mailbox.Subscribe<ImageReplacementArgs>(DesktopMessages.ReplaceImage, OnReplaceImage);
			}
		}

		public void Destroy()
		{
			if (_image != null)
			{
				picBox.Image = null;
				_image.ReleaseImage();
				_image = null;
			}
		}

		public void SetImage(CharacterImage image)
		{
			if (_image == image)
			{
				return;
			}
			Destroy();
			_image = image;
			if (image == null)
			{
				picBox.Image = null;
			}
			else
			{
				picBox.Image = image.GetImage();
			}
		}

		private void OnReplaceImage(ImageReplacementArgs args)
		{
			if (_image == args.Reference)
			{
				picBox.Image = args.NewImage;
			}
		}
	}
}
