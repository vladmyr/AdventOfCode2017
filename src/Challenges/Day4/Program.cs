using System;
using System.Collections.Generic;

namespace Day4 {
    public class Program {

        // Day 4, Part 1
        public static bool CalcIsValidPassphrase(string str) {
            bool isValid = true;
            HashSet<string> set = new HashSet<string>();

            while(isValid && str != null && str.Length > 0) {
                // break string into two parts - head and tail
                string[] body = str.Split(' ', 2);
                string head = body[0];

                isValid = !set.Contains(head);
                set.Add(head);

                str = body.Length > 1
                    ? body[1]
                    : null;
            }

            return isValid;
        }

        // Day 4, Part 2
        public static HashSet<char> ToCharHashSet(string str) {
            HashSet<char> hashSet = new HashSet<char>();
            foreach(char chr in str.ToCharArray()) {
                hashSet.Add(chr);
            }
            
            return hashSet;
        }
        public static bool CalcIsAnagram(HashSet<char> strSet, HashSet<char> ofStrSet) {
            bool isValid = false;

            if (strSet.Count != ofStrSet.Count) {
                return isValid;
            }

            HashSet<char>.Enumerator strSetEnumerator = strSet.GetEnumerator();
            
            isValid = !isValid;
            while(isValid && strSetEnumerator.MoveNext()) {
                isValid = isValid && ofStrSet.Contains(strSetEnumerator.Current);
            }

            return isValid;
        }

        public static bool CalcIsValidPassphraseNoAnagram(string str) {
            bool isValid = true;
            string[] lemmas = str.Split(' ');
            List<HashSet<char>> lemmaSetList = new List<HashSet<char>>();
            int i = 0;
            int j = 1;

            while(isValid && i < lemmas.Length) {
                if (lemmaSetList.Count <= i) {
                    lemmaSetList.Add(ToCharHashSet(lemmas[i]));
                }

                while(isValid && j < lemmas.Length) {
                    if (lemmaSetList.Count <= j) {
                        lemmaSetList.Add(ToCharHashSet(lemmas[j]));
                    }

                    isValid = !CalcIsAnagram(lemmaSetList[i], lemmaSetList[j]);

                    j++;
                }

                i++;
                j = i + 1;
            }

            return isValid;
        }

        static void Main(string[] agrs) {
            int count = 0;
            int countAnagrams = 0;
            string[] inputLines = System.IO.File.ReadAllLines("./input.txt");

            foreach(string inputLine in inputLines) {
                if (CalcIsValidPassphrase(inputLine)) {
                    count++;
                }

                if (CalcIsValidPassphraseNoAnagram(inputLine)) {
                    countAnagrams++;
                }
            }

            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}