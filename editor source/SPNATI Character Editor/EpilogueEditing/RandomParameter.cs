using System.Drawing;
using System.Globalization;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class RandomParameter
	{
		public float Min;
		public float Max;

		public override string ToString()
		{
			return $"[{Min}-{Max}]";
		}

		public RandomParameter(float min, float max)
		{
			Min = min;
			Max = max;
		}

		public float Get()
		{
			return MathUtil.Lerp(Min, Max, MathUtil.GetRandom());
		}

		public static RandomParameter Create(string value, float defaultMin, float defaultMax)
		{
			if (!string.IsNullOrEmpty(value))
			{
				string[] range = value.Split(':');
				string minString = range[0];
				string maxString = range.Length > 1 ? range[1] : range[0];
				float min, max;
				if (float.TryParse(minString, out min) && float.TryParse(maxString, out max))
				{
					return new RandomParameter(min, max);
				}
			}

			return new RandomParameter(defaultMin, defaultMax);
		}

		public static RandomParameter Create(string value, RandomParameter defaultParameter)
		{
			return Create(value, defaultParameter?.Min ?? 0, defaultParameter?.Max ?? 0);
		}

		public string Serialize()
		{
			if (Min == Max)
			{
				return Min.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				return $"{Min.ToString(CultureInfo.InvariantCulture)}:{Max.ToString(CultureInfo.InvariantCulture)}";
			}
		}
	}

	public class RandomColor
	{
		public Color Min;
		public Color Max;

		public override string ToString()
		{
			return $"[{Min}-{Max}]";
		}

		public Color Get()
		{
			return MathUtil.Lerp(Min, Max, MathUtil.GetRandom());
		}

		public RandomColor(Color min, Color max)
		{
			Min = min;
			Max = max;
		}

		public static RandomColor Create(string value, Color defaultMin, Color defaultMax)
		{
			if (!string.IsNullOrEmpty(value))
			{
				string[] range = value.Split(':');
				string minString = range[0];
				string maxString = range.Length > 1 ? range[1] : range[0];
				Color min;
				Color max;
				try
				{
					min = ColorTranslator.FromHtml(minString);
					max = ColorTranslator.FromHtml(maxString);
					return new RandomColor(min, max);
				}
				catch { }
			}

			return new RandomColor(defaultMin, defaultMax);
		}

		public static RandomColor Create(string value, RandomColor defaultParameter)
		{
			return Create(value, defaultParameter.Min, defaultParameter.Max);
		}

		public string Serialize()
		{
			if (Min == Max)
			{
				return Min.ToHexValue();
			}
			else
			{
				return $"{Min.ToHexValue()}:{Max.ToHexValue()}";
			}
		}
	}

	public class TweenableParameter
	{
		public float Start;
		public float End;
		public float Value;

		public override string ToString()
		{
			return $"{Value} [{Start}-{End}]";
		}

		public TweenableParameter(float start, float end)
		{
			Start = start;
			End = end;
			Value = start;
		}

		public float Tween(float t)
		{
			Value = MathUtil.Lerp(Start, End, t);
			return Value;
		}
	}

	public class TweenableColor
	{
		public Color Start;
		public Color End;
		public Color Value;

		public override string ToString()
		{
			return $"{Value} [{Start}-{End}]";
		}

		public TweenableColor(Color start, Color end)
		{
			Start = start;
			End = end;
			Value = start;
		}

		public Color Tween(float t)
		{
			Value = MathUtil.Lerp(Start, End, t);
			return Value;
		}
	}
}
