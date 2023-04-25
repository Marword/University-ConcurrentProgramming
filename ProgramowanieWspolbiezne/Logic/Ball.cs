namespace Logic
{
    public class Ball : IEquatable<Ball>
    {
        public int Radius { get; }
        public Vector2 Coordinates { get; set; }
        public Vector2 Tempo { get; set; }

        public Ball(int radius, int coordX, int coordY, float tempoX, float tempoY)
            : this(radius, new Vector2(coordX, coordY), new Vector2(tempoX, tempoY)) { }
        public Ball(int radius, Vector2 coordinates, Vector2 tempo)
        {
            Radius = radius;
            Coordinates = coordinates;
            Tempo = tempo;
        }

        public void Move(Vector2 boundX, Vector2 boundY, float power = 1f)
        {
            if (!power.Inside(0f, 1f))
            {
                throw new ArgumentException("Value of power needs to be between 0 and 1.", nameof(power));
            }

            Coordinates += Tempo * power;
            var (coordX, coordY) = Coordinates;

            if (!coordX.Inside(boundX.X, boundX.Y)) Tempo = new Vector2(-Tempo.X, Tempo.Y);

            if (!coordY.Inside(boundY.X, boundY.Y)) Tempo = new Vector2(Tempo.X, -Tempo.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is Ball ball
                && Equals(ball);
        }

        public bool Equals(Ball other)
        {
            return !(other is null)
                && Radius == other.Radius
                && Coordinates == other.Coordinates
                && Tempo == other.Tempo;
        }

        public override int GetHashCode()
        {
            int hashCode = 883467613;
            hashCode = hashCode * -1521134295 + Radius.GetHashCode();
            hashCode = hashCode * -1521134295 + Coordinates.GetHashCode();
            hashCode = hashCode * -1521134295 + Tempo.GetHashCode();
            return hashCode;
        }
    }
}