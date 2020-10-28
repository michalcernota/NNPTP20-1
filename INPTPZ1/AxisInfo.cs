namespace INPTPZ1
{
    class AxisInfo
    {
        public AxisInfo(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public void CalculateStep(int size)
        {
            Step = (Max - Min) / size;
        }

        public double Min { get; private set; }
        public double Max { get; private set; }
        public double Step { get; private set; }
    }
}
