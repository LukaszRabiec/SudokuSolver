using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace SudokuSolver
{
    public class Sudoku
    {
        public int[,] Board { get; }
        public int[,] UnfilledBoard { get; }

        public Sudoku(string jsonFilePath)
        {
            Board = GetSudokuFromJsonFile(jsonFilePath);
            UnfilledBoard = Board;
        }

        public Sudoku(int[,] board)
        {
            Board = board;
            UnfilledBoard = board;
        }

        public int[,] Solve()
        {
            return TryToSolve(0, 0) ? Board : UnfilledBoard;
        }

        private bool TryToSolve(int row, int col)
        {
            while (true)
            {
                if (row < 9 && col < 9)
                {
                    if (Board[row, col] != 0)
                    {
                        if (col + 1 < 9)
                        {
                            col = col + 1;
                            continue;
                        }
                        if (row + 1 < 9)
                        {
                            row = row + 1;
                            col = 0;
                            continue;
                        }
                        return true;
                    }

                    for (int number = 1; number <= 9; number++)
                    {
                        if (CanFillWithNumber(row, col, number))
                        {
                            Board[row, col] = number;

                            if (col + 1 < 9)
                            {
                                if (TryToSolve(row, col + 1))
                                {
                                    return true;
                                }
                                Board[row, col] = 0;
                            }
                            else if (row + 1 < 9)
                            {
                                if (TryToSolve(row + 1, 0))
                                {
                                    return true;
                                }
                                Board[row, col] = 0;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
                return true;
            }
        }

        private bool CanFillWithNumber(int row, int col, int number)
        {
            int rowStart = row / 3 * 3;
            int colStart = col / 3 * 3;

            for (int i = 0; i < 9; i++)
            {
                if (Board[row, i] == number)
                {
                    return false;
                }
                if (Board[i, col] == number)
                {
                    return false;
                }
                if (Board[rowStart + i % 3, colStart + i / 3] == number)
                {
                    return false;
                }
            }

            return true;
        }

        private int[,] GetSudokuFromJsonFile(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<int[,]>(streamReader.ReadToEnd());
            }
        }

        public override string ToString()
        {
            return ConvertMatrixToString(Board);
        }

        private string ConvertMatrixToString(int[,] matrix)
        {
            string resultString = default(string);
            StringBuilder stringBuilder = new StringBuilder();
            const string horizontalLine = "+-----+-----+-----+\n";

            resultString += horizontalLine;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                stringBuilder.Clear();
                stringBuilder.Append("|");

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    stringBuilder.Append(matrix[row, col]);
                    if ((col + 1) % 3 == 0)
                    {
                        if (col + 1 != 9)
                        {
                            stringBuilder.Append("|");
                        }
                    }
                    else
                    {
                        stringBuilder.Append(" ");
                    }
                }

                stringBuilder.Append("|\n");

                if ((row + 1) % 3 == 0)
                {
                    stringBuilder.Append(horizontalLine);
                }

                resultString += stringBuilder;
            }

            return resultString;
        }
    }
}
