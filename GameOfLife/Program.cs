using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board(20, 20);
            board.Randomize();

            while(true)
            {
                Console.WriteLine("Step:");
                Console.WriteLine(board.ToString());
                Console.ReadLine();
                board.Step();
            }
        }
    }
}
