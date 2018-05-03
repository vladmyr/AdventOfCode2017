using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day7
{
    public class Node {
        public string Name { get; private set; }
        public int Weight { get; set; }
        public int TotalWeight { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; private set; }

        public Node(string name, int weight = 0, Node parent = null) {
            Name = name;
            Weight = weight;
            TotalWeight = Weight;
            Parent = parent;
            Children = new List<Node>();
        }

        public void AddChild(Node child) {
            Children.Add(child);
        }

        public int CalcTotalWeight() {
            int weight = Weight;

            foreach(Node child in Children) {
                weight += child.CalcTotalWeight();
            }

            TotalWeight = weight;
            return weight;
        }
    }

    public class Tree {
        
        private List<List<Node>> _Forest { get; set; }
        private HashSet<Node> _NodeWoParentSet { get; set; }
        private SortedList<int, List<Node>> _IdxWeight { get; set; }
        private SortedList<string, Node> _IdxNode { get; set; }

        public int ProperBalancedWeight { get; private set; } = 0;

        public Node RootNode { get; private set; }

        public Tree(string[] input) {
            _NodeWoParentSet = new HashSet<Node>();
            _Forest = new List<List<Node>>();
            _IdxWeight = new SortedList<int, List<Node>>();
            _IdxNode = new SortedList<string, Node>();

            _ParseInput(input);
            _SetRootNode();

            RootNode.CalcTotalWeight();

            ProperBalancedWeight = _CalcProperBalancedWeight(RootNode);
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

        private int _CalcProperBalancedWeight(Node node, int properWeightCandidate = 0) {
            if (node.Children.Count < 2)
                return node.TotalWeight;

            Node childNode1 = node.Children[0];
            Node childNode2 = node.Children[1];
            int index = 2;
            int weightDiff = childNode1.TotalWeight - childNode2.TotalWeight;

            while (weightDiff == 0 && index < node.Children.Count) {
                childNode2 = node.Children[index];
                weightDiff = childNode1.TotalWeight - childNode2.TotalWeight;
                index++;
            }

            if (weightDiff == 0) {
                return properWeightCandidate;
            }

            Node nodeToTraverse = childNode1.TotalWeight > childNode2.TotalWeight
                ? childNode1
                : childNode2;

            return _CalcProperBalancedWeight(nodeToTraverse, nodeToTraverse.Weight - Math.Abs(weightDiff));
        }
    }

    class Program {
        static void Main(string[] args) {
            string[] input = System.IO.File.ReadAllLines("./input.txt");

            Tree tree = new Tree(input);

            // Part 1
            Console.WriteLine(tree.RootNode.Name);

            // Part 2
            Console.WriteLine(tree.ProperBalancedWeight);

            Console.ReadKey();
        }
    }
}
