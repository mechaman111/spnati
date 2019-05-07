using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SPNATI_Character_Editor
{
	public static class AnimationHelpers
	{
		public static object Interpolate(Type type, object lastValue, object nextValue, string interpolationMode, float t, object lastLastValue, object nextNextValue)
		{
			if (type == typeof(float?))
			{
				return (float?)Interpolate((float)lastValue, (float)nextValue, interpolationMode, t, (float)lastLastValue, (float)nextNextValue);
			}
			else if (type == typeof(int?))
			{
				return (int?)Math.Round(Interpolate((int)lastValue, (int)nextValue, interpolationMode, t, (int)lastLastValue, (int)nextNextValue));
			}
			else if (type == typeof(string))
			{
				return t >= 1 ? nextValue : lastValue;
			}
			else
			{
				throw new Exception($"Cannot interpolate type: {type.Name}");
			}
		}

		public static float Interpolate(float lastValue, float nextValue, string interpolationMode, float t, float lastLastValue, float nextNextValue)
		{
			switch (interpolationMode)
			{
				case "spline":
					float p0 = lastLastValue;
					float p1 = lastValue;
					float p2 = nextValue;
					float p3 = nextNextValue;
					float a = 2 * p1;
					float b = p2 - p0;
					float c = 2 * p0 - 5 * p1 + 4 * p2 - p3;
					float d = -p0 + 3 * p1 - 3 * p2 + p3;
					float p = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
					return p;
				case "none":
					return t >= 1 ? nextValue : lastValue;
				default:
					return (nextValue - lastValue) * t + lastValue;
			}
		}

		public static float Ease(string method, float t)
		{
			switch (method)
			{
				case "linear":
					return t;
				case "ease-in":
					return t * t;
				case "ease-out":
					return t * (2 - t);
				case "elastic":
					return t == 0 ? 0 : (0.04f - 0.04f / t) * (float)Math.Sin(25 * t) + 1;
				case "ease-in-cubic":
					return t * t * t;
				case "ease-out-cubic":
					t--;
					return 1 + t * t * t;
				case "ease-in-sin":
					return 1 + (float)Math.Sin(Math.PI / 2 * t - Math.PI / 2);
				case "ease-out-sin":
					return (float)Math.Sin(Math.PI / 2 * t);
				case "ease-in-out-cubic":
					return t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
				case "ease-out-in":
					return t < 0.5f ? Ease("ease-out", 2 * t) * 0.5f : Ease("ease-in", 2 * (t - 0.5f)) * 0.5f + 0.5f;
				case "ease-out-in-cubic":
					return t < 0.5f ? Ease("ease-out-cubic", 2 * t) * 0.5f : Ease("ease-in-cubic", 2 * (t - 0.5f)) * 0.5f + 0.5f;
				case "bounce":
					if (t < 0.3636f)
					{
						return 7.5625f * t * t;
					}
					else if (t < 0.7273f)
					{
						t -= 0.5455f;
						return 7.5625f * t * t + 0.75f;
					}
					else if (t < 0.9091f)
					{
						t -= 0.8182f;
						return 7.5625f * t * t + 0.9375f;
					}
					else
					{
						t -= 0.9545f;
						return 7.5625f * t * t + 0.984375f;
					}
			}
			return 3 * t * t - 2 * t * t * t;
		}

		public static T Find<T>(this ObservableCollection<T> list, Func<T, bool> searchDelegate)
		{
			foreach (T val in list)
			{
				if (searchDelegate(val))
				{
					return val;
				}
			}
			return default(T);
		}

		public static void Sort<T>(this ObservableCollection<T> list, Comparison<T> comparison)
		{
			List<T> sorted = new List<T>(list);
			sorted.Sort(comparison);

			for (int i = 0; i < sorted.Count; i++)
			{
				list.Move(list.IndexOf(sorted[i]), i);
			}
		}
	}
}
