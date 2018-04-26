using System;

namespace Day5 {
    public class Program {
        public static int CountStepsBeforeExit(int[] input) {
            int count = 0;
            int index = 0;

            while (index < input.Length) {
                index += input[index]++;
                count++;
            }

            return count;
        }
        
        static int Main(string[] args) {
            string[] inputStr = System.IO.File.ReadAllLines("./input.txt");
            int[] inputInt = new int[inputStr.Length];

            // Part 1
            try {
                for(int i = 0; i < inputInt.Length; i++) {
                    inputInt[i] = Int32.Parse(inputStr[i]);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return ex.HResult;
            }
            
            Console.WriteLine(CountStepsBeforeExit(inputInt));

            return 0;
        }
    }
}
