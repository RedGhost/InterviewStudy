using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new int?[][]
            {
                new int?[] {5, 3, null, null, 7, null, null, null, null},
                new int?[] {6, null, null, 1, 9, 5, null, null, null},
                new int?[] {null, 9, 8, null, null, null, null, 6, null},
                new int?[] {8, null, null, null, 6, null, null, null, 3},
                new int?[] {4, null, null, 8, null, 3, null, null, 1},
                new int?[] {7, null, null, null, 2, null, null, null, 6},
                new int?[] {null, 6, null, null, null, null, 2, 8, null},
                new int?[] {null, null, null, 4, 1, 9, null, null, 5},
                new int?[] {null, null, null, null, 8, null, null, 7, 9},
            };

            var board = new SudokuGrid(grid);

            Console.WriteLine(board);
            Console.WriteLine("Board is " + (board.Validate() ? "Valid!" : "Invalid!"));
            Console.ReadLine();
            Console.WriteLine("Board is solved: " + board.Solve());
            Console.WriteLine(board);
            Console.WriteLine("Board is " + (board.Validate() ? "Valid!" : "Invalid!"));
            Console.ReadLine();
        }
    }
}
