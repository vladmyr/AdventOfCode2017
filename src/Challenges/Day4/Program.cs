using System;
using System.Collections.Generic;

namespace Day4 {

    class AnagramChar {
        public char Chr { get; }
        public int Count { get; private set; }

        public AnagramChar(char chr) {
            Chr = chr;
            Count = 1;
        }

        public void IncrementCount() {
            Count++;
        }

        public bool EqualsTo(AnagramChar anagramChar) {
            return Chr == anagramChar.Chr && Count == anagramChar.Count;
        }
    }
    class Anagram {
        private Dictionary<char, AnagramChar> AnagramCharDict = new Dictionary<char, AnagramChar>();

        public Anagram(string str) {
            char[] chars = str.ToCharArray();

            foreach(char chr in chars) {
                if (AnagramCharDict.ContainsKey(chr)) {
                    AnagramCharDict.GetValueOrDefault(chr).IncrementCount();
                } else {
                    AnagramCharDict.Add(chr, new AnagramChar(chr));
                }
            }
        }

        public bool GetIsAnagramTo(Anagram anagram) {
            bool isAnagram = true;

            if (AnagramCharDict.Count != anagram.AnagramCharDict.Count) {
                return !isAnagram;
            }

            var anagramCharDictEnumerator = AnagramCharDict.GetEnumerator();
            while (isAnagram && anagramCharDictEnumerator.MoveNext()) {
                char chr = anagramCharDictEnumerator.Current.Key;
                AnagramChar anagramChar = anagramCharDictEnumerator.Current.Value;

                isAnagram = isAnagram 
                    && anagram.AnagramCharDict.ContainsKey(chr)
                    && anagramChar.EqualsTo(anagram.AnagramCharDict.GetValueOrDefault(chr));
            }

            return isAnagram;
        }
    }
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
        public static bool CalcIsAnagram(string input, string compareTo) {
            Anagram inputAnagram = new Anagram(input);
            Anagram comparatorAnagram = new Anagram(compareTo);

            return inputAnagram.GetIsAnagramTo(comparatorAnagram);
        }

        public static bool CalcIsValidPassphraseNoAnagram(string str) {
            bool isValid = true;
            string[] lemmas = str.Split(' ');
            int i = 0;
            int j = 1;

            while(isValid && i < lemmas.Length) {
                while(isValid && j < lemmas.Length) {
                    isValid = !CalcIsAnagram(lemmas[i], lemmas[j]);
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
            Console.WriteLine(countAnagrams);
            
            Console.ReadKey();
        }
    }
}