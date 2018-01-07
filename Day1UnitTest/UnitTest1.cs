using System;
using Xunit;
using Day1;

namespace Day1UnitTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("1122", 3)]
        [InlineData("1111", 4)]
        [InlineData("1221", 3)]
        [InlineData("1234", 0)]
        [InlineData("91212129", 9)]
        [InlineData("11183136", 2)]
        [InlineData("23545511", 6)]
        [InlineData("1118313623545511", 9)]
        public void CalcPart1Captcha(string input, int output)
        {
            Assert.Equal(output, Program.CalcPart1Captcha(input));
        }

        [Theory]
        [InlineData("1212", 6)]
        [InlineData("1221", 0)]
        [InlineData("123425", 4)]
        [InlineData("123123", 12)]
        [InlineData("12131415", 4)]
        public void CalcPart2Captcha(string input,int output) {
            Assert.Equal(output,Program.CalcPart2Captcha(input));
        }
    }
}
