using Desktop;
using Desktop.Messaging;
using SPNATI_Character_Editor.EpilogueEditing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class CharacterImageBox : UserControl
	{
		private CharacterImage _image;
		private Mailbox _mailbox;
		private Image _imageReference;
		private PosePreview _imagePose;
		private bool _animating;

		private DateTime _lastTick;
		
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
				if (_image.GetPose() != null)
				{
					_imagePose = null;
				}
				else
				{
					_image.ReleaseImage();
				}
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
			_imagePose = null;
			tmrTick.Stop();
			if (image == null)
			{
				_imageReference = null;
			}
			else
			{
				if (image.GetPose() != null)
				{
					_imagePose = new PosePreview(image.Skin, image.GetPose(), null, null, null, null);
					_lastTick = DateTime.Now;
					tmrTick.Enabled = _imagePose.IsAnimated;
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
				if (_image.GetPose() != null)
				{
					_imagePose?.Dispose();
					_imagePose = new PosePreview(_image.Skin, _image.GetPose(), null, null, null, null);
				}
				_imageReference = args.NewImage;
			}
		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (_imagePose != null)
			{
				_imagePose.Draw(g, canvas.Width, canvas.Height, new Point(0, 0));
			}
			else if (_imageReference != null)
			{
				ImageAnimator.UpdateFrames();

				//scale to the height
				int height = canvas.Height;
				int width = (int)(_imageReference.Width / (float)_imageReference.Height * canvas.Height);
				g.DrawImage(_imageReference, canvas.Width / 2 - width / 2, 0, width, height);
			}
		}

		private void tmrTick_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			TimeSpan elapsed = now - _lastTick;
			float elapsedSec = (float)elapsed.TotalSeconds;
			_lastTick = now;

			if (_imagePose == null)
			{
				tmrTick.Enabled = false;
				return;
			}

			_imagePose.Update(elapsedSec);
			canvas.Invalidate();
		}
	}
}
