using System;

namespace Day5 {
    public class Program {
        // Part 1
        public static int CountStepsBeforeExit(int[] input) {
            int count = 0;
            int index = 0;

            while (index < input.Length) {
                index += input[index]++;
                count++;
            }

            return count;
        }

        // Part 2
        public static int CountStepsBeforeExit2(int[] input) {
            int count = 0;
            int index = 0;

            while(index < input.Length) {
                if (input[index] > 2) {
                    index += input[index]--;
                } else {
                    index += input[index]++;
                }

                count++;
            }

            return count;
        }
        
        static int Main(string[] args) {
            string[] inputStr = System.IO.File.ReadAllLines("./input.txt");
            int[] inputInt = new int[inputStr.Length];

            try {
                for(int i = 0; i < inputInt.Length; i++) {
                    inputInt[i] = Int32.Parse(inputStr[i]);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return ex.HResult;
            }
            
            // Part 1
            Console.WriteLine(CountStepsBeforeExit((int[]) inputInt.Clone()));
            // Part 2
            Console.WriteLine(CountStepsBeforeExit2((int[]) inputInt.Clone()));

            return 0;
        }
    }
}
