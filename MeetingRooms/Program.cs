using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRooms
{
    class Program
    {
        public class Item
        {
            public int Time;
            public bool Start;
        }

        static void Main(string[] args)
        {
            var input = new int[][]
            {
                new int[] { 0, 30 },
                new int[] { 5, 10 },
                new int[] { 15, 20 },
                new int[] { 2, 17 }
            };

            var squashedInput = input.SelectMany(a => new Item[] { new Item { Time = a.First(), Start = true }, new Item { Time = a.Last(), Start = false } });
            var sortedSquashedInput = squashedInput.OrderBy(item => item.Time).ThenBy(item => item.Start);

            var numRooms = 0;
            var maxRooms = 0;
            foreach(var item in sortedSquashedInput)
            {
                if (item.Start)
                {
                    numRooms++;
                    if (maxRooms < numRooms)
                    {
                        maxRooms = numRooms;
                    }
                }
                else
                {
                    numRooms--;
                }
            }

            Console.WriteLine("Max number of rooms required: " + maxRooms);
            Console.ReadLine();
        }
    }
}
