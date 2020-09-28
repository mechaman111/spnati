using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedListView : ListView, ISkinControl
	{
		private StringFormat _sf = new StringFormat() { LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter };
		private SolidBrush _fieldBrush = new SolidBrush(Color.White);
		private SolidBrush _foreBrush = new SolidBrush(Color.Black);

		public SkinnedListView()
		{
			OwnerDraw = true;
		}

		public void OnUpdateSkin(Skin skin)
		{
			_fieldBrush.Color = skin.FieldBackColor;
			_foreBrush.Color = skin.Surface.ForeColor;
			ForeColor = skin.Surface.ForeColor;
			BackColor = skin.FieldBackColor;
			Font = Skin.TextFont;
			Invalidate(true);
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground(pevent);
		}

		protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			Graphics g = e.Graphics;
			base.OnDrawColumnHeader(e);
			SolidBrush back = skin.Surface.GetBrush(VisualState.Normal, false, true);
			g.FillRectangle(back, e.Bounds);
			using (SolidBrush fore = new SolidBrush(skin.Surface.ForeColor))
			{
				Rectangle textRect = new Rectangle(e.Bounds.X + 5, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height);
				g.DrawString(e.Header.Text, Font, fore, textRect, _sf);
			}
			Pen border = skin.Surface.GetBorderPen(VisualState.Normal, false, true);
			g.DrawLine(border, e.Bounds.X, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
			if (e.ColumnIndex > 0)
			{
				g.DrawLine(border, e.Bounds.X, e.Bounds.Y, e.Bounds.X, e.Bounds.Bottom - 2);
			}
		}

		protected override void OnDrawItem(DrawListViewItemEventArgs e)
		{
			base.OnDrawItem(e);
			if (Columns.Count <= 0)
			{
				Rectangle bounds = e.Bounds;
				if (CheckBoxes)
				{
					SkinnedCheckBox.RenderCheckbox(e.Graphics, e.Bounds.X, e.Bounds.Y, SkinnedFieldType.Primary, e.Item.Checked ? CheckState.Checked : CheckState.Unchecked, VisualState.Normal, Enabled);

					bounds.X += SkinnedCheckBox.CheckBoxSize + 4;
					bounds.Width -= SkinnedCheckBox.CheckBoxSize + 4;
				}
				DrawListItem(e.Graphics, e.Item, bounds, SkinManager.Instance.CurrentSkin, e.Item.Text);
			}
		}

		protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;

			base.OnDrawSubItem(e);
			string text = e.SubItem.Text;
			DrawListItem(e.Graphics, e.Item, e.Bounds, skin, text);
		}

		private void DrawListItem(Graphics g, ListViewItem item, Rectangle bounds, Skin skin, string text)
		{
			SolidBrush fore = _foreBrush;
			Color oldColor = fore.Color;
			if (item.Selected)
			{
				SolidBrush brush = skin.PrimaryLightColor.GetBrush(VisualState.Normal, false, Enabled);
				g.FillRectangle(brush, bounds);
				fore.Color = Enabled ? skin.PrimaryLightColor.ForeColor : skin.PrimaryLightColor.DisabledForeColor;
			}
			else
			{
				using (SolidBrush back = new SolidBrush(BackColor))
				{
					g.FillRectangle(back, bounds);
				}
			}

			g.DrawString(text, Skin.TextFont, fore, bounds, _sf);

			fore.Color = oldColor;
		}
	}
}
