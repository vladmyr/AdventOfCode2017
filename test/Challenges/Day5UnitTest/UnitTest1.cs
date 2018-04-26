using Xunit;
using Day5;
using System.Collections;
using System.Collections.Generic;

namespace Day5UnitTest {
    public class UnitTest1 {
        // Day 5, Part 1
        [Theory]
        [InlineData(new int[] { 0, 3, 0, 1, -3 }, 5)]
        public void CountStepsBeforeExit(int[] input, int expectedResult) {
            Assert.Equal(expectedResult, Program.CountStepsBeforeExit(input));
        }

        // Day 5, Part 2
        [Theory]
        [InlineData(new int[] { 0, 3, 0, 1, -3 }, 10)]
        public void CountStepsBeforeExit2(int[] input, int expectedResult) {
            Assert.Equal(expectedResult, Program.CountStepsBeforeExit2(input));
        }
    }
}
