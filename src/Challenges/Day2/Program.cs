﻿using System;

namespace Day2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[,] matrix = new int[,] {
                { 5, 1, 9, 5 },
                { 7, 5, 3, 0 },
                { 2, 4, 6, 8 }
            };
            
            int[,] matrix2 = new int[,] {
                { 1136, 1129, 184, 452, 788, 1215, 355, 1109, 224, 1358, 1278, 176, 1302, 186, 128, 1148 },
                { 242, 53, 252, 62, 40, 55, 265, 283, 38, 157, 259, 226, 322, 48, 324, 299 },
                { 2330, 448, 268, 2703, 1695, 2010, 3930, 3923, 179, 3607, 217, 3632, 1252, 231, 286, 3689 },
                { 89, 92, 903, 156, 924, 364, 80, 992, 599, 998, 751, 827, 110, 969, 979, 734 },
                { 100, 304, 797, 81, 249, 1050, 90, 127, 675, 1038, 154, 715, 79, 1116, 723, 990 },
                { 1377, 353, 3635, 99, 118, 1030, 3186, 3385, 1921, 2821, 492, 3082, 2295, 139, 125, 2819 },
                { 3102, 213, 2462, 116, 701, 2985, 265, 165, 248, 680, 3147, 1362, 1026, 1447, 106, 2769 },
                { 5294, 295, 6266, 3966, 2549, 701, 2581, 6418, 5617, 292, 5835, 209, 2109, 3211, 241, 5753 },
                { 158, 955, 995, 51, 89, 875, 38, 793, 969, 63, 440, 202, 245, 58, 965, 74 },
                { 62, 47, 1268, 553, 45, 60, 650, 1247, 1140, 776, 1286, 200, 604, 399, 42, 572 },
                { 267, 395, 171, 261, 79, 66, 428, 371, 257, 284, 65, 25, 374, 70, 389, 51 },
                { 3162, 3236, 1598, 4680, 2258, 563, 1389, 3313, 501, 230, 195, 4107, 224, 225, 4242, 4581 },
                { 807, 918, 51, 1055, 732, 518, 826, 806, 58, 394, 632, 36, 53, 119, 667, 60 },
                { 839, 253, 1680, 108, 349, 1603, 1724, 172, 140, 167, 181, 38, 1758, 1577, 748, 1011 },
                { 1165, 1251, 702, 282, 1178, 834, 211, 1298, 382, 1339, 67, 914, 1273, 76, 81, 71 },
                { 6151, 5857, 4865, 437, 6210, 237, 37, 410, 544, 214, 233, 6532, 2114, 207, 5643, 6852 }
            };

            Console.WriteLine(CalcChecksumPart1(matrix));
            Console.WriteLine(CalcChecksumPart1(matrix2));
            Console.WriteLine(CalcChecksumPart2(matrix2));
            Console.ReadKey();
        }

        public static int CalcChecksumPart1(int[,] matrix) {
            int result = 0;
            int rowNumber = matrix.GetLength(0);
            int columnNumber = matrix.GetLength(1);

            for (int rowIndex = 0; rowIndex < rowNumber; rowIndex++) {
                int min = 0;
                int max = 0;

                for (int columnIndex = 0; columnIndex < columnNumber; columnIndex++) {
                    int value = matrix[rowIndex, columnIndex];

                    if (value > 0) {
                        if (min == 0 || value < min) { min = value; }
                        if (max == 0 || value > max) { max = value; }
                    }
                }

                result += (max - min);
            }

            return result;
        }

        public static int CalcChecksumPart2(int[,] matrix) {
            int result = 0;
            int rowNumber = matrix.GetLength(0);
            int columnNumber = matrix.GetLength(1);

            for (int rowIndex = 0; rowIndex < rowNumber; rowIndex++) {

                int[] sorted = MergeSortDimension1(matrix, rowIndex);
                int sortedIndex = sorted.Length - 1;
                bool isFound = false;

                do {
                    int comparisonIndex = 0;

                    do {
                        isFound = sorted[sortedIndex] % sorted[comparisonIndex] == 0;

                        if (isFound) {
                            result += sorted[sortedIndex] / sorted[comparisonIndex];
                        }

                        comparisonIndex++;
                    } while(!isFound && comparisonIndex < sortedIndex);

                    sortedIndex--;
                } while (!isFound && sortedIndex > 0);
            }

            return result;
        }

        public static int[] MergeSortDimension1(int[,] matrix, int rowIndex) {
            int startIndex = 0;
            int endIndex = matrix.GetLength(1) - 1;

            return MergeSortDimension1(matrix, rowIndex, startIndex, endIndex);
        }

        public static int[] MergeSortDimension1(
            int[,] matrix, 
            int rowIndex, 
            int startIndex, 
            int endIndex 
        ) {
            int highestIndex = matrix.GetLength(1) - 1;

            startIndex = startIndex > 0 ? startIndex : 0;
            endIndex = endIndex < highestIndex ? endIndex: highestIndex;

            if (startIndex >= endIndex) {
                return new int[1] { matrix[rowIndex, startIndex]};
            }
                        
            // 1. get median index
            int medianIndex = (startIndex + endIndex) / 2;

            // 2. recursively perform sorting on smaller index range
            int[] resultLeftSide = MergeSortDimension1(matrix, rowIndex, startIndex, medianIndex);
            int[] resultRightSide = MergeSortDimension1(matrix, rowIndex, medianIndex + 1, endIndex);

            int[] result = new int[endIndex - startIndex + 1];

            // 3. perform value sorting and merging into 1d array
            int resultIndex = 0;
            int leftSideIndex = 0;
            int rightSideIndex = 0;
                
            while (leftSideIndex < resultLeftSide.Length && rightSideIndex < resultRightSide.Length) {
                if (resultLeftSide[leftSideIndex] < resultRightSide[rightSideIndex]) {
                    result[resultIndex] = resultLeftSide[leftSideIndex];
                    leftSideIndex++;
                } else {
                    result[resultIndex] = resultRightSide[rightSideIndex];
                    rightSideIndex++;
                }

                resultIndex++;
            }

            // 3.1 copy remaining elements
            while (leftSideIndex < resultLeftSide.Length) {
                result[resultIndex] = resultLeftSide[leftSideIndex];
                leftSideIndex++;
                resultIndex++;
            }

            while (rightSideIndex < resultRightSide.Length) {
                result[resultIndex] = resultRightSide[rightSideIndex];
                rightSideIndex++;
                resultIndex++;
            }

            return result;
        }
    }
}
