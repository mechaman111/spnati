using Desktop;
using Desktop.Skinning;
using KisekaeImporter;
using KisekaeImporter.ImageImport;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Forms
{
	public partial class ImageCropper : SkinnedForm
	{
		private Image _previewImage;
		private ImageImporter _importer = new ImageImporter(false);
		private const int DefaultImageHeight = 1500;
		private RectangleF _cropBounds = new Rectangle(0, 0, 10, 10);
		private DragState _dragState = DragState.None;
		private PointF _downPoint;
		private bool _lockRect;
		private Dictionary<string, string> _extraData = new Dictionary<string, string>();
		private string _lastCode;
		private Pen _centerPen;

		public ImageCropper()
		{
			InitializeComponent();
			_centerPen = new Pen(Color.DarkGray, 1);
			_centerPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
			_centerPen.DashPattern = new float[] { 10, 10 };
		}

		public Image CroppedImage { get; private set; }

		public Rect CroppingRegion
		{
			get
			{
				Rect rect = _cropBounds.ToRect(ZoomRatio);
				rect.Left = rect.Left - ImageImporter.ImageXOffset;
				rect.Right = rect.Right - ImageImporter.ImageXOffset;
				return rect;
			}
		}

		[Flags]
		private enum DragState
		{
			None = 0,
			Left = 1,
			Right = 2,
			Top = 4,
			Bottom = 8,
			Horizontal = 16,
			Vertical = 32,
			Move = 64
		}

		private float ZoomRatio
		{
			get
			{
				return (float)previewPanel.Height / DefaultImageHeight;
			}
		}

		public async void Import(ImageMetadata metadata, ISkin character, bool lockRectSize)
		{
			cmdOK.Enabled = false;
			cmdCancel.Enabled = false;
			cmdReimport.Enabled = false;
			cmdAdvanced.Enabled = false;
			cmdCopy.Enabled = false;
			lblWait.Visible = true;
			tmrWait.Enabled = true;
			_lockRect = lockRectSize;
			float zoom = ZoomRatio;
			Rect cropBounds = metadata.CropInfo;
			_cropBounds = cropBounds.ToRectangle(zoom);
			_cropBounds.X = _cropBounds.X + ImageImporter.ImageXOffset * zoom;
			_extraData = metadata.ExtraData;
			UpdateRectBoxes();
			_previewImage = null;
			KisekaeCode code = new KisekaeCode(metadata.Data);
			_lastCode = code.Serialize();
			Image image = await CharacterGenerator.GetRawImage(code, character, metadata.ExtraData, metadata.SkipPreprocessing);
			cmdOK.Enabled = true;
			cmdCancel.Enabled = true;
			cmdReimport.Enabled = true;
			cmdAdvanced.Enabled = true;
			cmdCopy.Enabled = true;
			tmrWait.Enabled = false;
			lblWait.Visible = false;
			_previewImage = image;
			if (image == null)
			{
				FailedImport import = new FailedImport();
				import.ShowDialog();
				DialogResult = DialogResult.Cancel;
				this.Close();
			}

			if (code.TotalAssets > 1)
			{
				//panelManual.Visible = true;
			}

			previewPanel.Invalidate();
		}

		private void previewPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int screenHeight = previewPanel.Height;
			if (_previewImage != null)
			{
				int width = (int)(screenHeight * (float)_previewImage.Width / _previewImage.Height);
				g.DrawImage(_previewImage, 0, 0, width, screenHeight);

				//Crop boundary
				if (!chkNoCrop.Checked)
				{
					g.DrawRectangle(_dragState == DragState.None ? Pens.Red : Pens.Blue, _cropBounds.X, _cropBounds.Y, _cropBounds.Width, _cropBounds.Height);
					g.DrawLine(_centerPen, _cropBounds.X + _cropBounds.Width / 2, _cropBounds.Y - 5, _cropBounds.X + _cropBounds.Width / 2, _cropBounds.Y + _cropBounds.Height + 5);
				}
			}
		}

		/// <summary>
		/// Gets the appropriate drag operation based on a mouse position
		/// </summary>
		/// <param name="e">Mouse event args</param>
		/// <returns></returns>
		private DragState GetDragState(MouseEventArgs e)
		{
			const int GrabThreshold = 5;
			//See if we're on the cropping box
			if (!_lockRect)
			{
				if (e.Y >= _cropBounds.Y && e.Y <= _cropBounds.Y + _cropBounds.Height)
				{
					if (Math.Abs(_cropBounds.X - e.X) <= GrabThreshold)
					{
						return DragState.Left;
					}
					else if (Math.Abs(_cropBounds.X + _cropBounds.Width - e.X) <= GrabThreshold)
					{
						return DragState.Right;
					}
				}
				if (e.X >= _cropBounds.X && e.X <= _cropBounds.X + _cropBounds.Width)
				{
					if (Math.Abs(_cropBounds.Y - e.Y) <= GrabThreshold)
					{
						return DragState.Top;
					}
					else if (Math.Abs(_cropBounds.Y + _cropBounds.Height - e.Y) <= GrabThreshold)
					{
						return DragState.Bottom;
					}
				}
			}
			if (e.Y >= _cropBounds.Y && e.Y <= _cropBounds.Y + _cropBounds.Height && e.X >= _cropBounds.X && e.X <= _cropBounds.X + _cropBounds.Width)
			{
				return DragState.Move;
			}
			return DragState.None;
		}

		private void previewPanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (_previewImage == null || e.Button == MouseButtons.Middle || chkNoCrop.Checked)
				return;
			DragState hoverState = GetDragState(e);
			if (hoverState != DragState.None)
			{
				if (hoverState != DragState.Move && e.Button == MouseButtons.Right)
				{
					if (hoverState == DragState.Left || hoverState == DragState.Right)
						hoverState |= DragState.Horizontal;
					else if (hoverState == DragState.Top || hoverState == DragState.Bottom)
						hoverState |= DragState.Vertical;
				}
				SetCursor(hoverState);
				_downPoint = new PointF(e.X, e.Y);
				_dragState = hoverState;
				previewPanel.Invalidate();
			}
		}

		private void previewPanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (_previewImage == null || _dragState == DragState.None)
				return;
			_dragState = DragState.None;
			Cursor.Current = Cursors.Default;
			previewPanel.Invalidate();
		}

		private void previewPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (_previewImage == null)
				return;
			if (_dragState == DragState.None)
			{
				DragState hoverState = GetDragState(e);
				SetCursor(hoverState);
			}
			else
			{
				//Pull the appropriate bounds
				const int MinBuffer = 10;
				float top, left, right, bottom, diff;
				if ((_dragState & DragState.Horizontal) > 0)
				{
					right = _cropBounds.Right;
					left = _cropBounds.X;
					float midLine = (right + left) * 0.5f;
					if ((_dragState & DragState.Left) > 0)
					{
						float maxLeft = midLine - MinBuffer * 0.5f;
						left = Math.Max(0, Math.Min(maxLeft, e.X));
						float dist = midLine - left;
						right = midLine + dist;
						_cropBounds.Width = right - left;
						_cropBounds.X = left;
					}
					else if ((_dragState & DragState.Right) > 0)
					{
						float maxRight = midLine + MinBuffer * 0.5f;
						right = Math.Min(previewPanel.Width, Math.Max(maxRight, e.X));
						float dist = right - midLine;
						left = midLine - dist;
						_cropBounds.Width = right - left;
						_cropBounds.X = left;
					}
				}
				else if ((_dragState & DragState.Left) > 0)
				{
					right = _cropBounds.Right;
					left = Math.Max(0, Math.Min(right - MinBuffer, e.X));
					_cropBounds.Width = right - left;
					_cropBounds.X = left;
				}
				else if ((_dragState & DragState.Right) > 0)
				{
					left = _cropBounds.Left;
					right = Math.Min(previewPanel.Width, Math.Max(left + MinBuffer, e.X));
					_cropBounds.Width = right - left;
				}
				else if ((_dragState & DragState.Move) > 0)
				{
					left = _cropBounds.Left;
					top = _cropBounds.Top;
					diff = e.X - _downPoint.X;
					left = Math.Max(0, Math.Min(previewPanel.Width - _cropBounds.Width, left + diff));
					diff = e.Y - _downPoint.Y;
					top = Math.Max(0, Math.Min(previewPanel.Height - _cropBounds.Height, top + diff));
					_cropBounds.X = left;
					_cropBounds.Y = top;
					_downPoint = new PointF(e.X, e.Y);
				}
				else if ((_dragState & DragState.Vertical) > 0)
				{
					//not yet implemented
				}
				else if ((_dragState & DragState.Top) > 0)
				{
					bottom = _cropBounds.Bottom;
					top = Math.Max(0, Math.Min(bottom - MinBuffer, e.Y));
					_cropBounds.Height = bottom - top;
					_cropBounds.Y = top;
				}
				else if ((_dragState & DragState.Bottom) > 0)
				{
					top = _cropBounds.Top;
					bottom = Math.Min(previewPanel.Height, Math.Max(top + MinBuffer, e.Y));
					_cropBounds.Height = bottom - top;
				}

				UpdateRectBoxes();

				previewPanel.Invalidate();
			}
		}

		private void UpdateRectBoxes()
		{
			Rect rect = CroppingRegion;
			valLeft.Value = rect.Left;
			valRight.Value = rect.Right;
			valTop.Value = rect.Top;
			valBottom.Value = rect.Bottom;
		}

		/// <summary>
		/// Sets the mouse cursor based on the drag state
		/// </summary>
		/// <param name="state">Current dragging state</param>
		private void SetCursor(DragState state)
		{
			switch (state)
			{
				case DragState.Left:
				case DragState.Right:
				case DragState.Horizontal:
					Cursor.Current = Cursors.SizeWE;
					break;
				case DragState.Top:
				case DragState.Bottom:
				case DragState.Vertical:
					Cursor.Current = Cursors.SizeNS;
					break;
				case DragState.Move:
					Cursor.Current = Cursors.SizeAll;
					break;
				default:
					Cursor.Current = Cursors.Default;
					break;
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			if (_previewImage != null)
			{
				if (chkNoCrop.Checked)
				{
					CroppedImage = new Bitmap(_previewImage.Width, _previewImage.Height);
					Graphics g = Graphics.FromImage(CroppedImage);

					g.DrawImage(_previewImage, 0, 0);
					g.Dispose();
				}
				else
				{
					CroppedImage = _importer.Crop(_previewImage, _cropBounds.ToRect(ZoomRatio));
				}
				_previewImage.Dispose();
				_previewImage = null;
			}
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			_previewImage?.Dispose();
			_previewImage = null;
			Close();
		}

		private void ImageCropper_FormClosed(object sender, FormClosedEventArgs e)
		{
			Cursor.Current = Cursors.Default;
		}

		private void tmrWait_Tick(object sender, EventArgs e)
		{
			string text = lblWait.Text;
			int length = text.Length;
			if (length >= 14)
			{
				lblWait.Text = "Please wait";
			}
			else lblWait.Text += ".";
		}

		private void cmdReimport_Click(object sender, EventArgs e)
		{
			ImportUnprocessed(_extraData);
		}

		public void ImportUnprocessed(Dictionary<string, string> extraData)
		{
			if (_cropBounds == new RectangleF(0, 0, 10, 10))
			{
				_cropBounds = new Rect(0, 0, 600, 1400).ToRectangle(ZoomRatio);
				_cropBounds.X = _cropBounds.X + ImageImporter.ImageXOffset * ZoomRatio;
				UpdateRectBoxes();
			}
			_previewImage?.Dispose();
			_previewImage = null;
			Cursor.Current = Cursors.WaitCursor;
			_previewImage = _importer.Reimport(extraData);
			lblWait.Visible = false;
			Cursor.Current = Cursors.Default;
			previewPanel.Invalidate();
		}

		private void CropValueChanged(object sender, EventArgs e)
		{
			UpdateCropManually();
		}

		private void UpdateCropManually()
		{
			if (_dragState != DragState.None) { return; }
			Rect rect = new Rect((int)valLeft.Value, (int)valTop.Value, (int)valRight.Value, (int)valBottom.Value);
			float zoom = ZoomRatio;
			_cropBounds = rect.ToRectangle(zoom);
			_cropBounds.X = _cropBounds.X + ImageImporter.ImageXOffset * zoom;
			previewPanel.Invalidate();
		}

		private void chkNoCrop_CheckedChanged(object sender, EventArgs e)
		{
			previewPanel.Invalidate();
		}

		private void cmdAdvanced_Click(object sender, EventArgs e)
		{
			PoseSettingsForm form = new PoseSettingsForm();
			form.SetData(_extraData);
			if (form.ShowDialog() == DialogResult.OK)
			{
				_extraData = form.GetData();
			}
		}

		private void cmdCopy_Click(object sender, EventArgs e)
		{
			Clipboard.Clear();
			Clipboard.SetText(_lastCode);
			Shell.Instance.SetStatus("Code copied to clipboard.");
		}
	}
}
