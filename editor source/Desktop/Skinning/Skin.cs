using Desktop.CommonControls.PropertyControls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Desktop.Skinning
{
	public class Skin : IComparable<Skin>, IRecord
	{
		public static Font HeaderFont = new Font("Segoe UI", 12);
		public static Font TextFont = new Font("Microsoft Sans Serif", 8.25f);
		public static Font ButtonFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
		public static Font TabFont = new Font("Segoe UI", 9);
		public static Font ActiveTabFont = new Font("Segoe UI", 9, FontStyle.Bold);
		public static Font DecorationFont = new Font("Segoe UI", 8);
		public static Font TitleFont = new Font("Segoe UI", 16);
		public static Font CompletionFont = new Font("Segoe UI", 28);

		[Text(DisplayName = "Name")]
		public string Name { get; set; }
		[JsonIgnore]
		public string Key { get { return Name; } set { Name = value; } }
		[Text(DisplayName = "Group")]
		public string Group { get; set; }
		[Text(DisplayName = "Description")]
		public string Description { get; set; }

		[ColorSet(DisplayName = "Primary")]
		public ColorSet PrimaryColor { get; set; } = new ColorSet();
		[ColorSet(DisplayName = "Light 1")]
		public ColorSet PrimaryLightColor { get; set; } = new ColorSet();
		[ColorSet(DisplayName = "Dark 1")]
		public ColorSet PrimaryDarkColor { get; set; } = new ColorSet();

		[ColorSet(DisplayName = "Secondary")]
		public ColorSet SecondaryColor { get; set; } = new ColorSet();
		[ColorSet(DisplayName = "Light 2")]
		public ColorSet SecondaryLightColor { get; set; } = new ColorSet();
		[ColorSet(DisplayName = "Dark 2")]
		public ColorSet SecondaryDarkColor { get; set; } = new ColorSet();

		[ColorSet(DisplayName = "Surface")]
		public ColorSet Surface { get; set; } = new ColorSet();

		[ColorSet(DisplayName = "Background")]
		public ColorSet Background { get; set; } = new ColorSet();

		[ColorSet(DisplayName = "Widget")]
		public ColorSet PrimaryWidget { get; set; } = new ColorSet();

		[ColorSet(DisplayName = "SecondaryWidget")]
		public ColorSet SecondaryWidget { get; set; } = new ColorSet();

		[ColorSet(DisplayName = "Critical")]
		public ColorSet Critical { get; set; } = new ColorSet();

		[Color(DisplayName = "Shadow")]
		public Color SurfaceShadowColor = Color.LightGray;

		[Color(DisplayName = "Field Back")]
		public Color FieldBackColor = Color.White;
		[Color(DisplayName = "Field Alt")]
		public Color FieldAltBackColor = SystemColors.ControlLightLight;
		[Color(DisplayName = "Field Disabled")]
		public Color FieldDisabledBackColor = SystemColors.ControlLight;

		[Color(DisplayName = "Label Text")]
		public Color LabelForeColor = Color.FromArgb(127, 0, 0, 0);
		[Color(DisplayName = "Primary Text")]
		public Color PrimaryForeColor = Color.Blue;
		[Color(DisplayName = "Primary Text (Light)")]
		public Color PrimaryLightForeColor = Color.Blue;
		[Color(DisplayName = "Secondary Text")]
		public Color SecondaryForeColor = Color.Red;
		[Color(DisplayName = "Secondary Text (Light)")]
		public Color SecondaryLightForeColor = Color.Red;

		[Color(DisplayName = "Separator")]
		public Color Separator = Color.FromArgb(220, 220, 220);
		[Color(DisplayName = "Hover Effect")]
		public Color HoverOverlay = Color.FromArgb(50, 127, 127, 127);
		[Color(DisplayName = "Press Effect")]
		public Color PressOverlay = Color.FromArgb(50, 64, 64, 64);

		[Color(DisplayName = "Error")]
		public Color ErrorBackColor = Color.DarkRed;

		[Color(DisplayName = "Good")]
		public Color GoodForeColor = Color.Green;

		[Color(DisplayName = "Caution")]
		public Color CautionForeColor = Color.Yellow;

		[Color(DisplayName = "Bad")]
		public Color BadForeColor = Color.Red;

		[Color(DisplayName = "Focus Rect")]
		public Color FocusRectangle = Color.Black;

		[Color(DisplayName = "Empty Graph")]
		public Color EmptyColor = Color.LightGray;

		#region Colors visible on a field
		[Color(DisplayName = "Gray")]
		public Color Gray = Color.Gray;
		[Color(DisplayName = "Light Gray")]
		public Color LightGray = Color.LightGray;
		[Color(DisplayName = "Orange")]
		public Color Orange = Color.OrangeRed;
		[Color(DisplayName = "Green")]
		public Color Green = Color.Green;
		[Color(DisplayName = "Blue")]
		public Color Blue = Color.Blue;
		[Color(DisplayName = "Purple")]
		public Color Purple = Color.Purple;
		[Color(DisplayName = "Pink")]
		public Color Pink = Color.Pink;
		[Color(DisplayName = "Red")]
		public Color Red = Color.Red;
		#endregion

		#region Accordion Groupers
		[Color(DisplayName = "Group 1")]
		public Color Group1 = Color.FromArgb(148, 89, 160);
		[Color(DisplayName = "Group 2")]
		public Color Group2 = Color.FromArgb(0, 98, 173);
		[Color(DisplayName = "Group 3")]
		public Color Group3 = Color.FromArgb(86, 119, 41);
		[Color(DisplayName = "Group 4")]
		public Color Group4 = Color.FromArgb(175, 89, 49);
		[Color(DisplayName = "Group 5")]
		public Color Group5 = Color.Black;
		[ColorSet(DisplayName = "Group 1 Set")]
		public ColorSet Group1Set { get; set; } = new ColorSet();
		[ColorSet(DisplayName = "Group 2 Set")]
		public ColorSet Group2Set { get; set; } = new ColorSet();
		[ColorSet(DisplayName = "Group 3 Set")]
		public ColorSet Group3Set { get; set; } = new ColorSet();
		[ColorSet(DisplayName = "Group 4 Set")]
		public ColorSet Group4Set { get; set; } = new ColorSet();
		[ColorSet(DisplayName = "Group 5 Set")]
		public ColorSet Group5Set { get; set; } = new ColorSet();

		public Color GetGrouper(int number)
		{
			switch (number)
			{
				case 1:
					return Group1;
				case 2:
					return Group2;
				case 3:
					return Group3;
				case 4:
					return Group4;
				case 5:
					return Group5;
				default:
					return Surface.ForeColor;
			}
		}

		public ColorSet GetGrouperSet(int number)
		{
			switch (number)
			{
				case 1:
					return Group1Set;
				case 2:
					return Group2Set;
				case 3:
					return Group3Set;
				case 4:
					return Group4Set;
				case 5:
					return Group5Set;
				default:
					return Surface;
			}
		}
		#endregion

		#region App-specific colors
		public Dictionary<string, Color> AppColors = new Dictionary<string, Color>();
		#endregion

		public Skin()
		{
			ApplyDefaults();
		}

		public override string ToString()
		{
			return Name;
		}

		private void ApplyDefaults()
		{
			Name = "Default";
			Background = new ColorSet();
			PrimaryColor = new ColorSet()
			{
				Normal = Color.FromArgb(92, 107, 192),
				Hover = Color.FromArgb(117, 132, 217),
				Pressed = Color.FromArgb(67, 82, 167),
				Selected = Color.FromArgb(92, 107, 192),
				Disabled = Color.Gray,
				ForeColor = Color.White,
				DisabledForeColor = Color.FromArgb(191, 191, 191),
			};
			PrimaryDarkColor = new ColorSet()
			{
				Normal = Color.FromArgb(38, 65, 143),
				Hover = Color.FromArgb(63, 90, 168),
				Pressed = Color.FromArgb(13, 40, 118),
				Selected = Color.FromArgb(38, 65, 143),
				Disabled = Color.Gray,
				ForeColor = Color.White,
				DisabledForeColor = Color.FromArgb(191, 191, 191),
			};
			PrimaryLightColor = new ColorSet()
			{
				Normal = Color.FromArgb(142, 153, 243),
				Hover = Color.FromArgb(117, 128, 218),
				Pressed = Color.FromArgb(167, 178, 255),
				Selected = Color.FromArgb(142, 153, 243),
				Disabled = Color.FromArgb(205, 205, 205),
				ForeColor = Color.Black,
				DisabledForeColor = Color.FromArgb(191, 191, 191),
			};
			SecondaryColor = new ColorSet()
			{
				Normal = Color.White,
				Hover = Color.Gray,
				Pressed = Color.DarkGray,
				Selected = Color.White,
				Disabled = Color.Gray,
				ForeColor = Color.Black,
				DisabledForeColor = Color.Gray
			};

			FieldBackColor = Color.White;
			FieldDisabledBackColor = SystemColors.ControlLightLight;
		}

		public Color GetAppColor(string name)
		{
			Color output = Color.Black;
			AppColors.TryGetValue(name, out output);
			return output;
		}

		/// <summary>
		/// Creates an icon representing this theme
		/// </summary>
		/// <returns></returns>
		public Bitmap GetIcon()
		{
			Bitmap bmp = new Bitmap(16, 16);
			Graphics g = Graphics.FromImage(bmp);
			g.DrawRectangle(PrimaryColor.GetBorderPen(VisualState.Normal, false, true), 0, 0, bmp.Width - 1, bmp.Height - 1);
			//main color on top
			g.FillRectangle(PrimaryColor.GetBrush(VisualState.Normal, false, true), 1, 1, bmp.Width - 2, 6);
			//secondary in middle
			g.FillRectangle(SecondaryColor.GetBrush(VisualState.Normal, false, true), 1, 7, bmp.Width - 2, 3);
			//background on bottom
			g.FillRectangle(Background.GetBrush(VisualState.Normal, false, true), 1, 10, bmp.Width - 2, 5);

			g.Dispose();
			return bmp;
		}

		public int CompareTo(Skin other)
		{
			return Name.CompareTo(other.Name);
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public Color GetForeColor(SkinnedFieldType type)
		{
			switch (type)
			{

				case SkinnedFieldType.Primary:
					return PrimaryForeColor;
				case SkinnedFieldType.Secondary:
					return SecondaryForeColor;
				default:
					return Surface.ForeColor;
			}
		}

		public ColorSet GetFieldColorSet(SkinnedFieldType type, SkinnedLightLevel level)
		{
			switch (type)
			{
				case SkinnedFieldType.Primary:
					switch (level)
					{
						case SkinnedLightLevel.Dark:
							return PrimaryDarkColor;
						case SkinnedLightLevel.Light:
							return PrimaryLightColor;
						default:
							return PrimaryColor;
					}
				case SkinnedFieldType.Secondary:
					switch (level)
					{
						case SkinnedLightLevel.Dark:
							return SecondaryDarkColor;
						case SkinnedLightLevel.Light:
							return SecondaryLightColor;
						default:
							return SecondaryColor;
					}
				default:
					return Surface;
			}
		}

		public ColorSet GetWidgetColorSet(SkinnedFieldType type)
		{
			switch (type)
			{
				case SkinnedFieldType.Secondary:
					return SecondaryWidget;
				default:
					return PrimaryWidget;
			}
		}

		public ColorSet GetColorSet(SkinnedBackgroundType type)
		{
			switch (type)
			{
				case SkinnedBackgroundType.Surface:
					return Surface;
				case SkinnedBackgroundType.Primary:
					return PrimaryColor;
				case SkinnedBackgroundType.PrimaryDark:
					return PrimaryDarkColor;
				case SkinnedBackgroundType.PrimaryLight:
					return PrimaryLightColor;
				case SkinnedBackgroundType.Secondary:
					return SecondaryColor;
				case SkinnedBackgroundType.SecondaryDark:
					return SecondaryDarkColor;
				case SkinnedBackgroundType.SecondaryLight:
					return SecondaryLightColor;
				default:
					return Background;
			}
		}

		public Color GetBackColor(SkinnedBackgroundType type)
		{
			switch (type)
			{
				case SkinnedBackgroundType.Surface:
					return Surface.Normal;
				case SkinnedBackgroundType.Primary:
					return PrimaryColor.Normal;
				case SkinnedBackgroundType.PrimaryDark:
					return PrimaryDarkColor.Normal;
				case SkinnedBackgroundType.PrimaryLight:
					return PrimaryLightColor.Normal;
				case SkinnedBackgroundType.Secondary:
					return SecondaryColor.Normal;
				case SkinnedBackgroundType.SecondaryDark:
					return SecondaryDarkColor.Normal;
				case SkinnedBackgroundType.SecondaryLight:
					return SecondaryLightColor.Normal;
				case SkinnedBackgroundType.Transparent:
					return Color.Transparent;
				case SkinnedBackgroundType.Field:
					return FieldBackColor;
				case SkinnedBackgroundType.Group1:
					return Group1Set.Normal;
				case SkinnedBackgroundType.Group2:
					return Group2Set.Normal;
				case SkinnedBackgroundType.Group3:
					return Group3Set.Normal;
				case SkinnedBackgroundType.Group4:
					return Group4Set.Normal;
				case SkinnedBackgroundType.Group5:
					return Group5Set.Normal;
				default:
					return Background.Normal;
			}
		}

		public Color GetForeColor(SkinnedBackgroundType type)
		{
			switch (type)
			{
				case SkinnedBackgroundType.Surface:
					return Surface.ForeColor;
				case SkinnedBackgroundType.Primary:
					return PrimaryColor.ForeColor;
				case SkinnedBackgroundType.PrimaryDark:
					return PrimaryDarkColor.ForeColor;
				case SkinnedBackgroundType.PrimaryLight:
					return PrimaryLightColor.ForeColor;
				case SkinnedBackgroundType.Secondary:
					return SecondaryColor.ForeColor;
				case SkinnedBackgroundType.SecondaryDark:
					return SecondaryDarkColor.ForeColor;
				case SkinnedBackgroundType.SecondaryLight:
					return SecondaryLightColor.ForeColor;
				case SkinnedBackgroundType.Background:
					return Background.ForeColor;
				default:
					return Surface.ForeColor;
			}
		}

		public Color GetHighlightColor(DataHighlight highlight)
		{
			switch (highlight)
			{
				case DataHighlight.Important:
					return CautionForeColor;
				default:
					return PrimaryColor.Normal;
			}
		}

		public Color GetHighlightColor(SkinnedHighlight highlight)
		{
			switch (highlight)
			{
				case SkinnedHighlight.Label:
					return LabelForeColor;
				case SkinnedHighlight.Good:
					return GoodForeColor;
				case SkinnedHighlight.Bad:
					return BadForeColor;
				case SkinnedHighlight.Heading:
					return PrimaryForeColor;
				case SkinnedHighlight.SecondaryHeading:
					return SecondaryForeColor;
				default:
					return LabelForeColor;
			}
		}

		public string ToLookupString()
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Which color set to use for a control's background
	/// </summary>
	public enum SkinnedBackgroundType
	{
		Background,
		Surface,
		PrimaryDark,
		Primary,
		PrimaryLight,
		SecondaryDark,
		Secondary,
		SecondaryLight,
		Transparent,
		Field,
		Group1,
		Group2,
		Group3,
		Group4,
		Group5,
		Critical,
	}

	/// <summary>
	/// Which color set to use for a field
	/// </summary>
	public enum SkinnedFieldType
	{
		Surface,
		Primary,
		Secondary
	}

	public enum SkinnedLightLevel
	{
		Normal,
		Light,
		Dark
	}

	public enum SkinnedHighlight
	{
		Normal,
		Label,
		Good,
		Bad,
		Heading,
		SecondaryHeading,
	}

	public enum DataHighlight
	{
		Normal,
		Important
	}
}
