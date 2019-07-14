using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.EditControls;
using SPNATI_Character_Editor.EpilogueEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Scene : ICloneable
	{
		[DefaultValue(false)]
		[XmlAttribute("transition")]
		public bool Transition;

		[Text(DisplayName = "ID", GroupOrder = -2, Description = "Scene ID")]
		[XmlAttribute("id")]
		public string Id;

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

		/// <summary>
		/// Converts a LiveScene into a Scene definition
		/// </summary>
		/// <param name="pose"></param>
		public void CreateFrom(LiveScene scene)
		{
			Directives.Clear();
			Name = scene.Name;

			Background = FixPath(scene.BackgroundImage, scene.Character);
			BackgroundColor = scene.BackColor.ToHexValue();
			Width = scene.Width.ToString(CultureInfo.InvariantCulture);
			Height = scene.Height.ToString(CultureInfo.InvariantCulture);

			//Create directives for all animation blocks. We'll put them into the right places later
			Dictionary<string, WorkingDirective> createdObjects = new Dictionary<string, WorkingDirective>();
			List<WorkingDirective> directives = new List<WorkingDirective>();

			foreach (LiveObject obj in scene.Tracks)
			{
				ParseObject(obj, createdObjects, directives);
			}

			for (int i = 0; i < directives.Count; i++)
			{
				//split apart camera and fade directives
				Directive d = directives[i].Directive;
				if (d.DirectiveType == "camera")
				{
					if (!string.IsNullOrEmpty(d.Opacity) || !string.IsNullOrEmpty(d.Color))
					{
						if (string.IsNullOrEmpty(d.X) && string.IsNullOrEmpty(d.Y) && string.IsNullOrEmpty(d.Zoom))
						{
							//can simply switch directive type
							d.DirectiveType = "fade";
						}
						else
						{
							//need to make two directives
							Directive fade = new Directive();
							fade.DirectiveType = "fade";
							if (!string.IsNullOrEmpty(d.Opacity))
							{
								fade.Opacity = d.Opacity;
								d.Opacity = null;
							}
							if (!string.IsNullOrEmpty(d.Color))
							{
								fade.Color = d.Color;
								d.Color = null;
							}
							directives.Add(new WorkingDirective(fade, directives[i].StartTime));
						}
					}
				}
			}

			directives.Sort((d1, d2) =>
			{
				int compare = d1.StartTime.CompareTo(d2.StartTime);
				if (compare == 0)
				{
					DirectiveDefinition def1 = Definitions.Instance.Get<DirectiveDefinition>(d1.Directive.DirectiveType);
					DirectiveDefinition def2 = Definitions.Instance.Get<DirectiveDefinition>(d2.Directive.DirectiveType);
					int sort1 = def1?.SortOrder ?? 0;
					int sort2 = def2?.SortOrder ?? 0;
					return sort1.CompareTo(sort2);
				}
				return compare;
			});

			//float offset = 0;
			foreach (LiveBreak brk in scene.Breaks)
			{
				float time = brk.Time;

				//move everything that occurs prior to this break into the scene
				int start = directives.FindIndex(d => d.StartTime >= time);
				if (start == -1)
				{
					start = directives.Count;
				}
				for (int i = 0; i < directives.Count; i++)
				{
				}
			}
		}

		/// <summary>
		/// Corrects a path used by live objects (rooted at opponents) to the path used by scenes, which is relative to character's folder, or / if not in the character's folder
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="character"></param>
		/// <returns></returns>
		public static string FixPath(string path, Character character)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			string folderName = character.FolderName;
			int slash = path.IndexOf('/');
			if (slash >= 0)
			{
				string folder = path.Substring(0, slash);
				if (folder == folderName)
				{
					return path.Substring(slash + 1);
				}
				else
				{
					return "/opponents/" + path;
				}
			}
			return path;
		}

		private void ParseObject(LiveObject obj, Dictionary<string, WorkingDirective> createdObjects, List<WorkingDirective> directives)
		{
			Dictionary<string, Directive> activeDirectives = new Dictionary<string, Directive>();
			Dictionary<string, float> startPoints = new Dictionary<string, float>();
			List<WorkingDirective> keyframeDirectives = new List<WorkingDirective>();
			if (obj is LiveAnimatedObject)
			{
				LiveAnimatedObject anim = obj as LiveAnimatedObject;

				if (!string.IsNullOrEmpty(obj.Id) && !createdObjects.ContainsKey(obj.Id))
				{
					//creation directive
					Directive dir = anim.CreateCreationDirective(this);
					if (dir != null)
					{
						WorkingDirective d = new WorkingDirective(dir, obj.Start);
						createdObjects[obj.Id] = d;
						directives.Add(d);
					}
				}

				if (!anim.LinkedToEnd && !(anim is LiveCamera))
				{
					Directive end = new Directive();
					if (anim is LiveBubble)
					{
						end.DirectiveType = "clear";
					}
					else
					{
						end.DirectiveType = "remove";
					}
					end.Id = anim.Id;
					directives.Add(new WorkingDirective(end, obj.Start + anim.Length));
				}

				for (int i = 0; i < anim.Keyframes.Count; i++)
				{
					LiveKeyframe kf = anim.Keyframes[i];
					if (anim is LiveBubble)
					{
						if (i > 0)
						{
							LiveBubbleKeyframe textFrame = kf as LiveBubbleKeyframe;
							Directive currentDirective = new Directive();
							currentDirective.DirectiveType = "text";
							currentDirective.Id = anim.Id;
							currentDirective.Text = textFrame.Text;
							currentDirective.Marker = anim.Marker;
							directives.Add(new WorkingDirective(currentDirective, obj.Start + kf.Time));
						}
					}
					else
					{
						foreach (string property in anim.Properties)
						{
							if (kf.HasProperty(property))
							{
								Directive currentDirective = null;
								KeyframeType type = kf.GetFrameType(property);
								LiveKeyframe blockFrame = anim.GetBlockKeyframe(property, kf.Time);
								LiveKeyframeMetadata metadata = blockFrame.GetMetadata(property, false);
								string metaKey = metadata.ToKey();
								activeDirectives.TryGetValue(metaKey, out currentDirective);
								bool newBeginning = (type != KeyframeType.Normal);
								float activeTime;
								if (newBeginning && startPoints.TryGetValue(metaKey, out activeTime) && activeTime == blockFrame.Time)
								{
									newBeginning = false;
								}
								if (currentDirective == null || newBeginning)
								{
									if (currentDirective != null && type == KeyframeType.Split)
									{
										ParseKeyframe(kf, currentDirective);
									}
									activeDirectives.Remove(metaKey);

									//new directive
									currentDirective = new Directive();
									currentDirective.Id = anim.Id;
									currentDirective.Marker = anim.Marker;
									currentDirective.Z = anim.Z;

									currentDirective.EasingMethod = metadata.Ease;
									currentDirective.ClampingMethod = metadata.ClampMethod;
									currentDirective.Iterations = metadata.Iterations;
									currentDirective.Looped = metadata.Looped;
									currentDirective.InterpolationMethod = metadata.Interpolation;

									if (anim is LiveSprite || anim is LiveEmitter)
									{
										currentDirective.DirectiveType = "move";
									}
									else if (anim is LiveCamera)
									{
										currentDirective.DirectiveType = "camera";
									}

									activeDirectives[metaKey] = currentDirective;
									startPoints[metaKey] = blockFrame.Time;
									keyframeDirectives.Add(new WorkingDirective(currentDirective, obj.Start + blockFrame.Time));
								}

								ParseKeyframe(kf, currentDirective);
							}
						}
					}
				}

				for (int i = 0; i < anim.Events.Count; i++)
				{
					LiveEvent evt = anim.Events[i];
					directives.Add(new WorkingDirective(evt.CreateDirectiveDefinition(), obj.Start + evt.Time));
				}

				foreach (WorkingDirective d in keyframeDirectives)
				{
					if (d.Directive.Keyframes.Count > 1 || d.StartTime > obj.Start)
					{
						directives.Add(d);
					}
				}
			}
		}

		private void ParseKeyframe(LiveKeyframe keyframe, Directive directive)
		{
			Keyframe kf = keyframe.CreateKeyframeDefinition(directive);
			if (directive.Keyframes.IndexOf(kf) == -1)
			{
				directive.Keyframes.Add(kf);
			}
		}

		private class WorkingDirective
		{
			public Directive Directive;
			public float StartTime;

			public WorkingDirective(Directive directive, float startTime)
			{
				Directive = directive;
				StartTime = startTime;
			}

			public override string ToString()
			{
				return $"{StartTime}s - {Directive.ToString()}";
			}
		}
	}
}
