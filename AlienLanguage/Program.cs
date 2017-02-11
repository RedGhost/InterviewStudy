using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienLanguage
{
    public class TrieNode
    {
        public char Letter;
        public Dictionary<char, TrieNode> Children;
        public List<char> OrderedLetters;

        public TrieNode(char letter)
        {
            Letter = letter;
            Children = new Dictionary<char, TrieNode>();
            OrderedLetters = new List<char>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var words = new string[]
            {
                "wrt",
                "wrf",
                "er",
                "ett",
                "rftt"
            };

            // Create Trie
            var root = new TrieNode('\0');
            var interestingNodes = new List<TrieNode>();
            var allLetters = new HashSet<char>();
            foreach(var word in words)
            {
                if(!InsertWord(root, word, 0, interestingNodes, allLetters))
                {
                    Console.WriteLine("Input is invalid");
                    Console.ReadLine();
                    return;
                }
            }

            var outgoingEdges = new Dictionary<char, HashSet<char>>();
            var incomingEdges = new Dictionary<char, HashSet<char>>();

            var startingLetters = new HashSet<char>(allLetters);
            foreach (var node in interestingNodes)
            {
                var from = node.OrderedLetters.First();
                var orderedLetters = node.OrderedLetters.Skip(1);
                foreach (var letter in orderedLetters)
                {
                    HashSet<char> outgoing;
                    if(!outgoingEdges.TryGetValue(from, out outgoing))
                    {
                        outgoing = new HashSet<char>();
                        outgoingEdges[from] = outgoing;
                    }
                    outgoing.Add(letter);

                    HashSet<char> incoming;
                    if (!incomingEdges.TryGetValue(letter, out incoming))
                    {
                        incoming = new HashSet<char>();
                        incomingEdges[letter] = incoming;
                    }
                    incoming.Add(from);

                    from = letter;
                    startingLetters.Remove(letter);
                }
            }

            var result = new List<char>();
            while(startingLetters.Count() > 0)
            {
                var node = startingLetters.First();
                startingLetters.Remove(node);
                result.Add(node);
                HashSet<char> adjacentNodes = outgoingEdges.ContainsKey(node) ? new HashSet<char>(outgoingEdges[node]) : new HashSet<char>();
                foreach(var adjacentNode in adjacentNodes)
                {
                    outgoingEdges[node].Remove(adjacentNode);
                    incomingEdges[adjacentNode].Remove(node);
                    if (incomingEdges[adjacentNode].Count() == 0)
                    {
                        startingLetters.Add(adjacentNode);
                        incomingEdges.Remove(adjacentNode);
                    }
                }
            }
            if (incomingEdges.Count() > 0)
            {
                Console.WriteLine("Input is invalid");
                Console.ReadLine();
            }

            Console.WriteLine(string.Join("", result));
            Console.ReadLine();
        }

        static bool InsertWord(TrieNode node, string word, int position, List<TrieNode> interestingNodes, HashSet<char> allLetters)
        {
            if (position >= word.Count())
            {
                return true;
            }

            var letter = word[position];
            allLetters.Add(letter);
            TrieNode child;
            if (!node.Children.TryGetValue(letter, out child))
            {
                child = new TrieNode(letter);
                node.Children.Add(letter, child);
                node.OrderedLetters.Add(letter);

                // these nodes become interesting when we have have just added a second child.
                // This ordered letters array gives information into the graph.
                if (node.OrderedLetters.Count() == 2)
                {
                    interestingNodes.Add(node);
                }
            }

            if (node.OrderedLetters.Last() != letter)
            {
                return false;
            }

            return InsertWord(child, word, position + 1, interestingNodes, allLetters);
        }
    }
}
