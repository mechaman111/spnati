using Desktop;
using Desktop.Messaging;
using Desktop.Skinning;
using SPNATI_Character_Editor.EpilogueEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class CharacterImageBox : UserControl, ISkinControl
	{
		private const int ScreenMargin = 5;
		private const float TextPercent = 0.2f;
		private const int TextMargin = 2;
		private const int TextBorder = 2;
		private const int TextPadding = 10;
		private const float TextBuffer = 0.1f; //90% height of textbox row
		private const string FontName = "Trebuchet MS";
		private const int ArrowSize = 15;

		private float _time;
		private Mailbox _mailbox;
		private Image _singleUseImage;
		private Image _imageReference;
		private bool _animating;
		private DialogueLine _line = null;
		private string _text = null;
		private List<Word> _words = null;
		private float _percent = 0.5f;
		private List<string> _markers = new List<string>();

		private ISkin _character;

		private PoseMapping _currentPose;
		private int _currentStage;
		private ImageReference _reference;

		private DateTime _lastTick;

		private Font _textFont;
		private Font _italicFont;
		private Pen _textBorder;

		public Matrix SceneTransform;
		public LivePose Pose;
		public bool AutoPlayback = true;

		public CharacterImageBox()
		{
			InitializeComponent();

			if (Shell.Instance != null)
			{
				_mailbox = Shell.Instance.PostOffice.GetMailbox();
				_mailbox.Subscribe(DesktopMessages.ToggleImages, OnToggleImages);
			}

			_textBorder = new Pen(Color.Black, TextBorder);
			UpdateFont();
		}

		public void SetCharacter(ISkin character)
		{
			if (_character != character)
			{
				_currentPose = null;
				if (_character != null && _character is INotifyPropertyChanged)
				{
					((INotifyPropertyChanged)_character).PropertyChanged -= Character_PropertyChanged;
				}
				_character = character;
				if (_character != null && _character is INotifyPropertyChanged)
				{
					((INotifyPropertyChanged)_character).PropertyChanged += Character_PropertyChanged;
				}
			}
		}

		private void Character_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "CurrentSkin")
			{
				PoseMapping pose = _currentPose;
				if (pose != null)
				{
					_currentPose = null;
					SetImage(pose, _currentStage);
				}
			}
		}

		public bool ShowTextBox
		{
			get
			{
				return Config.GetBoolean(Settings.ShowPreviewText);
			}
		}

		private void OnToggleImages()
		{
			UpdateSceneTransform();
			canvas.Invalidate();
		}

		private void UpdateFont()
		{
			_textFont?.Dispose();
			_italicFont?.Dispose();

			int screenWidth = (int)(canvas.Height * 1.33f);

			const float BaseSize = 12;
			float size = BaseSize * (screenWidth / 1000f);
			_textFont = new Font("Trebuchet MS", size == 0 ? BaseSize : size);
			_italicFont = new Font(_textFont, FontStyle.Italic);
		}

		private void UpdateSceneTransform()
		{
			SceneTransform = new Matrix();
			int screenHeight = canvas.Height - ScreenMargin * 2;
			int availableHeight = ShowTextBox ? (int)(screenHeight * (1 - TextPercent)) : (int)(screenHeight * 0.9f);
			float screenScale = availableHeight / (Pose == null ? 1400.0f : Pose.BaseHeight);
			SceneTransform.Scale(screenScale, screenScale, MatrixOrder.Append); // scale to display
			SceneTransform.Translate(canvas.Width * 0.5f, screenHeight - availableHeight, MatrixOrder.Append); // center horizontally
		}

		public void Destroy()
		{
			if (_imageReference != null && _animating)
			{
				ImageAnimator.StopAnimate(_imageReference, OnFrameChanged);
			}

			if (_reference != null)
			{
				ImageCache.Release(_reference.FileName);
				_reference = null;
			}

			if (_singleUseImage != null)
			{
				_singleUseImage.Dispose();
				_singleUseImage = null;
			}
			Pose = null;
		}

		public void SetText(DialogueLine line)
		{
			_line = line;
			if (line == null || line.Text == null)
			{
				_text = null;
				_words = null;
			}
			else
			{
				_text = line.Text;
				_words = GUIHelper.ParseWords(_text);
				_percent = 0.5f;
				if (!string.IsNullOrEmpty(line.Location) && line.Location.EndsWith("%"))
				{
					int percent;
					if (int.TryParse(line.Location.Substring(0, line.Location.Length - 1), out percent))
					{
						_percent = percent / 100.0f;
					}
				}
			}
			canvas.Invalidate();
		}

		public void SetMarkers(List<string> markers)
		{
			_markers = markers;
		}

		public void SetImage(Image image, bool disposeImage = true)
		{
			Destroy();
			tmrTick.Stop();
			_currentPose = null;
			_currentStage = -1;
			if (disposeImage)
			{
				_singleUseImage = image;
			}
			_imageReference = image;
			canvas.Invalidate();
		}

		public void SetImage(PoseMapping pose, int stage)
		{
			if (_currentPose == pose && _currentStage == stage && _imageReference != null)
			{
				return;
			}
			Destroy();

			UpdateSceneTransform();
			tmrTick.Stop();
			_currentPose = pose;
			_currentStage = stage;
			if (pose != null)
			{
				PoseReference poseRef = pose.GetPose(stage);
				if (poseRef != null)
				{
					if (poseRef.Pose == null)
					{
						string file = Path.Combine(_character.Skin.GetDirectory(), poseRef.FileName);
						if (!File.Exists(file))
						{
							file = Path.Combine(_character.GetDirectory(), poseRef.FileName);
						}
						_reference = ImageCache.Get(file);
						_imageReference = _reference?.Image;
						if (ImageAnimator.CanAnimate(_imageReference))
						{
							_animating = true;
							ImageAnimator.Animate(_imageReference, OnFrameChanged);
						}
					}
					else
					{
						Pose p = _character.Skin.CustomPoses.Find(cp => cp.Id == poseRef.Pose.Id);
						if (p == null)
						{
							p = poseRef.Pose;
						}
						Pose = new LivePose(_character, p, _currentStage);
						if (AutoPlayback)
						{
							_time = 0;
							_lastTick = DateTime.Now;
							tmrTick.Enabled = true;
						}
					}
				}
				else
				{
					_imageReference = null;
				}
			}
			else
			{
				_imageReference = null;
			}
			canvas.Invalidate();
		}

		private void OnFrameChanged(object sender, EventArgs e)
		{
			canvas.Invalidate();
		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
			if (Config.GetBoolean(Settings.HideImages))
				return;

			Graphics g = e.Graphics;

			int screenHeight = canvas.Height - ScreenMargin * 2;
			if (_line == null || _line.Layer != "over")
			{
				DrawSpeechBubble(g, screenHeight);
			}

			if (Pose != null)
			{
				foreach (LiveSprite sprite in Pose.DrawingOrder)
				{
					sprite.Draw(g, SceneTransform, _markers, true);
				}
			}
			else if (_imageReference != null)
			{
				ImageAnimator.UpdateFrames();

				//scale to the height
				float availableHeight = ShowTextBox ? screenHeight * (1 - TextPercent) : screenHeight * 0.9f;
				int width = (int)(_imageReference.Width / (float)_imageReference.Height * availableHeight);
				g.DrawImage(_imageReference, canvas.Width / 2 - width / 2, screenHeight - availableHeight + ScreenMargin, width, availableHeight);
			}

			if (_line != null && _line.Layer == "over")
			{
				DrawSpeechBubble(g, screenHeight);
			}
		}

		private void DrawSpeechBubble(Graphics g, int screenHeight)
		{
			bool showText = Config.GetBoolean(Settings.ShowPreviewText);
			if (showText && !string.IsNullOrEmpty(_text))
			{
				bool formatText = !Config.GetBoolean(Settings.DisablePreviewFormatting);
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

				//group words into lines
				List<List<Word>> lines = new List<List<Word>>();
				List<Word> line = new List<Word>();
				float remainingWidth = bounds.Width;
				bool italics = false;
				float height = 0;
				for (int i = 0; i < _words.Count; i++)
				{
					Word word = _words[i];
					if (!string.IsNullOrEmpty(word.Text))
					{
						string text = word.Text;
						Font font = (italics ? _italicFont : _textFont);
						SizeF textSize = g.MeasureString(text, font);
						word.Width = textSize.Width;
						height = Math.Max(textSize.Height, height);

						if (word.Width >= remainingWidth)
						{
							//need a new line
							if (line.Count > 0)
							{
								lines.Add(line);
							}
							line = new List<Word>();
							remainingWidth = bounds.Width;
						}

						line.Add(word);
						remainingWidth -= word.Width;
					}
					else
					{
						switch (word.Formatter)
						{
							case FormatMarker.ItalicOn:
								italics = true;
								line.Add(word);
								break;
							case FormatMarker.ItalicOff:
								italics = false;
								line.Add(word);
								break;
							case FormatMarker.LineBreak:
								if (line.Count > 0)
								{
									lines.Add(line);
								}
								line = new List<Word>();
								remainingWidth = bounds.Width;
								break;
						}
					}
				}
				if (line.Count > 0)
				{
					lines.Add(line);
				}

				const int TopOffset = 4;
				using (SolidBrush br = new SolidBrush(SkinManager.Instance.CurrentSkin.FieldBackColor))
				{
					g.FillRectangle(br, TextMargin, topPadding + TopOffset, canvas.Width - TextMargin * 2, textboxHeight - TopOffset);

					if (formatText)
					{
						using (SolidBrush fr = new SolidBrush(SkinManager.Instance.CurrentSkin.Surface.ForeColor))
						{
							//print each line
							float requiredHeight = height * lines.Count;
							float lineY = bounds.Y + bounds.Height / 2 - requiredHeight / 2;
							italics = false;
							foreach (List<Word> workingLine in lines)
							{
								float totalWidth = workingLine.Sum(w => w.Width);
								float lineX = bounds.X + bounds.Width / 2 - totalWidth / 2;
								for (int j = 0; j < workingLine.Count; j++)
								{
									Word word = workingLine[j];
									switch (word.Formatter)
									{
										case FormatMarker.ItalicOn:
											italics = true;
											break;
										case FormatMarker.ItalicOff:
											italics = false;
											break;
										case FormatMarker.None:
											string text = word.Text;
											float width = word.Width;
											Font font = (italics ? _italicFont : _textFont);
											g.DrawString(text, font, fr, lineX, lineY);
											lineX += width;
											break;
									}

								}
								lineY += height;
							}
						}
					}
					else
					{
						using (SolidBrush fr = new SolidBrush(SkinManager.Instance.CurrentSkin.Surface.ForeColor))
						{
							g.DrawString(_text, _textFont, fr, bounds, sf);
						}
					}

					g.DrawRectangle(_textBorder, TextMargin, topPadding + TopOffset, canvas.Width - TextMargin * 2, textboxHeight - TopOffset);
					if (_line.Direction != "none")
					{
						Point[] triangle = new Point[] {
						new Point((int)(canvas.Width * _percent) - ArrowSize, topPadding  + textboxHeight - 1),
						new Point((int)(canvas.Width * _percent) + ArrowSize, topPadding  + textboxHeight - 1),
						new Point((int)(canvas.Width * _percent), topPadding + textboxHeight + ArrowSize - 1),
					};
						g.FillPolygon(br, triangle);
						g.DrawLine(_textBorder, triangle[0], triangle[2]);
						g.DrawLine(_textBorder, triangle[1], triangle[2]);
					}
				}
			}
		}

		/// <summary>
		/// Converts what's currently visible into an image
		/// </summary>
		/// <returns></returns>
		public Bitmap GetImage()
		{
			Bitmap bmp = new Bitmap(canvas.Width, canvas.Height);
			canvas.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
			return bmp;
		}

		public void SetTime(float time)
		{
			_time = time;
			Pose.UpdateTime(_time, _time, true);
			canvas.Invalidate();
			canvas.Update();
		}

		private void tmrTick_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			TimeSpan elapsed = now - _lastTick;
			float elapsedSec = (float)elapsed.TotalSeconds;
			_lastTick = now;
			_time += elapsedSec;

			if (Pose == null)
			{
				tmrTick.Enabled = false;
				return;
			}

			Pose.UpdateTime(_time, _time, true);
			canvas.Invalidate();
		}

		private void CharacterImageBox_Resize(object sender, EventArgs e)
		{
			UpdateFont();
			UpdateSceneTransform();
		}

		public void OnUpdateSkin(Skin skin)
		{
			canvas.BackColor = skin.Background.Normal;
		}
	}
}
