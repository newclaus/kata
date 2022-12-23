namespace Kata.Klondike;

public class KlondikeGame
{
    public Board Board { get; private set; }

    public Status Status { get; private set; } = Status.New;

    public void StartNew()
    {
        StartNew(Board.CreateNew());
    }

    public void StartNew(Board board)
    {
        Board = board;
        Status = Status.Playing;
    }
}

public enum Status
{
    New,
    Playing
}