using Xunit;
using Day7;
using System.Collections;
using System.Collections.Generic;

namespace Day7UnitTest {
    public class UnitTest1 {
        [Theory]
        [InlineData(
            new string[] { "pbga (66)", "xhth (57)", "ebii (61)", "havc (66)", "ktlj (57)", "fwft (72) -> ktlj, cntj, xhth", "qoyq (66)", "padx (45) -> pbga, havc, qoyq", "tknk (41) -> ugml, padx, fwft", "jptl (61)", "ugml (68) -> gyxo, ebii, jptl", "gyxo (61)", "cntj (57)" },
            "tknk"
        )]
        public void Tree__RootNodeName(string[] input, string expectedResult) {
            Tree tree = new Tree(input);

            Assert.Equal(expectedResult, tree.RootNode.Name);
        }

        [Theory]
        [InlineData(
            new string[] { "pbga (66)", "xhth (57)", "ebii (61)", "havc (66)", "ktlj (57)", "fwft (72) -> ktlj, cntj, xhth", "qoyq (66)", "padx (45) -> pbga, havc, qoyq", "tknk (41) -> ugml, padx, fwft", "jptl (61)", "ugml (68) -> gyxo, ebii, jptl", "gyxo (61)", "cntj (57)" },
            8
        )]
        public void Tree__OffBalance(string[] input, int expectedResult) {
            Tree tree = new Tree(input);

            Assert.Equal(expectedResult, tree.OffBalance);
        }
    }
}
