namespace Logic
{
    public class Ball
    {
        public int Radius { get; set; }  
        public int CoordX { get; set; }
        public int CoordY { get; set; }

        public Ball(int radius, int coordX, int coordY)
        {
            Radius = radius;    
            CoordX = coordX;    
            CoordY = coordY; 
        }
    }
}