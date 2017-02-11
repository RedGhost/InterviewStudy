using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    class Program
    {
        static void Main(string[] args)
        {
            var cache = new Cache(2);

            cache.Set(1, 1);
            cache.Set(2, 2);
            Console.WriteLine(cache.Get(1));       // returns 1
            cache.Set(3, 3);    // evicts key 2
            Console.WriteLine(cache.Get(2));       // returns -1 (not found)
            Console.WriteLine(cache.Get(3));       // returns 3.
            cache.Set(4, 4);    // evicts key 1.
            Console.WriteLine(cache.Get(1));       // returns -1 (not found)
            Console.WriteLine(cache.Get(3));       // returns 3
            Console.WriteLine(cache.Get(4));       // returns 4
            Console.ReadLine();
        }
    }
}
