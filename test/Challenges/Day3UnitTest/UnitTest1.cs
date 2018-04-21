using System;
using Xunit;
using Day3;
using System.Collections;
using System.Collections.Generic;

namespace Day3UnitTest
{
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
        [InlineData(1, 8)]
        [InlineData(2, 24)]
        [InlineData(3, 48)]
        [InlineData(4, 80)]
        [InlineData(5, 120)]
        public void RadiusMeta__CalcStartingIndex(int radius, int startingIndex) {
            Assert.Equal(startingIndex, RadiusMeta.CalcStartingIndex(radius));
        }
    }
}
