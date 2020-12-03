using System;
using System.Drawing;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor.DataStructures
{
	/// <summary>
	/// Serialization friendly Point
	/// </summary>
	[Serializable]
	public class Point2D
	{
		[XmlAttribute("x")]
		public int X;
		[XmlAttribute("y")]
		public int Y;

		public Point2D() { }

		public Point2D(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return $"({X},{Y})";
		}

		public Point ToPoint()
		{
			return new Point(X, Y);
		}

		public static implicit operator Point(Point2D p)
		{
			return new Point(p.X, p.Y);
		}

		public static implicit operator Point2D(Point pt)
		{
			return new Point2D(pt.X, pt.Y);
		}
	}
}
