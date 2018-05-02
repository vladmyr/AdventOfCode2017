using Xunit;
using Day6;
using System.Collections;
using System.Collections.Generic;

namespace Day6UnitTest {
    public class UnitTest1 {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 2, 1)]
        [InlineData(2, 2, 0)]
        [InlineData(5, 4, 1)]
        [InlineData(5, 16, 5)]
        [InlineData(16, 16, 0)]
        [InlineData(17, 16, 1)]
        [InlineData(18, 16, 2)]
        public void CalcNormalizedIndex(int value, int ceiling, int expectedValue) {
            Assert.Equal(expectedValue, Bank.CalcNormalizedIndex(value, ceiling));
        }

        [Theory]
        [InlineData(new int[]{ 0, 2, 7, 0 }, new int[] { 2, 4, 1, 2 }, 1)]
        [InlineData(new int[]{ 2, 4, 1, 2 }, new int[] { 3, 1, 2, 3 }, 0)]
        [InlineData(new int[]{ 3, 1, 2, 3 }, new int[] { 0, 2, 3, 4 }, 3)]
        [InlineData(new int[]{ 0, 2, 3, 4 }, new int[] { 1, 3, 4, 1 }, 2)]
        public void Bank__Redistibute(int[] blocks, int[] expectedBlocks, int expectedIndex) {
            Bank bank = new Bank(blocks);
            Bank expectedBank = new Bank(expectedBlocks);

            bank.Redistribute();

            Assert.True(expectedBank.EqualsTo(bank));
            Assert.Equal(expectedIndex, bank.Index);
        }

        [Theory]
        [InlineData(new int[]{ 0, 2, 7, 0 }, 5)]
        [InlineData(new int[]{ 5, 1, 10, 0, 1, 7, 13, 14, 3, 12, 8, 10, 7, 12, 0, 6 }, 5042)]
        public void CountRedistributionCycles(int[] blocks, int expectedResult) {
            Assert.Equal(expectedResult, Program.CountRedistributionCycles(blocks));
        }

        [Theory]
        [InlineData(new int[]{ 0, 2, 7, 0}, 5)]
        [InlineData(new int[]{ 5, 1, 10, 0, 1, 7, 13, 14, 3, 12, 8, 10, 7, 12, 0, 6 }, 5042)]
        public void CountRedistributionCyclesBrent(int[] blocks, int expectedResult) {
            int[] cycleCount = Program.CountCyclesBrent(blocks);

            Assert.Equal(expectedResult, cycleCount[0] + cycleCount[1]);
        }
    }
}
