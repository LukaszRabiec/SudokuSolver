using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace SudokuSolver.Tests
{
    public class SudokuTests
    {
        private const string _dirPath = "Data/";
        private readonly ITestOutputHelper _output;

        public SudokuTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void SolvingEasySudokuShouldReturnCorrectSolution()
        {
            var sudoku = new Sudoku(_dirPath + "sudoku1.json");
            var correctSudoku = new Sudoku(_dirPath + "sudoku1Solved.json");

            _output.WriteLine(sudoku.ToString());

            var solvedSudoku = sudoku.Solve();

            solvedSudoku.ShouldBeEquivalentTo(correctSudoku.Board);
            _output.WriteLine(sudoku.ToString());
        }

        [Fact]
        public void SolvingHardSudokuShouldReturnCorrectSolution()
        {
            var sudoku = new Sudoku(_dirPath + "sudoku2.json");
            var correctSudoku = new Sudoku(_dirPath + "sudoku2Solved.json");

            _output.WriteLine(sudoku.ToString());

            var solvedSudoku = sudoku.Solve();

            solvedSudoku.ShouldBeEquivalentTo(correctSudoku.Board);
            _output.WriteLine(sudoku.ToString());
        }
    }
}
