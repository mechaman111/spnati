using System;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedColorTable : ProfessionalColorTable
	{
		public Skin Skin;
		public ColorSet MenuSet;
		public ColorSet CheckedSet;
		private SkinnedBackgroundType _type;

		public SkinnedColorTable(Skin skin, Control owner)
		{
			Skin = skin;
			ParseMenuType(owner, owner.Tag?.ToString());
		}

		private void ParseMenuType(Control owner, string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				//use default for the control type
				if (owner is StatusStrip)
				{
					_type = SkinnedBackgroundType.PrimaryDark;
				}
				else if (owner is MenuStrip)
				{
					_type = SkinnedBackgroundType.Primary;
				}
				else
				{
					_type = SkinnedBackgroundType.PrimaryLight;
				}
			}
			else
			{
				Enum.TryParse(tag, out _type);
			}

			CheckedSet = Skin.PrimaryLightColor;
			switch (_type)
			{
				case SkinnedBackgroundType.Surface:
					MenuSet = Skin.Surface;
					break;
				case SkinnedBackgroundType.Primary:
					MenuSet = Skin.PrimaryColor;
					break;
				case SkinnedBackgroundType.PrimaryDark:
					MenuSet = Skin.PrimaryDarkColor;
					break;
				case SkinnedBackgroundType.PrimaryLight:
					MenuSet = Skin.PrimaryLightColor;
					CheckedSet = Skin.PrimaryDarkColor;
					break;
				case SkinnedBackgroundType.Secondary:
					MenuSet = Skin.SecondaryColor;
					CheckedSet = Skin.SecondaryLightColor;
					break;
				case SkinnedBackgroundType.SecondaryDark:
					MenuSet = Skin.SecondaryDarkColor;
					CheckedSet = Skin.SecondaryLightColor;
					break;
				case SkinnedBackgroundType.SecondaryLight:
					MenuSet = Skin.SecondaryLightColor;
					CheckedSet = Skin.SecondaryDarkColor;
					break;
				case SkinnedBackgroundType.Background:
					MenuSet = Skin.Background;
					CheckedSet = Skin.Surface;
					break;
				case SkinnedBackgroundType.Group1:
					MenuSet = Skin.Group1Set;
					CheckedSet = Skin.Group1Set;
					break;
				case SkinnedBackgroundType.Group2:
					MenuSet = Skin.Group2Set;
					CheckedSet = Skin.Group2Set;
					break;
				case SkinnedBackgroundType.Group3:
					MenuSet = Skin.Group3Set;
					CheckedSet = Skin.Group3Set;
					break;
				case SkinnedBackgroundType.Group4:
					MenuSet = Skin.Group4Set;
					CheckedSet = Skin.Group4Set;
					break;
				case SkinnedBackgroundType.Group5:
					MenuSet = Skin.Group5Set;
					CheckedSet = Skin.Group5Set;
					break;
				default:
					MenuSet = Skin.PrimaryColor;
					break;
			}
		}

		//Toolstrip bottom border
		public override Color ToolStripBorder
		{
			get { return Color.Transparent; }
		}

		// Status strip background
		public override Color StatusStripGradientBegin
		{
			get { return MenuSet.Normal; }
		}
		public override Color StatusStripGradientEnd
		{
			get { return StatusStripGradientBegin; }
		}

		// Drop down menu border
		public override Color MenuBorder
		{
			get { return MenuSet.Border; }
		}

		// All menu hover effect borders (not tool strip)
		public override Color MenuItemBorder
		{
			get { return Color.Transparent; }
		}
		// Toolstrip selected+hover buttons
		public override Color ButtonSelectedBorder
		{
			get { return MenuSet.BorderHover; }
		}

		// MenuStrip background
		public override Color MenuStripGradientBegin
		{
			get { return MenuSet.Normal; }
		}
		public override Color MenuStripGradientEnd
		{
			get { return MenuStripGradientBegin; }
		}

		// top-level menu hover effect
		public override Color MenuItemSelectedGradientBegin
		{
			get { return MenuSet.Hover; }
		}
		public override Color MenuItemSelectedGradientEnd
		{
			get { return MenuItemSelectedGradientBegin; }
		}

		// Toolstrip background
		public override Color ToolStripGradientBegin
		{
			get { return MenuSet.Normal; }
		}
		public override Color ToolStripGradientMiddle
		{
			get { return ToolStripGradientBegin; }
		}
		public override Color ToolStripGradientEnd
		{
			get { return ToolStripGradientBegin; }
		}

		// Drop Down Menu background
		public override Color ToolStripDropDownBackground
		{
			get { return Skin.Surface.Normal; }
		}
		public override Color ImageMarginGradientBegin
		{
			get { return ToolStripDropDownBackground; }
		}
		public override Color ImageMarginGradientMiddle
		{
			get { return ToolStripDropDownBackground; }
		}
		public override Color ImageMarginGradientEnd
		{
			get { return ToolStripDropDownBackground; }
		}

		// top-level menu button background with dropdown open
		public override Color MenuItemPressedGradientBegin
		{
			get { return ToolStripDropDownBackground; }
		}
		public override Color MenuItemPressedGradientMiddle
		{
			get { return MenuItemPressedGradientBegin; }
		}
		public override Color MenuItemPressedGradientEnd
		{
			get { return MenuItemPressedGradientBegin; }
		}

		// Drop down Item hover effect
		public override Color MenuItemSelected
		{
			get { return MenuSet.Hover; }
		}

		// Toolstrip item hover effect
		public override Color ButtonSelectedGradientBegin
		{
			get { return MenuSet.Hover; }
		}
		public override Color ButtonSelectedGradientMiddle
		{
			get { return ButtonSelectedGradientBegin; }
		}
		public override Color ButtonSelectedGradientEnd
		{
			get { return ButtonSelectedGradientBegin; }
		}

		// Checked button background
		public override Color ButtonCheckedGradientBegin
		{
			get { return CheckedSet.Normal; }
		}
		public override Color ButtonCheckedGradientMiddle
		{
			get { return ButtonCheckedGradientBegin; }
		}
		public override Color ButtonCheckedGradientEnd
		{
			get { return ButtonCheckedGradientBegin; }
		}
		public override Color CheckBackground
		{
			get { return ButtonCheckedGradientBegin; }
		}
		public override Color CheckSelectedBackground
		{
			get { return CheckedSet.Selected; }
		}
		public override Color CheckPressedBackground
		{
			get { return CheckSelectedBackground; }
		}

		// Separator
		public override Color SeparatorDark
		{
			get { return Skin.Separator; }
		}
		public override Color SeparatorLight
		{
			get { return SeparatorDark; }
		}

		// Overflow button background
		public override Color OverflowButtonGradientBegin
		{
			get { return MenuSet.Normal; }
		}
		public override Color OverflowButtonGradientMiddle
		{
			get { return OverflowButtonGradientBegin; }
		}
		public override Color OverflowButtonGradientEnd
		{
			get { return OverflowButtonGradientBegin; }
		}
		public override Color ButtonPressedGradientBegin
		{
			get { return MenuSet.Pressed; }
		}
		public override Color ButtonPressedGradientMiddle
		{
			get { return ButtonPressedGradientBegin; }
		}
		public override Color ButtonPressedGradientEnd
		{
			get { return ButtonPressedGradientBegin; }
		}
	}

	public class SkinnedToolStripRenderer : ToolStripProfessionalRenderer
	{
		public ColorSet MenuSet;

		public SkinnedToolStripRenderer(Skin skin, Control owner) : base(new SkinnedColorTable(skin, owner))
		{
			Skin = skin;
			RoundedEdges = false;
			MenuSet = (ColorTable as SkinnedColorTable).MenuSet;
		}

		public Skin Skin;

		private Color GetForeColor(ToolStripItem item)
		{
			if (item.IsOnDropDown)
			{
				if (item.IsOnOverflow)
				{
					return item.Selected ? MenuSet.ForeColor : Skin.Surface.ForeColor;
				}
				else
				{
					return item.Selected ? MenuSet.ForeColor : Skin.Surface.ForeColor;
				}
			}
			else
			{
				ToolStripMenuItem mi = item as ToolStripMenuItem;
				if (mi != null)
				{
					if (mi.DropDownItems.Count > 0 && item.Pressed)
					{
						return Skin.Surface.ForeColor;
					}
					else
					{
						return MenuSet.ForeColor;
					}
				}
				else
				{
					ToolStripButton btn = item as ToolStripButton;
					ToolStripDropDownItem dropdownBtn = item as ToolStripDropDownItem;
					if (btn != null && btn.Checked && !item.Pressed && !item.Selected)
					{
						return (ColorTable as SkinnedColorTable).CheckedSet.ForeColor;
					}
					else if (dropdownBtn != null && dropdownBtn.Pressed)
					{
						return Skin.Surface.ForeColor;
					}
					return MenuSet.ForeColor;
				}
			}
		}

		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			e.TextColor = GetForeColor(e.Item);
			base.OnRenderItemText(e);
		}

		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			e.ArrowColor = GetForeColor(e.Item);
			base.OnRenderArrow(e);
		}

		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (!(e.ToolStrip is StatusStrip))
			{
				base.OnRenderToolStripBorder(e);
			}
		}
	}
}
