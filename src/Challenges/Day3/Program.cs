using System;
using System.Collections.Generic;

namespace Day3
{
    public class RadiusMeta {
        public static int CIRCUMFERENCE_COEF = 8;

        public RadiusMeta InnerRadiusMeta { get; set; } = null;
        public int Radius { get; }

        public RadiusMeta(int radius) {
            Radius = radius;
        }

        public static int CalcStartingIndex(int radius) {
            int startingIndex = 0;

            if (radius != 0) {
                for (int i = 1; i < radius; i++) {
                    startingIndex += CIRCUMFERENCE_COEF * i;
                }

                startingIndex++;
            }

            return startingIndex;
        }

        public static SideFace CalcSideFace(int radius, int index) {
            byte face = 0;
            int remaining = 0;

            if (radius == 0) {
                return new SideFace(face, remaining);
            }

            int startingIndex = CalcStartingIndex(radius);
            int endingIndex = CalcStartingIndex(radius + 1) - 1;
            int indexOffset = startingIndex - 1;
            int sideLength = 2 * radius;

            if (index < startingIndex || index > endingIndex) {
                throw new Exception($"Index value {index} is out of range of radius with value {radius}");
            }

            face = (byte) ((index - startingIndex) / sideLength);
            remaining = (face + 1) * sideLength - (index - indexOffset);

            return new SideFace(face, remaining, sideLength);
        }

        public static int CalcNeighbourIndexFromInnerRadius(int radius, int index, SideFace sideFace) {
            if (radius < 2) return 0;

            int startingIndex = CalcStartingIndex(radius);
            int endingIndex = CalcStartingIndex(radius + 1) - 1;

            if (index == startingIndex || index == endingIndex) return startingIndex - 1;

            int indexOffset = (CIRCUMFERENCE_COEF + 2 * sideFace.Face + 1) + CIRCUMFERENCE_COEF * (radius - 2);

            if (sideFace.Remaining == 0) indexOffset++;

            return index - indexOffset;
        }

        public static int[] CalcNeighbourIndexes(int radius, int index) {
            HashSet<int> neighbourIndexSet = new HashSet<int>();
            SideFace sideFace = CalcSideFace(radius, index);
 
            int startingIndex = CalcStartingIndex(radius);
            int endingIndex = CalcStartingIndex(radius + 1) - 1;
            int innerRadiusStartingIndex = radius == 0
                ? 0
                : CalcStartingIndex(radius - 1);
            int innerRadiusIndex = CalcNeighbourIndexFromInnerRadius(radius, index, sideFace);

            // 1. Get indexes from current radius
            if (index != startingIndex) {
                neighbourIndexSet.Add(index - 1);

                if (sideFace.Face != 0 && sideFace.RemainingLeft == 0) {
                    neighbourIndexSet.Add(index - 2);
                }
            }

            if (index > endingIndex - 2) {
                neighbourIndexSet.Add(startingIndex);
            }

            // 2. Get indexes from inner radius
            neighbourIndexSet.Add(innerRadiusIndex);

            if (index > startingIndex + 1) {
                if (sideFace.Remaining > 1) {
                    neighbourIndexSet.Add(innerRadiusIndex + 1);
                }

                if (sideFace.RemainingLeft > 0 && sideFace.Remaining > 0) {
                    neighbourIndexSet.Add(innerRadiusIndex - 1);
                }
            } else if (index == startingIndex) {
                neighbourIndexSet.Add(innerRadiusStartingIndex);
            } else if (index == startingIndex + 1) {
                neighbourIndexSet.Add(startingIndex - 1);
                neighbourIndexSet.Add(innerRadiusStartingIndex + 1);
            }

            // 3. Cast set to int[]
            int[] indexes = new int[neighbourIndexSet.Count];
            neighbourIndexSet.CopyTo(indexes);

            return indexes;
        }
    }

    public class SideFace {
        public byte Face { get; }
        public int Remaining { get; }
        public int RemainingLeft { get; }
        public int SideLength { get; }

        public SideFace(byte face, int remaining, int sideLength = 1) {
            Face = face;
            Remaining = remaining;
            RemainingLeft = sideLength - remaining - 1;
            SideLength = sideLength;
        }
    }

    class Point {
        public int X { get; set; }
        public int Y { get; set; }

        public Point (int x, int y) {
            X = x;
            Y = y;
        }
    }

    public class Program {
        public static int CIRCLE_CIRCUMFERENCE = 8;

        static void Main(string[] args) {
            // PART 1
            //Console.WriteLine(CalcRootTaxicabDistance(1));
            //Console.WriteLine(CalcRootTaxicabDistance(2));
            //Console.WriteLine(CalcRootTaxicabDistance(5));
            //Console.WriteLine(CalcRootTaxicabDistance(12));
            //Console.WriteLine(CalcRootTaxicabDistance(23));
            //Console.WriteLine(CalcRootTaxicabDistance(1024));
            //Console.WriteLine(CalcRootTaxicabDistance(368078));

            // PART 2
            //Console.WriteLine(CalcNextValueNeighbourBased(1));
            //Console.WriteLine(CalcNextValueNeighbourBased(2));
            Console.WriteLine(CalcNextValueNeighbourBased(747));
            Console.WriteLine(CalcNextValueNeighbourBased(368078));

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
        private static int CalcNextValueNeighbourBased(int inputPointValue) {
            List<int> matrixValueList = new List<int>();
            List<RadiusMeta> radiusMetaList = new List<RadiusMeta>();
            RadiusMeta lastRadiusMeta = new RadiusMeta(0);
            int radius = 1;
            int pointValue = 1;
            int index = RadiusMeta.CalcStartingIndex(radius);
            int endingIndex = RadiusMeta.CalcStartingIndex(radius + 1) - 1;

            matrixValueList.Add(pointValue);

            while (pointValue <= inputPointValue) {
                int[] neighbourIndexes = RadiusMeta.CalcNeighbourIndexes(radius, index);

                pointValue = 0;

                for (int i = 0; i < neighbourIndexes.Length; i++) {
                    pointValue += matrixValueList[neighbourIndexes[i]];
                }

                matrixValueList.Add(pointValue);

                if (index == endingIndex) {
                    RadiusMeta radiusMeta = new RadiusMeta(radius);
                    radiusMeta.InnerRadiusMeta = radiusMeta;
                    lastRadiusMeta = radiusMeta;
                    radius++;
                    endingIndex = RadiusMeta.CalcStartingIndex(radius + 1) - 1;
                }

                index++;
            }

            return pointValue;
        }
    }
}
