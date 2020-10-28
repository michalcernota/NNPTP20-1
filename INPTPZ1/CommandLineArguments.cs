using System.Drawing;

namespace INPTPZ1
{
    class CommandLineArguments
    {
        public Size BitmapSize { get; set; }
        public AxisInfo XAxisInfo { get; set; }
        public AxisInfo YAxisInfo { get; set; }
        public string OutputFilePath { get; set; }
    }
}
