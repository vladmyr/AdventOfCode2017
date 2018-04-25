using Xunit;
using Day4;
using System.Collections;
using System.Collections.Generic;

namespace Day4UnitTest {
    public class UnitTest1 {
        // Day 4, Part 1
        [Theory]
        [InlineData("", true)]
        [InlineData("aa bb cc dd", true)]
        [InlineData("aa bb cc dd aa", false)]
        [InlineData("aa bb cc dd aaa", true)]
        public void CalcIsValidPassphrase(string str, bool expectedResult) {
            Assert.Equal(expectedResult, Program.CalcIsValidPassphrase(str));
        }

        // Day 4, Part 2
        [Theory]
        [InlineData("abcde", "xyz", false)]
        [InlineData("abcde", "ecdab", true)]
        [InlineData("abcde", "abced", true)]
        public void CalcIsAnagram(string str, string ofStr, bool expectedResult) {
            HashSet<char> strSet = Program.ToCharHashSet(str);
            HashSet<char> ofStrSet = Program.ToCharHashSet(ofStr);

            Assert.Equal(expectedResult, Program.CalcIsAnagram(strSet, ofStrSet));
        }

        [Theory]
        // [InlineData("abcde fghij", true)]
        // [InlineData("abcde xyz ecdab", false)]
        // [InlineData("a ab abc abd abf abj", true)]
        [InlineData("iiii oiii ooii oooi oooo", true)]
        // [InlineData("oiii ioii iioi iiio", false)]
        public void CalcIsValidPassphraseNoAnagram(string str, bool expectedResult) {
            Assert.Equal(expectedResult, Program.CalcIsValidPassphraseNoAnagram(str));
        }
    }
}
