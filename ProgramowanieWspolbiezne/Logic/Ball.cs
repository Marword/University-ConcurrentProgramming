using System.Numerics;

namespace Logic
{
    public class Ball
    {
        public int Radius { get; set; }
        public Vector2 Position { get; set; }


        public Ball(int radius, int coordX, int coordY) : this(radius, new Vector2(coordX, coordY)) { }
        public Ball(int radius, Vector2 position)
        {
            Radius = radius;
            Position = position;
        }
    }
}