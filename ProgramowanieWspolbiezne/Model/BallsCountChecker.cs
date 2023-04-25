using Logic;

namespace Presentation.Model
{
    public class BallsCountChecker : IChecker<int>
    {
        private int _min;
        private int _max;

        public BallsCountChecker() : this(Int32.MinValue) { }
        public BallsCountChecker(int min) : this(min, Int32.MaxValue) { }

        public BallsCountChecker(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public bool Check(int amount)
        {
            return amount.Inside(_min, _max);
        }
    }
}
