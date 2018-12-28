using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.EditControls;

namespace SPNATI_Character_Editor
{
	public class Scene
	{
		[FileSelect(DisplayName = "Background", Description = "Scene's background image", Default = true)]
		[XmlAttribute("background")]
		public string Background;

		[Color(DisplayName = "Back Color", Description = "Scene's background color")]
		[XmlAttribute("color")]
		public string BackgroundColor;

		[Measurement(DisplayName = "Camera X", Description = "Camera's initial X position")]
		[XmlAttribute("x")]
		public string X;

		[Measurement(DisplayName = "Camera Y", Description = "Camera's initial Y position")]
		[XmlAttribute("y")]
		public string Y;

		[Measurement(DisplayName = "Width", Description = "Scene width in pixels when at full scale", Required = true)]
		[XmlAttribute("width")]
		public string Width;

		[Measurement(DisplayName = "Height", Description = "Scene height in pixels when at full scale", Required = true)]
		[XmlAttribute("height")]
		public string Height;

		[Float(DisplayName = "Zoom", Description = "Zoom scaling factor for the camera", DecimalPlaces = 2, Minimum = 0.01f, Maximum = 100, Increment = 0.1f, DefaultValue = "1")]
		[XmlAttribute("zoom")]
		public string Zoom;

		[Color(DisplayName = "Fade Color", Description = "Initial fade overlay color")]
		[XmlAttribute("overlay")]
		public string FadeColor;

		[Slider(DisplayName = "Fade Opacity", Description = "Initial fade overlay opacity")]
		[XmlAttribute("overlay-alpha")]
		public string FadeOpacity;

		[XmlElement("directive")]
		public List<Directive> Directives = new List<Directive>();

		#region Legacy properties
		[XmlAttribute("background-zoom")]
		public string LegacyZoom;

		[XmlAttribute("background-position-x")]
		public string LegacyX;

		[XmlAttribute("background-position-y")]
		public string LegacyY;

		[XmlElement("sprite")]
		public List<EndingSprite> LegacySprites = new List<EndingSprite>();

		[XmlElement("text")]
		public List<EndingText> LegacyText = new List<EndingText>();
		#endregion

		public override string ToString()
		{
			string background = string.IsNullOrEmpty(Background) ? BackgroundColor : Background;
			return $"Scene: {background} ({Width},{Height})";
		}

		public Scene() { }
		public Scene(int width, int height)
		{
			Width = width.ToString();
			Height = height.ToString();
		}

		/// <summary>
		/// Gets the directive that creates an object with the given ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Directive GetDirective(string id)
		{
			for (int i = 0; i < Directives.Count; i++)
			{
				Directive d = Directives[i];
				if (d.Id == id && (d.DirectiveType == "sprite" || d.DirectiveType == "text"))
				{
					return d;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the last directive/keyframe in the scene that modifies a particular object ID
		/// </summary>
		/// <param name="id">ID of the object to look for</param>
		/// <param name="start">Start the search from before this point</param>
		/// <returns>The frame that last touched this object, or start if none was found</returns>
		public Keyframe GetLastFrame(string id, Directive start)
		{
			int index = Directives.IndexOf(start) - 1;
			if (index < 0)
			{
				return start;
			}
			for (; index >= 0; index--)
			{
				Directive d = Directives[index];
				if (d.Id == id || d.DirectiveType == "camera" && id == "camera")
				{
					if (d.Keyframes.Count > 0)
					{
						return d.Keyframes[d.Keyframes.Count - 1];
					}
					return d;
				}
			}
			return start;
		}
	}

	public class Directive : Keyframe
	{
		[XmlAttribute("type")]
		public string DirectiveType;

		[Text(DisplayName = "Id", Key = "id", Description = "Unique identifier")]
		[XmlAttribute("id")]
		public string Id;

		[FileSelect(DisplayName = "Source", Key = "source", Description = "Sprite source image")]
		[XmlAttribute("src")]
		public string Src;

		[Measurement(DisplayName = "Width", Key = "width", Description = "Custom sprite width relative to the scene width")]
		[XmlAttribute("width")]
		public string Width;

		[Measurement(DisplayName = "Height", Key = "height", Description = "Custom sprite height relative to the scene height")]
		[XmlAttribute("height")]
		public string Height;

		[ComboBox(DisplayName = "Arrow", Key = "arrow", Description = "Speech bubble arrow direction", Default = true, Options = new string[] { "down", "up", "left", "right" })]
		[XmlAttribute("arrow")]
		public string Arrow;

		//The engine doesn't even use this currently, so not exposing it to the property table
		[XmlAttribute("css")]
		public string Css;

		[Boolean(DisplayName = "Looped", Key = "loop", GroupName = "Animation", GroupOrder = 4, Description = "If true, the animation will repeat indefinitely until stopped")]
		[XmlAttribute("loop")]
		public bool Looped;

		[ComboBox(DisplayName = "Easing Function", Key = "ease", GroupName = "Animation", GroupOrder = 2, Description = "Easing function for how fast the animation progresses over time", Options = new string[] { "linear", "smooth", "ease-in", "ease-out" })]
		[XmlAttribute("ease")]
		public string EasingMethod;

		[ComboBox(DisplayName = "Tweening Function", Key = "tween", GroupName = "Animation", GroupOrder = 3, Description = "Tweening function for how positions between keyframes are computed", Options = new string[] { "linear", "spline" })]
		[XmlAttribute("interpolation")]
		public string InterpolationMethod;

		[Text(DisplayName = "Text", Key = "text", Description = "Speech bubble text", RowHeight = 52, Multiline = true)]
		[XmlText]
		public string Text;

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
					return $"Add sprite ({Id}) {(!string.IsNullOrEmpty(Src) ? "- " + Src : "")}";
				case "move":
					string time = string.IsNullOrEmpty(Time) ? "" : $"{Time}s";
					string loop = Looped ? " (loop)" : "";
					string props = GetProperties();
					return $"Move sprite ({Id}) {time}{loop}{(props.Length > 0 ? "-" + props : "")}";
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
	}

	/// <summary>
	/// Animatable properties
	/// </summary>
	public class Keyframe
	{
		/// <summary>
		/// Parent directive
		/// </summary>
		[XmlIgnore]
		public Directive Directive { get; set; }

		[Float(DisplayName = "Time (s)", Key = "time", GroupName = "Animation", GroupOrder = 0, Description = "Time in seconds since the start of the animation", Minimum = 0, Maximum = 100, Increment = 0.5f)]
		[XmlAttribute("time")]
		public string Time;

		[Measurement(DisplayName = "X", Key = "x", Description = "Scene X position")]
		[XmlAttribute("x")]
		public string X;

		[Measurement(DisplayName = "Y", Key = "y", Description = "Scene Y position")]
		[XmlAttribute("y")]
		public string Y;

		[Float(DisplayName = "Scale", Key = "scale", Description = "Sprite scaling factor", DecimalPlaces = 2, Minimum = 0.01f, Maximum = 100, Increment = 0.1f, DefaultValue = "1")]
		[XmlAttribute("scale")]
		public string Scale;

		[Color(DisplayName = "Color", Key = "color", Description = "Color")]
		[XmlAttribute("color")]
		public string Color;

		[Slider(DisplayName = "Opacity (0-100)", Key = "alpha", Description = "Opacity/transparency level")]
		[XmlAttribute("alpha")]
		public string Opacity;

		[Float(DisplayName = "Rotation (deg)", Key = "rotation", Description = "Sprite rotation", DecimalPlaces = 0, Minimum = -7020, Maximum = 7020)]
		[XmlAttribute("rotation")]
		public string Rotation;

		[Float(DisplayName = "Zoom", Key = "zoom", Description = "Zoom scaling factor for the camera", DecimalPlaces = 2, Minimum = 0.01f, Maximum = 100, Increment = 0.1f, DefaultValue = "1")]
		[XmlAttribute("zoom")]
		public string Zoom;

		/// <summary>
		/// Gets whether any animatable properties are currently set
		/// </summary>
		/// <returns></returns>
		public bool HasAnimatableProperties()
		{
			return !string.IsNullOrEmpty(X) || !string.IsNullOrEmpty(Y) || !string.IsNullOrEmpty(Scale)
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
			if (!string.IsNullOrEmpty(Scale))
			{
				sb.Append($" Scale:{Scale}");
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
	}
}
