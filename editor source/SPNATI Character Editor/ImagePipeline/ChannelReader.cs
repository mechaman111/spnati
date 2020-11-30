using System;
using System.Drawing;

namespace ImagePipeline
{
	/// <summary>
	/// Reads one of the RGBA channels from a bitmap
	/// </summary>
	public class ChannelReader : IFloatNodeInput, IDisposable
	{
		private DirectBitmap _bitmap;
		private ColorChannel _channel;

		public ChannelReader(DirectBitmap bmp, ColorChannel channel)
		{
			_bitmap = bmp;
			_channel = channel;
		}

		public void Dispose()
		{
			_bitmap = null;
		}

		public float Get(int x, int y)
		{
			x = x % _bitmap.Width;
			y = y % _bitmap.Height;
			if (x < 0 || y < 0 || x >= _bitmap.Width || y >= _bitmap.Height)
			{
				return 0;
			}
			Color color = _bitmap.GetPixel(x, y);
			switch (_channel)
			{
				case ColorChannel.A:
					return color.A / 255.0f;
				case ColorChannel.R:
					return color.R / 255.0f;
				case ColorChannel.G:
					return color.G / 255.0f;
				case ColorChannel.B:
					return color.B / 255.0f;
			}
			return 0;
		}
	}

	/// <summary>
	/// Produces a constant value between 0 and 1
	/// </summary>
	public class ConstantFloat : IFloatNodeInput
	{
		private float _value;

		public ConstantFloat(float v)
		{
			_value = v;
		}

		public float Get(int x, int y)
		{
			return _value;
		}
	}

	public enum ColorChannel
	{
		A = 0,
		R = 1,
		G = 2,
		B = 3
	}
}
