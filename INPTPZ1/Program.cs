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
            CommandLineHandler commandLineHandler = new CommandLineHandler();
            CommandLineArguments commandLineArguments = commandLineHandler.ParseCommandLineArguments(args);
            if (commandLineArguments == null)
            {
                return;
            }

            Size bitmapSize = commandLineArguments.BitmapSize;
            AxisInfo xAxisInfo = commandLineArguments.XAxisInfo;
            AxisInfo yAxisInfo = commandLineArguments.YAxisInfo;
            string outputFilePath = commandLineArguments.OutputFilePath;

            List<ComplexNumber> roots = new List<ComplexNumber>();
            Poly poly = new Poly();
            poly.Coefficients.Add(new ComplexNumber() { Real = 1 });
            poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(new ComplexNumber() { Real = 1 });
            Poly derivatedPoly = poly.Derivate();

            commandLineHandler.PrintToConsole(poly);
            commandLineHandler.PrintToConsole(derivatedPoly);

            BitmapHandler bitmapHandler = new BitmapHandler(bitmapSize.Width, bitmapSize.Height);

            // for every pixel in image...
            for (int i = 0; i < bitmapSize.Width; i++)
            {
                for (int j = 0; j < bitmapSize.Height; j++)
                {
                    // find "world" coordinates of pixel
                    double coordinationY = yAxisInfo.Min + (i * yAxisInfo.Step);
                    double coordinationX = xAxisInfo.Min + (j * xAxisInfo.Step);

                    ComplexNumber currentComplexNumber = new ComplexNumber()
                    {
                        Real = coordinationX,
                        Imaginary = coordinationY
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
                    Color pixelColor = bitmapHandler.GetColorForPixel(id, iterationsCounter);
                    bitmapHandler.ColorizePixel(i, j, pixelColor);
                }
            }

            bitmapHandler.SaveBitmap(outputFilePath);
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
    }
}
