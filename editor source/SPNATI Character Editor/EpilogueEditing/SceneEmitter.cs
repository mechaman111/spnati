using System;
using System.Collections.Generic;
using System.Globalization;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class SceneEmitter : SceneObject
	{
		public const int EmitterRadius = 15;

		internal Random Random = new Random();
		public float EmissionTimer = 0;
		public float Angle;

		public ScenePreview Scene;

		public List<SceneParticle> ActiveParticles = new List<SceneParticle>();

		public float ParticleWidth;
		public float ParticleHeight;

		public RandomParameter StartScaleX;
		public RandomParameter EndScaleX;
		public RandomParameter StartScaleY;
		public RandomParameter EndScaleY;
		public RandomParameter StartSkewX;
		public RandomParameter EndSkewX;
		public RandomParameter StartSkewY;
		public RandomParameter EndSkewY;
		public RandomParameter Speed;
		public RandomParameter Acceleration;
		public RandomParameter ForceX;
		public RandomParameter ForceY;
		public RandomColor StartColor;
		public RandomColor EndColor;
		public RandomParameter StartAlpha;
		public RandomParameter EndAlpha;
		public RandomParameter StartRotation;
		public RandomParameter EndRotation;
		public RandomParameter Lifetime;
		public bool IgnoreParent;

		public SceneEmitter() { }

		public SceneEmitter(ScenePreview scene, Character character, Directive directive) : base(scene, character, directive)
		{
			Scene = scene;

			if (string.IsNullOrEmpty(directive.Src))
			{
				if (string.IsNullOrEmpty(directive.Width))
				{
					Width = 10;
				}
				if (string.IsNullOrEmpty(directive.Height))
				{
					Height = 10;
				}
			}

			ObjectType = SceneObjectType.Emitter;
			if (!string.IsNullOrEmpty(directive.Angle))
			{
				float.TryParse(directive.Angle, out Angle);
			}
			if (Rate > 0)
			{
				EmissionTimer = 1000 / Rate;
			}

			StartScaleX = RandomParameter.Create(directive.StartScaleX, 1, 1);
			EndScaleX = RandomParameter.Create(directive.EndScaleX, StartScaleX);
			StartScaleY = RandomParameter.Create(directive.StartScaleY, 1, 1);
			EndScaleY = RandomParameter.Create(directive.EndScaleY, StartScaleY);
			StartSkewX = RandomParameter.Create(directive.StartSkewX, 0, 0);
			EndSkewX = RandomParameter.Create(directive.EndSkewX, StartSkewX);
			StartSkewY = RandomParameter.Create(directive.StartSkewY, 0, 0);
			EndSkewY = RandomParameter.Create(directive.EndSkewY, StartSkewY);
			Speed = RandomParameter.Create(directive.Speed, 0, 0);
			Acceleration = RandomParameter.Create(directive.Acceleration, 0, 0);
			ForceX = RandomParameter.Create(directive.ForceX, 0, 0);
			ForceY = RandomParameter.Create(directive.ForceY, 0, 0);
			StartColor = RandomColor.Create(directive.StartColor, System.Drawing.Color.White, System.Drawing.Color.White);
			EndColor = RandomColor.Create(directive.EndColor, StartColor);
			StartAlpha = RandomParameter.Create(directive.StartAlpha, 100, 100);
			EndAlpha = RandomParameter.Create(directive.EndAlpha, StartAlpha);
			StartRotation = RandomParameter.Create(directive.StartRotation, 0, 0);
			EndRotation = RandomParameter.Create(directive.EndRotation, StartRotation);
			Lifetime = RandomParameter.Create(directive.Lifetime, 1, 1);
			IgnoreParent = directive.IgnoreRotation;

			ParticleWidth = Width;
			ParticleHeight = Height;

			PivotX = EmitterRadius;
			PivotY = EmitterRadius;
			Width = EmitterRadius * 2;
			Height = EmitterRadius * 2;
		}

		public override SceneObject Copy()
		{
			SceneEmitter copy = new SceneEmitter();
			copy.CopyValuesFrom(this);

			copy.EmissionTimer = EmissionTimer;
			copy.ParticleWidth = ParticleWidth;
			copy.ParticleHeight = ParticleHeight;
			copy.Scene = Scene;
			copy.StartScaleX = StartScaleX;
			copy.EndScaleX = EndScaleX;
			copy.StartScaleY = StartScaleY;
			copy.EndScaleY = EndScaleY;
			copy.StartSkewX = StartSkewX;
			copy.EndSkewX = EndSkewX;
			copy.StartSkewY = StartSkewY;
			copy.EndSkewY = EndSkewY;
			copy.Speed = Speed;
			copy.Acceleration = Acceleration;
			copy.ForceX = ForceX;
			copy.ForceY = ForceY;
			copy.StartColor = StartColor;
			copy.EndColor = EndColor;
			copy.StartAlpha = StartAlpha;
			copy.EndAlpha = EndAlpha;
			copy.StartRotation = StartRotation;
			copy.EndRotation = EndRotation;
			copy.Lifetime = Lifetime;
			copy.Angle = Angle;
			copy.IgnoreParent = IgnoreParent;
			return copy;
		}

		public override void Dispose()
		{
			base.Dispose();
			if (!Scene.IsDisposing)
			{
				foreach (SceneObject particle in ActiveParticles)
				{
					particle.Dispose();
				}
			}
			ActiveParticles.Clear();
		}

		public override void UpdateTick(float elapsedSec, ScenePreview scene)
		{
			float elapsedMs = elapsedSec * 1000;
			base.UpdateTick(elapsedMs, scene);

			if (Rate > 0)
			{
				float cooldown = 1000 / Rate;
				EmissionTimer += elapsedMs;
				while (EmissionTimer >= cooldown)
				{
					Emit();
					EmissionTimer -= cooldown;
				}
			}

			for (int i = ActiveParticles.Count - 1; i >= 0; i--)
			{
				SceneParticle particle = ActiveParticles[i];
				if (particle.IsDead)
				{
					ActiveParticles.RemoveAt(i);
					particle.Dispose();
				}
			}
		}

		public override void Update(Keyframe frame, ScenePreview scene)
		{
			base.Update(frame, scene);
			if (!string.IsNullOrEmpty(frame.Rate))
			{
				float.TryParse(frame.Rate, NumberStyles.Float, CultureInfo.InvariantCulture, out Rate);
			}
		}

		public void Emit()
		{
			SceneParticle particle = new SceneParticle(LinkedAnimation?.PreviewObject as SceneEmitter ?? this);
			Scene.AddObject(particle);
		}
	}
}
