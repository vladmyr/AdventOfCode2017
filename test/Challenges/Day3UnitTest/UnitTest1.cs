using System;
using Xunit;
using Day3;

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
            Assert.Equal(distance, Program.CalcTaxicabDistance(n));
        }
    }
}
