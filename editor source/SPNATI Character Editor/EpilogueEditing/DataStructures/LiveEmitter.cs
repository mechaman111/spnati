using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls.EditControls;
using SPNATI_Character_Editor.EpilogueEditing;
using SPNATI_Character_Editor.Properties;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveEmitter : LiveAnimatedObject, ICanInvalidate
	{
		public EmitterWidget Widget;
		internal Random Random = new Random();

		private List<LiveParticle> _particles = new List<LiveParticle>();

		private float _emissionTimer;

		public event EventHandler Invalidated;

		public float Rate
		{
			get { return Get<float>(); }
			set { Set(value); }
		}

		[Numeric(DisplayName = "Width", Key = "width", GroupOrder = 15, Description = "Particle width")]
		public int ParticleWidth
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[Numeric(DisplayName = "Height", Key = "height", GroupOrder = 16, Description = "Particle height")]
		public int ParticleHeight
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		[ComboBox(DisplayName = "Easing Function", Key = "ease", GroupOrder = 17, Description = "Easing function for how fast the animation progresses over time", Options = new string[] { "linear", "smooth", "ease-in", "ease-in-sin", "ease-in-cubic", "ease-out", "ease-out-sin", "ease-out-cubic", "ease-in-out-cubic", "ease-out-in", "ease-out-in-cubic", "bounce", "elastic" })]
		public string ParticleEase
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Boolean(DisplayName = "Ignore rotation", Key = "ignoreRotation", GroupOrder = 19, Description = "If true, particles will spawn facing upwards regardless of the emitter's current rotation")]
		public bool IgnoreRotation
		{
			get { return Get<bool>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Particle Life (s)", Key = "lifetime", GroupOrder = 20, Description = "Time in seconds before an emitted object disappears", Minimum = 0.1f, Maximum = 1000, DecimalPlaces = 2)]
		public RandomParameter Lifetime
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[Float(DisplayName = "Angle", Key = "angle", GroupOrder = 21, Description = "Degrees away from emitter's forward direction that objects can be emitted in", Minimum = -180, Maximum = 180, DecimalPlaces = 0)]
		public float Angle
		{
			get { return Get<float>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Start Scale X", Key = "startScaleX", GroupOrder = 22, Description = "Initial horizontal stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		public RandomParameter StartScaleX
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Start Scale Y", Key = "startScaleY", GroupOrder = 23, Description = "Initial vertical stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		public RandomParameter StartScaleY
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "End Scale X", Key = "endScaleX", GroupOrder = 24, Description = "Ending horizontal stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		public RandomParameter EndScaleX
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "End Scale Y", Key = "endScaleY", GroupOrder = 25, Description = "Ending vertical stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		public RandomParameter EndScaleY
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Speed", Key = "speed", GroupOrder = 26, Description = "Initial speed range (px/sec)", Minimum = -10000, Maximum = 10000, DecimalPlaces = 0)]
		public RandomParameter Speed
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Acceleration", Key = "accel", GroupOrder = 27, Description = "Initial acceleration range (px/sec^2)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		public RandomParameter Acceleration
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Force X", Key = "forceX", GroupOrder = 28, Description = "World horizontal force (wind) (px/sec^2)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		public RandomParameter ForceX
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Force Y", Key = "forceY", GroupOrder = 29, Description = "World vertical force (gravity) (px/sec^2)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		public RandomParameter ForceY
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleColor(DisplayName = "Start Color", Key = "startColor", GroupOrder = 30, Description = "Initial particle color")]
		public RandomColor StartColor
		{
			get { return Get<RandomColor>(); }
			set { Set(value); }
		}

		[ParticleColor(DisplayName = "End Color", Key = "startColor", GroupOrder = 31, Description = "Ending particle color")]
		public RandomColor EndColor
		{
			get { return Get<RandomColor>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Start Alpha", Key = "startAlpha", GroupOrder = 32, Description = "Initial transparency range", Minimum = 0, Maximum = 100, DecimalPlaces = 0)]
		public RandomParameter StartAlpha
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "End Alpha", Key = "endAlpha", GroupOrder = 33, Description = "Ending transparency range", Minimum = 0, Maximum = 100, DecimalPlaces = 0)]
		public RandomParameter EndAlpha
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Start Spin", Key = "startRotation", GroupOrder = 34, Description = "Initial spin range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		public RandomParameter StartRotation
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "End Spin", Key = "endRotation", GroupOrder = 35, Description = "Ending spin range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		public RandomParameter EndRotation
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Start Skew X", Key = "startSkewX", GroupOrder = 36, Description = "Initial horizontal shearing range", Minimum = -89, Maximum = 89, DecimalPlaces = 2)]
		public RandomParameter StartSkewX
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "Start Skew Y", Key = "startSkewY", GroupOrder = 36, Description = "Initial vertical shearing range", Minimum = -89, Maximum = 89, DecimalPlaces = 2)]
		public RandomParameter StartSkewY
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "End Skew X", Key = "endSkewX", GroupOrder = 37, Description = "Ending horizontal shearing range", Minimum = -89, Maximum = 89, DecimalPlaces = 2)]
		public RandomParameter EndSkewX
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		[ParticleFloat(DisplayName = "End Skew Y", Key = "endSkewY", GroupOrder = 37, Description = "Ending vertical shearing range", Minimum = -89, Maximum = 89, DecimalPlaces = 2)]
		public RandomParameter EndSkewY
		{
			get { return Get<RandomParameter>(); }
			set { Set(value); }
		}

		public LiveEmitter()
		{
			DisplayPastEnd = false;
			CenterX = true;
			CenterY = true;
			PivotX = 0.5f;
			PivotY = 0.5f;
			ScaleX = 0.5f;
			ScaleY = 0.5f;
			ParticleWidth = 10;
			ParticleHeight = 10;
			Lifetime = new RandomParameter(1, 1);
			StartAlpha = new RandomParameter(100, 100);
			EndAlpha = new RandomParameter(0, 0);
		}

		public LiveEmitter(LiveData data, float time) : this()
		{
			Data = data;
			DisplayPastEnd = false;
			Length = 1;
			Start = time;

			LiveEmitterKeyframe startFrame = CreateKeyframe(0) as LiveEmitterKeyframe;
			startFrame.Rate = 1;
			AddKeyframe(startFrame);
			Update(time, 0, false);
			InvalidateTransform();
		}

		#region Epilogue import
		public LiveEmitter(LiveSceneSegment scene, Directive directive, Character character, float time) : this()
		{
			DisplayPastEnd = false;
			Data = scene;
			ParentId = directive.ParentId;
			Length = 1;
			Character = character;
			Id = directive.Id;
			Z = directive.Layer;
			LinkedToEnd = true;
			Start = time;
			if (!string.IsNullOrEmpty(directive.Delay))
			{
				float start;
				float.TryParse(directive.Delay, NumberStyles.Number, CultureInfo.InvariantCulture, out start);
				Start += start;
				Length = 1;
			}

			if (!string.IsNullOrEmpty(directive.Angle))
			{
				float angle;
				float.TryParse(directive.Angle, NumberStyles.Number, CultureInfo.InvariantCulture, out angle);
				Angle = angle;
			}

			if (!string.IsNullOrEmpty(directive.Src))
			{
				string path = LiveSceneSegment.FixPath(directive.Src, character);
				Image = LiveImageCache.Get(path);
				if (Image != null)
				{
					ParticleWidth = Image.Width;
					ParticleHeight = Image.Height;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(directive.Width))
				{
					int width;
					if (int.TryParse(directive.Width, out width))
					{
						ParticleWidth = width;
					}
				}
				if (!string.IsNullOrEmpty(directive.Height))
				{
					int height;
					if (int.TryParse(directive.Height, out height))
					{
						ParticleHeight = height;
					}
				}
			}
			if (!string.IsNullOrEmpty(directive.EasingMethod))
			{
				ParticleEase = directive.EasingMethod;
			}

			InitializeParameters(directive);

			AddKeyframe(directive, 0, false, 0);
			Update(time, 0, false);
		}

		private void InitializeParameters(Directive directive)
		{
			if (!string.IsNullOrEmpty(directive.StartScaleX))
			{
				StartScaleX = RandomParameter.Create(directive.StartScaleX, 1, 1);
			}
			if (!string.IsNullOrEmpty(directive.EndScaleX))
			{
				EndScaleX = RandomParameter.Create(directive.EndScaleX, StartScaleX);
			}
			if (!string.IsNullOrEmpty(directive.StartScaleY))
			{
				StartScaleY = RandomParameter.Create(directive.StartScaleY, 1, 1);
			}
			if (!string.IsNullOrEmpty(directive.EndScaleY))
			{
				EndScaleY = RandomParameter.Create(directive.EndScaleY, StartScaleY);
			}
			if (!string.IsNullOrEmpty(directive.StartSkewX))
			{
				StartSkewX = RandomParameter.Create(directive.StartSkewX, 0, 0);
			}
			if (!string.IsNullOrEmpty(directive.EndSkewX))
			{
				EndSkewX = RandomParameter.Create(directive.EndSkewX, StartSkewX);
			}
			if (!string.IsNullOrEmpty(directive.StartSkewY))
			{
				StartSkewY = RandomParameter.Create(directive.StartSkewY, 0, 0);
			}
			if (!string.IsNullOrEmpty(directive.EndSkewY))
			{
				EndSkewY = RandomParameter.Create(directive.EndSkewY, StartSkewY);
			}
			if (!string.IsNullOrEmpty(directive.Speed))
			{
				Speed = RandomParameter.Create(directive.Speed, 0, 0);
			}
			if (!string.IsNullOrEmpty(directive.Acceleration))
			{
				Acceleration = RandomParameter.Create(directive.Acceleration, 0, 0);
			}
			if (!string.IsNullOrEmpty(directive.ForceX))
			{
				ForceX = RandomParameter.Create(directive.ForceX, 0, 0);
			}
			if (!string.IsNullOrEmpty(directive.ForceY))
			{
				ForceY = RandomParameter.Create(directive.ForceY, 0, 0);
			}
			if (!string.IsNullOrEmpty(directive.StartColor))
			{
				StartColor = RandomColor.Create(directive.StartColor, Color.White, Color.White);
			}
			if (!string.IsNullOrEmpty(directive.EndColor))
			{
				EndColor = RandomColor.Create(directive.EndColor, StartColor);
			}
			if (!string.IsNullOrEmpty(directive.StartAlpha))
			{
				StartAlpha = RandomParameter.Create(directive.StartAlpha, 100, 100);
			}
			if (!string.IsNullOrEmpty(directive.EndAlpha))
			{
				EndAlpha = RandomParameter.Create(directive.EndAlpha, StartAlpha);
			}
			if (!string.IsNullOrEmpty(directive.StartRotation))
			{
				StartRotation = RandomParameter.Create(directive.StartRotation, 0, 0);
			}
			if (!string.IsNullOrEmpty(directive.EndRotation))
			{
				EndRotation = RandomParameter.Create(directive.EndRotation, StartRotation);
			}
			if (!string.IsNullOrEmpty(directive.Lifetime))
			{
				Lifetime = RandomParameter.Create(directive.Lifetime, 1, 1);
			}
			IgnoreRotation = directive.IgnoreRotation;
		}
		#endregion

		public override string GetLabel()
		{
			return $"Emitter Settings: {Id}";
		}

		public override Type GetKeyframeType()
		{
			return typeof(LiveEmitterKeyframe);
		}

		public override Type GetEventType()
		{
			return typeof(LiveBurst);
		}

		protected override void ParseKeyframe(Keyframe kf, bool addBreak, HashSet<string> properties, float time, float origin)
		{
			if (!string.IsNullOrEmpty(kf.X))
			{
				if (time > origin && !AnimatedProperties.Contains("X"))
				{
					//AddValue<float>(origin, "X", "0", true);
				}
				AddValue<float>(time, "X", kf.X, addBreak);
				properties.Add("X");
			}
			if (!string.IsNullOrEmpty(kf.Y))
			{
				if (time > origin && !AnimatedProperties.Contains("Y"))
				{
					//AddValue<float>(origin, "Y", "0", true);
				}
				AddValue<float>(time, "Y", kf.Y, addBreak);
				properties.Add("Y");
			}
			if (!string.IsNullOrEmpty(kf.Src))
			{
				if (time > origin && !AnimatedProperties.Contains("Src"))
				{
					//AddValue<string>(origin, "Src", "", true);
				}
				string path = LiveSceneSegment.FixPath(kf.Src, Character);
				AddValue<string>(time, "Src", path, addBreak);
				properties.Add("Src");
			}
			if (!string.IsNullOrEmpty(kf.Rotation))
			{
				if (time > origin && !AnimatedProperties.Contains("Rotation"))
				{
					//AddValue<float>(origin, "Rotation", "0", true);
				}
				AddValue<float>(time, "Rotation", kf.Rotation, addBreak);
				properties.Add("Rotation");
			}
			if (!string.IsNullOrEmpty(kf.Rate))
			{
				if (time > origin && !AnimatedProperties.Contains("Rate"))
				{
					//AddValue<float>(origin, "Rate", "0", true);
				}
				AddValue<float>(time, "Rate", kf.Rate, addBreak);
				properties.Add("Rate");
			}
		}

		public override bool UpdateRealTime(float elapsed, bool inPlayback)
		{
			Console.WriteLine(Id + " Real time update" + elapsed);
			if (Hidden)
			{
				return false;
			}
			if ((inPlayback || LinkedPreview != null) && IsVisible)
			{
				_emissionTimer += elapsed;
				float cooldown = 1000 / Rate;
				while (_emissionTimer >= cooldown)
				{
					Emit();
					_emissionTimer -= cooldown;
				}
			}
			else
			{
				_emissionTimer = 0;
			}

			if (_particles.Count > 0)
			{
				for (int i = 0; i < _particles.Count; i++)
				{
					_particles[i].UpdateRealTime(elapsed, inPlayback);
					if (_particles[i].IsDead)
					{
						_particles.RemoveAt(i--);
					}
				}
				Invalidated?.Invoke(this, EventArgs.Empty);
				return true;
			}
			return false;
		}

		protected override void OnUpdate(float time, float offset, string easeOverride, string interpolationOverride, bool? looped, bool inPlayback)
		{
			X = GetPropertyValue("X", time, offset, 0.0f, easeOverride, interpolationOverride, looped);
			Y = GetPropertyValue("Y", time, offset, 0.0f, easeOverride, interpolationOverride, looped);
			Rate = GetPropertyValue("Rate", time, offset, 1.0f, easeOverride, interpolationOverride, looped);
			string src = GetPropertyValue<string>("Src", time, offset, null, easeOverride, interpolationOverride, looped);
			Src = src;
			Image = LiveImageCache.Get(src);
			if (Image != null)
			{
				ParticleWidth = Image.Width;
				ParticleHeight = Image.Height;
			}
			Rotation = GetPropertyValue("Rotation", time, offset, 0.0f, easeOverride, interpolationOverride, looped);
		}

		public override void DestroyLivePreview()
		{
			base.DestroyLivePreview();
			_particles.Clear();
		}

		public void Emit()
		{
			_particles.Add(new LiveParticle(this));
		}

		public override ITimelineWidget CreateWidget(Timeline timeline)
		{
			return new EmitterWidget(this, timeline);
		}

		protected override void OnUpdateDimensions()
		{
			Width = Resources.emitter.Width;
			Height = Resources.emitter.Height;
		}

		public override bool FilterRecord(string key)
		{
			switch (key)
			{
				case "x":
				case "y":
				case "pivotx":
				case "pivoty":
					return false;
				default:
					return true;
			}
		}

		public override void Draw(Graphics g, Matrix sceneTransform, List<string> markers, bool inPlayback)
		{
			if (HiddenByMarker(markers))
			{
				return;
			}

			//emitter
			if (IsVisible && !Hidden && !inPlayback)
			{
				g.MultiplyTransform(WorldTransform);
				g.MultiplyTransform(sceneTransform, MatrixOrder.Append);

				Image img = Resources.emitter;

				//angle
				if (LinkedPreview != null)
				{
					int angleLength = img.Width;
					int dx = Angle == 90 ? angleLength : (int)(Math.Sin(Angle * Math.PI / 180.0f) * angleLength);
					int dy = (int)(Math.Cos(Angle * Math.PI / 180.0f) * angleLength);

					g.DrawLine(Pens.LightGray, Width / 2, Height / 2, Width / 2 + dx, Height / 2 - dy);
					g.DrawLine(Pens.LightGray, Width / 2, Height / 2, Width / 2 - dx, Height / 2 - dy);
				}

				g.DrawImage(img, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);

				g.ResetTransform();
			}

			//particles
			if (!Hidden)
			{
				foreach (LiveParticle particle in _particles)
				{
					particle.Draw(g, sceneTransform, markers, inPlayback);
				}
			}
		}

		public override bool CanScale { get { return false; } }
		public override bool CanSkew { get { return false; } }
		public override bool CanPivot { get { return false; } }

		public override Directive CreateCreationDirective(Scene scene)
		{
			Directive emitter = new Directive()
			{
				Id = Id,
				DirectiveType = "emitter",
				Delay = Start.ToString(CultureInfo.InvariantCulture),
				Layer = Z,
				ParentId = ParentId,
				Marker = Marker,
				Width = ParticleWidth.ToString(CultureInfo.InvariantCulture),
				Height = ParticleHeight.ToString(CultureInfo.InvariantCulture),
				EasingMethod = ParticleEase,
				IgnoreRotation = IgnoreRotation,
				Angle = Angle.ToString(CultureInfo.InvariantCulture),
			};

			if (Angle != 0)
			{
				emitter.Angle = Angle.ToString(CultureInfo.InvariantCulture);
			}
			if (Lifetime != null)
			{
				emitter.Lifetime = Lifetime.Serialize();
			}
			if (StartScaleX != null)
			{
				emitter.StartScaleX = StartScaleX.Serialize();
			}
			if (EndScaleX != null)
			{
				emitter.EndScaleX = EndScaleX.Serialize();
			}
			if (StartScaleY != null)
			{
				emitter.StartScaleY = StartScaleY.Serialize();
			}
			if (EndScaleY != null)
			{
				emitter.EndScaleY = EndScaleY.Serialize();
			}
			if (StartSkewX != null)
			{
				emitter.StartSkewX = StartSkewX.Serialize();
			}
			if (EndSkewX != null)
			{
				emitter.EndSkewX = EndSkewX.Serialize();
			}
			if (StartSkewY != null)
			{
				emitter.StartSkewY = StartSkewY.Serialize();
			}
			if (EndSkewY != null)
			{
				emitter.EndSkewY = EndSkewY.Serialize();
			}
			if (Speed != null)
			{
				emitter.Speed = Speed.Serialize();
			}
			if (Acceleration != null)
			{
				emitter.Acceleration = Acceleration.Serialize();
			}
			if (ForceX != null)
			{
				emitter.ForceX = ForceX.Serialize();
			}
			if (ForceY != null)
			{
				emitter.ForceY = ForceY.Serialize();
			}
			if (StartColor != null)
			{
				emitter.StartColor = StartColor.Serialize();
			}
			if (EndColor != null)
			{
				emitter.EndColor = EndColor.Serialize();
			}
			if (StartAlpha != null)
			{
				emitter.StartAlpha = StartAlpha.Serialize();
			}
			if (EndAlpha != null)
			{
				emitter.EndAlpha = EndAlpha.Serialize();
			}
			if (StartRotation != null)
			{
				emitter.StartRotation = StartRotation.Serialize();
			}
			if (EndRotation != null)
			{
				emitter.EndRotation = EndRotation.Serialize();
			}

			if (Keyframes.Count > 0)
			{
				LiveEmitterKeyframe initialFrame = Keyframes[0] as LiveEmitterKeyframe;
				if (!string.IsNullOrEmpty(initialFrame.Src))
				{
					emitter.Src = Scene.FixPath(initialFrame.Src, (Data as LiveSceneSegment).Character);
				}
				if (initialFrame.X.HasValue)
				{
					emitter.X = initialFrame.X.Value.ToString(CultureInfo.InvariantCulture);
				}
				if (initialFrame.Y.HasValue)
				{
					emitter.Y = initialFrame.Y.Value.ToString(CultureInfo.InvariantCulture);
				}
				if (initialFrame.Rotation.HasValue)
				{
					emitter.Rotation = initialFrame.Rotation.Value.ToString(CultureInfo.InvariantCulture);
				}
				if (initialFrame.Rate.HasValue)
				{
					emitter.Rate = initialFrame.Rate.Value.ToString(CultureInfo.InvariantCulture);
				}

				UpdateHistory(this, initialFrame);
			}

			return emitter;
		}
	}
}
