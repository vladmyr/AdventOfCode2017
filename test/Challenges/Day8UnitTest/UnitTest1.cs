using System;
using Xunit;
using Day8;

namespace Day8UnitTest {
    public class UnitTest1 {
        [Theory]
        [InlineData(
            new string[] {
                "b inc 5 if a > 1",
                "a inc 1 if b < 5",
                "c dec -10 if a >= 1",
                "c inc -20 if c == 10"
            },
            1
        )]
        public void JumpInstruction__Largest(string[] input, int expectedResult) {
            JumpInstruction jumpInstruction = new JumpInstruction(input);
            Assert.Equal(expectedResult, jumpInstruction.CalcLargest());
        }
    }
}
