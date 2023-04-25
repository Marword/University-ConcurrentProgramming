namespace Data
{
    internal class DataApi : DataAbstractApi
    {
        public override int GameHeight { get; } = 300;
        public override int GameWidth { get; } = 630;
        public override float MaxTempo => 30f;
        public override int MinDiameter => 20;
        public override int MaxDiameter => 50;

    }
}