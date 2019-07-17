using System.Collections.Generic;
using System.Drawing;

namespace Desktop.Skinning
{
	public class ColorSet
	{
		public Color Normal = Color.White;
		public Color Hover = Color.Gray;
		public Color Pressed = Color.DarkGray;
		public Color Selected = Color.LightGray;
		public Color Disabled = Color.Gray;
		public Color DisabledSelected = Color.DarkGray;
		public Color ForeColor = Color.Black;
		public Color DisabledForeColor = Color.DarkGray;
		public Color Border = Color.LightGray;
		public Color BorderHover = Color.Gray;
		public Color BorderSelected = Color.Gray;
		public Color BorderDisabled = Color.Gray;

		private Dictionary<VisualState, SolidBrush> _brushes = new Dictionary<VisualState, SolidBrush>();
		private Dictionary<VisualState, Pen> _pens = new Dictionary<VisualState, Pen>();
		private Dictionary<VisualState, Pen> _borderPens = new Dictionary<VisualState, Pen>();

		public void ClearCache()
		{
			_brushes.Clear();
			_pens.Clear();
			_borderPens.Clear();
		}

		private VisualState GetState(VisualState state, bool focused, bool enabled)
		{
			switch (state)
			{
				case VisualState.Hover:
					return enabled ? VisualState.Hover : focused ? VisualState.DisabledSelected : VisualState.Disabled;
				case VisualState.DisabledSelected:
					return VisualState.DisabledSelected;
				default:
					if (focused)
					{
						return enabled ? VisualState.Focused : VisualState.DisabledSelected;
					}
					return enabled ? state : VisualState.Disabled;
			}
		}

		public Color GetColor(VisualState state, bool focused, bool enabled)
		{
			state = GetState(state, focused, enabled);
			return GetColor(state);
		}

		private Color GetColor(VisualState state)
		{
			switch (state)
			{
				case VisualState.Hover:
					return Hover;
				case VisualState.Pressed:
					return Pressed;
				case VisualState.Focused:
					return Selected;
				case VisualState.Disabled:
					return Disabled;
				case VisualState.DisabledSelected:
					return DisabledSelected;
				default:
					return Normal;
			}
		}

		public SolidBrush GetBrush(VisualState state, bool focused, bool enabled)
		{
			state = GetState(state, focused, enabled);
			return GetBrush(state);
		}
		public SolidBrush GetBrush(VisualState state)
		{
			SolidBrush br;
			if (!_brushes.TryGetValue(state, out br))
			{
				br = new SolidBrush(GetColor(state));
				_brushes[state] = br;
			}
			return br;
		}

		public Pen GetPen(VisualState state, bool focused, bool enabled)
		{
			state = GetState(state, focused, enabled);
			return GetPen(state);
		}
		public Pen GetPen(VisualState state)
		{
			Pen pen;
			if (!_pens.TryGetValue(state, out pen))
			{
				pen = new Pen(GetColor(state));
				_pens[state] = pen;
			}
			return pen;
		}

		private Color GetBorderColor(VisualState state)
		{
			switch (state)
			{
				case VisualState.Hover:
					return BorderHover;
				case VisualState.Disabled:
					return BorderDisabled;
				case VisualState.Focused:
					return BorderSelected;
				default:
					return Border;
			}
		}

		public Pen GetBorderPen(VisualState state, bool focused, bool enabled)
		{
			Pen pen;
			state = GetState(state, focused, enabled);
			if (!_borderPens.TryGetValue(state, out pen))
			{
				pen = new Pen(GetBorderColor(state));
				_borderPens[state] = pen;
			}
			return pen;
		}

		public static Color BlendColor(Color c1, Color c2, float a)
		{
			float r1 = c1.R / 255.0f;
			float g1 = c1.G / 255.0f;
			float b1 = c1.B / 255.0f;
			float a1 = c1.A / 255.0f;
			float r2 = c2.R / 255.0f;
			float g2 = c2.G / 255.0f;
			float b2 = c2.B / 255.0f;
			float a2 = c2.A / 255.0f;

			float r = r1 * (1 - a) + r2 * a;
			float g = g1 * (1 - a) + g2 * a;
			float b = b1 * (1 - a) + b2 * a;
			float alpha = a1 * (1 - a) + a2 * a;
			return Color.FromArgb((byte)(alpha * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
		}
	}
}
