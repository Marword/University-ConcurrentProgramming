namespace BallSimulator.Data
{
    public class Game
    {
        public int Height { get; init; }
        public int Width { get; init; }
        public Vector2 boundX => new(0, Width);
        public Vector2 boundY => new(0, Height);

        public Game(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }
}