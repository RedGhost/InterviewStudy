using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Board
    {
        private int M;
        private int N;
        public int[][] Data;

        public Board(int m, int n)
        {
            if (m <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(m));
            }
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            M = m;
            N = n;
            Data = new int[m][];
            for(int i = 0; i < m; i++)
            {
                Data[i] = new int[n];
            }
        }

        public void Randomize()
        {
            var random = new Random();
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Data[i][j] = random.Next(2);
                }
            }
        }

        public void Step()
        {
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    var numNeighbors = NumberOfLiveNeighbors(i, j);
                    if (Data[i][j] == 1)
                    {
                        if (numNeighbors < 2 || numNeighbors > 3)
                        {
                            Data[i][j] = 2;
                        }
                    }
                    else if (Data[i][j] == 0 && numNeighbors == 3)
                    {
                        Data[i][j] = 3;
                    }
                }
            }

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if(Data[i][j] == 2)
                    {
                        Data[i][j] = 0;
                    }
                    else if (Data[i][j] == 3)
                    {
                        Data[i][j] = 1;
                    }
                }
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < M; i++)
            {
                stringBuilder.Append("|");
                for (int j = 0; j < N; j++)
                {
                    stringBuilder.Append(" ");
                    stringBuilder.Append(Data[i][j]);
                    stringBuilder.Append(" |");
                }
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }

        public int NumberOfLiveNeighbors(int i, int j)
        {
            if (i < 0 || i >= M)
            {
                throw new ArgumentOutOfRangeException(nameof(i));
            }
            if (j < 0 || j >= N)
            {
                throw new ArgumentOutOfRangeException(nameof(j));
            }

            var numNeighbors = 0;
            if (i > 0 && j > 0 && (Data[i - 1][j - 1] == 1 || Data[i - 1][j - 1] == 2))
            {
                numNeighbors++;
            }

            if (i > 0 && (Data[i - 1][j] == 1 || Data[i - 1][j] == 2))
            {
                numNeighbors++;
            }

            if (j > 0 && (Data[i][j-1] == 1 || Data[i][j-1] == 2))
            {
                numNeighbors++;
            }

            if (i < (M-1) && j < (N-1) && (Data[i + 1][j + 1] == 1 || Data[i + 1][j + 1] == 2))
            {
                numNeighbors++;
            }

            if (i < (M-1) && (Data[i + 1][j] == 1 || Data[i + 1][j] == 2))
            {
                numNeighbors++;
            }

            if (j < (N-1) && (Data[i][j+1] == 1 || Data[i][j+1] == 2))
            {
                numNeighbors++;
            }

            if (i < (M - 1) && j > 0 && (Data[i + 1][j - 1] == 1 || Data[i + 1][j - 1] == 2))
            {
                numNeighbors++;
            }

            if (i > 0 && j < (N - 1) && (Data[i - 1][j + 1] == 1 || Data[i - 1][j + 1] == 2))
            {
                numNeighbors++;
            }

            return numNeighbors;
        }
    }
}
