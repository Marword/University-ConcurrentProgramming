namespace Logic
{
    public static class Extensions
    {
        public static bool Inside(this float value, float min, float max)
        {
            return (value >= min) && (value <= max);
        }

        public static bool Inside(this int value, int min, int max)
        {
            return (value >= min) && (value <= max);
        }
    }
}
