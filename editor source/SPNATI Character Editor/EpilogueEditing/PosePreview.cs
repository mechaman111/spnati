using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class PosePreview : IDisposable
	{
		public ISkin Character;
		public Pose Pose;
		public float BaseHeight;

		public Dictionary<string, Image> Images = new Dictionary<string, Image>();

		public SpritePreview SelectedObject;
		public List<SpritePreview> Sprites = new List<SpritePreview>();
		private Dictionary<string, SpritePreview> _spriteMap = new Dictionary<string, SpritePreview>();

		public List<PoseAnimation> Animations = new List<PoseAnimation>();
		public Dictionary<string, string> Markers = new Dictionary<string, string>();

		public float ElapsedTime;
		public float TotalDuration;

		public PosePreview(ISkin character, Pose pose, PoseDirective selectedDirective, Keyframe selectedKeyframe, Sprite selectedSprite, List<string> markers, bool allowSparseFrames)
		{
			Character = character;
			Pose = pose;
			int baseHeight;
			if (!int.TryParse(pose.BaseHeight, out baseHeight))
			{
				BaseHeight = 1400;
			}
			else
			{
				BaseHeight = baseHeight;
			}

			if (markers != null)
			{
				foreach (string marker in markers)
				{
					Markers[marker] = "1";
				}
			}

			foreach (Sprite sprite in pose.Sprites)
			{
				if (!string.IsNullOrEmpty(sprite.Marker) && !Marker.CheckMarker(sprite.Marker, Markers))
				{
					continue;
				}
				if (!string.IsNullOrEmpty(sprite.Src))
				{
					AddImage(sprite.Src);
				}
				SpritePreview preview = new SpritePreview(this, sprite, Sprites.Count);

				if (selectedKeyframe != null && selectedDirective.Id == sprite.Id || (selectedSprite == sprite && pose.Directives.Find(d => d.DirectiveType == "animation" && d.Id == sprite.Id) != null))
				{
					SelectedObject = new SpritePreview(this, sprite, Sprites.Count);
					preview.KeyframeActive = true;
				}

				Sprites.Add(preview);
				if (!string.IsNullOrEmpty(sprite.Id))
				{
					_spriteMap[sprite.Id] = preview;
				}
			}
			foreach (PoseDirective directive in pose.Directives)
			{
				if (!string.IsNullOrEmpty(directive.Marker) && !Marker.CheckMarker(directive.Marker, Markers))
				{
					continue;
				}
				if (directive.DirectiveType == "animation")
				{
					if (directive.Keyframes.Count > 1 && allowSparseFrames)
					{
						//first split the properties into buckets of frame indices where they appear
						Dictionary<string, List<int>> propertyMap = new Dictionary<string, List<int>>();

						for (int i = 0; i < directive.Keyframes.Count; i++)
						{
							Keyframe frame = directive.Keyframes[i];
							MapFrameProperty(i, "X", frame.X, propertyMap);
							MapFrameProperty(i, "Y", frame.Y, propertyMap);
							MapFrameProperty(i, "Rotation", frame.Rotation, propertyMap);
							MapFrameProperty(i, "ScaleX", frame.ScaleX, propertyMap);
							MapFrameProperty(i, "ScaleY", frame.ScaleY, propertyMap);
							MapFrameProperty(i, "SkewX", frame.SkewX, propertyMap);
							MapFrameProperty(i, "SkewY", frame.SkewY, propertyMap);
							MapFrameProperty(i, "Opacity", frame.Opacity, propertyMap);
							MapFrameProperty(i, "Src", frame.Src, propertyMap);
						}

						//next create animations for each combination of frames
						Dictionary<string, PoseDirective> directives = new Dictionary<string, PoseDirective>();
						foreach (KeyValuePair<string, List<int>> kvp in propertyMap)
						{
							string prop = kvp.Key;
							string key = string.Join(",", kvp.Value);
							List<int> indices = kvp.Value;
							PoseDirective workingDirective;
							if (!directives.TryGetValue(key, out workingDirective))
							{
								//shallow copy the directive
								workingDirective = directive.Clone() as PoseDirective;
								workingDirective.Keyframes.Clear();
								directives[key] = workingDirective;
							}

							for (var i = 0; i < indices.Count; i++)
							{
								Keyframe srcFrame = directive.Keyframes[indices[i]];
								Keyframe targetFrame;
								if (workingDirective.Keyframes.Count <= i)
								{
									//shallow copy the frame minus the animatable properties
									targetFrame = srcFrame.Clone() as Keyframe;
									targetFrame.X = null;
									targetFrame.Y = null;
									targetFrame.Src = null;
									targetFrame.Opacity = null;
									targetFrame.ScaleX = null;
									targetFrame.ScaleY = null;
									targetFrame.Rotation = null;
									targetFrame.SkewX = null;
									targetFrame.SkewY = null;

									targetFrame.Time = srcFrame.Time;
									workingDirective.Keyframes.Add(targetFrame);
								}
								else
								{
									targetFrame = workingDirective.Keyframes[i];
								}
								switch (prop)
								{
									case "X":
										targetFrame.X = srcFrame.X;
										break;
									case "Y":
										targetFrame.Y = srcFrame.Y;
										break;
									case "Src":
										targetFrame.Src = srcFrame.Src;
										break;
									case "Opacity":
										targetFrame.Opacity = srcFrame.Opacity;
										break;
									case "ScaleX":
										targetFrame.ScaleX = srcFrame.ScaleX;
										break;
									case "ScaleY":
										targetFrame.ScaleY = srcFrame.ScaleY;
										break;
									case "Rotation":
										targetFrame.Rotation = srcFrame.Rotation;
										break;
									case "SkewX":
										targetFrame.SkewX = srcFrame.SkewX;
										break;
									case "SkewY":
										targetFrame.SkewY = srcFrame.SkewY;
										break;
								}
							}
						}

						//turn working directives into animations
						foreach (PoseDirective working in directives.Values)
						{
							AddAnimation(selectedKeyframe, working);
						}
					}
					else
					{
						AddAnimation(selectedKeyframe, directive);
					}
				}
				else
				{
					foreach (PoseAnimFrame frame in directive.AnimFrames)
					{
						if (!string.IsNullOrEmpty(frame.Id))
						{
							AddImage(frame.Id);
						}
					}
				}
			}
			ResortSprites();

			//update the preview to match the current timeline at its point
			if (SelectedObject != null && SelectedObject.LinkedAnimation != null)
			{
				float time = SelectedObject.LinkedAnimation.Delay + SelectedObject.LinkedFramePreview.Time;
				foreach (PoseAnimation anim in Animations)
				{
					if (anim.Sprite.Id == SelectedObject.Id && anim.Delay <= time)
					{
						anim.UpdateTo(anim == SelectedObject.LinkedAnimation ? time - anim.Delay : time, SelectedObject);
						anim.UpdateTo(0, anim.Sprite);
					}
				}
			}

		}

		private void AddAnimation(Keyframe selectedKeyframe, PoseDirective working)
		{
			if (!string.IsNullOrEmpty(working.Src))
			{
				AddImage(working.Src);
			}
			foreach (Keyframe keyframe in working.Keyframes)
			{
				if (!string.IsNullOrEmpty(keyframe.Src))
				{
					AddImage(keyframe.Src);
				}
			}
			PoseAnimation animation = new PoseAnimation(this, working);
			if (SelectedObject != null)
			{
				foreach (KeyframePreview kf in animation.Frames)
				{
					if (kf.Keyframe == selectedKeyframe)
					{
						SelectedObject.LinkedFramePreview = kf;
						SelectedObject.LinkedAnimation = animation;
						break;
					}
				}
			}
			TotalDuration = Math.Max(TotalDuration, animation.Delay + animation.Duration);
			Animations.Add(animation);
		}

		private void MapFrameProperty(int frameIndex, string property, string value, Dictionary<string, List<int>> propertyMap)
		{
			if (!string.IsNullOrEmpty(value))
			{
				List<int> frameIndices;
				if (!propertyMap.TryGetValue(property, out frameIndices))
				{
					frameIndices = new List<int>();
					propertyMap[property] = frameIndices;
				}
				frameIndices.Add(frameIndex);
			}
		}

		public bool IsAnimated
		{
			get
			{
				return Animations.Count > 0;
			}
		}

		public SpritePreview GetSprite(string id)
		{
			return _spriteMap.Get(id);
		}

		public void Dispose()
		{
			foreach (Image img in Images.Values)
			{
				img.Dispose();
			}
			Images.Clear();
		}

		private void AddImage(string src)
		{
			if (string.IsNullOrEmpty(src)) { return; }
			if (Images.ContainsKey(src))
			{
				return;
			}
			string path = GetImagePath(src);
			try
			{
				using (var temp = new Bitmap(path))
				{
					Bitmap img = new Bitmap(temp);
					Images[src] = img;
				}
			}
			catch { }
		}

		protected string GetImagePath(string path)
		{
			return Path.Combine(Config.SpnatiDirectory, "opponents", path);
		}

		private void ResortSprites()
		{
			Sprites.Sort((s1, s2) =>
			{
				int compare = s1.Z.CompareTo(s2.Z);
				if (compare == 0)
				{
					compare = s1.AddIndex.CompareTo(s2.AddIndex);
				}
				return compare;
			});
		}

		public void Draw(Graphics g, int width, int height, Point offset)
		{
			foreach (SpritePreview sprite in Sprites)
			{
				sprite.Draw(g, width, height, offset);
			}

			if (SelectedObject != null)
			{
				SelectedObject.Draw(g, width, height, offset);
			}
		}

		public void Update(float elapsedSec)
		{
			float elapsedMs = elapsedSec * 1000;
			ElapsedTime += elapsedMs;
			if (SelectedObject != null && ElapsedTime >= TotalDuration)
			{
				ElapsedTime = 0;
				for (int i = 0; i < Animations.Count; i++)
				{
					if (Animations[i].Sprite == null) { continue; }
					Animations[i].Sprite.SetInitialParameters();
					Animations[i].UpdateTo(-Animations[i].Delay, Animations[i].Sprite);
				}
				return;
			}
			for (int i = 0; i < Animations.Count; i++)
			{
				Animations[i].Update(elapsedMs);
			}
		}
	}
}
