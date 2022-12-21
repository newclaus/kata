namespace Kata.Klondike;

public class KlondikeGame
{
    private Board _board;

    public Status Status { get; private set; } = Status.New;

    public void StartNew()
    {
        StartNew(Board.CreateNew());
    }

    public void StartNew(Board board)
    {
        _board = board;
        Status = Status.Playing;
    }

    public bool HasMoves()
    {
        return true;
    }
}

public enum Status
{
    New,
    Playing
}