using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.EditControls;
using System.ComponentModel;
using System;
using SPNATI_Character_Editor.Controls.EditControls;

namespace SPNATI_Character_Editor
{
	public class Directive : Keyframe
	{
		[XmlAttribute("type")]
		public string DirectiveType;

		[Text(DisplayName = "ID", GroupOrder = 0, Key = "id", Description = "Unique identifier")]
		[XmlAttribute("id")]
		public string Id;

		[FileSelect(DisplayName = "Source", GroupOrder = 5, Key = "src", Description = "Sprite source image")]
		[XmlAttribute("src")]
		public string Src;

		[Measurement(DisplayName = "Width", Key = "width", GroupOrder = 95, Description = "Custom sprite width relative to the scene width")]
		[XmlAttribute("width")]
		public string Width;

		[Measurement(DisplayName = "Height", Key = "height", GroupOrder = 96, Description = "Custom sprite height relative to the scene height")]
		[XmlAttribute("height")]
		public string Height;

		[ComboBox(DisplayName = "Arrow", Key = "arrow", GroupOrder = 30, Description = "Speech bubble arrow direction", Default = true, Options = new string[] { "down", "up", "left", "right" })]
		[XmlAttribute("arrow")]
		public string Arrow;

		//The engine doesn't even use this currently, so not exposing it to the property table
		[XmlAttribute("css")]
		public string Css;

		[Float(DisplayName = "Delay (s)", Key = "delay", GroupOrder = 39, Description = "Time in seconds before the animation begins", Minimum = 0, Maximum = 100, Increment = 0.5f)]
		[XmlAttribute("delay")]
		public string Delay;

		[Boolean(DisplayName = "Looped", Key = "loop", GroupOrder = 42, Description = "If true, the animation will repeat indefinitely until stopped")]
		[DefaultValue(false)]
		[XmlAttribute("loop")]
		public bool Looped;

		[ComboBox(DisplayName = "Easing Function", Key = "ease", GroupOrder = 40, Description = "Easing function for how fast the animation progresses over time", Options = new string[] { "linear", "smooth", "ease-in", "ease-in-sin", "ease-in-cubic", "ease-out", "ease-out-sin", "ease-out-cubic", "ease-in-out-cubic", "bounce", "elastic" })]
		[XmlAttribute("ease")]
		public string EasingMethod;

		[ComboBox(DisplayName = "Tweening Function", Key = "tween", GroupOrder = 41, Description = "Tweening function for how positions between keyframes are computed", Options = new string[] { "linear", "spline" })]
		[XmlAttribute("interpolation")]
		public string InterpolationMethod;

		[ComboBox(DisplayName = "Repeat Method", Key = "clamp", GroupOrder = 43, Description = "How a looping animation loops", Options = new string[] { "clamp", "wrap", "mirror" })]
		[XmlAttribute("clamp")]
		public string ClampingMethod;

		[DefaultValue(0)]
		[Numeric(DisplayName = "Iterations", Key = "iterations", GroupOrder = 44, Description = "How many times to repeat a looped animation. 0 means infinite.", Minimum = 0, Maximum = 1000)]
		[XmlAttribute("iterations")]
		public int Iterations;

		[DefaultValue(0)]
		[Numeric(DisplayName = "Layer", Key = "layer", GroupOrder = 6, Description = "Sort order layer", Minimum = 0, Maximum = 100)]
		[XmlAttribute("layer")]
		public int Layer;

		[Text(DisplayName = "Text", Key = "text", GroupOrder = 5, Description = "Speech bubble text", RowHeight = 52, Multiline = true)]
		[XmlText]
		public string Text;

		#region Emitter attributes
		[Float(DisplayName = "Rate", Key = "rate", GroupOrder = 100, Description = "Emissions per second", Minimum = 0, Maximum = 100, DecimalPlaces = 2)]
		[XmlAttribute("rate")]
		public string Rate;

		[ParticleFloat(DisplayName = "Start Scale X", Key = "startScaleX", GroupOrder = 20, Description = "Initial horizontal stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		[XmlAttribute("startScaleX")]
		public string StartScaleX;

		[ParticleFloat(DisplayName = "Start Scale Y", Key = "startScaleY", GroupOrder = 21, Description = "Initial vertical stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		[XmlAttribute("startScaleY")]
		public string StartScaleY;

		[ParticleFloat(DisplayName = "End Scale X", Key = "endScaleX", GroupOrder = 22, Description = "Ending horizontal stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		[XmlAttribute("endScaleX")]
		public string EndScaleX;

		[ParticleFloat(DisplayName = "End Scale Y", Key = "endScaleY", GroupOrder = 23, Description = "Ending vertical stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		[XmlAttribute("endScaleY")]
		public string EndScaleY;

		[ParticleFloat(DisplayName = "Speed", Key = "speed", GroupOrder = 24, Description = "Initial speed range (px/sec)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("speed")]
		public string Speed;

		[ParticleFloat(DisplayName = "Acceleration", Key = "accel", GroupOrder = 25, Description = "Initial acceleration range (px/sec^2)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("accel")]
		public string Acceleration;

		[ParticleFloat(DisplayName = "Force X", Key = "forceX", GroupOrder = 26, Description = "World horizontal force (wind) (px/sec^2)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("forceX")]
		public string ForceX;

		[ParticleFloat(DisplayName = "Force Y", Key = "forceY", GroupOrder = 27, Description = "World vertical force (gravity) (px/sec^2)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("forceY")]
		public string ForceY;

		[ParticleColor(DisplayName = "Start Color", Key = "startColor", GroupOrder = 28, Description = "Initial particle color")]
		[XmlAttribute("startColor")]
		public string StartColor;

		[ParticleColor(DisplayName = "End Color", Key = "startColor", GroupOrder = 29, Description = "Ending particle color")]
		[XmlAttribute("endColor")]
		public string EndColor;

		[ParticleFloat(DisplayName = "Start Alpha", Key = "startAlpha", GroupOrder = 30, Description = "Initial transparency range", Minimum = 0, Maximum = 100, DecimalPlaces = 0)]
		[XmlAttribute("startAlpha")]
		public string StartAlpha;

		[ParticleFloat(DisplayName = "End Alpha", Key = "endAlpha", GroupOrder = 31, Description = "Ending transparency range", Minimum = 0, Maximum = 100, DecimalPlaces = 0)]
		[XmlAttribute("endAlpha")]
		public string EndAlpha;

		[ParticleFloat(DisplayName = "Start Spin", Key = "startRotation", GroupOrder = 32, Description = "Initial spin range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("startRotation")]
		public string StartRotation;

		[ParticleFloat(DisplayName = "End Spin", Key = "endRotation", GroupOrder = 33, Description = "Ending spin range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("endRotation")]
		public string EndRotation;

		[ParticleFloat(DisplayName = "Particle Life (s)", Key = "lifetime", GroupOrder = 34, Description = "Time in seconds before an emitted object disappears", Minimum = 0.1f, Maximum = 1000, DecimalPlaces = 2)]
		[XmlAttribute("lifetime")]
		public string Lifetime;

		[Float(DisplayName = "Angle", Key = "angle", GroupOrder = 35, Description = "Degrees away from emitter's forward direction that objects can be emitted in", Minimum = -180, Maximum = 180, DecimalPlaces = 0)]
		[XmlAttribute("angle")]
		public string Angle;
		#endregion

		[XmlElement("keyframe")]
		public List<Keyframe> Keyframes = new List<Keyframe>();

		public Directive() { }

		public Directive(string type)
		{
			DirectiveType = type;
		}

		public override string ToString()
		{
			switch (DirectiveType)
			{
				case "sprite":
					return $"Add sprite ({Id}) {(!string.IsNullOrEmpty(Src) ? "- " + Src : "")} ({X},{Y})";
				case "move":
					string time = string.IsNullOrEmpty(Time) ? "" : $"{Time}s";
					string delay = string.IsNullOrEmpty(Delay) ? "" : $" delay {Delay}s";
					string loop = Looped ? " (loop)" : "";
					string props = GetProperties();
					return $"Move sprite ({Id}) {time}{loop}{delay}{(props.Length > 0 ? "-" + props : "")}";
				case "wait":
					return "Wait for Animations";
				case "pause":
					return "Pause";
				case "text":
					return $"{Id}: {(Text ?? "Speech Bubble")}";
				case "clear":
					return $"Clear bubble {Id}";
				case "fade":
					if (Keyframes.Count > 0)
					{
						return "Fade";
					}
					else
					{
						time = string.IsNullOrEmpty(Time) ? "" : $"{Time}s";
						loop = Looped ? " (loop)" : "";
						return $"Fade to {Color} (opacity: {Opacity}) {time}{loop}";
					}
				case "clear-all":
					return "Clear all bubbles";
				case "camera":
					time = string.IsNullOrEmpty(Time) ? "" : $"{Time}s";
					loop = Looped ? " (loop)" : "";
					props = GetProperties();
					return $"Camera {time}{loop}{(props.Length > 0 ? "-" + props : "")}";
				case "stop":
					return $"Stop {Id}";
				case "remove":
					return $"Remove {Id}";
				case "emitter":
					return $"Add emitter ({Id}) {(!string.IsNullOrEmpty(Src) ? "- " + Src : "")} ({X},{Y})";
				default:
					return DirectiveType;
			}
		}

		[XmlIgnore]
		public string Duration
		{
			get
			{
				if (Keyframes.Count > 0)
				{
					return Keyframes[Keyframes.Count - 1].Time;
				}
				return Time;
			}
		}

		public override object Clone()
		{
			Directive clone = MemberwiseClone() as Directive;
			clone.Keyframes = new List<Keyframe>();
			foreach (Keyframe kf in Keyframes)
			{
				Keyframe clonedFrame = kf.Clone() as Keyframe;
				clonedFrame.Directive = clone;
				clone.Keyframes.Add(clonedFrame);
			}
			return clone;
		}
	}

	/// <summary>
	/// Animatable properties
	/// </summary>
	public class Keyframe : ICloneable
	{
		/// <summary>
		/// Parent directive
		/// </summary>
		[XmlIgnore]
		public Directive Directive { get; set; }

		[Float(DisplayName = "Time (s)", Key = "time", GroupOrder = 1, Description = "Time in seconds since the start of the animation", Minimum = 0, Maximum = 100, Increment = 0.5f)]
		[XmlAttribute("time")]
		public string Time;

		[Measurement(DisplayName = "X", Key = "x", GroupOrder = 10, Description = "Scene X position")]
		[XmlAttribute("x")]
		public string X;

		[Measurement(DisplayName = "Y", Key = "y", GroupOrder = 11, Description = "Scene Y position")]
		[XmlAttribute("y")]
		public string Y;

		[XmlAttribute("scale")]
		public string Scale;

		[Float(DisplayName = "Scale X", Key = "scalex", GroupOrder = 17, Description = "Sprite scaling factor horizontally", DecimalPlaces = 2, Minimum = -100, Maximum = 100, Increment = 0.1f)]
		[XmlAttribute("scalex")]
		public string ScaleX;

		[Float(DisplayName = "Scale Y", Key = "scaley", GroupOrder = 17, Description = "Sprite scaling factor vertically", DecimalPlaces = 2, Minimum = -100, Maximum = 100, Increment = 0.1f)]
		[XmlAttribute("scaley")]
		public string ScaleY;

		[Measurement(DisplayName = "Pivot X", Key = "pivotx", GroupOrder = 13, Description = "X value of rotation/scale point of origin as a percentage of the sprite's physical size", Minimum = -1000, Maximum = 1000)]
		[XmlAttribute("pivotx")]
		public string PivotX;

		[Measurement(DisplayName = "Pivot Y", Key = "pivoty", GroupOrder = 14, Description = "Y value of Rotation/scale point of origin as a percentage of the sprite's physical size", Minimum = -1000, Maximum = 1000)]
		[XmlAttribute("pivoty")]
		public string PivotY;

		[Color(DisplayName = "Color", Key = "color", GroupOrder = 20, Description = "Color")]
		[XmlAttribute("color")]
		public string Color;

		[Slider(DisplayName = "Opacity (0-100)", Key = "alpha", GroupOrder = 21, Description = "Opacity/transparency level")]
		[XmlAttribute("alpha")]
		public string Opacity;

		[Float(DisplayName = "Rotation (deg)", Key = "rotation", GroupOrder = 18, Description = "Sprite rotation", DecimalPlaces = 0, Minimum = -7020, Maximum = 7020)]
		[XmlAttribute("rotation")]
		public string Rotation;

		[Float(DisplayName = "Zoom", Key = "zoom", GroupOrder = 17, Description = "Zoom scaling factor for the camera", DecimalPlaces = 2, Minimum = 0.01f, Maximum = 100, Increment = 0.1f)]
		[XmlAttribute("zoom")]
		public string Zoom;

		/// <summary>
		/// Gets whether any animatable properties are currently set
		/// </summary>
		/// <returns></returns>
		public bool HasAnimatableProperties()
		{
			return !string.IsNullOrEmpty(X) || !string.IsNullOrEmpty(Y) || !string.IsNullOrEmpty(Scale) || !string.IsNullOrEmpty(ScaleX) || !string.IsNullOrEmpty(ScaleY)
				 || !string.IsNullOrEmpty(Color) || !string.IsNullOrEmpty(Opacity) || !string.IsNullOrEmpty(Rotation) || !string.IsNullOrEmpty(Zoom);
		}

		public Keyframe() { }

		/// <summary>
		/// Moves animatable properties from another keyframe (or directive) into this keyframe
		/// </summary>
		/// <param name="src"></param>
		public void TransferPropertiesFrom(Keyframe src)
		{
			Time = src.Time;
			X = src.X;
			Y = src.Y;
			Scale = src.Scale;
			ScaleX = src.ScaleX;
			ScaleY = src.ScaleY;
			PivotX = src.PivotX;
			PivotY = src.PivotY;
			Color = src.Color;
			Opacity = src.Opacity;
			Rotation = src.Rotation;
			Zoom = src.Zoom;

			src.Time = null;
			src.X = null;
			src.Y = null;
			src.Scale = null;
			src.Color = null;
			src.Opacity = null;
			src.Rotation = null;
			src.Zoom = null;
			src.ScaleX = null;
			src.ScaleY = null;
		}

		public string GetProperties()
		{
			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(X))
			{
				sb.Append($" X:{X}");
			}
			if (!string.IsNullOrEmpty(Y))
			{
				sb.Append($" Y:{Y}");
			}
			if (!string.IsNullOrEmpty(ScaleX) || !string.IsNullOrEmpty(ScaleY))
			{
				sb.Append($" Scale:{ScaleX},{ScaleY}");
			}
			if (!string.IsNullOrEmpty(Color))
			{
				sb.Append($" Color:{Color}");
			}
			if (!string.IsNullOrEmpty(Opacity))
			{
				sb.Append($" Opacity:{Opacity}");
			}
			if (!string.IsNullOrEmpty(Rotation))
			{
				sb.Append($" Rotation:{Rotation.Split('.')[0]}");
			}
			if (!string.IsNullOrEmpty(Zoom))
			{
				sb.Append($" Zoom:{Zoom}");
			}
			return sb.ToString();
		}

		public override string ToString()
		{
			string props = GetProperties();
			if (string.IsNullOrEmpty(props))
			{
				props = " new frame";
			}
			return $"@{Time}s -{props}";
		}

		public virtual object Clone()
		{
			return MemberwiseClone();
		}
	}
}
