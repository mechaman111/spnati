using Desktop;
using Desktop.CommonControls;
using Desktop.CommonControls.PropertyControls;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.EditControls;
using SPNATI_Character_Editor.EpilogueEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
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

		[Measurement(DisplayName = "Width", GroupOrder = 5, Description = "Scene width in pixels when at full scale", BoundProperties = new string[] { "Background" }, AllowPercentages = false)]
		[XmlAttribute("width")]
		public string Width;

		[Measurement(DisplayName = "Height", GroupOrder = 6, Description = "Scene height in pixels when at full scale", BoundProperties = new string[] { "Background" }, AllowPercentages = false)]
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

		public int UniqueId = 1;
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

		public string SceneName
		{
			get { return Name ?? ToString(); }
		}

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
		/// Splits alpha/color out of X/Y/zoom on a camera directive
		/// </summary>
		/// <param name="d"></param>
		/// <returns>The new fade directive if one needed to be created</returns>
		private WorkingDirective SplitFadeDirective(WorkingDirective directive)
		{
			Directive d = directive.Directive;
			List<Keyframe> frames = new List<Keyframe>();
			frames.Add(d);
			frames.AddRange(d.Keyframes);

			bool hasFade = false;
			bool hasCamera = false;
			foreach (Keyframe kf in frames)
			{
				if (!string.IsNullOrEmpty(kf.Opacity) || !string.IsNullOrEmpty(kf.Color))
				{
					hasFade = true;
				}
				if (!string.IsNullOrEmpty(kf.X) || !string.IsNullOrEmpty(kf.Y) || !string.IsNullOrEmpty(kf.Zoom))
				{
					hasCamera = true;
				}
				if (hasFade && hasCamera)
				{
					break;
				}
			}
			if (hasFade)
			{
				if (!hasCamera)
				{
					//no camera properties so this can just be swapped to a fade directive
					d.DirectiveType = "fade";
					return null;
				}
				else
				{
					//need to copy the directive and only include the fade properties
					Directive fade = new Directive("fade");
					fade.Time = d.Time;
					fade.Delay = d.Delay;
					fade.InterpolationMethod = d.InterpolationMethod;
					fade.Looped = d.Looped;
					fade.EasingMethod = d.EasingMethod;
					fade.ClampingMethod = d.ClampingMethod;
					fade.Opacity = d.Opacity;
					fade.AnimationId = d.AnimationId;
					d.Opacity = null;
					fade.Color = d.Color;
					d.Color = null;

					for (int i = 0; i < d.Keyframes.Count; i++)
					{
						Keyframe kf = d.Keyframes[i];
						if (!string.IsNullOrEmpty(kf.Opacity) || !string.IsNullOrEmpty(kf.Color))
						{
							Keyframe fadeFrame = new Keyframe();
							fadeFrame.Time = kf.Time;
							fadeFrame.Opacity = kf.Opacity;
							fadeFrame.Color = kf.Color;
							kf.Opacity = null;
							kf.Color = null;
							fade.Keyframes.Add(fadeFrame);

							if (string.IsNullOrEmpty(kf.X) && string.IsNullOrEmpty(kf.Y) && string.IsNullOrEmpty(kf.Zoom))
							{
								//frame is no longer needed in the original
								d.Keyframes.RemoveAt(i--);
							}
						}
					}
					return new WorkingDirective(fade, directive.StartTime);
				}
			}
			return null;
		}

		/// <summary>
		/// Converts a LiveScene into a Scene definition
		/// </summary>
		/// <param name="pose"></param>
		public void CreateFrom(LiveScene scene)
		{
			UniqueId = 1;
			Directives.Clear();
			Name = scene.Name;

			Background = FixPath(scene.BackgroundImage, scene.Character);
			BackgroundColor = scene.BackColor.A == 0 ? null : scene.BackColor.ToHexValue();
			Width = scene.Width.ToString(CultureInfo.InvariantCulture);
			Height = scene.Height.ToString(CultureInfo.InvariantCulture);

			//Create directives for all animation blocks. We'll put them into the right places later
			Dictionary<string, WorkingDirective> createdObjects = new Dictionary<string, WorkingDirective>();
			List<WorkingDirective> directives = new List<WorkingDirective>();

			for(int i = 0; i < scene.Tracks.Count; i++)
			{
				LiveObject obj = scene.Tracks[i];
				ParseObject(obj, i, createdObjects, directives);
			}

			//insert breaks
			foreach (LiveBreak brk in scene.BreakSet.Pauses)
			{
				Directive pause = new Directive("pause");
				pause.Delay = brk.Time.ToString(CultureInfo.InvariantCulture);
				WorkingDirective d = new WorkingDirective(pause, brk.Time);
				directives.Add(d);
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
					compare = sort1.CompareTo(sort2);
					if (compare == 0)
					{
						compare = d1.Track.CompareTo(d2.Track);
					}
				}
				return compare;
			});

			float offset = 0;
			bool onlyClears = true;
			int lastPauseIndex = -1;
			for (int i = 0; i < directives.Count; i++)
			{
				WorkingDirective wd = directives[i];
				Directive d = wd.Directive;

				if (d.DirectiveType != "pause")
				{
					if (d.DirectiveType != "remove" && d.DirectiveType != "clear" && d.DirectiveType != "stop")
					{
						onlyClears = false;
					}
				}

				wd.StartTime -= offset;
				if (wd.StartTime <= 0)
				{
					d.Delay = null;
				}
				else
				{
					d.Delay = wd.StartTime.ToString(CultureInfo.InvariantCulture);
				}

				Directives.Add(d);
				if (d.DirectiveType == "pause")
				{
					d.Delay = null; //Pauses don't make sense to have a delay since the user can click through them anyway
					onlyClears = true;
					lastPauseIndex = i;
					offset += wd.StartTime;
				}
			}
			if (lastPauseIndex >= 0 && onlyClears)
			{
				//if there's nothing changing the scene after the last pause, throw it all out
				for (int i = directives.Count - 1; i > lastPauseIndex; i--)
				{
					Directives.RemoveAt(i);
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
				return null;
			}
			if (path.StartsWith(character.FolderName))
			{
				return path.Substring(character.FolderName.Length + 1);
			}
			else
			{
				return "/opponents/" + path;
			}
		}

		private string GetKey(float time, string metadata)
		{
			return $"{time}-{metadata}";
		}

		private void ParseObject(LiveObject obj, int trackIndex, Dictionary<string, WorkingDirective> createdObjects, List<WorkingDirective> directives)
		{
			Dictionary<string, WorkingDirective> activeDirectives = new Dictionary<string, WorkingDirective>();
			Dictionary<string, float> startPoints = new Dictionary<string, float>();
			List<WorkingDirective> objDirectives = new List<WorkingDirective>();

			if (obj is LiveBubble)
			{
				LiveBubble bubble = obj as LiveBubble;
				Directive dir = bubble.CreateCreationDirective(this);
				WorkingDirective d = new WorkingDirective(dir, obj.Start);
				d.Track = trackIndex;
				createdObjects[obj.Id] = d;
				objDirectives.Add(d);
				directives.Add(d);

				if (!bubble.LinkedToEnd)
				{
					Directive end = new Directive();
					end.DirectiveType = "clear";
					end.Id = obj.Id;
					end.Delay = (obj.Start + bubble.Length).ToString(CultureInfo.InvariantCulture);
					WorkingDirective wd = new WorkingDirective(end, bubble.Start + bubble.Length);
					wd.Track = trackIndex;
					objDirectives.Add(wd);
					directives.Add(wd);					
				}
			}
			else if (obj is LiveAnimatedObject)
			{
				LiveAnimatedObject anim = obj as LiveAnimatedObject;

				if (!string.IsNullOrEmpty(obj.Id) && !createdObjects.ContainsKey(obj.Id))
				{
					//creation directive
					Directive dir = anim.CreateCreationDirective(this);
					if (dir != null)
					{
						WorkingDirective d = new WorkingDirective(dir, obj.Start);
						d.Track = trackIndex;
						createdObjects[obj.Id] = d;
						objDirectives.Add(d);
					}
				}

				if (!anim.LinkedToEnd && !(anim is LiveCamera))
				{
					Directive end = new Directive();
					end.DirectiveType = "remove";
					end.Delay = (obj.Start + anim.Length).ToString(CultureInfo.InvariantCulture);

					end.Id = anim.Id;
					WorkingDirective d = new WorkingDirective(end, obj.Start + anim.Length);
					d.Track = trackIndex;
					objDirectives.Add(d);
				}

				//Copy keyframes into directives
				Dictionary<string, WorkingDirective> lastDirectivePerProperty = new Dictionary<string, WorkingDirective>();
				Dictionary<string, WorkingDirective> stopDirectives = new Dictionary<string, WorkingDirective>();
				for (int i = 1; i < anim.Keyframes.Count; i++)
				{
					LiveKeyframe kf = anim.Keyframes[i];
					foreach (string property in anim.Properties)
					{
						if (kf.HasProperty(property))
						{
							LiveKeyframeMetadata blockMetadata = anim.GetBlockMetadata(property, kf.Time);
							string metakey = blockMetadata.ToKey();
							LiveKeyframeMetadata metadata = kf.GetMetadata(property, false);

							float startTime = obj.Start;

							WorkingDirective previousDirective;
							WorkingDirective directive = null;
							if (lastDirectivePerProperty.TryGetValue(property, out previousDirective))
							{
								switch (metadata.FrameType)
								{
									case KeyframeType.Begin:
										if (previousDirective.Directive.Looped)
										{
											//if the previous frame is part of a loop, create or add a stop directive
											float stopTime = startTime + kf.Time;

											WorkingDirective stop = null;
											Directive stopDirective = new Directive("stop");
											stopDirective.Id = obj.Id;
											stopDirective.Delay = stopTime.ToString(CultureInfo.InvariantCulture);
											stop = new WorkingDirective(stopDirective, stopTime);
											stop.Track = trackIndex;
											objDirectives.Add(stop);

											stop.StopKey = previousDirective.MetaKey;
											previousDirective.HasStop = true;
										}

										//this frame doesn't get retained at all.
										//just force a new directive at the time
										startTime += kf.Time;
										previousDirective = null;
										break;
									case KeyframeType.Split:
										//need to put the keyframe into the last directive. No need to add the frame to the new one too since
										//the engine assumes that it uses the previous values
										float time = kf.Time - previousDirective.StartTime + obj.Start;
										previousDirective.AddKeyframe(kf, property, time.ToString(CultureInfo.InvariantCulture));
										previousDirective = null;
										startTime += kf.Time;
										break;
								}
							}
							else if (metadata.FrameType != KeyframeType.Normal)
							{
								startTime += kf.Time;
							}
							string directiveKey = GetKey(startTime, metakey);
							if (previousDirective == null)
							{
								//see if there's already a directive that fits this start
								//activeDirectives.TryGetValue(directiveKey, out directive);

								//commented out - use a new directive for every property. They'll be merged later.
							}
							else
							{
								directive = previousDirective;
							}

							if (directive == null)
							{
								//time for a new directive
								Directive currentDirective = new Directive();
								currentDirective.Id = anim.Id;
								currentDirective.Marker = anim.Marker;
								currentDirective.Z = anim.Z;
								float delay = startTime;
								if (delay > 0)
								{
									currentDirective.Delay = startTime.ToString(CultureInfo.InvariantCulture);
								}

								currentDirective.EasingMethod = blockMetadata.Ease;
								currentDirective.ClampingMethod = blockMetadata.ClampMethod;
								currentDirective.Iterations = blockMetadata.Iterations;
								currentDirective.Looped = blockMetadata.Looped;
								currentDirective.InterpolationMethod = blockMetadata.Interpolation;

								directive = new WorkingDirective(currentDirective, startTime, GetKey(startTime, blockMetadata.ToKey()));
								directive.Track = trackIndex;
								objDirectives.Add(directive);
								activeDirectives[directiveKey] = directive;

								if (anim is LiveSprite || anim is LiveEmitter)
								{
									currentDirective.DirectiveType = "move";
								}
								else if (anim is LiveCamera)
								{
									if (property == "Opacity" || property == "Color")
									{
										currentDirective.DirectiveType = "fade";
									}
									else
									{
										currentDirective.DirectiveType = "camera";
									}
									currentDirective.Id = null;
								}
							}
							lastDirectivePerProperty[property] = directive;

							if (metadata.FrameType == KeyframeType.Normal)
							{
								float time = kf.Time - directive.StartTime + obj.Start;
								directive.AddKeyframe(kf, property, time.ToString(CultureInfo.InvariantCulture));
							}
						}
					}
				}

				//merge directives
				Dictionary<string, string> animIds = new Dictionary<string, string>();
				for (int i = 0; i < objDirectives.Count; i++)
				{
					WorkingDirective d1 = objDirectives[i];
					for (int j = i + 1; j < objDirectives.Count; j++)
					{
						WorkingDirective d2 = objDirectives[j];
						if (d1.MatchesType(d2))
						{
							d2.Directive.CopyInto(d1.Directive);
							objDirectives.RemoveAt(j--);
						}
					}
;
					if (d1.HasStop)
					{
						animIds[d1.MetaKey] = d1.Directive.AnimationId = d1.Directive.Id + "-" + UniqueId++;
					}
					else if (!string.IsNullOrEmpty(d1.StopKey))
					{
						string animId;
						if (animIds.TryGetValue(d1.StopKey, out animId))
						{
							d1.Directive.AnimationId = animId;
						}
					}
				}

				//finalize them
				for (int i = 0; i < objDirectives.Count; i++)
				{
					WorkingDirective d = objDirectives[i];
					if (d.IsEmpty())
					{
						objDirectives.RemoveAt(i--);
						continue;
					}
					d.Bake();
					directives.Add(d);
				}

				//events
				for (int i = 0; i < anim.Events.Count; i++)
				{
					LiveEvent evt = anim.Events[i];
					WorkingDirective d = new WorkingDirective(evt.CreateDirectiveDefinition(), obj.Start + evt.Time);
					d.Directive.Id = obj.Id;
					d.Track = trackIndex;
					directives.Add(d);
				}
			}
		}

		private class WorkingDirective
		{
			public Directive Directive;
			public float StartTime;
			public string MetaKey;
			public bool HasStop;
			public string StopKey;
			public int Track;

			public WorkingDirective(Directive directive, float startTime, string metakey = "")
			{
				Directive = directive;
				StartTime = startTime;
				MetaKey = metakey;
			}

			public override string ToString()
			{
				return $"{StartTime}s - {Directive.ToString()}";
			}

			public bool IsEmpty()
			{
				string type = Directive.DirectiveType;
				if (type != "move" && type != "camera" && type != "fade")
				{
					return false;
				}
				return Directive.Properties.Count == 0 && Directive.Keyframes.Count == 0;
			}

			/// <summary>
			/// Gets whether two working directives can be merged
			/// </summary>
			/// <param name="other"></param>
			/// <returns></returns>
			public bool MatchesType(WorkingDirective other)
			{
				Directive d = other.Directive;
				if (Directive.DirectiveType != d.DirectiveType)
				{
					return false;
				}
				if (StartTime != other.StartTime)
				{
					return false;
				}
				if (MetaKey != other.MetaKey)
				{
					return false;
				}
				if (HasStop != other.HasStop)
				{
					return false;
				}

				return true;
			}

			/// <summary>
			/// Adds a keyframe
			/// </summary>
			/// <param name="liveFrame">Source LiveKeyframe</param>
			/// <param name="property">Property on the keyframe to add</param>
			public void AddKeyframe(LiveKeyframe liveFrame, string property, string time)
			{
				MemberInfo mi = PropertyTypeInfo.GetMemberInfo(typeof(Keyframe), property);
				if (mi == null)
				{
					throw new InvalidOperationException($"No property on Keyframe called {property} found.");
				}
				Keyframe kf = Directive.Keyframes.Find(k => k.Time == time);
				if (kf == null)
				{
					kf = new Keyframe();
					kf.Time = time;
					Directive.Keyframes.Add(kf);
				}

				Type dataType = mi.GetDataType();
				object value = null;
				if (dataType == typeof(int))
				{
					value = liveFrame.Get<int>(property);
				}
				else if (dataType == typeof(float))
				{
					value = liveFrame.Get<float>(property);
				}
				else if (dataType == typeof(bool))
				{
					value = liveFrame.Get<bool>(property);
				}
				else
				{
					object rawValue = liveFrame.Get<object>(property);
					string stringValue;
					if (rawValue.GetType() == typeof(Color))
					{
						stringValue = ((Color)rawValue).ToHexValue();
					}
					else
					{
						stringValue = Convert.ToString(rawValue, CultureInfo.InvariantCulture);
					}
					value = stringValue;
				}
				mi.SetValue(kf, value);
				kf.Properties[property] = value;
			}

			public void Bake()
			{
				MergeFrames();
				Directive.BakeProperties();
				if (Directive.Delay == "0")
				{
					Directive.Delay = null;
				}
				if (Directive.InterpolationMethod == "linear")
				{
					Directive.InterpolationMethod = null;
				}
				if (Directive.EasingMethod == "smooth")
				{
					Directive.EasingMethod = null;
				}
				foreach (Keyframe kf in Directive.Keyframes)
				{
					kf.BakeProperties();
				}
			}

			/// <summary>
			/// Moves information from a lone keyframe into the base directive
			/// </summary>
			public void MergeFrames()
			{
				if (Directive.Keyframes.Count != 1)
				{
					return;
				}
				Keyframe kf = Directive.Keyframes[0];
				Directive.TransferPropertiesFrom(kf);
				Directive.Keyframes.Clear();
			}
		}
	}
}
