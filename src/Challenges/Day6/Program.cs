using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Day6 {
    public class Bank {
        public static int CalcNormalizedIndex (int value, int ceiling) {
            if (ceiling < 2) {
                return 0;
            }

            return value >= ceiling
                ? CalcNormalizedIndex(value - ceiling, ceiling)
                : value;
        }

        public int Index { get; private set; }
        public int[] Blocks { get; private set; }

        public Bank(int[] blocks) {
            Blocks = new int[blocks.Length];
            blocks.CopyTo(Blocks, 0);
            _CalcBiggestBankIndex();
        }

        public Bank(Bank bank) {
            Index = bank.Index;
            Blocks = (int[]) bank.Blocks.Clone();
        }

        public void Redistribute() {
            int newIndex = Index;
            int iterationIndex = Bank.CalcNormalizedIndex(Index + 1, Blocks.Length);
            int capacity = Blocks[Index];
            int maxRedisValuePerBlock = (int) Math
                .Ceiling((double) capacity / Blocks.Length);

            Blocks[Index] = 0;

            for(int i = 0; i < Blocks.Length; i++) {
                int redisValue = maxRedisValuePerBlock;

                // last block scenario
                if (capacity < maxRedisValuePerBlock) {
                    redisValue = Math.Max(capacity, 0);
                }

                capacity -= redisValue;

                Blocks[iterationIndex] += redisValue;
                int newValue = Blocks[iterationIndex];

                // calc index of a block with highest value
                if ((newValue > Blocks[newIndex]) 
                    || (newValue == Blocks[newIndex] && iterationIndex < newIndex)
                ) {
                    newIndex = iterationIndex;
                }

                // normalize iteration index
                iterationIndex++;
                iterationIndex = Bank.CalcNormalizedIndex(iterationIndex, Blocks.Length);
            }

            Index = newIndex;
        }
        public bool EqualsTo(Bank bank) {
            bool isEqual = true;
            int i = 0;

            if (bank.Blocks.Length != Blocks.Length) {
                return !isEqual;
            } 

            while (isEqual && i < Blocks.Length) {
                isEqual = isEqual && Blocks[i] == bank.Blocks[i];
                i++;
            }

            return isEqual;
        }

        private void _CalcBiggestBankIndex() {
            for(int index = 0; index < Blocks.Length; index++) {
                if (Blocks[index] > Blocks[Index]) {
                    Index = index;
                }
            }
        }
    }

    public class Program {
        /**
         * Naive algorithm
         */
        public static int CountRedistributionCycles(int[] blocks) {
            int count = 0;
            bool isMatchFound = false;
            List<Bank> bankList = new List<Bank>();
            Bank lastBank = new Bank(blocks);
            bankList.Add(lastBank);

            while (!isMatchFound) {
                Bank bank = new Bank(lastBank);
                bank.Redistribute();
                bankList.Add(bank);
                lastBank = bank;
                count++;

                int i = count - 1;
                while (!isMatchFound && i > 0) {
                    isMatchFound = lastBank.EqualsTo(bankList[i]);
                    i--;
                }
            }

            return count;
        }

        /**
         * Brent's algorithm
         */
        public static int [] CountCyclesBrent(int[] blocks) {
            int lambda = 1;
            int power = 1;

            Bank tortoiseBank = new Bank(blocks);
            Bank hareBank = new Bank(blocks);
            hareBank.Redistribute();

            while(!tortoiseBank.EqualsTo(hareBank)) {
                if (power == lambda) {
                    tortoiseBank = hareBank;
                    power *= 2;
                    lambda = 0;
                }

                hareBank = new Bank(hareBank);
                hareBank.Redistribute();
                lambda++;
            }

            int mu = 0;
            tortoiseBank = new Bank(blocks);
            hareBank = new Bank(blocks);

            for(int i = 0; i < lambda; i++) {
                hareBank.Redistribute();
            }

            while(!tortoiseBank.EqualsTo(hareBank)) {
                tortoiseBank.Redistribute();
                hareBank.Redistribute();
                mu++;
            }

            return new int[] { mu, lambda };
        }

        static void Main(string[] args) {
            int[] input = new int[]{ 5, 1, 10, 0, 1, 7, 13, 14, 3, 12, 8, 10, 7, 12, 0, 6 };
            int[] cycleCount = CountCyclesBrent(input);

            // Part 1
            Console.WriteLine(cycleCount[0] + cycleCount[1]);

            // Part 2
            Console.WriteLine(cycleCount[1]);

            Console.ReadLine();
        }
    }
}