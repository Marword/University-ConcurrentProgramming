namespace BallSimulator.Data.API
{
    public abstract class DataAbstractApi
    {

        public abstract int GameHeight { get; }
        public abstract int GameWidth { get; }
        public abstract float MaxTempo { get; }
        public abstract int MinDiameter { get; }
        public abstract int MaxDiameter { get; }

        public static DataAbstractApi CreateDataApi()
        {
            return new DataApi();
        }
    }
}