using Desktop;
using Desktop.Messaging;
using SPNATI_Character_Editor.EpilogueEditing;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class CharacterImageBox : UserControl
	{
		private const int ScreenMargin = 5;
		private const float TextPercent = 0.2f;
		private const int TextMargin = 2;
		private const int TextBorder = 2;
		private const int TextPadding = 10;
		private const float TextBuffer = 0.1f; //90% height of textbox row
		private const string FontName = "Trebuchet MS";
		private const int ArrowSize = 15;

		private CharacterImage _image;
		private Mailbox _mailbox;
		private Image _imageReference;
		private PosePreview _imagePose;
		private bool _animating;
		private string _text = null;

		private DateTime _lastTick;

		private Font _textFont;
		private Pen _textBorder;

		public Matrix SceneTransform;

		public CharacterImageBox()
		{
			InitializeComponent();

			if (Shell.Instance != null)
			{
				_mailbox = Shell.Instance.PostOffice.GetMailbox();
				_mailbox.Subscribe<ImageReplacementArgs>(DesktopMessages.ReplaceImage, OnReplaceImage);
			}

			_textBorder = new Pen(Color.Black, TextBorder);
			UpdateFont();
		}

		private bool _showText;
		public bool ShowTextBox
		{
			get { return _showText; }
			set
			{
				_showText = value;
				canvas.Invalidate();
			}
		}

		private void UpdateFont()
		{
			_textFont?.Dispose();

			int screenWidth = (int)(canvas.Height * 1.33f);

			float size = 14 * (screenWidth / 1000f);
			_textFont = new Font("Trebuchet MS", size);
		}

		private void UpdateSceneTransform()
		{
			SceneTransform = new Matrix();
			int screenHeight = canvas.Height - ScreenMargin * 2;
			int availableHeight = ShowTextBox ? (int)(screenHeight * (1 - TextPercent)) : (int)(screenHeight * 0.9f);
			float screenScale = availableHeight / (_imagePose == null ? 1400 : _imagePose.BaseHeight);
			SceneTransform.Scale(screenScale, screenScale, MatrixOrder.Append); // scale to display
			SceneTransform.Translate(canvas.Width * 0.5f, screenHeight - availableHeight, MatrixOrder.Append); // center horizontally
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

		public void SetText(string text)
		{
			_text = text;
			canvas.Invalidate();
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
			UpdateSceneTransform();
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
					_imagePose = new PosePreview(image.Skin, image.GetPose(), null, null, null, null, true);
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
					_imagePose = new PosePreview(_image.Skin, _image.GetPose(), null, null, null, null, true);
				}
				_imageReference = args.NewImage;
			}
		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			//text box
			int screenHeight = canvas.Height - ScreenMargin * 2;
			
			if (_imagePose != null)
			{
				_imagePose.Draw(g, SceneTransform);
			}
			else if (_imageReference != null)
			{
				ImageAnimator.UpdateFrames();

				//scale to the height
				float availableHeight = ShowTextBox ? screenHeight * (1 - TextPercent) : screenHeight * 0.9f;
				int height = (int)availableHeight;
				int width = (int)(_imageReference.Width / (float)_imageReference.Height * availableHeight);
				g.DrawImage(_imageReference, canvas.Width / 2 - width / 2, screenHeight - availableHeight + ScreenMargin, width, availableHeight);
			}

			if (_showText && !string.IsNullOrEmpty(_text))
			{

				int textboxHeight = (int)(screenHeight * TextPercent);
				int topPadding = (int)(textboxHeight * TextBuffer);
				textboxHeight -= topPadding;

				RectangleF bounds = new RectangleF(TextMargin + TextBorder + TextPadding,
								topPadding + TextBorder + TextPadding,
								canvas.Width - TextMargin * 2 - TextPadding * 2,
								textboxHeight - TextBorder * 2 - TextPadding * 2);
				StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
				SizeF size = g.MeasureString(_text, _textFont, (int)bounds.Width, sf);
				if (size.Height > bounds.Height)
				{
					textboxHeight += (int)(size.Height - bounds.Height);
				}
				bounds.Height = Math.Max(size.Height, bounds.Height);

				g.FillRectangle(Brushes.White, TextMargin, topPadding, canvas.Width - TextMargin * 2, textboxHeight);
				g.DrawString(_text, _textFont, Brushes.Black, bounds, sf);
				g.DrawRectangle(_textBorder, TextMargin, topPadding, canvas.Width - TextMargin * 2, textboxHeight);
				Point[] triangle = new Point[] {
					new Point(canvas.Width / 2 - ArrowSize, topPadding  + textboxHeight - 1),
					new Point(canvas.Width / 2 + ArrowSize, topPadding  + textboxHeight - 1),
					new Point(canvas.Width / 2, topPadding + textboxHeight + ArrowSize - 1),
				};
				g.FillPolygon(Brushes.White, triangle);
				g.DrawLine(_textBorder, triangle[0], triangle[2]);
				g.DrawLine(_textBorder, triangle[1], triangle[2]);
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

		private void CharacterImageBox_Resize(object sender, EventArgs e)
		{
			UpdateFont();
			UpdateSceneTransform();
		}
	}
}
