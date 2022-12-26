using System;
using System.Threading;

namespace _Couriers.Proj
{
    public class Location
    {
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public Location(int X, int Y)
        {
            CoordX = X;
            CoordY = Y;
        }
        public static Location Create()
        {
            Random rnd = new Random();
            Thread.Sleep(12);
            int X = rnd.Next(0, Program.Field);
            Thread.Sleep(12);
            int Y = rnd.Next(0, Program.Field);
            return new Location(X, Y);
        }
        public override string ToString()
        {
            return $"({CoordX},{CoordY})";
        }
    }
}


