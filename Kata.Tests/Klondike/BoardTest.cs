using Kata.Klondike;

namespace Kata.Tests.Klondike;

public class BoardTest
{
    private readonly Board _board;

    public BoardTest()
    {
        _board = Board.CreateNew();
    }

    [Fact]
    public void Test_SevenTableauPiles()
    {
        Assert.Equal(7, _board.Tableau.Count);
    }

    [Fact]
    public void Test_CardsInTableauPiles()
    {
        Assert.Equal(new[] { 1, 2, 3, 4, 5, 6, 7 }, _board.Tableau.Select(pile => pile.Size));
    }

    [Fact]
    public void Test_StockPiles()
    {
        Assert.Equal(24, _board.StockPile.Size);
    }

    [Fact]
    public void Test_FoundationPiles()
    {
        Assert.Equal(4, _board.FoundationPiles.Count);
        Assert.All(_board.FoundationPiles, pair => Assert.Equal(pair.Key, pair.Value.Suit));
    }
}