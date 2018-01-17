using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Day2;

namespace Day2UnitTest
{
    public class TestData: IEnumerable<object[]> {
        public IEnumerator<object[]> GetEnumerator() {
            yield return new object[] {
                new int[,] { { 1,2,3,4,5 } },
                new int[] { 1,2,3,4,5 }
            };

            yield return new object[] {
                new int[,] { { 5,4,3,2,1 } },
                new int[] { 1,2,3,4,5 }
            };

            yield return new object[] {
                new int[,] { { 1,5,2,4,3 } },
                new int[] { 1,2,3,4,5 }
            };

            yield return new object[] {
                new int[,] { { 1,3,5,4,2 } },
                new int[] { 1,2,3,4,5 }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class UnitTest1
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void MergeSortTest1(int[,] matrix, int[] expectedResult)
        {
            Assert.Equal(
                expectedResult,
                Program.MergeSortDimension1(matrix, 0, 0, matrix.GetLength(1))
            );
        }
    }
}
