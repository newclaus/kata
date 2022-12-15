namespace Kata;

public class TicTacToeGame
{
    public int IsSolved(int[,] board)
    {
        if (CheckNonDiagonal(board, out int playerId))
        {
            return playerId;
        }
        
        if (CheckDiagonal(board, out playerId))
        {
            return playerId;
        }
        
        for (int rowId = 0; rowId < 3; rowId++)
        for (int colId = 0; colId < 3; colId++)
        {
            if (board[rowId, colId] == 0)
            {
                return -1;
            }
        }
        
        
        return 0;
    }

    private bool CheckNonDiagonal(int[,] board, out int playerId)
    {
        for (int lineId = 0; lineId < 3; lineId++)
        {
            if (CheckLine(board, 1, lineId, 1, 0, out playerId) ||
                CheckLine(board, lineId, 1, 0, 1, out playerId))
            {
                return true;
            }
        }

        playerId = 0;
        return false;
    }

    private bool CheckDiagonal(int[,] board, out int playerId)
    {
        return CheckLine(board, 1, 1, 1, 1, out playerId) ||
               CheckLine(board, 1, 1, 1, -1, out playerId);
    }

    private bool CheckLine(int[,] board, int row, int col, int rowDelta, int colDelta, out int playerId)
    {
        var value1 = board[row, col];
        var value2 = board[row - rowDelta, col - colDelta];
        var value3 = board[row + rowDelta, col + colDelta];

        if (value1 != 0 && value1 == value2 && value1 == value3)
        {
            playerId = value1;
            return true;
        }

        playerId = 0;
        return false;
    }
}