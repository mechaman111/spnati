using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Helper functions for setting and reading from input controls
	/// </summary>
	public static class GUIHelper
	{
		/// <summary>
		/// Sets a range value into its boxes
		/// </summary>
		/// <param name="minBox"></param>
		/// <param name="maxBox"></param>
		/// <param name="value"></param>
		public static void SetRange(ComboBox minBox, ComboBox maxBox, string value)
		{
			if (value == null)
			{
				minBox.Text = "";
				maxBox.Text = "";
				return;
			}
			string[] pieces = value.Split('-');
			string min = pieces[0];
			string max = null;
			if (pieces.Length > 1)
			{
				max = pieces[1];
			}
			if (string.IsNullOrEmpty(min))
			{
				minBox.Text = "";
			}
			else
			{
				minBox.Text = min;
			}
			if (string.IsNullOrEmpty(max))
			{
				maxBox.Text = "";
			}
			else
			{
				maxBox.Text = max;
			}
		}

		/// <summary>
		/// Sets a range value into its boxes
		/// </summary>
		/// <param name="minBox"></param>
		/// <param name="maxBox"></param>
		/// <param name="value"></param>
		public static void SetRange(NumericUpDown minBox, NumericUpDown maxBox, string value)
		{
			if (value == null)
			{
				SetNumericBox(minBox, null);
				SetNumericBox(maxBox, null);
				return;
			}
			string[] pieces = value.Split('-');
			string min = pieces[0];
			string max = null;
			if (pieces.Length > 1)
			{
				max = pieces[1];
			}
			SetNumericBox(minBox, min);
			SetNumericBox(maxBox, max);
		}

		public static string ReadRange(ComboBox minBox, ComboBox maxBox)
		{
			string min = minBox.Text;
			if (string.IsNullOrEmpty(min))
				return null;
			string max = maxBox.Text;
			if (string.IsNullOrEmpty(max))
				return min;
			return min + "-" + max;
		}

		public static string ReadRange(NumericUpDown minBox, NumericUpDown maxBox)
		{
			string min = ReadNumericBox(minBox);
			if (string.IsNullOrEmpty(min))
				return null;
			string max = ReadNumericBox(maxBox);
			if (string.IsNullOrEmpty(max))
				return min;
			return min + "-" + max;
		}

		/// <summary>
		/// Attempts to set a combo box's value to the provided text
		/// </summary>
		/// <param name="box"></param>
		/// <param name="text"></param>
		public static void SetComboBox(ComboBox box, string text)
		{
			box.Text = text;
		}

		/// <summary>
		/// Reads the value from a combo box
		/// </summary>
		/// <param name="box"></param>
		/// <returns></returns>
		public static string ReadComboBox(ComboBox box)
		{
			if (box.SelectedItem is Trigger)
			{
				return ((Trigger)box.SelectedItem).Tag;
			}
			else if (box.SelectedItem is Tag)
			{
				return TagDatabase.StringToTag(box.Text);
			}
			else if (box.SelectedItem is KeyValuePair<string, string>)
			{
				return (string)box.SelectedValue;
			}
			string value = box.Text;
			if (value == "")
				return null;
			else return value;
		}

		public static void SetNumericBox(NumericUpDown box, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				box.Text = "";
			}
			else
			{
				int v;
				if (int.TryParse(value, out v) && v >= box.Minimum && v <= box.Maximum)
				{
					box.Value = v;
					box.Text = v.ToString();
				}
			}
		}

		public static string ReadNumericBox(NumericUpDown box)
		{
			if (string.IsNullOrEmpty(box.Text))
				return null;
			return box.Value.ToString();
		}

		/// <summary>
		/// Converts a range string to a display-friendly format
		/// </summary>
		/// <param name="range"></param>
		/// <returns></returns>
		public static string RangeToString(string range)
		{
			string[] pieces = range.Split('-');
			if (pieces.Length == 1 || pieces[0] == pieces[1])
			{
				return pieces[0];
			}
			if (pieces.Length == 2 && string.IsNullOrEmpty(pieces[0]))
			{
				return $"0-{pieces[1]}";
			}
			if (pieces.Length == 2 && string.IsNullOrEmpty(pieces[1]))
			{
				return $"{pieces[0]}+";
			}
			return range;
		}

		/// <summary>
		/// Converts a min and max to a range string
		/// </summary>
		/// <param name="min">Min bound, or -1 if no min bound</param>
		/// <param name="max">Max bound, or -1 if no max bound</param>
		/// <returns></returns>
		public static string ToRange(int min, int max)
		{
			if (min == -1 && max == -1)
			{
				return null;
			}
			else if (min == -1)
			{
				//open-ended upper bound
				return $"-{max}";
			}
			else if (max == -1)
			{
				//open-ended lower bound
				return $"{min}-";
			}
			else if (max < min)
			{
				return $"{min}-{min}";
			}
			else if (max == min)
			{
				return $"{min}";
			}
			else
			{
				return $"{min}-{max}";
			}
		}
	}
}
