using System;
using System.Collections.Generic;
using System.Drawing;
using INPTPZ1.Mathematics;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const int MAX_ITERATIONS = 30;
        private const double TOLERANCE = 0.0001;

        static void Main(string[] args)
        {
            Size bitmapSize = new Size()
            {
                Width = int.Parse(args[0]),
                Height = int.Parse(args[1])
            };

            Bitmap bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string output = args[6];

            double xstep = (xmax - xmin) / bitmapSize.Width;
            double ystep = (ymax - ymin) / bitmapSize.Height;

            List<ComplexNumber> roots = new List<ComplexNumber>();
            Poly poly = new Poly();
            poly.Coefficients.Add(new ComplexNumber() { Real = 1 });
            poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(new ComplexNumber() { Real = 1 });
            Poly derivatedPoly = poly.Derivate();

            Console.WriteLine(poly);
            Console.WriteLine(derivatedPoly);

            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            // for every pixel in image...
            for (int i = 0; i < bitmapSize.Width; i++)
            {
                for (int j = 0; j < bitmapSize.Height; j++)
                {
                    // find "world" coordinates of pixel
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    ComplexNumber currentComplexNumber = new ComplexNumber()
                    {
                        Real = x,
                        Imaginary = y
                    };

                    if (currentComplexNumber.Real == 0)
                    {
                        currentComplexNumber.Real = TOLERANCE;
                    }

                    if (currentComplexNumber.Imaginary == 0)
                    {
                        currentComplexNumber.Imaginary = TOLERANCE;
                    }

                    // find solution of equation using newton's iteration
                    int iterationsCounter = 0;
                    FindSolutionUsingNewtonsIteration(poly, derivatedPoly, ref currentComplexNumber, ref iterationsCounter);

                    // find solution root number
                    int id = GetRoot(roots, currentComplexNumber);

                    // colorize pixel according to root number
                    Color pixelColor = GetPixelColor(colors, iterationsCounter, id);
                    bitmap.SetPixel(j, i, pixelColor);
                }
            }

            bitmap.Save(output ?? "../../../out.png");
        }

        private static void FindSolutionUsingNewtonsIteration(Poly poly, Poly derivedPoly, ref ComplexNumber currentComplexNumber, ref int iterationsCounter)
        {
            for (int k = 0; k < MAX_ITERATIONS; k++)
            {
                ComplexNumber difference = poly.Evaluate(currentComplexNumber).Divide(derivedPoly.Evaluate(currentComplexNumber));
                currentComplexNumber = currentComplexNumber.Subtract(difference);

                if (Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2) >= 0.5)
                {
                    k--;
                }
                iterationsCounter++;
            }
        }

        private static int GetRoot(List<ComplexNumber> rootsCollection, ComplexNumber complexNumber)
        {
            bool known = false;
            int id = 0;
            for (int k = 0; k < rootsCollection.Count; k++)
            {
                if (Math.Pow(complexNumber.Real - rootsCollection[k].Real, 2) + Math.Pow(complexNumber.Imaginary - rootsCollection[k].Imaginary, 2) <= 0.01)
                {
                    known = true;
                    id = k;
                }
            }
            if (!known)
            {
                rootsCollection.Add(complexNumber);
                id = rootsCollection.Count;
            }

            return id;
        }

        private static Color GetPixelColor(Color[] colorsCollection, int iterationsCounter, int id)
        {
            Color pixelColor = colorsCollection[id % colorsCollection.Length];
            pixelColor = Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - iterationsCounter * 2), 255),
                Math.Min(Math.Max(0, pixelColor.G - iterationsCounter * 2), 255),
                Math.Min(Math.Max(0, pixelColor.B - iterationsCounter * 2), 255));
            return pixelColor;
        }
    }
}
