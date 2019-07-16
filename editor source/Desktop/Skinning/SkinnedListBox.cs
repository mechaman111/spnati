using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedListBox : ListBox, ISkinControl
	{
		protected override void OnCreateControl()
		{
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void OnUpdateSkin(Skin skin)
		{
			ForeColor = skin.Surface.ForeColor;
			BackColor = skin.FieldBackColor;
			Font = Skin.TextFont;
		}

		public void RefreshListItems()
		{
			this.RefreshItems();
		}
	}

	public class SkinnedCheckedListBox : CheckedListBox, ISkinControl
	{
		public SkinnedCheckedListBox()
		{
			DoubleBuffered = true;
		}

		protected override void OnCreateControl()
		{
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void OnUpdateSkin(Skin skin)
		{
			ForeColor = skin.Surface.ForeColor;
			BackColor = skin.FieldBackColor;
			Font = Skin.TextFont;
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;

			e.DrawBackground();
			if (e.Index >= Items.Count)
			{
				return;
			}
			bool isChecked = GetItemChecked(e.Index);

			Graphics g = e.Graphics;

			using (Brush boxColor = new SolidBrush(skin.FieldBackColor))
			{
				int start = e.Bounds.Y + e.Bounds.Height / 2 - SkinnedCheckBox.CheckBoxSize / 2;
				Rectangle rect = new Rectangle(e.Bounds.X + 1, start, SkinnedCheckBox.CheckBoxSize, SkinnedCheckBox.CheckBoxSize);
				g.FillRectangle(boxColor, rect);
				Pen borderPen = skin.PrimaryColor.GetPen(VisualState.Normal, Focused, Enabled);
				g.DrawRectangle(borderPen, rect);

				if (isChecked)
				{
					Brush checkBrush = skin.PrimaryColor.GetBrush(VisualState.Normal, true, Enabled);
					g.FillRectangle(checkBrush, rect);
					using (Pen check = new Pen(skin.PrimaryColor.ForeColor, 2))
					{
						for (int i = 0; i < SkinnedCheckBox.CheckmarkPoints.Length - 1; i++)
						{
							Point pt0 = new Point(SkinnedCheckBox.CheckmarkPoints[i].X + e.Bounds.X + 1, SkinnedCheckBox.CheckmarkPoints[i].Y + e.Bounds.Y - 1);
							Point pt1 = new Point(SkinnedCheckBox.CheckmarkPoints[i + 1].X + e.Bounds.X + 1, SkinnedCheckBox.CheckmarkPoints[i + 1].Y + e.Bounds.Y - 1);
							g.DrawLine(check, pt0, pt1);
						}
					}
				}
			}

			using (StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
			{
				using (Brush brush = new SolidBrush(ForeColor))
				{
					e.Graphics.DrawString(Items[e.Index].ToString(), Font, brush, new Rectangle(e.Bounds.Left + SkinnedCheckBox.CheckBoxSize + 3, e.Bounds.Top, e.Bounds.Width - SkinnedCheckBox.CheckBoxSize - 3, e.Bounds.Height), sf);
				}
			}
		}
	}
}
