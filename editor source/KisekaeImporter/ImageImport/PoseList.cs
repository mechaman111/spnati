using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace KisekaeImporter.ImageImport
{
	/// <summary>
	/// List of pose data
	/// </summary>
	public class PoseList
	{
		/// <summary>
		/// Cropping information
		/// </summary>
		public Rect Crop = new Rect(0, 0, 600, 1400);

		/// <summary>
		/// List of poses
		/// </summary>
		public List<ImageMetadata> Poses = new List<ImageMetadata>();
	}

	[Serializable]
	public struct Rect
	{
		[XmlAttribute("top")]
		public int Top;
		[XmlAttribute("right")]
		public int Right;
		[XmlAttribute("bottom")]
		public int Bottom;
		[XmlAttribute("left")]
		public int Left;

		public Rect(int l, int t, int r, int b)
		{
			Top = t;
			Right = r;
			Bottom = b;
			Left = l;
		}

		public Rect(string l, string t, string r, string b)
		{
			int.TryParse(l, out Left);
			int.TryParse(t, out Top);
			int.TryParse(r, out Right);
			int.TryParse(b, out Bottom);
		}

		public RectangleF ToRectangle(float zoom)
		{
			float l = Left * zoom;
			float t = Top * zoom;
			float r = Right * zoom;
			float b = Bottom * zoom;
			return new RectangleF(l, t, (r - l), (b - t));
		}

		public override string ToString()
		{
			return string.Format("{0},{1},{2},{3}", Left, Top, Right - Left, Bottom - Top);
		}

		public string Serialize()
		{
			return string.Format("{0},{1},{2},{3}", Left, Top, Right, Bottom);
		}

		public static bool operator ==(Rect a, Rect b)
		{
			return a.Left == b.Left && a.Top == b.Top && a.Right == b.Right && a.Bottom == b.Bottom;
		}

		public static bool operator !=(Rect a, Rect b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj is Rect)
				return this == (Rect)obj;
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	public static class RectExtensions
	{
		public static Rect ToRect(this RectangleF rect, float zoom)
		{
			float l = rect.Left / zoom;
			float r = rect.Right / zoom;
			float t = rect.Top / zoom;
			float b = rect.Bottom / zoom;
			return new Rect((int)l, (int)t, (int)r, (int)b);
		}
	}
}
