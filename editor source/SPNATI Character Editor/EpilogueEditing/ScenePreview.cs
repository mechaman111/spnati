using SPNATI_Character_Editor.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class ScenePreview : SceneObject
	{
		public float AspectRatio { get { return Width / (float)Height; } }
		public SolidBrush BackgroundColor;
		public Color OverlayColor = System.Drawing.Color.Black;
		public Scene LinkedScene;

		public List<SceneObject> Objects = new List<SceneObject>();
		public List<SceneObject> TextBoxes = new List<SceneObject>();

		public Dictionary<string, Image> Images = new Dictionary<string, Image>();

		public ScenePreview(Scene scene, Character character)
		{
			PreviewScene = this;
			Character = character;
			LinkedScene = scene;
			try
			{
				BackgroundColor = new SolidBrush(ColorTranslator.FromHtml(scene.BackgroundColor));
			}
			catch { }

			if (scene.Width == null)
			{
				scene.Width = "100";
			}
			if (scene.Height == null)
			{
				scene.Height = "100";
			}
			string w = scene.Width.Split(new string[] { "px" }, StringSplitOptions.None)[0];
			string h = scene.Height.Split(new string[] { "px" }, StringSplitOptions.None)[0];
			float.TryParse(w, NumberStyles.Integer, CultureInfo.InvariantCulture, out Width);
			float.TryParse(h, NumberStyles.Integer, CultureInfo.InvariantCulture, out Height);

			X = (int)Parse(scene.X, Width);
			Y = (int)Parse(scene.Y, Height);

			if (!string.IsNullOrEmpty(scene.Zoom))
			{
				float.TryParse(scene.Zoom, NumberStyles.Float, CultureInfo.InvariantCulture, out Zoom);
			}
			if (!string.IsNullOrEmpty(scene.FadeColor))
			{
				try
				{
					OverlayColor = ColorTranslator.FromHtml(scene.FadeColor);
				}
				catch { }
			}
			if (!string.IsNullOrEmpty(scene.FadeOpacity))
			{
				float alpha;
				float.TryParse(scene.FadeOpacity, NumberStyles.Float, CultureInfo.InvariantCulture, out alpha);
				Alpha = alpha;
				int a = (int)(alpha / 100 * 255);
				OverlayColor = System.Drawing.Color.FromArgb(a, OverlayColor);
			}
			else
			{
				OverlayColor = System.Drawing.Color.FromArgb(0, OverlayColor);
			}

			AddImage(scene.Background);
			foreach (Directive directive in scene.Directives)
			{
				AddImage(directive.Src);
				foreach (Keyframe frame in directive.Keyframes)
				{
					AddImage(frame.Src);
				}
			}
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

		internal bool IsDisposing;
		public override void Dispose()
		{
			IsDisposing = true;
			BackgroundColor?.Dispose();
			foreach (SceneObject obj in Objects)
			{
				obj.Dispose();
			}
			foreach (SceneObject obj in TextBoxes)
			{
				obj.Dispose();
			}
			Objects.Clear();
			TextBoxes.Clear();
			base.Dispose();

			foreach (Image img in Images.Values)
			{
				img.Dispose();
			}
			Images.Clear();

			IsDisposing = false;
		}

		public override bool AdjustPosition(int x, int y, ScenePreview scene)
		{
			if (X == x && Y == y)
			{
				return false;
			}
			X = x;
			Y = y;

			if (LinkedFrame != null)
			{
				LinkedFrame.X = ApplyPosition(x, LinkedFrame.X, (int)scene.Width);
				LinkedFrame.Y = ApplyPosition(y, LinkedFrame.Y, (int)scene.Height);
				ResyncAnimation();
			}
			else
			{
				LinkedScene.X = ApplyPosition(x, LinkedScene.X, (int)scene.Width);
				LinkedScene.Y = ApplyPosition(y, LinkedScene.Y, (int)scene.Height);
			}
			return true;
		}

		public override bool AdjustSize(Point pt, HoverContext context, ScenePreview scene)
		{
			float zoom = Zoom;
			switch (context)
			{
				case HoverContext.CameraSizeRight:
					int rt = Math.Max((int)X + 10, pt.X);

					int width = (int)((-2 * rt + 2 * X) / (-zoom - 1));
					if (Width == width)
					{
						return false;
					}
					Width = width;
					LinkedScene.Width = ApplyPosition((int)Width, LinkedScene.Width, (int)scene.Width);
					return true;
				case HoverContext.CameraSizeLeft:
					//tricker than adjusting the right side since it involves translating X too

					//first get the target width, which is the same as moving the right side by the amount the left side moved
					int lt = Math.Min((int)(X + Width - 10), pt.X); //new left position
					int dx = (int)(X - lt);
					rt = (int)(X + Width + dx);

					//use formula for SizeRight to get the width now
					width = (int)((-2 * rt + 2 * X) / (-zoom - 1));
					if (Width == width)
					{
						return false;
					}

					//now solve for X given the width, scale, and new left where l = X - (w * s) / 2
					int x = (int)((width * zoom - width) / 2 + lt);
					Width = width;
					X = x;
					LinkedScene.X = ApplyPosition((int)X, LinkedScene.X, (int)scene.Width);
					LinkedScene.Width = ApplyPosition((int)Width, LinkedScene.Width, (int)scene.Width);
					return true;
				case HoverContext.CameraSizeBottom:
					int b = Math.Max((int)Y + 10, pt.Y);

					int height = (int)((-2 * b + 2 * Y) / (-zoom - 1));
					if (Height == height)
					{
						return false;
					}
					Height = height;
					LinkedScene.Height = ApplyPosition((int)Height, LinkedScene.Height, (int)scene.Height);
					return true;
				case HoverContext.CameraSizeTop:
					//works the same as left

					int t = Math.Min((int)(Y + Height - 10), pt.Y); //new left position
					int dy = (int)(Y - t);
					b = (int)(Y + Height + dy);

					height = (int)((-2 * b + 2 * Y) / (-zoom - 1));
					if (Height == height)
					{
						return false;
					}

					int y = (int)((height * zoom - height) / 2 + t);
					Height = height;
					Y = y;
					LinkedScene.Y = ApplyPosition((int)Y, LinkedScene.Y, (int)scene.Height);
					LinkedScene.Height = ApplyPosition((int)Height, LinkedScene.Height, (int)scene.Height);
					return true;
			}
			return false;
		}

		public override bool AdjustScale(Point point, ScenePreview scene, Point startPoint, HoverContext context, bool locked)
		{
			//get the object's center
			int cx = (int)(X + Width / 2);
			int cy = (int)(Y + Height / 2);

			int tx = point.X;
			int ty = point.Y;

			//work with whichever of X/Y is furthest from the center
			int dx = Math.Abs(tx - cx);
			int dy = Math.Abs(ty - cy);

			int offset = Math.Max(dx, dy);

			//transform this offset to left side of the scaled box
			int t = cx - offset;

			//now we have the top-left scaled world-space point. Use the formula to convert from object space to world space and solve for scale (worldX = objX - (objWidth * Scale - objWidth) / 2)
			float zoom = ((t - X) / -0.5f + Width) / Width;
			if (float.IsInfinity(zoom))
			{
				zoom = 0.001f;
			}

			if (Zoom == zoom)
			{
				return false;
			}
			zoom = 1 / zoom;
			Zoom = zoom;
			if (LinkedFrame != null)
			{
				LinkedFrame.Zoom = zoom.ToString(CultureInfo.InvariantCulture);
				ResyncAnimation();
			}
			else
			{
				LinkedScene.Zoom = zoom.ToString(CultureInfo.InvariantCulture);
			}

			return true;
		}

		public void AddObject(SceneObject obj)
		{
			Objects.Add(obj);

			ResortObjects();
		}

		public void ResortObjects()
		{
			Objects.Sort((o1, o2) =>
			{
				int compare = o1.Layer.CompareTo(o2.Layer);
				if (compare == 0)
				{
					compare = o1.SortLayer.CompareTo(o2.SortLayer);
				}

				return compare;
			});
		}
	}
}
