using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImagePipeline
{
	/// <summary>
	/// Wrapper around a Bitmap for faster pixel manipulation
	/// </summary>
	public class DirectBitmap : IDisposable
	{
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(string path)
        {
            using (Bitmap src = new Bitmap(path))
            {
                Initialize(src.Width, src.Height, src);
            }
        }

        public DirectBitmap(DirectBitmap src) : this(src.Bitmap)
        {
        }

        public DirectBitmap(Bitmap src)
        {
            Initialize(src.Width, src.Height, src);
        }

        public DirectBitmap(int width, int height)
        {
            Initialize(width, height, null);
        }

        private void Initialize(int width, int height, Bitmap src)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());

            if (src != null)
            {
                using (Graphics g = Graphics.FromImage(Bitmap))
                {
                    g.DrawImage(src, 0, 0);
                }
            }
        }

        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + (y * Width);
            int col = colour.ToArgb();

            Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}
