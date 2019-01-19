using Desktop;
using Desktop.Messaging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class CharacterImageBox : UserControl
	{
		private CharacterImage _image;
		private Mailbox _mailbox;
		private Image _imageReference;
		private bool _animating;

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
				_imageReference = null;
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
			if (_imageReference != null && _animating)
			{
				ImageAnimator.StopAnimate(_imageReference, OnFrameChanged);
			}
			_image = image;
			if (image == null)
			{
				_imageReference = null;
			}
			else
			{
				_imageReference = image.GetImage();
				if (ImageAnimator.CanAnimate(_imageReference))
				{
					_animating = true;
					ImageAnimator.Animate(_imageReference, OnFrameChanged);
				}
			}
			canvas.Invalidate();
		}

		private void OnFrameChanged(object sender, EventArgs e)
		{
			canvas.Invalidate();
		}

		private void OnReplaceImage(ImageReplacementArgs args)
		{
			if (_image == args.Reference)
			{
				canvas.Invalidate();
				_imageReference = args.NewImage;
			}
		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (_imageReference != null)
			{
				ImageAnimator.UpdateFrames();

				//scale to the height
				int height = canvas.Height;
				int width = (int)(_imageReference.Width / (float)_imageReference.Height * canvas.Height);
				g.DrawImage(_imageReference, canvas.Width / 2 - width / 2, 0, width, height);
			}
		}
	}
}
