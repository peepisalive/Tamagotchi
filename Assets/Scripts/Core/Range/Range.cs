namespace Core
{
    public sealed class Range
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}