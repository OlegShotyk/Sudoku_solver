namespace Sudoku
{
    public class SudokuSolver
    {
        private const int boardSize = 9;
        public bool ValidateBoard(int[][] board)
        {
            if(board.GetLength(0) != boardSize)
            { 
                return false; 
            }
            foreach (int[] boardRow in board)
            {
                if (boardRow.Length != boardSize)
                {
                    return false;
                }
                foreach(int element in boardRow)
                {
                    if(element > 9 || element < 0)
                    {
                        return false;
                    }
                }
            }

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (board[row][col] != 0)
                    {
                       if(!IsSafe(board, row, col, board[row][col]))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool IsSafe(int[][] board, int row, int column, int checkedNumber)
        {
            for (int rowElement = 0; rowElement < boardSize; rowElement++)
            {
                if (board[row][rowElement] == checkedNumber && rowElement != column)
                {
                    return false;
                }
            }

            for (int columnElement = 0; columnElement < boardSize; columnElement++)
            {
                if (board[columnElement][column] == checkedNumber && columnElement != row)
                {
                    return false;
                }
            }

            int startRow = row - row % 3;
            int startCol = column - column % 3;
            for (int squareWidth = 0; squareWidth < 3; squareWidth++)
            {
                for (int squareHeight = 0; squareHeight < 3; squareHeight++)
                {
                    if (board[squareWidth + startRow][squareHeight + startCol] == checkedNumber)
                    {
                        if(squareWidth + startRow != row || squareHeight + startCol != column)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;

        }

        public int[][] SolveBoard(int[][] board)
        {
            try
            {
                for (int row = 0; row < boardSize; row++)
                {
                    for (int col = 0; col < boardSize; col++)
                    {
                        if (board[row][col] == 0)
                        {
                            for (int num = 1; num <= 9; num++)
                            {
                                if (IsSafe(board, row, col, num))
                                {
                                    board[row][col] = num;

                                    if (SolveBoard(board) != null)
                                    {
                                        return board;
                                    }

                                    board[row][col] = 0;
                                }
                            }

                            return null;
                        }
                    }
                }

                return board;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }

        }
    }
}
