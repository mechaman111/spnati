using System;
using System.Drawing;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class SimpleIK
	{
		public void Solve(LiveSprite hand, PointF target)
		{
			if (hand.Parent == null) { return; }
			LiveSprite Joint1 = hand.Parent;
			if (Joint1.Parent == null) { return; }
			LiveSprite Joint0 = Joint1.Parent;

			PointF pos0 = Joint0.ToWorldPt(0, 0);
			PointF pos1 = Joint1.ToWorldPt(0, 0);
			PointF posHand = hand.ToWorldPt(0, 0);

			float length0 = pos0.Distance(pos1);
			float length1 = pos1.Distance(posHand);

			//distance from origin to target
			float length2 = pos0.Distance(target);

			//inner angle alpha
			float cosAngle0 = ((length2 * length2) + (length0 * length0) - (length1 * length1)) / (2 * length2 * length0);
			float angle0 = (float)Math.Acos(cosAngle0) * MathUtil.Rad2Deg;

			//inner angle beta
			float cosAngle1 = ((length1 * length1) + (length0 * length0) - (length2 * length2)) / (2 * length1 * length0);
			float angle1 = (float)Math.Acos(cosAngle1) * MathUtil.Rad2Deg;

			//angle from origin and target
			PointF diff = new PointF(target.X - pos0.X, target.Y - pos0.Y);
			float atan = (float)Math.Atan2(diff.Y, diff.X) * MathUtil.Rad2Deg;

			float jointAngle0 = atan - angle0;
			float jointAngle1 = 180 - angle1;

			Joint0.Rotation = jointAngle0;
			Joint1.Rotation = jointAngle1;
		}
	}
}
