namespace Data
{
    public static class Extensions
    {
        public static bool Inside(this float value, float min, float max, float padding = 0f)
        {
            if (padding < 0f) throw new ArgumentException("Padding must be positive!", nameof(padding));
            return (value - padding >= min) && (value + padding <= max);
        }

        public static bool Inside(this int value, int min, int max)
        {
            return (value >= min) && (value <= max);
        }

        public static float Clamp(this float value, float min, float max)
        {
            if (value > max) return max;
            if (value < min) return min;
            return value;
        }
    }
}