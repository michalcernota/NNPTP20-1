using System;
using System.Drawing;

namespace INPTPZ1
{
    class BitmapHandler
    {
        public Bitmap Bitmap { get; private set; }
        public Color[] ColorsCollection { get; set; }

        public BitmapHandler(int bitmapWidth, int bitmapHeight)
        {
            Bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            ColorsCollection = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
        }

        public void ColorizePixel(int coordinationX, int coordinationY, Color pixelColor)
        {
            Bitmap.SetPixel(coordinationX, coordinationY, pixelColor);
        }

        public Color GetColorForPixel(int solutionRootNumber, int iterationCounter)
        {
            Color pixelColor = ColorsCollection[solutionRootNumber % ColorsCollection.Length];
            pixelColor = Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - iterationCounter * 2), 255),
                Math.Min(Math.Max(0, pixelColor.G - iterationCounter * 2), 255),
                Math.Min(Math.Max(0, pixelColor.B - iterationCounter * 2), 255));
            return pixelColor;
        }

        public void SaveBitmap(string outputhFilePath)
        {
            Bitmap.Save(outputhFilePath);
        }
    }
}
