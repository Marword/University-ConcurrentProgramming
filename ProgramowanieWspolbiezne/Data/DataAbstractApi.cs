namespace Data
{
    public abstract class DataAbstractApi
    {

        public abstract int GameHeight { get; }
        public abstract int GameWidth { get; }
        public abstract int BallRadius { get; }

        public static DataAbstractApi CreateDataApi()
        {
            return new DataApi();
        }
    }
}