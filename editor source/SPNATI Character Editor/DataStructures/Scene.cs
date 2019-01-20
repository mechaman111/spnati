using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.EditControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Scene : ICloneable
	{
		[DefaultValue(false)]
		[XmlAttribute("transition")]
		public bool Transition;

		[Text(DisplayName = "Name", GroupOrder = -1, Description = "Scene name")]
		[XmlAttribute("name")]
		public string Name;

		[FileSelect(DisplayName = "Background", GroupOrder = 0, Description = "Scene's background image")]
		[XmlAttribute("background")]
		public string Background;

		[Color(DisplayName = "Back Color", Key = "color", GroupOrder = 1, Description = "Scene's background color")]
		[XmlAttribute("color")]
		public string BackgroundColor;

		[Measurement(DisplayName = "Camera X", GroupOrder = 10, Description = "Camera's initial X position")]
		[XmlAttribute("x")]
		public string X;

		[Measurement(DisplayName = "Camera Y", GroupOrder = 11, Description = "Camera's initial Y position")]
		[XmlAttribute("y")]
		public string Y;

		[Measurement(DisplayName = "Width", GroupOrder = 5, Description = "Scene width in pixels when at full scale", BoundProperties = new string[] { "Background" })]
		[XmlAttribute("width")]
		public string Width;

		[Measurement(DisplayName = "Height", GroupOrder = 6, Description = "Scene height in pixels when at full scale", BoundProperties = new string[] { "Background" })]
		[XmlAttribute("height")]
		public string Height;

		[Float(DisplayName = "Zoom", GroupOrder = 12, Description = "Zoom scaling factor for the camera", DecimalPlaces = 2, Minimum = 0.01f, Maximum = 100, Increment = 0.1f)]
		[XmlAttribute("zoom")]
		public string Zoom;

		[Color(DisplayName = "Fade Color", GroupOrder = 15, Description = "Initial fade overlay color")]
		[XmlAttribute("overlay")]
		public string FadeColor;

		[Slider(DisplayName = "Fade Opacity", GroupOrder = 16, Description = "Initial fade overlay opacity")]
		[XmlAttribute("overlay-alpha")]
		public string FadeOpacity;

		[XmlElement("directive")]
		public List<Directive> Directives = new List<Directive>();

		[ComboBox(DisplayName = "Effect", Key = "effect", GroupOrder = 0, Description = "Visual transition effect between scenes", 
			Options = new string[] { "dissolve", "fade", "slide-right", "slide-left", "slide-up", "slide-down", "wipe-right", "wipe-left", "wipe-up", "wipe-down",
				"push-right", "push-left", "push-up", "push-down", "uncover-right", "uncover-left", "uncover-up", "uncover-down",
				"barn-open-horizontal", "barn-close-horizontal", "barn-open-vertical", "barn-close-vertical", "fly-through", "spin" })]
		[XmlAttribute("effect")]
		public string Effect;

		[ComboBox(DisplayName = "Easing Function", Key = "ease", GroupOrder = 40, Description = "Easing function for how fast the animation progresses over time", Options = new string[] { "linear", "smooth", "ease-in", "ease-in-sin", "ease-in-cubic", "ease-out", "ease-out-sin", "ease-out-cubic", "ease-in-out-cubic", "bounce", "elastic" })]
		[XmlAttribute("ease")]
		public string EasingMethod;

		[Float(DisplayName = "Time (s)", Key = "time", GroupOrder = 1, Description = "Time in seconds since the start of the animation", Minimum = 0, Maximum = 100, Increment = 0.5f)]
		[XmlAttribute("time")]
		public string Time;

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

		[XmlIgnore]
		public bool Locked;

		public override string ToString()
		{
			string prefix = (Locked ? "🔒 " : "");
			if (Transition)
			{
				return $"{prefix}Transition: {Effect} ({Time}s)";
			}
			else
			{
				string background = string.IsNullOrEmpty(Background) ? BackgroundColor : Background;
				return $"{prefix}Scene: {Name} ({background}: {Width},{Height})";
			}
		}

		public Scene() { }
		public Scene(int width, int height)
		{
			Width = width.ToString();
			Height = height.ToString();
		}

		public Scene(bool transition)
		{
			if (transition)
			{
				Time = "1";
				Effect = "dissolve";
			}
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
				if (d.Id == id && (d.DirectiveType == "sprite" || d.DirectiveType == "text" || d.DirectiveType == "emitter"))
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

		public object Clone()
		{
			Scene clone = MemberwiseClone() as Scene;
			clone.Directives = new List<Directive>();
			foreach (Directive dir in Directives)
			{
				Directive copy = dir.Clone() as Directive;
				copy.Directive = null;
				clone.Directives.Add(copy);
			}
			return clone;
		}
	}
}
