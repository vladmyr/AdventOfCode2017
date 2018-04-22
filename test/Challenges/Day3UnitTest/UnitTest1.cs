using Xunit;
using Day3;
using System.Collections;
using System.Collections.Generic;

namespace Day3UnitTest
{
    class TestData__RadiusMeta__CalcNeighbourIndexes : IEnumerable<object[]> {
        public IEnumerator<object[]> GetEnumerator() {
            yield return new object[] { 0, 0, new int[] { 0 } };
            yield return new object[] { 1, 1, new int[] { 0 } };
            yield return new object[] { 1, 2, new int[] { 1, 0 } };
            yield return new object[] { 1, 3, new int[] { 2, 1, 0 } };
            yield return new object[] { 1, 4, new int[] { 3, 0 } };
            yield return new object[] { 1, 6, new int[] { 5, 0 } };
            yield return new object[] { 1, 7, new int[] { 6, 5, 1, 0 } };
            yield return new object[] { 1, 8, new int[] { 7, 1, 0 } };
            yield return new object[] { 2, 9, new int[] { 8, 1 } };
            yield return new object[] { 2, 10, new int[] { 9, 1, 8, 2 } };
            yield return new object[] { 2, 11, new int[] { 10, 2, 1 } };
            yield return new object[] { 2, 12, new int[] { 11, 2 } };
            yield return new object[] { 2, 13, new int[] { 12, 11, 2, 3 } };
            yield return new object[] { 2, 14, new int[] { 13, 3, 4, 2 } };
            yield return new object[] { 2, 15, new int[] { 14, 4, 3 } };
            yield return new object[] { 2, 16, new int[] { 15, 4 } };
            yield return new object[] { 2, 17, new int[] { 16, 15, 4, 5 } };
            yield return new object[] { 2, 18, new int[] { 17, 5, 6, 4 } };
            yield return new object[] { 2, 19, new int[] { 18, 6, 5 } };
            yield return new object[] { 2, 20, new int[] { 19, 6 } };
            yield return new object[] { 2, 21, new int[] { 20, 19, 6, 7 } };
            yield return new object[] { 2, 22, new int[] { 21, 7, 8, 6 } };
            yield return new object[] { 2, 23, new int[] { 22, 9, 8, 7 } };
            yield return new object[] { 2, 24, new int[] { 23, 9, 8 } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class UnitTest1
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        [InlineData(12, 3)]
        [InlineData(23, 2)]
        [InlineData(1024, 31)]
        public void CalcDistance(int n, int distance) {
            Assert.Equal(distance, Program.CalcRootTaxicabDistance(n));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 9)]
        [InlineData(3, 25)]
        [InlineData(4, 49)]
        [InlineData(5, 81)]
        [InlineData(6, 121)]
        public void RadiusMeta__CalcStartingIndex(int radius, int startingIndex) {
            Assert.Equal(startingIndex, RadiusMeta.CalcStartingIndex(radius));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 1)]
        [InlineData(1, 1, 0, 1, 2)]
        [InlineData(1, 2, 0, 0, 2)]
        [InlineData(1, 3, 1, 1, 2)]
        [InlineData(1, 4, 1, 0, 2)]
        [InlineData(1, 5, 2, 1, 2)]
        [InlineData(1, 6, 2, 0, 2)]
        [InlineData(1, 7, 3, 1, 2)]
        [InlineData(1, 8, 3, 0, 2)]
        [InlineData(2, 9, 0, 3, 4)]
        [InlineData(2, 23, 3, 1, 4)]
        [InlineData(2, 24, 3, 0, 4)]
        [InlineData(3, 25, 0, 5, 6)]
        [InlineData(3, 28, 0, 2, 6)]
        [InlineData(3, 36, 1, 0, 6)]
        [InlineData(3, 48, 3, 0, 6)]
        public void RadiusMeta__CalcSideFace(int radius, int index, byte face, int remaining, int sideLength) {
            SideFace expectedSideFace = new SideFace(face, remaining, sideLength);
            SideFace sideFace = RadiusMeta.CalcSideFace(radius, index);

            Assert.Equal(expectedSideFace.Face, sideFace.Face);
            Assert.Equal(expectedSideFace.Remaining, sideFace.Remaining);
            Assert.Equal(expectedSideFace.RemainingLeft, sideFace.RemainingLeft);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(1, 1, 0, 1, 0)]
        [InlineData(1, 2, 0, 0, 0)]
        [InlineData(1, 3, 1, 1, 0)]
        [InlineData(1, 4, 1, 0, 0)]
        [InlineData(1, 8, 3, 0, 0)]
        [InlineData(2, 9, 0, 3, 8)]
        [InlineData(2, 10, 0, 2, 1)]
        [InlineData(2, 12, 0, 0, 2)]
        [InlineData(2, 14, 1, 2, 3)]
        [InlineData(2, 15, 1, 1, 4)]
        [InlineData(2, 21, 3, 3, 6)]
        [InlineData(2, 24, 3, 0, 8)]
        public void RadiusMeta__CalcNeighbourIndexFromInnerRadius(int radius, int index, byte face, int remaining, int expectedIndex) {
            SideFace sideFace = new SideFace(face, remaining);

            Assert.Equal(expectedIndex, RadiusMeta.CalcNeighbourIndexFromInnerRadius(radius, index, sideFace));
        }

        [Theory]
        [ClassData(typeof(TestData__RadiusMeta__CalcNeighbourIndexes))]
        public void RadiusMeta__CalcNeighbourIndexes(int radius, int index, int[] expectedIndexes) {
            int[] actualIndexes = RadiusMeta.CalcNeighbourIndexes(radius, index);

            Assert.Equal(expectedIndexes, actualIndexes);
        }
    }
}
