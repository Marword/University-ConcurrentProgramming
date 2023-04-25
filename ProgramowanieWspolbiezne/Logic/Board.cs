using System;

namespace Logic
{
    public class Board
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Board(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }
}