using System;
using System.Drawing;

namespace SPNATI_Character_Editor
{
	public static class MathExtensions
	{
		/// <summary>
		/// Gets the shortest distance between a point and a line segment
		/// </summary>
		/// <param name="pt"></param>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns></returns>
		public static float DistanceFromLineSegment(this PointF pt, PointF p1, PointF p2)
		{
			PointF ptOnLine = GetClosestPointOnLineSegment(pt, p1, p2);

			float dx = pt.X - ptOnLine.X;
			float dy = pt.Y - ptOnLine.Y;
			return (float)Math.Sqrt(dx * dx + dy * dy);
		}

		/// <summary>
		/// Gets the closest point on a line segment to another point
		/// </summary>
		/// <param name="pt"></param>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns></returns>
		public static PointF GetClosestPointOnLineSegment(this PointF pt, PointF p1, PointF p2)
		{
			float a = pt.X - p1.X;
			float b = pt.Y - p1.Y;
			float c = p2.X - p1.X;
			float d = p2.Y - p1.Y;

			float dot = a * c + b * d;
			float sqrLength = c * c + d * d;
			float side = -1;
			if (sqrLength != 0)
			{
				side = dot / sqrLength;
			}

			float xx, yy;
			if (side < 0)
			{
				xx = p1.X;
				yy = p1.Y;
			}
			else if (side > 1)
			{
				xx = p2.X;
				yy = p2.Y;
			}
			else
			{
				xx = p1.X + side * c;
				yy = p1.Y + side * d;
			}
			return new PointF(xx, yy);
		}
		public static Point GetClosestPointOnLineSegment(this Point pt, Point p1, Point p2)
		{
			PointF ptOnLine = GetClosestPointOnLineSegment(new PointF(pt.X, pt.Y), new PointF(p1.X, p1.Y), new PointF(p2.X, p2.Y));
			return new Point((int)Math.Round(ptOnLine.X), (int)Math.Round(ptOnLine.Y));
		}

		public static float DistanceFromLineSegment(this Point pt, Point p1, Point p2)
		{
			return (int)Math.Round(DistanceFromLineSegment(new PointF(pt.X, pt.Y), new PointF(p1.X, p1.Y), new PointF(p2.X, p2.Y)));
		}

		public static PointF Add(this PointF p1, PointF p2)
		{
			return new PointF(p1.X + p2.X, p1.Y + p2.Y);
		}

		public static float Distance(this PointF p1, PointF p2)
		{
			return (float)Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
		}

		public static float Distance(this Point p1, Point p2)
		{
			return (float)Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
		}
	}
}
