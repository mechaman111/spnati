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
using System.Linq;
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

		[DefaultValue("1")]
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

			if (scene.Segments.Count > 0)
			{
				LiveSceneSegment initialSetting = scene.Segments[0];
				initialSetting.Camera.AddToScene(this);

				foreach (LiveSceneSegment segment in scene.Segments)
				{
					List<Directive> directives = CreateFrom(segment);
					Directives.AddRange(directives);
				}

				//alter the last pause to use the scene's desired wait type
				for (int i = Directives.Count - 1; i >= 0; i--)
				{
					Directive d = Directives[i];
					if (d.DirectiveType == "pause")
					{
						//throw out everything after this directive
						if (i < Directives.Count - 1)
						{
							Directives.RemoveRange(i + 1, Directives.Count - 1 - i);
						}

						if (scene.WaitMethod == PauseType.AdvanceImmediately)
						{
							Directives.RemoveAt(i); //remove the wait too
						}
						else if (scene.WaitMethod == PauseType.WaitForAnimations)
						{
							d.DirectiveType = "wait";
						}
						break;
					}
				}
			}
		}

		/// <summary>
		/// Creates a directive to clear a bubble
		/// </summary>
		/// <param name="bubble"></param>
		/// <returns></returns>
		private WorkingDirective CreateClearDirective(IFixedLength l, int trackIndex, float delay)
		{
			LiveObject obj = l as LiveObject;
			Directive end = new Directive();
			end.DirectiveType = l is LiveBubble ? "clear" : "remove";
			end.Id = obj.Id;
			if (end.Id == null && end.DirectiveType == "clear")
			{
				//can't clear an individual ID-less text box
				end.DirectiveType = "clear-all";
			}
			if (delay > 0)
			{
				end.Delay = delay.ToString(CultureInfo.InvariantCulture);
			}
			WorkingDirective wd = new WorkingDirective(end, obj.Start + l.Length);
			wd.Track = trackIndex;
			return wd;
		}

		/// <summary>
		/// Creates the directives belonging to a single segment
		/// </summary>
		/// <param name="segment"></param>
		/// <returns></returns>
		private List<Directive> CreateFrom(LiveSceneSegment segment)
		{
			List<Directive> finalDirectives = new List<Directive>();

			//Create directives for all animation blocks. We'll put them into the right places later
			List<WorkingDirective> directives = new List<WorkingDirective>();
			//Objects that need to be cleared at the end of the segment
			List<LiveObject> pendingRemovals = new List<LiveObject>();
			int pendingBubbles = 0;
			int persistentBubbles = 0;

			float duration = segment.GetDuration();

			for (int i = 0; i < segment.Tracks.Count; i++)
			{
				LiveObject obj = segment.Tracks[i];
				ParseObject(obj, i, directives, segment.Character);

				if (obj is IFixedLength && !(obj is LiveCamera))
				{
					IFixedLength l = obj as IFixedLength;
					if (!obj.LinkedToEnd)
					{
						if (obj.Start + l.Length < duration)
						{
							WorkingDirective wd = CreateClearDirective(l, i, obj.Start + l.Length);
							directives.Add(wd);
						}
						else
						{
							if (obj is LiveBubble)
							{
								pendingBubbles++;
							}
							pendingRemovals.Add(obj);
						}
					}
					else if (obj is LiveBubble)
					{
						persistentBubbles++;
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
					compare = sort1.CompareTo(sort2);
					if (compare == 0)
					{
						compare = d1.Track.CompareTo(d2.Track);
					}
				}
				return compare;
			});

			if (!string.IsNullOrEmpty(segment.Name))
			{
				finalDirectives.Add(new Directive("metadata")
				{
					Name = segment.Name
				});
			}
			finalDirectives.AddRange(directives.Select(wd => wd.Directive));

			Directive wait = new Directive("pause");
			finalDirectives.Add(wait);

			//if there are any objects still open, clear them
			if (pendingRemovals.Count > 0)
			{
				bool removedText = false;
				if (pendingBubbles > 0 && persistentBubbles == 0)
				{
					//can just use a clear-all for the text boxes
					Directive clear = new Directive("clear-all");
					finalDirectives.Add(clear);
					removedText = true;
				}

				//others need to be cleared individually
				foreach (LiveObject obj in pendingRemovals)
				{
					if (removedText && obj is LiveBubble)
					{
						continue;
					}
					IFixedLength l = obj as IFixedLength;
					float delay = obj.Start + l.Length;
					if (delay >= duration)
					{
						delay = 0;
					}
					finalDirectives.Add(CreateClearDirective(l, 0, delay).Directive);
				}
			}

			return finalDirectives;
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

		private WorkingDirective CreateStopDirective(string id, float delay, int trackIndex)
		{
			Directive stopDirective = new Directive("stop");
			stopDirective.Id = id;
			if (id == "Camera")
			{
				stopDirective.Id = "camera";
			}
			if (delay > 0)
			{
				stopDirective.Delay = delay.ToString(CultureInfo.InvariantCulture);
			}
			WorkingDirective stop = new WorkingDirective(stopDirective, delay);
			stop.Track = trackIndex;
			return stop;
		}

		private void ParseObject(LiveObject obj, int trackIndex, List<WorkingDirective> directives, Character character)
		{
			Dictionary<string, float> startPoints = new Dictionary<string, float>();
			List<WorkingDirective> objDirectives = new List<WorkingDirective>();

			if (obj is LiveBubble)
			{
				if (!obj.LinkedFromPrevious)
				{
					LiveBubble bubble = obj as LiveBubble;
					Directive dir = bubble.CreateCreationDirective(this);
					WorkingDirective d = new WorkingDirective(dir, obj.Start);
					d.Track = trackIndex;
					if (d.StartTime == 0)
					{
						d.Directive.Delay = null;
					}
					directives.Add(d);
				}
			}
			else if (obj is LiveAnimatedObject)
			{
				LiveAnimatedObject anim = obj as LiveAnimatedObject;
				Dictionary<string, WorkingDirective> lastDirectivePerProperty = new Dictionary<string, WorkingDirective>();

				if (!string.IsNullOrEmpty(obj.Id))
				{
					if (!obj.LinkedFromPrevious)
					{
						//creation directive
						Directive dir = anim.CreateCreationDirective(this);
						if (dir != null)
						{
							WorkingDirective d = new WorkingDirective(dir, obj.Start);
							d.Track = trackIndex;
							if (d.StartTime == 0)
							{
								d.Directive.Delay = null;
							}
							directives.Add(d);
						}
					}
					else
					{
						LiveAnimatedObject previous = obj.Previous as LiveAnimatedObject;
						if (anim.Keyframes.Count > 0)
						{
							//any time 0 frames that changed from the previous should get time 0 movement
							LiveKeyframe first = anim.Keyframes[0];
							if (first.Time == 0)
							{
								foreach (string property in first.TrackedProperties)
								{
									if (first.HasProperty(property))
									{
										LiveKeyframe lastFrame = previous.GetLastFrame(property);
										if (lastFrame != null)
										{
											if (!lastFrame.Get<object>(property).Equals(first.Get<object>(property)))
											{
												Directive currentDirective = new Directive("move");
												currentDirective.Id = anim.Id;
												currentDirective.Marker = anim.Marker;
												currentDirective.Layer = anim.Z;
												LiveKeyframeMetadata blockMetadata = first.GetMetadata(property, false);

												WorkingDirective directive = new WorkingDirective(currentDirective, 0, GetKey(0, blockMetadata.ToKey()));
												directive.Track = trackIndex;
												objDirectives.Add(directive);

												if (anim is LiveCamera)
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

												directive.AddKeyframe(first, property, "0", character);
												lastDirectivePerProperty[property] = directive;
											}
										}
									}
								}
							}
						}
					}
				}

				//stop looped animations from previous segments if there's a new keyframe for that property
				if (obj.Previous != null && anim.Keyframes.Count > 0)
				{
					Dictionary<string, WorkingDirective> stoppages = new Dictionary<string, WorkingDirective>();
					WorkingDirective initialStoppage = null;
					LiveAnimatedObject previous = obj.Previous as LiveAnimatedObject;
					foreach (string property in anim.Properties)
					{
						LiveKeyframe first = anim.GetFirstFrame(property);
						if (first != null)
						{
							LiveAnimatedObject prev = previous;
							LiveKeyframeMetadata prevMetadata = null;
							while (prev != null && prevMetadata == null)
							{
								prevMetadata = prev.GetLastBlockMetadata(property);
								prev = prev.Previous as LiveAnimatedObject;
							}
							if (prevMetadata != null && prevMetadata.Looped)
							{
								string id = anim.Id;
								if (id == "Camera" && (property == "Color" || property == "Opacity"))
								{
									id = "fade";
								}

								stoppages.TryGetValue(id, out initialStoppage);
								if (initialStoppage == null)
								{
									float delay = anim.Start + first.Time;

									initialStoppage = CreateStopDirective(id, delay, trackIndex);
									initialStoppage.LoopedProperties = anim.GetLoopedProperties(first, property);
									directives.Add(initialStoppage);
									stoppages[id] = initialStoppage;
								}
								initialStoppage.AddStopProperty(property);
							}
						}
					}
				}

				//Copy keyframes into directives

				DualKeyDictionary<float, string, WorkingDirective> stopDirectives = new DualKeyDictionary<float, string, WorkingDirective>();
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
											//if the previous frame is part of a loop, create or add to a stop directive
											float stopTime = startTime + kf.Time;

											string id = obj.Id;
											if (id == "Camera" && (property == "Color" || property == "Opacity"))
											{
												id = "fade";
											}

											WorkingDirective stop = stopDirectives.Get(stopTime, id);
											if (stop == null)
											{
												stop = CreateStopDirective(id, stopTime, trackIndex);
												stop.LoopedProperties = anim.GetLoopedProperties(kf, property);
												stopDirectives.Set(stopTime, id, stop);
												directives.Add(stop);
											}
											stop.AddStopProperty(property);
										}

										object previousValue = anim.GetPreviousValue(property, kf.Time);
										object value = kf.Get<object>(property);
										if (previousValue != null && !previousValue.Equals(value))
										{
											//need a time 0 frame in the directive since this is not the same value as the previous animation touching it
										}
										else
										{
											//this frame doesn't get retained at all.
											//just force a new directive at the time
											startTime += kf.Time;
											previousDirective = null;
										}
										break;
									case KeyframeType.Split:
										//need to put the keyframe into the last directive. No need to add the frame to the new one too since
										//the engine assumes that it uses the previous values
										float time = kf.Time - previousDirective.StartTime + obj.Start;
										previousDirective.AddKeyframe(kf, property, time.ToString(CultureInfo.InvariantCulture), character);
										previousDirective = null;
										startTime += kf.Time;
										break;
								}
							}
							else if (metadata.FrameType != KeyframeType.Normal)
							{
								if (metadata.FrameType == KeyframeType.Split)
								{
									//there was no previous directive, but if this was split, clearly we were supposed to be moving, so make a movement directive
									Directive move = new Directive("move");

									move.Id = anim.Id;
									move.Marker = anim.Marker;
									move.Layer = anim.Z;

									if (anim is LiveCamera)
									{
										if (property == "Opacity" || property == "Color")
										{
											move.DirectiveType = "fade";
										}
										else
										{
											move.DirectiveType = "camera";
										}
										move.Id = null;
									}

									LiveKeyframeMetadata md = anim.GetBlockMetadata(property, 0);
									move.PopulateMetadata(md);

									WorkingDirective moveDir = new WorkingDirective(move, 0, GetKey(0, md.ToKey()));
									moveDir.Track = trackIndex;
									moveDir.AddKeyframe(kf, property, (obj.Start + kf.Time).ToString(CultureInfo.InvariantCulture), character);
									objDirectives.Add(moveDir);
								}
								startTime += kf.Time;
							}
							string directiveKey = GetKey(startTime, metakey);
							if (previousDirective != null)
							{
								directive = previousDirective;
							}

							if (directive == null)
							{
								//time for a new directive
								Directive currentDirective = new Directive("move");
								currentDirective.Id = anim.Id;
								currentDirective.Marker = anim.Marker;
								currentDirective.Layer = anim.Z;
								float delay = startTime;
								if (delay > 0)
								{
									currentDirective.Delay = startTime.ToString(CultureInfo.InvariantCulture);
								}

								currentDirective.PopulateMetadata(blockMetadata);

								directive = new WorkingDirective(currentDirective, startTime, GetKey(startTime, blockMetadata.ToKey()));
								directive.Track = trackIndex;
								objDirectives.Add(directive);

								if (anim is LiveCamera)
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
								directive.AddKeyframe(kf, property, time.ToString(CultureInfo.InvariantCulture), character);
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
			public int Track;

			public HashSet<string> LoopedProperties = new HashSet<string>();

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

				return true;
			}

			/// <summary>
			/// Adds a keyframe
			/// </summary>
			/// <param name="liveFrame">Source LiveKeyframe</param>
			/// <param name="property">Property on the keyframe to add</param>
			public void AddKeyframe(LiveKeyframe liveFrame, string property, string time, Character character)
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

						if (property == "Src")
						{
							stringValue = FixPath(stringValue, character);
						}
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

			public void AddStopProperty(string property)
			{
				property = property.ToLowerInvariant();
				if (property == "opacity")
				{
					property = "alpha";
				}
				if (Directive.Id == "camera" && (property == "color" || property == "alpha"))
				{
					Directive.Id = "fade";
				}
				Directive.AffectedProperties.Add(property);
				LoopedProperties.Remove(property);
				if (LoopedProperties.Count == 0)
				{
					//this is stopping every looped property, so no need to list them individually
					Directive.AffectedProperties.Clear();
				}
			}
		}
	}
}
