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

		public List<SpritePreview> Sprites = new List<SpritePreview>();
		private Dictionary<string, SpritePreview> _spriteMap = new Dictionary<string, SpritePreview>();

		public List<PoseAnimation> Animations = new List<PoseAnimation>();

		public PosePreview(ISkin character, Pose pose)
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

			foreach (Sprite sprite in pose.Sprites)
			{
				if (!string.IsNullOrEmpty(sprite.Src))
				{
					AddImage(sprite.Src);
				}
				SpritePreview preview = new SpritePreview(this, sprite, Sprites.Count);
				Sprites.Add(preview);
				if (!string.IsNullOrEmpty(sprite.Id))
				{
					_spriteMap[sprite.Id] = preview;
				}
			}
			foreach (PoseDirective directive in pose.Directives)
			{
				if (directive.DirectiveType == "animation")
				{
					if (!string.IsNullOrEmpty(directive.Src))
					{
						AddImage(directive.Src);
					}
					Animations.Add(new PoseAnimation(this, directive));
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

		internal bool IsDisposing;
		public void Dispose()
		{
			IsDisposing = true;

			foreach (Image img in Images.Values)
			{
				img.Dispose();
			}
			Images.Clear();

			IsDisposing = false;
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

		public void Draw(Graphics g, int width, int height)
		{
			foreach (SpritePreview sprite in Sprites)
			{
				sprite.Draw(g, width, height);
			}
		}

		public void Update(float elapsedSec)
		{
			float elapsedMs = elapsedSec * 1000;
			for (int i = 0; i < Animations.Count; i++)
			{
				Animations[i].Update(elapsedMs);
			}
		}
	}
}
