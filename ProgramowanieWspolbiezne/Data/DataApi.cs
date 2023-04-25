namespace Data
{
    internal class DataApi : DataAbstractApi
    {
        public override int GameHeight { get; } = 300;
        public override int GameWidth { get; } = 630;
        public override int BallRadius { get; } = 10;

    }
}