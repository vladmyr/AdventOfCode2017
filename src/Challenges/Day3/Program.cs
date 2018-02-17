using System;

namespace Day3
{
    class Point {
        public int X { get; set; }
        public int Y { get; set; }

        public Point (int X, int Y) {
            this.X = X;
            this.Y = Y;
        }
    }

    public class Program {
        public static int CIRCLE_CIRCUMFERENCE = 8;

        static void Main(string[] args) {
            Console.WriteLine(CalcTaxicabDistance(1));
            Console.WriteLine(CalcTaxicabDistance(2));
            Console.WriteLine(CalcTaxicabDistance(5));
            Console.WriteLine(CalcTaxicabDistance(12));
            Console.WriteLine(CalcTaxicabDistance(23));
            Console.WriteLine(CalcTaxicabDistance(1024));
            Console.WriteLine(CalcTaxicabDistance(368078));

            Console.ReadLine();
        }

        public static int CalcTaxicabDistance(int n) {
            Point q = _CalcGridPoint(n);
            return _CalcTaxicabDistance(new Point(0, 0), q);
        }

        private static int _CalcTaxicabDistance(Point p, Point q) {
            return Math.Abs(p.X - q.X) + Math.Abs(p.Y - q.Y);
        }

        private static Point _CalcGridPoint(int n) {
            // 1. calc radius
            int radiusMaxValue = 1;
            int digitCountInRadius = CIRCLE_CIRCUMFERENCE;
            int radius = 0;

            while (radiusMaxValue < n) {
                radiusMaxValue += digitCountInRadius;
                digitCountInRadius += CIRCLE_CIRCUMFERENCE;
                radius++;
            }

            int x = radius;
            int y = radius;

            if (radius == 0) return new Point(0, 0);

            int circleSideSize = Math.Max(2 * radius - 1, 0);
            int diff = radiusMaxValue - n;

            while (diff > circleSideSize) {
                diff -= circleSideSize + 1;
            }
                        
            if (diff != 0) {
                y = Math.Abs(radius - diff);
                //y = radius - x;
            }             

            return new Point(x, y);
        }
    }
}
