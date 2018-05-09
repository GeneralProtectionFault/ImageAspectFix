using System;
using System.Drawing;

namespace ImageAspectFix
{
    public struct SumoImage
    {
        public string Path { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalResolution { get; set; }
        public int VerticalResolution { get; set; }

        public Bitmap ImageBitmap { get; set; }
    }
}
