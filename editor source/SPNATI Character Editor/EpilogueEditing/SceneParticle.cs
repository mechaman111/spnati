using System;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class SceneParticle : SceneObject
	{
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

		public ScenePreview Scene;

		public SceneParticle(SceneEmitter emitter)
		{
			Scene = emitter.Scene;
			ObjectType = SceneObjectType.Particle;
			//randomize the rotation by the emission angle range
			float rotation = emitter.Rotation;
			float angle = emitter.Random.Next(-(int)emitter.Angle, (int)emitter.Angle + 1);
			rotation += angle;

			X = emitter.X - emitter.ParticleWidth / 2;
			Y = emitter.Y - emitter.ParticleHeight / 2;

			Image = emitter.Image;
			Width = emitter.ParticleWidth;
			Height = emitter.ParticleHeight;
			PivotX = Width / 2.0f;
			PivotY = Height / 2.0f;

			Rotation = rotation;
			float degrees = rotation;
			float radians = degrees * (float)Math.PI / 180.0f;
			float speed = emitter.Speed.Get();

			InitialAngle = radians;

			//oncvert rotation angle to directive vector where 0 deg = [0,-1], 90 deg = [1,0]
			float u = (float)Math.Sin(radians);
			float v = -(float)Math.Cos(radians);

			SpeedX = speed * u;
			SpeedY = speed * v;

			Acceleration = emitter.Acceleration.Get();
			ForceX = emitter.ForceX.Get();
			ForceY = emitter.ForceY.Get();

			Ease = emitter.Ease;

			ScaleXTween = new TweenableParameter(emitter.StartScaleX.Get(), emitter.EndScaleX.Get());
			ScaleYTween = new TweenableParameter(emitter.StartScaleY.Get(), emitter.EndScaleY.Get());
			AlphaTween = new TweenableParameter(emitter.StartAlpha.Get(), emitter.EndAlpha.Get());
			ColorTween = new TweenableColor(emitter.StartColor.Get(), emitter.EndColor.Get());
			SpinTween = new TweenableParameter(emitter.StartRotation.Get(), emitter.EndRotation.Get());

			Elapsed = 0;
			Duration = emitter.Lifetime.Get() * 1000;
			UpdateTick(0, null);
		}

		public bool IsDead
		{
			get
			{
				return Elapsed >= Duration;
			}
		}

		public override void UpdateTick(float elapsedSec, ScenePreview scene)
		{
			float elapsedMs = elapsedSec * 1000;
			Elapsed += elapsedMs;

			if (IsDead)
			{
				Die();
			}

			float dt = elapsedSec;

			float t = Math.Min(1, Elapsed / Duration);
			t = SceneAnimation.Ease(Ease, t);
			ScaleX = ScaleXTween.Tween(t);
			ScaleY = ScaleYTween.Tween(t);
			Color.Color = System.Drawing.Color.FromArgb((int)AlphaTween.Tween(t), ColorTween.Tween(t));
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

			base.UpdateTick(elapsedSec, scene);
		}

		public void Die()
		{
			Scene?.Objects?.Remove(this);
		}
	}
}
