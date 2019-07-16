using System;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedLabel : Label, ISkinControl
	{
		private SkinnedLabelLevel _level = SkinnedLabelLevel.Normal;
		public SkinnedLabelLevel Level
		{
			get { return _level; }
			set
			{
				_level = value;
				OnUpdateSkin(SkinManager.Instance.CurrentSkin);
				Invalidate(true);
			}
		}

		private SkinnedHighlight _highlight = SkinnedHighlight.Normal;
		public SkinnedHighlight Highlight
		{
			get { return _highlight; }
			set
			{
				_highlight = value;
				OnUpdateSkin(SkinManager.Instance.CurrentSkin);
				Invalidate(true);
			}
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			SkinManager.Instance.UnregisterControl(this);
			base.OnHandleDestroyed(e);
		}

		public void OnUpdateSkin(Skin skin)
		{
			switch (Level)
			{
				case SkinnedLabelLevel.Normal:
					ForeColor = DesignMode ? ForeColor : this.GetSkinnedPanelForeColor();
					Font = Skin.TextFont;
					break;
				case SkinnedLabelLevel.Label:
					ForeColor = skin.LabelForeColor;
					Font = Skin.TextFont;
					break;
				case SkinnedLabelLevel.Heading:
					ForeColor = skin.PrimaryForeColor;
					Font = Skin.HeaderFont;
					break;
				case SkinnedLabelLevel.Primary:
					ForeColor = skin.PrimaryColor.ForeColor;
					Font = Skin.TextFont;
					break;
				case SkinnedLabelLevel.PrimaryLight:
					ForeColor = skin.PrimaryLightColor.ForeColor;
					Font = Skin.TextFont;
					break;
				case SkinnedLabelLevel.PrimaryDark:
					ForeColor = skin.PrimaryDarkColor.ForeColor;
					Font = Skin.TextFont;
					break;
				case SkinnedLabelLevel.Secondary:
					ForeColor = skin.SecondaryColor.ForeColor;
					Font = Skin.TextFont;
					break;
				case SkinnedLabelLevel.SecondaryLight:
					ForeColor = skin.SecondaryLightColor.ForeColor;
					Font = Skin.TextFont;
					break;
				case SkinnedLabelLevel.SecondaryDark:
					ForeColor = skin.SecondaryDarkColor.ForeColor;
					Font = Skin.TextFont;
					break;
				case SkinnedLabelLevel.Title:
					ForeColor = skin.PrimaryColor.ForeColor;
					Font = Skin.TitleFont;
					break;
				case SkinnedLabelLevel.Finished:
					ForeColor = skin.GoodForeColor;
					Font = Skin.CompletionFont;
					break;
			}

			switch (Highlight)
			{
				case SkinnedHighlight.Label:
					ForeColor = skin.LabelForeColor;
					break;
				case SkinnedHighlight.Good:
					ForeColor = skin.GoodForeColor;
					break;
				case SkinnedHighlight.Bad:
					ForeColor = skin.BadForeColor;
					break;
				case SkinnedHighlight.Heading:
					ForeColor = skin.PrimaryForeColor;
					break;
			}
		}
	}

	public enum SkinnedLabelLevel
	{
		Normal,
		Label,
		Heading,
		Primary,
		PrimaryLight,
		PrimaryDark,
		Secondary,
		SecondaryLight,
		SecondaryDark,
		Title,
		Finished,
	}

	public class SkinnedLinkLabel : LinkLabel, ISkinControl
	{
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void OnUpdateSkin(Skin skin)
		{
			Font = Skin.TextFont;
			ForeColor = skin.Surface.ForeColor;
			LinkColor = skin.Blue;
		}
	}
}
