namespace Logic
{
    public class Game
    {
        public int Height { get; }
        public int Width { get; }
        public Vector2 boundX => new Vector2(0, Width);
        public Vector2 boundY => new Vector2(0, Height);

        public Game(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }
}