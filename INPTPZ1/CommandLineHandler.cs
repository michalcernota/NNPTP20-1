using System;
using System.Drawing;

namespace INPTPZ1
{
    class CommandLineHandler
    {
        public const string DefaultOutputFilePath = "../../../out.png";

        public CommandLineArguments ParseCommandLineArguments(string[] commandLineArguments)
        {
            if (commandLineArguments.Length < 5 || commandLineArguments.Length > 6)
            {
                Console.WriteLine("Count of command line arguments must me 5 or 6.");
                return null;
            }

            Size bitmapSize = new Size()
            {
                Width = int.Parse(commandLineArguments[0]),
                Height = int.Parse(commandLineArguments[1])
            };

            AxisInfo xAxisInfo = new AxisInfo(double.Parse(commandLineArguments[2]), double.Parse(commandLineArguments[3]));
            xAxisInfo.CalculateStep(bitmapSize.Width);

            AxisInfo yAxisInfo = new AxisInfo(double.Parse(commandLineArguments[4]), double.Parse(commandLineArguments[5]));
            yAxisInfo.CalculateStep(bitmapSize.Height);

            string outputFilePath = null;
            if (commandLineArguments.Length == 6)
            {
                outputFilePath = commandLineArguments[6];
            }

            return new CommandLineArguments()
            {
                BitmapSize = bitmapSize,
                XAxisInfo = xAxisInfo, 
                YAxisInfo = yAxisInfo,
                OutputFilePath = outputFilePath
            };
        }

        public void PrintToConsole(object objectToPrint)
        {
            Console.WriteLine(objectToPrint.ToString());
        }
    }
}
