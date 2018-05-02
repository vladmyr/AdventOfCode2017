using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day7
{
    public class Node {
        public string Name { get; private set; }
        public int Weight { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; private set; }

        public Node(string name, int weight = 0, Node parent = null) {
            Name = name;
            Weight = weight;
            Parent = parent;
            Children = new List<Node>();
        }

        public void AddChild(Node child) {
            Children.Add(child);
        }
    }

    public class Tree {
        
        private List<List<Node>> _Forest { get; set; }
        private HashSet<Node> _NodeWoParentSet { get; set; }
        private SortedList<int, List<Node>> _IdxWeight { get; set; }
        private SortedList<string, Node> _IdxNode { get; set; }

        public Node RootNode { get; private set; }

        public Tree(string[] input) {
            _NodeWoParentSet = new HashSet<Node>();
            _Forest = new List<List<Node>>();
            _IdxWeight = new SortedList<int, List<Node>>();
            _IdxNode = new SortedList<string, Node>();

            _ParseInput(input);
            _SetRootNode();
        }

        private void _ParseInput(string[] input) {
            foreach (string line in input) {
                _ParseInputLine(line);
            }
        }

        private void _ParseInputLine(string line) {
            string pattern = line.Contains("->")
                ? @"(\w+)\s\((\d+)\)\s->\s(.+)"
                : @"(\w+)\s\((\d+)\)*";
            Regex regex = new Regex(pattern, RegexOptions.ECMAScript);

            Match match = regex.Match(line);

            // parse node information
            string nodeName = match.Groups[1].Value;
            int nodeWeight = Int32.Parse(match.Groups[2].Value);
            Node node = _IdxNode.GetValueOrDefault(nodeName);

            // populate node and add to indexes
            if (node == null) {
                node = new Node(nodeName, nodeWeight);

                _NodeWoParentSet.Add(node);

                _IdxNode.Add(nodeName, node);
                _AddWeightIndex(node);
            } else {
                node.Weight = nodeWeight;
                _AddWeightIndex(node);
            }

            // parse children nodes
            if (match.Groups.Count < 4)
                return;

            string[] childrenNames = match.Groups[3].Value.Split(", ");

            foreach(string childNodeName in childrenNames) {
                Node childNode = _IdxNode.GetValueOrDefault(childNodeName);

                if (childNode == null) {
                    childNode = new Node(childNodeName);
                    _IdxNode.Add(childNodeName, childNode);
                }

                node.AddChild(childNode);
                childNode.Parent = node;

                _NodeWoParentSet.Remove(childNode);
            }

            return;
        }

        private void _AddWeightIndex(Node node) {
            if (node.Weight == 0) 
                return;

            if (!_IdxWeight.ContainsKey(node.Weight)) {
                _IdxWeight.Add(node.Weight, new List<Node>());
            }

            _IdxWeight
                .GetValueOrDefault(node.Weight)
                .Add(node);
        }

        private void _SetRootNode() {
            var enumerator = _NodeWoParentSet.GetEnumerator();
            enumerator.MoveNext();

            RootNode = enumerator.Current;
        }
    }

    class Program {
        static void Main(string[] args) {
            string[] input = System.IO.File.ReadAllLines("./input.txt");

            Tree tree = new Tree(input);

            Console.WriteLine(tree.RootNode.Name);

            Console.ReadKey();
        }
    }
}
