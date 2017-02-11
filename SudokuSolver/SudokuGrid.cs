using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class SudokuGrid
    {
        private int?[][] Grid;

        public SudokuGrid()
        {
            Grid = new int?[9][];
            for (int i = 0; i < 9; i++)
            {
                Grid[i] = new int?[9];
            }
        }

        public SudokuGrid(int?[][] numbers)
        {
            if (numbers == null)
            {
                throw new ArgumentNullException(nameof(numbers));
            }
            if (numbers.Count() != 9)
            {
                throw new ArgumentOutOfRangeException(nameof(numbers));
            }
            foreach (var row in numbers)
            {
                if (row == null)
                {
                    throw new ArgumentNullException(nameof(row));
                }

                if (row.Count() != 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(numbers));

                }
            }

            Grid = numbers;
        }

        public bool Solve()
        {
            return SolveInner(0, 0);
        }

        public bool SolveNext(int row, int col)
        {
            if (col < 8)
            {
                return SolveInner(row, ++col);
            }
            else
            {
                if (row < 8)
                {
                    return SolveInner(++row, 0);
                }
                else
                {
                    return true;
                }
            }
        }

        public bool SolveInner(int row, int col)
        {
            if (!Grid[row][col].HasValue)
            {
                for(int i = 1; i < 10; i++)
                {
                    if (Validate(row, col, i))
                    {
                        Grid[row][col] = i;
                        var works = SolveNext(row, col);
                        if (works)
                        {
                            return true;
                        } else
                        {
                            Grid[row][col] = null;
                        }
                    }
                }
                return false;
            }
            else
            {
                return SolveNext(row, col);
            }
        }

        public bool Validate(int row, int col, int newValue)
        {
            var oldValue = Grid[row][col];
            Grid[row][col] = newValue;
            var smallerGrid = row / 3 + (col /3)*3;
            var rowSet = new HashSet<int>();
            var columnSet = new HashSet<int>();
            var smallerGridSet = new HashSet<int>();
            for (int j = 0; j < 9; j++)
            {
                var rowVal = Grid[row][j];
                var colVal = Grid[j][col];
                var smallerGridVal = Grid[((smallerGrid % 3) * 3) + j % 3][((smallerGrid / 3) * 3) + j / 3];

                if ((rowVal.HasValue && !rowSet.Add(rowVal.Value)) || 
                    (colVal.HasValue && !columnSet.Add(colVal.Value)) ||
                    (smallerGridVal.HasValue && !smallerGridSet.Add(smallerGridVal.Value)))
                {
                    Grid[row][col] = oldValue;
                    return false;
                }
            }

            Grid[row][col] = oldValue;
            return true;
        }

        public bool Validate()
        {
            for(int i = 0; i < 9; i++)
            {
                var rowSet = new HashSet<int>();
                var columnSet = new HashSet<int>();
                var smallerGridSet = new HashSet<int>();
                for(int j = 0; j < 9; j++)
                {
                    var rowVal = Grid[i][j];
                    var colVal = Grid[j][i];
                    var smallerGridVal = Grid[((i%3)*3) + j%3][((i/3)*3) + j/3];
                    if (!rowVal.HasValue || !colVal.HasValue || !smallerGridVal.HasValue)
                    {
                        return false;
                    }

                    if(!rowSet.Add(rowVal.Value) || !columnSet.Add(colVal.Value) || !smallerGridSet.Add(smallerGridVal.Value))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach(var row in Grid)
            {
                stringBuilder.Append("|");
                foreach(var cell in row)
                {
                    stringBuilder.Append(" ");
                    stringBuilder.Append(cell.HasValue ? cell.ToString() : " ");
                    stringBuilder.Append(" |");
                }
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }

    }
}
