using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    internal class Node
    {
        public int Key;
        public int Value;
        public Node Next;
        public Node Previous;

        public Node(int key, int value)
        {
            Key = key;
            Value = value;
            Next = null;
            Previous = null;
        }
    }

    public class Cache
    {
        private int Capacity;
        private Node Head;
        private Node Tail;
        private Dictionary<int, Node> Data;

        public Cache(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            Capacity = capacity;
            Data = new Dictionary<int, Node>(capacity);
            Head = null;
            Tail = null;
        }

        public int Get(int key)
        {
            var node = MoveNodeToHead(key);
            return node != null ? node.Value : -1;
        }

        public void Set(int key, int value)
        {
            var node = MoveNodeToHead(key);
            // Didn't find a node with a previous value in cache.
            if(node == null)
            {
                // we are at capacity so we have to remove the least recently used item.
                if (Data.Count() == Capacity)
                {
                    Tail.Previous.Next = null;
                    Data.Remove(Tail.Key);
                    Tail = Tail.Previous;
                }

                node = new Node(key, value);
                AddFirst(node);
                Data[key] = node;
            }
            else
            {
                node.Value = value;
            }
        }

        private void AddFirst(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (Head == null)
            {
                node.Previous = null;
                node.Next = null;
                Head = node;
                Tail = node;
            }
            else
            {
                node.Next = Head;
                Head.Previous = node;
                Head = node;
            }
        }

        private Node MoveNodeToHead(int key)
        {
            Node node;
            if (Data.TryGetValue(key, out node))
            {
                // Don't need to do anything if the node is already the head.
                if (node != Head)
                {
                    // Node is tail.
                    if (node == Tail)
                    {
                        // Reset tail node
                        node.Previous.Next = null;
                        Tail = node.Previous;
                    }
                    // Node is somewhere in the middle.
                    else
                    {
                        // Remove the node.
                        node.Previous.Next = node.Next;
                        node.Next.Previous = node.Previous;
                    }

                    // Set node as the head.
                    AddFirst(node);
                }
                return node;
            }
            return null;
        }
    }
}
