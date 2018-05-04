using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day8 {
    public class JumpInstructions {

        public int Largest { get; private set; } = 0;
        private SortedList<string, int> _RegisterList { get; set; } = new SortedList<string, int>();

        private void _ParseInstructions(string[] input) {
            foreach (string instruction in input) {
                _ParseInstruction(instruction);
            }
        }

        private void _ParseInstruction(string instruction) {
            string pattern = @"(\w{1})\s(\w+)\s(\d+)\sif\s(\w{1})\s(\S{1,2})\s(\S+)";
            Regex regex = new Regex(pattern, RegexOptions.ECMAScript);
            Match match = regex.Match(instruction);

            
        }

        private bool _EvaluateInstructionResult(string operation, string leftComparer, string rightComparer) {
            bool isPositive = false;

            _CreateRegisterIfNotExist(leftComparer);
            _CreateRegisterIfNotExist(rightComparer);

            switch (operation) {
                case ">":
                    isPositive = _Greater(leftComparer, rightComparer);
                    break;
                case ">=":
                    isPositive = _Greater(leftComparer, rightComparer) || _Equal(leftComparer, rightComparer);
                    break;
                case "<":
                    isPositive = _Less(leftComparer, rightComparer);
                    break;
                case "<=":
                    isPositive = _Less(leftComparer, rightComparer) || _Equal(leftComparer, rightComparer);
                    break;
                case "==":
                    isPositive = _Equal(leftComparer, rightComparer);
                    break;
                case "!=":
                    isPositive = !_Equal(leftComparer, rightComparer);
                    break;
            }

            return isPositive;
        }

        private void _CreateRegisterIfNotExist(string name) {
            if (_RegisterList.ContainsKey(name)) {
                return;
            }

            _RegisterList.Add(name, 0);
        }

        private bool _Greater(string leftComparer, string rightComparer) {
            return _RegisterList.GetValueOrDefault(leftComparer) > _RegisterList.GetValueOrDefault(rightComparer);
        }

        private bool _Less(string leftComparer, string rightComparer) {
            return _RegisterList.GetValueOrDefault(leftComparer) < _RegisterList.GetValueOrDefault(rightComparer);
        }

        private bool _Equal(string leftComparer, string rightComparer) {
            return _RegisterList.GetValueOrDefault(leftComparer) == _RegisterList.GetValueOrDefault(rightComparer);
        }

        private int _Inc(string name, int value) {
            return _RegisterList.GetValueOrDefault(name) + value;
        }

        private int _Dec(string name, int value) {
            return _RegisterList.GetValueOrDefault(name) - value;
        }

        public JumpInstructions(string[] input) {
            _ParseInstructions(input);
        }
    }

    public class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
        }
    }
}
