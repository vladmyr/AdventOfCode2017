using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day8 {
    public class JumpInstruction {
        private Dictionary<string, int> _RegisterDict { get; set; } = new Dictionary<string, int>();

        private void _ParseInstructions(string[] input) {
            foreach (string instruction in input) {
                _ParseInstruction(instruction);
            }
        }

        private void _ParseInstruction(string instruction) {
            string pattern = @"(\w+)\s(\w+)\s(\S+)\sif\s(\w+)\s(\S+)\s(\S+)";
            Regex regex = new Regex(pattern, RegexOptions.ECMAScript);
            Match match = regex.Match(instruction);

            string register = match.Groups[1].Value;
            string registerInstruction = match.Groups[2].Value;
            int registerInstructionValue = Int32.Parse(match.Groups[3].Value);
            string operation = match.Groups[5].Value;
            string leftComparer = match.Groups[4].Value;
            int rightComparer = Int32.Parse(match.Groups[6].Value);

            _CreateRegisterIfNotExist(leftComparer);

            if (_EvaluateConditionResult(operation, leftComparer, rightComparer)) {
                _RegisterDict[register] = _EvaluateInstructionResult(registerInstruction, register, registerInstructionValue);
            }

            return;
        }
 
        private bool _EvaluateConditionResult(string operation, string leftComparer, int value) {
            bool isPositive = false;

            switch (operation) {
                case ">":
                    isPositive = _Greater(leftComparer, value);
                    break;
                case ">=":
                    isPositive = _Greater(leftComparer, value) || _Equal(leftComparer, value);
                    break;
                case "<":
                    isPositive = _Less(leftComparer, value);
                    break;
                case "<=":
                    isPositive = _Less(leftComparer, value) || _Equal(leftComparer, value);
                    break;
                case "==":
                    isPositive = _Equal(leftComparer, value);
                    break;
                case "!=":
                    isPositive = !_Equal(leftComparer, value);
                    break;
            }

            return isPositive;
        }

        private int _EvaluateInstructionResult(string instruction, string name, int value) {
            int instructionResult = _RegisterDict.GetValueOrDefault(name);

            switch (instruction) {
                case "inc":
                    instructionResult = _Inc(name, value);
                    break;
                case "dec":
                    instructionResult = _Dec(name, value);
                    break;
            }

            return instructionResult;
        }

        private void _CreateRegisterIfNotExist(string name) {
            if (_RegisterDict.ContainsKey(name)) {
                return;
            }

            _RegisterDict.Add(name, 0);
        }

        private bool _Greater(string leftComparer, int value) {
            return _RegisterDict.GetValueOrDefault(leftComparer) > value;
        }

        private bool _Less(string leftComparer, int value) {
            return _RegisterDict.GetValueOrDefault(leftComparer) < value;
        }

        private bool _Equal(string leftComparer, int value) {
            return _RegisterDict.GetValueOrDefault(leftComparer) == value;
        }

        private int _Inc(string name, int value) {
            return _RegisterDict.GetValueOrDefault(name) + value;
        }

        private int _Dec(string name, int value) {
            return _RegisterDict.GetValueOrDefault(name) - value;
        }

        public JumpInstruction(string[] input) {
            _ParseInstructions(input);
        }

        public int CalcLargest() {
            int largest = 0;

            foreach (int value in _RegisterDict.Values) {
                largest = value > largest 
                    ? value 
                    : largest;
            }

            return largest;
        }
    }

    public class Program {
        static void Main(string[] args) {
            string[] input = File.ReadAllLines("./input.txt");
            JumpInstruction jumpInstruction = new JumpInstruction(input);

            // Part 1
            Console.WriteLine(jumpInstruction.CalcLargest());
        }
    }
}
