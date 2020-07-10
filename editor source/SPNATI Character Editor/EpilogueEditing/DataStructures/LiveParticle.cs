using SPNATI_Character_Editor.EpilogueEditing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveParticle : LiveObject
	{
		public LiveEmitter Emitter;
		public float Duration;
		public float Elapsed;
		public float Spin;
		public float InitialAngle;
		public float SpeedX;
		public float SpeedY;
		public float Acceleration;
		public float ForceX;
		public float ForceY;

		public TweenableParameter ScaleXTween;
		public TweenableParameter ScaleYTween;
		public TweenableParameter AlphaTween;
		public TweenableColor ColorTween;
		public TweenableParameter SpinTween;
		public TweenableParameter SkewXTween;
		public TweenableParameter SkewYTween;

		public string Ease;
		public int Layer;

		private SolidBrush _brush = new SolidBrush(Color.White);

		public LiveParticle(LiveEmitter emitter)
		{
			Emitter = emitter;
			CenterX = true;
			CenterY = true;

			//randomize the rotation by the emission angle range
			float rotation = emitter.ToWorldRotation(emitter.Rotation);
			float angle = emitter.Random.Next(-(int)emitter.Angle, (int)emitter.Angle + 1);
			rotation += angle;

			PointF worldPoint = emitter.ToWorldPt(emitter.X, emitter.Y);
			X = worldPoint.X;
			Y = worldPoint.Y;

			Image = emitter.Image;
			Width = emitter.ParticleWidth;
			Height = emitter.ParticleHeight;
			PivotX = Width / 2.0f;
			PivotY = Height / 2.0f;

			Rotation = emitter.IgnoreRotation ? 0 : rotation;
			float degrees = rotation;
			float radians = degrees * (float)Math.PI / 180.0f;
			float speed = (emitter.Speed ?? new RandomParameter(0, 0)).Get();

			InitialAngle = radians;

			//convert rotation angle to direction vector where 0 deg = [0,-1], 90 deg = [1,0]
			float u = (float)Math.Sin(radians);
			float v = -(float)Math.Cos(radians);

			SpeedX = speed * u;
			SpeedY = speed * v;

			Acceleration = (emitter.Acceleration ?? new RandomParameter(0, 0)).Get();
			ForceX = (emitter.ForceX ?? new RandomParameter(0, 0)).Get();
			ForceY = (emitter.ForceY ?? new RandomParameter(0, 0)).Get();

			Ease = emitter.ParticleEase;

			PivotX = 0.5f;
			PivotY = 0.5f;

			ScaleXTween = new TweenableParameter((emitter.StartScaleX ?? new RandomParameter(1, 1)).Get(), (emitter.EndScaleX ?? emitter.StartScaleX ?? new RandomParameter(1, 1)).Get());
			ScaleYTween = new TweenableParameter((emitter.StartScaleY ?? new RandomParameter(1, 1)).Get(), (emitter.EndScaleY ?? emitter.StartScaleY ?? new RandomParameter(1, 1)).Get());
			AlphaTween = new TweenableParameter((emitter.StartAlpha ?? new RandomParameter(100, 100)).Get(), (emitter.EndAlpha ?? emitter.StartAlpha ?? new RandomParameter(0, 0)).Get());
			ColorTween = new TweenableColor((emitter.StartColor ?? new RandomColor(Color.White, Color.White)).Get(), (emitter.EndColor ?? emitter.StartColor ?? new RandomColor(Color.White, Color.White)).Get());
			SpinTween = new TweenableParameter((emitter.StartRotation ?? new RandomParameter(0, 0)).Get(), (emitter.EndRotation ?? emitter.StartRotation ?? new RandomParameter(0, 0)).Get());
			SkewXTween = new TweenableParameter((emitter.StartSkewX ?? new RandomParameter(0, 0)).Get(), (emitter.EndSkewX ?? emitter.StartSkewX ?? new RandomParameter(0, 0)).Get());
			SkewYTween = new TweenableParameter((emitter.StartSkewY ?? new RandomParameter(0, 0)).Get(), (emitter.EndSkewY ?? emitter.StartSkewY ?? new RandomParameter(0, 0)).Get());

			Layer = emitter.Z;

			Elapsed = 0;
			Duration = (emitter.Lifetime ?? new RandomParameter(1, 1)).Get() * 1000;

			UpdateRealTime(0, false);
		}

		public override bool IsVisible
		{
			get { return true; }
		}

		public override LiveObject CreateLivePreview(float time)
		{
			throw new NotImplementedException();
		}

		public override ITimelineWidget CreateWidget(Timeline timeline)
		{
			throw new NotImplementedException();
		}

		public override void DestroyLivePreview()
		{
			throw new NotImplementedException();
		}

		public override void Draw(Graphics g, Matrix sceneTransform, List<string> markers, bool inPlayback)
		{
			if (Alpha > 0)
			{
				g.MultiplyTransform(WorldTransform);
				g.MultiplyTransform(sceneTransform, MatrixOrder.Append);

				if (Image != null)
				{
					if (Alpha < 100)
					{
						float[][] matrixItems = new float[][] {
						  new float[] { 1, 0, 0, 0, 0 },
						  new float[] { 0, 1, 0, 0, 0 },
						  new float[] { 0, 0, 1, 0, 0 },
						  new float[] { 0, 0, 0, Alpha / 100.0f, 0 },
						  new float[] { 0, 0, 0, 0, 1 }
						 };
						ColorMatrix cm = new ColorMatrix(matrixItems);
						ImageAttributes ia = new ImageAttributes();
						ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

						g.DrawImage(Image, new Rectangle(0, 0, Width, Height), 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, ia);
					}
					else
					{
						g.DrawImage(Image, 0, 0, Width, Height);
					}
				}
				else
				{
					g.FillEllipse(_brush, 0, 0, Width, Height);
				}

				g.ResetTransform();
			}
		}

		public override void Update(float time, float elapsedTime, bool inPlayback)
		{

		}

		public bool IsDead
		{
			get
			{
				return Elapsed >= Duration;
			}
		}

		public override bool UpdateRealTime(float deltaTime, bool inPlayback)
		{
			Elapsed += deltaTime;

			if (IsDead)
			{
				return false;
			}

			float elapsedSec = deltaTime / 1000.0f;
			float dt = elapsedSec;

			float t = Math.Min(1, Elapsed / Duration);
			t = AnimationHelpers.Ease(Ease, t);
			ScaleX = ScaleXTween.Tween(t);
			ScaleY = ScaleYTween.Tween(t);
			SkewX = SkewXTween.Tween(t);
			SkewY = SkewYTween.Tween(t);
			_brush.Color = System.Drawing.Color.FromArgb((int)(AlphaTween.Tween(t) / 100.0 * 255.0f), ColorTween.Tween(t));
			Alpha = AlphaTween.Value;
			Spin = SpinTween.Tween(t);

			Rotation += Spin * dt;

			//accelerate in the forward direction
			float forward = InitialAngle;
			float u = (float)Math.Sin(forward);
			float v = -(float)Math.Cos(forward);

			var accelX = Acceleration * u;
			var accelY = Acceleration * v;

			SpeedX += (accelX + ForceX) * dt;
			SpeedY += (accelY + ForceY) * dt;

			X += dt * SpeedX;
			Y += dt * SpeedY;
			return true;
		}
	}
}
