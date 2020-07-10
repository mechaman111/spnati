using Desktop;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.Controls.EditControls;
using SPNATI_Character_Editor.EditControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Directive : Keyframe
	{
		[XmlAttribute("type")]
		public string DirectiveType;

		[Text(DisplayName = "ID", GroupOrder = 0, Key = "id", Description = "Unique identifier", Validator = "ValidateId")]
		[XmlAttribute("id")]
		public string Id;

		[DefaultValue("")]
		[XmlAttribute("parent")]
		public string ParentId;

		[DirectiveMarker(DisplayName = "Marker", GroupOrder = 0, Key = "marker", Description = "Run this directive only if the marker's condition is met", ShowPrivate = true)]
		[XmlAttribute("marker")]
		public string Marker;

		[Measurement(DisplayName = "Width", Key = "width", GroupOrder = 95, Description = "Custom sprite width relative to the scene width")]
		[XmlAttribute("width")]
		public string Width;

		[Measurement(DisplayName = "Height", Key = "height", GroupOrder = 96, Description = "Custom sprite height relative to the scene height")]
		[XmlAttribute("height")]
		public string Height;

		#region Textbox properties
		[ComboBox(DisplayName = "Arrow", Key = "arrow", GroupOrder = 30, Description = "Speech bubble arrow direction", Options = new string[] { "down", "up", "left", "right" })]
		[XmlAttribute("arrow")]
		public string Arrow;

		[HorizontalAlignment(DisplayName = "X Alignment", Key = "alignmentx", GroupOrder = 31, Description = "Horizontal alignment")]
		[XmlAttribute("alignmentx")]
		public string AlignmentX;

		[VerticalAlignment(DisplayName = "Y Alignment", Key = "alignmenty", GroupOrder = 32, Description = "Vertical alignment")]
		[XmlAttribute("alignmenty")]
		public string AlignmentY;
		#endregion

		//The engine doesn't even use this currently, so not exposing it to the property table
		[XmlAttribute("css")]
		public string Css;

		[Float(DisplayName = "Delay (s)", Key = "delay", GroupOrder = 39, Description = "Time in seconds before the animation begins", Minimum = 0, Maximum = 100, Increment = 0.5f)]
		[XmlAttribute("delay")]
		public string Delay;

		[XmlAttribute("animid")]
		public string AnimationId;

		[Boolean(DisplayName = "Looped", Key = "loop", GroupOrder = 42, Description = "If true, the animation will repeat indefinitely until stopped")]
		[DefaultValue(false)]
		[XmlAttribute("loop")]
		public bool Looped;

		[ComboBox(DisplayName = "Easing Function", Key = "ease", GroupOrder = 40, Description = "Easing function for how fast the animation progresses over time", Options = new string[] { "linear", "smooth", "ease-in", "ease-in-sin", "ease-in-cubic", "ease-out", "ease-out-sin", "ease-out-cubic", "ease-in-out-cubic", "ease-out-in", "ease-out-in-cubic", "bounce", "elastic" })]
		[XmlAttribute("ease")]
		public string EasingMethod;

		[ComboBox(DisplayName = "Tweening Function", Key = "tween", GroupOrder = 41, Description = "Tweening function for how positions between keyframes are computed", Options = new string[] { "linear", "spline", "none" })]
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
		[Numeric(DisplayName = "Layer", Key = "layer", GroupOrder = 6, Description = "Sort order layer", Minimum = -100, Maximum = 100)]
		[XmlAttribute("layer")]
		public int Layer;

		[DefaultValue(0)]
		[Numeric(DisplayName = "Layer", Key = "z", GroupOrder = 6, Description = "Sort order layer", Minimum = -100, Maximum = 100)]
		[XmlAttribute("z")]
		public int Z;

		[Text(DisplayName = "Text", Key = "text", GroupOrder = 5, Description = "Speech bubble text", RowHeight = 52, Multiline = true)]
		[XmlText]
		public string Text;

		#region Emitter attributes
		[ParticleFloat(DisplayName = "Particle Life (s)", Key = "lifetime", GroupOrder = 20, Description = "Time in seconds before an emitted object disappears", Minimum = 0.1f, Maximum = 1000, DecimalPlaces = 2)]
		[XmlAttribute("lifetime")]
		public string Lifetime;

		[Float(DisplayName = "Angle", Key = "angle", GroupOrder = 21, Description = "Degrees away from emitter's forward direction that objects can be emitted in", Minimum = -180, Maximum = 180, DecimalPlaces = 0)]
		[XmlAttribute("angle")]
		public string Angle;

		[ParticleFloat(DisplayName = "Start Scale X", Key = "startScaleX", GroupOrder = 22, Description = "Initial horizontal stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		[XmlAttribute("startScaleX")]
		public string StartScaleX;

		[ParticleFloat(DisplayName = "Start Scale Y", Key = "startScaleY", GroupOrder = 23, Description = "Initial vertical stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		[XmlAttribute("startScaleY")]
		public string StartScaleY;

		[ParticleFloat(DisplayName = "End Scale X", Key = "endScaleX", GroupOrder = 24, Description = "Ending horizontal stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		[XmlAttribute("endScaleX")]
		public string EndScaleX;

		[ParticleFloat(DisplayName = "End Scale Y", Key = "endScaleY", GroupOrder = 25, Description = "Ending vertical stretching range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 2)]
		[XmlAttribute("endScaleY")]
		public string EndScaleY;

		[ParticleFloat(DisplayName = "Speed", Key = "speed", GroupOrder = 26, Description = "Initial speed range (px/sec)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("speed")]
		public string Speed;

		[ParticleFloat(DisplayName = "Acceleration", Key = "accel", GroupOrder = 27, Description = "Initial acceleration range (px/sec^2)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("accel")]
		public string Acceleration;

		[ParticleFloat(DisplayName = "Force X", Key = "forceX", GroupOrder = 28, Description = "World horizontal force (wind) (px/sec^2)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("forceX")]
		public string ForceX;

		[ParticleFloat(DisplayName = "Force Y", Key = "forceY", GroupOrder = 29, Description = "World vertical force (gravity) (px/sec^2)", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("forceY")]
		public string ForceY;

		[ParticleColor(DisplayName = "Start Color", Key = "startColor", GroupOrder = 30, Description = "Initial particle color")]
		[XmlAttribute("startColor")]
		public string StartColor;

		[ParticleColor(DisplayName = "End Color", Key = "startColor", GroupOrder = 31, Description = "Ending particle color")]
		[XmlAttribute("endColor")]
		public string EndColor;

		[ParticleFloat(DisplayName = "Start Alpha", Key = "startAlpha", GroupOrder = 32, Description = "Initial transparency range", Minimum = 0, Maximum = 100, DecimalPlaces = 0)]
		[XmlAttribute("startAlpha")]
		public string StartAlpha;

		[ParticleFloat(DisplayName = "End Alpha", Key = "endAlpha", GroupOrder = 33, Description = "Ending transparency range", Minimum = 0, Maximum = 100, DecimalPlaces = 0)]
		[XmlAttribute("endAlpha")]
		public string EndAlpha;

		[ParticleFloat(DisplayName = "Start Spin", Key = "startRotation", GroupOrder = 34, Description = "Initial spin range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("startRotation")]
		public string StartRotation;

		[ParticleFloat(DisplayName = "End Spin", Key = "endRotation", GroupOrder = 35, Description = "Ending spin range", Minimum = -1000, Maximum = 1000, DecimalPlaces = 0)]
		[XmlAttribute("endRotation")]
		public string EndRotation;

		[ParticleFloat(DisplayName = "Start Skew X", Key = "startSkewX", GroupOrder = 36, Description = "Initial horizontal shearing range", Minimum = -89, Maximum = 89, DecimalPlaces = 2)]
		[XmlAttribute("startSkewX")]
		public string StartSkewX;

		[ParticleFloat(DisplayName = "Start Skew Y", Key = "startSkewY", GroupOrder = 36, Description = "Initial vertical shearing range", Minimum = -89, Maximum = 89, DecimalPlaces = 2)]
		[XmlAttribute("startSkewY")]
		public string StartSkewY;

		[ParticleFloat(DisplayName = "End Skew X", Key = "endSkewX", GroupOrder = 37, Description = "Ending horizontal shearing range", Minimum = -89, Maximum = 89, DecimalPlaces = 2)]
		[XmlAttribute("endSkewX")]
		public string EndSkewX;

		[ParticleFloat(DisplayName = "End Skew Y", Key = "endSkewY", GroupOrder = 37, Description = "Ending vertical shearing range", Minimum = -89, Maximum = 89, DecimalPlaces = 2)]
		[XmlAttribute("endSkewY")]
		public string EndSkewY;

		[DefaultValue(0)]
		[Numeric(DisplayName = "Count", Key = "count", GroupOrder = 10, Description = "Number of particles to emit", Minimum = 1, Maximum = 100)]
		[XmlAttribute("count")]
		public int Count;

		[Boolean(DisplayName = "Ignore rotation", Key = "ignoreRotation", GroupOrder = 19, Description = "If true, particles will spawn facing upwards regardless of the emitter's current rotation")]
		[DefaultValue(false)]
		[XmlAttribute("ignoreRotation")]
		public bool IgnoreRotation;
		#endregion

		[Text(DisplayName = "Title", Key = "title", GroupOrder = -1, Description = "User prompt title text", RowHeight = 52, Multiline = true)]
		[XmlElement("title")]
		public string Title;

		[XmlElement("keyframe")]
		public List<Keyframe> Keyframes = new List<Keyframe>();

		[XmlElement("choice")]
		public List<Choice> Choices = new List<Choice>();

		public Directive() { }

		public Directive(string type)
		{
			DirectiveType = type;
		}

		public override string ToString()
		{
			string prefix = (Locked ? "🔒 " : "");
			string text = DirectiveType;
			switch (DirectiveType)
			{
				case "sprite":
					text = $"Add sprite ({Id}) {(!string.IsNullOrEmpty(Src) ? "- " + Src : "")} ({X},{Y})";
					break;
				case "move":
					string time = string.IsNullOrEmpty(Time) ? "" : $"{Time}s";
					string delay = string.IsNullOrEmpty(Delay) ? "" : $" delay {Delay}s";
					string loop = Looped ? " (loop)" : "";
					string props = GetProperties();
					text = $"Animate ({Id}) {time}{loop}{delay}{(props.Length > 0 ? "-" + props : "")}";
					break;
				case "wait":
					text = "Wait for Animations";
					break;
				case "pause":
					text = "Pause";
					break;
				case "text":
					text = $"{Id}: {(Text ?? "Speech Bubble")}";
					break;
				case "clear":
					text = $"Clear bubble {Id}";
					break;
				case "fade":
					if (Keyframes.Count > 0)
					{
						text = "Fade";
					}
					else
					{
						time = string.IsNullOrEmpty(Time) ? "" : $"{Time}s";
						loop = Looped ? " (loop)" : "";
						text = $"Fade to {Color} (opacity: {Opacity}) {time}{loop}";
					}
					break;
				case "clear-all":
					text = "Clear all bubbles";
					break;
				case "camera":
					time = string.IsNullOrEmpty(Time) ? "" : $"{Time}s";
					loop = Looped ? " (loop)" : "";
					props = GetProperties();
					text = $"Camera {time}{loop}{(props.Length > 0 ? "-" + props : "")}";
					break;
				case "stop":
					text = $"Stop {Id} {(!string.IsNullOrEmpty(AnimationId) ? "- " + AnimationId : "")}";
					break;
				case "remove":
					text = $"Remove {Id}";
					break;
				case "emitter":
					text = $"Add emitter ({Id}) {(!string.IsNullOrEmpty(Src) ? "- " + Src : "")} ({X},{Y})";
					break;
				case "emit":
					text = $"Emit {Id} ({Count})";
					break;
				case "jump":
					text = $"Jump to scene {Id}";
					break;
				case "prompt":
					text = $"Prompt: {Title}";
					break;
			}

			return $"{prefix}{text}";
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
			clone.Choices = new List<Choice>();
			foreach (Choice c in Choices)
			{
				Choice clonedChoice = c.Clone() as Choice;
				clonedChoice.Directive = clone;
				clone.Choices.Add(clonedChoice);
			}
			return clone;
		}

		public string ValidateId(string id, object context)
		{
			if (string.IsNullOrEmpty(id))
			{
				return null;
			}

			//check for reserved variables
			if (DirectiveType == "sprite" || DirectiveType == "text" || DirectiveType == "emitter")
			{
				if (id == "background" || id == "camera" || id == "fade")
				{
					return $"{id} is a reserved ID and cannot be used here.";
				}
			}

			EpilogueContext cxt = context as EpilogueContext;
			if (cxt == null || cxt.Scene == null)
			{
				return null;
			}
			Scene scene = cxt.Scene;

			HashSet<string> usedIds = new HashSet<string>();
			usedIds.Add("camera");
			usedIds.Add("fade");
			usedIds.Add("background");

			//make sure this is the first "add" directive with this ID
			for (int i = 0; i < scene.Directives.Count; i++)
			{
				Directive directive = scene.Directives[i];
				if (directive == this)
				{
					if (DirectiveType == "sprite" || DirectiveType == "text" || DirectiveType == "emitter")
					{
						if (usedIds.Contains(id))
						{
							return $"{id} is already used by an earlier object in the scene.";
						}
					}
					else
					{
						if (!usedIds.Contains(id))
						{
							return $"No earlier directive defines an object with this {id}.";
						}
					}
					break;
				}
				else if (!string.IsNullOrEmpty(directive.Id))
				{
					usedIds.Add(directive.Id);
				}
			}
			return null;
		}

		/// <summary>
		/// Copies all keyframes into another directive
		/// </summary>
		/// <param name="destination"></param>
		public void CopyInto(Directive destination)
		{
			MergeInto(destination);
			foreach (Keyframe kf in Keyframes)
			{
				float srcTime;
				float.TryParse(kf.Time, out srcTime);

				bool found = false;
				for (int i = 0; i < destination.Keyframes.Count; i++)
				{
					Keyframe destFrame = destination.Keyframes[i];
					float destTime;
					float.TryParse(destFrame.Time, out destTime);
					if (srcTime == destTime)
					{
						//found a matching frame
						kf.MergeInto(destFrame);
						found = true;
						break;
					}
					else if (destTime > srcTime)
					{
						//passed where the frame would go, so insert it
						destination.Keyframes.Insert(i, kf);
						found = true;
						break;
					}
				}
				if (!found)
				{
					//can copy the whole frame over directly
					destination.Keyframes.Add(kf);
				}
			}
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

		[Float(DisplayName = "Rate", Key = "rate", GroupOrder = 19, Description = "Emissions per second", Minimum = 0, Maximum = 100, DecimalPlaces = 2)]
		[XmlAttribute("rate")]
		public string Rate;

		[FileSelect(DisplayName = "Source", GroupOrder = 5, Key = "src", Description = "Sprite source image")]
		[XmlAttribute("src")]
		public string Src;

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

		[Float(DisplayName = "Skew X", Key = "skewx", GroupOrder = 22, Description = "Sprite shearing factor horizontally", DecimalPlaces = 2, Minimum = -89, Maximum = 89, Increment = 1f)]
		[XmlAttribute("skewx")]
		public string SkewX;

		[Float(DisplayName = "Skew Y", Key = "skewy", GroupOrder = 23, Description = "Sprite shearing factor vertically", DecimalPlaces = 2, Minimum = -89, Maximum = 89, Increment = 1f)]
		[XmlAttribute("skewy")]
		public string SkewY;

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

		[XmlIgnore]
		public Dictionary<string, object> Properties = new Dictionary<string, object>();

		[XmlIgnore]
		public bool Locked;

		/// <summary>
		/// Gets whether any animatable properties are currently set
		/// </summary>
		/// <returns></returns>
		public bool HasAnimatableProperties()
		{
			return !string.IsNullOrEmpty(X) || !string.IsNullOrEmpty(Y) || !string.IsNullOrEmpty(Scale) || !string.IsNullOrEmpty(ScaleX) || !string.IsNullOrEmpty(ScaleY)
				 || !string.IsNullOrEmpty(Color) || !string.IsNullOrEmpty(Opacity) || !string.IsNullOrEmpty(Rotation) || !string.IsNullOrEmpty(Zoom) || !string.IsNullOrEmpty(Src)
				 || !string.IsNullOrEmpty(SkewX) || !string.IsNullOrEmpty(SkewY);
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
			SkewX = src.SkewX;
			SkewY = src.SkewY;
			PivotX = src.PivotX;
			PivotY = src.PivotY;
			Color = src.Color;
			Opacity = src.Opacity;
			Rotation = src.Rotation;
			Zoom = src.Zoom;
			Src = src.Src;

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
			src.SkewX = null;
			src.SkewY = null;
			src.Src = null;

			Properties = src.Properties;
			src.Properties = new Dictionary<string, object>();
		}

		/// <summary>
		/// Moves all non-null properties from this frame into another one
		/// </summary>
		/// <param name="dest"></param>
		public void MergeInto(Keyframe dest)
		{
			foreach (KeyValuePair<string, object> kvp in Properties)
			{
				dest.Properties[kvp.Key] = kvp.Value;
			}
		}

		/// <summary>
		/// "Bakes" the properties dictionary into the actual property fields
		/// </summary>
		public void BakeProperties()
		{
			Type type = GetType();
			foreach (KeyValuePair<string, object> kvp in Properties)
			{
				string property = kvp.Key;
				object value = kvp.Value;
				FieldInfo fi = PropertyTypeInfo.GetFieldInfo(type, property);
				fi.SetValue(this, value);
			}
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
			if (!string.IsNullOrEmpty(SkewX) || !string.IsNullOrEmpty(SkewY))
			{
				sb.Append($" Shear:{SkewX},{SkewY}");
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
			if (!string.IsNullOrEmpty(Src))
			{
				sb.Append($" Src:{Src}");
			}
			return sb.ToString();
		}

		public override string ToString()
		{
			string prefix = (Locked ? "🔒 " : "");
			string props = GetProperties();
			if (string.IsNullOrEmpty(props))
			{
				props = " new frame";
			}
			return $"{prefix}@{Time}s -{props}";
		}

		public virtual object Clone()
		{
			return MemberwiseClone();
		}
	}
}
