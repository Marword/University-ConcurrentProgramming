using Model.API;

namespace Model
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
            return amount >= _min && amount <= _max;
        }

        public bool CheckNotCorrect(int amount)
        {
            return !Check(amount);
        }
    }
}