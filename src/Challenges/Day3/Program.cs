using System;

namespace Day3
{
    public class RadiusMeta {
        public static int CIRCUMFERENCE_COEF = 8;

        public RadiusMeta InnerRadiusMeta { get; set; } = null;
        public int Radius { get; set; }

        public RadiusMeta(int radius) {
            Radius = radius;
        }

        public static int CalcStartingIndex(int radius) {
            int startingIndex = 0;

            for (int i = 1; i <= radius; i++) {
                startingIndex += CIRCUMFERENCE_COEF * i;
            }

            return startingIndex;
        }

        //public int[] CalcNeighbourPointIndexes(int index) {
        //    int startingIndex = RadiusMeta.CalcStartingIndex(Radius);
        //    int endingIndex = RadiusMeta.CalcStartingIndex(Radius + 1) - 1;

        //    if (index < startingIndex || index > endingIndex) {
        //        throw new Exception($"Index value {index} is out of range of radius with value {Radius}");
        //    }


        //}
    }

    class Point {
        public int X { get; set; }
        public int Y { get; set; }

        public Point (int x, int y) {
            X = x;
            Y = y;
        }
    }

    class PointValue : Point {
        public int Value { get; set; }

        public PointValue(int x, int y, int value) : base(x, y) {
            Value = value;
        }
    }

    public class Program {
        public static int CIRCLE_CIRCUMFERENCE = 8;

        static void Main(string[] args) {
            // PART 1
            Console.WriteLine(CalcRootTaxicabDistance(1));
            Console.WriteLine(CalcRootTaxicabDistance(2));
            Console.WriteLine(CalcRootTaxicabDistance(5));
            Console.WriteLine(CalcRootTaxicabDistance(12));
            Console.WriteLine(CalcRootTaxicabDistance(23));
            Console.WriteLine(CalcRootTaxicabDistance(1024));
            Console.WriteLine(CalcRootTaxicabDistance(368078));

            // PART 2

            Console.ReadLine();
        }

        // PART 1
        public static int CalcRootTaxicabDistance(int n) {
            Point q = _CalcGridPoint(n);
            return _CalcTaxicabDistance(new Point(0, 0), q);
        }

        private static int _CalcTaxicabDistance(Point p, Point q) {
            return Math.Abs(p.X - q.X) + Math.Abs(p.Y - q.Y);
        }

        private static Point _CalcGridPoint(int n) {
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
            }

            return new Point(x, y);
        }

        // PART 2
        private int _calcNextGridValueNeighbourBased(int currentValue) {
            return 0;
        }
    }
}
