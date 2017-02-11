using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerticalOrderTraversal
{
    public class Tree
    {
        private int?[] Values;

        public Tree(int?[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Values = values;
        }

        public int Root()
        {
            if (Values.Count() == 0 || !Values[0].HasValue)
            {
                return -1;
            }
            return 0;
        }

        public int LeftChild(int parent)
        {
            if (parent == -1)
            {
                return -1;
            }

            var leftChild = (parent * 2)+1;
            if (leftChild >= Values.Count() || !Values[leftChild].HasValue)
            {
                return -1;
            }
            return leftChild;
        }

        public int RightChild(int parent)
        {
            if (parent == -1)
            {
                return -1;
            }

            var rightChild = (parent * 2) + 2;
            if (rightChild >= Values.Count() || !Values[rightChild].HasValue)
            {
                return -1;
            }
            return rightChild;
        }

        public int Parent(int child)
        {
            if (child <= 0)
            {
                return -1;
            }

            return (child-1) / 2;
        }

        public string ValueToString(int position)
        {
            return Values[position].HasValue ? $"{Values[position]}" : "null";
        }

        public int[][] CreateVerticalOrderTraversal()
        {
            var root = Root();
            if (root == -1)
            {
                return new int[0][];
            }

            var lineDictionary = new Dictionary<int, List<int>>();
            TraverseNode(root, lineDictionary, 0);

            var values = lineDictionary.Keys.OrderBy(v => v);
            var result = new int[values.Count()][];
            var i = 0;
            foreach(var value in values)
            {
                result[i] = lineDictionary[value].ToArray();
                i++;
            }
            return result;
        }

        private void TraverseNode(int node, Dictionary<int, List<int>> lineDictionary, int lineNum)
        {
            if (node == -1)
            {
                return;
            }

            var value = Values[node];
            if (!value.HasValue)
            {
                return;
            }

            List<int> list;
            if (!lineDictionary.TryGetValue(lineNum, out list))
            {
                list = new List<int>();
                lineDictionary.Add(lineNum, list);
            }
            list.Add(value.Value);

            var left = LeftChild(node);
            var right = RightChild(node);
            TraverseNode(left, lineDictionary, lineNum - 1);
            TraverseNode(right, lineDictionary, lineNum + 1);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            var queue = new Queue<int>();
            var root = Root();
            if (root != -1)
            {
                queue.Enqueue(root);
                queue.Enqueue(-1);
            }

            while(queue.Count() > 0)
            {
                var node = queue.Dequeue();

                if (node == -1)
                {
                    stringBuilder.AppendLine();
                    if (queue.Count() > 0)
                    {
                        queue.Enqueue(-1);
                    }
                }
                else
                {
                    stringBuilder.Append(ValueToString(node));

                    var leftChild = LeftChild(node);
                    if (leftChild != -1)
                    {
                        queue.Enqueue(leftChild);
                    }

                    var rightChild = RightChild(node);
                    if (rightChild != -1)
                    {
                        queue.Enqueue(rightChild);
                    }
                } 
            }

            return stringBuilder.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tree = new Tree(new int?[]
            {
                3,9,8,4,0,1,7,null,null,null,2,5
            });

            Console.WriteLine("Tree:");
            Console.WriteLine(tree.ToString());
            Console.ReadLine();
            Console.WriteLine("Vertical Order Traversal:");
            var traversal = tree.CreateVerticalOrderTraversal();
            Console.WriteLine("[");
            foreach(var list in traversal)
            {
                Console.Write("[");
                foreach(var value in list)
                {
                    Console.Write(value + " ");
                }
                Console.WriteLine("],");
            }
            Console.WriteLine("]");
            Console.ReadLine();
        }
    }
}
