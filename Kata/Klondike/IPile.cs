namespace Kata.Klondike;

public interface IPile
{
    Card? First { get; }

    Card? Last { get; }

    int Size { get; }
}