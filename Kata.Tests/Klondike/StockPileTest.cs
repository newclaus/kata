using Kata.Klondike;

namespace Kata.Tests.Klondike;

public class StockPileTest
{
    private const int InitialStockSize = 24;
    private readonly StockPile _stockPile;

    public StockPileTest()
    {
        using var deck = Deck.Create(Card.CreateAllCards());

        _stockPile = StockPile.Create(deck);
    }

    [Fact]
    public void Test_FirstIteration()
    {
        Assert.Equal(1, _stockPile.Iteration);
    }

    [Fact]
    public void Test_DiscardEmpty_NoCurrent()
    {
        Assert.Null(_stockPile.Current);
    }

    [Fact]
    public void Test_Size24()
    {
        Assert.Equal(InitialStockSize, _stockPile.Size);
    }

    [Fact]
    public void TestDiscard_Selected()
    {
        var selected = _stockPile.Discard();

        Assert.True(selected);
    }

    [Fact]
    public void TestDiscard_CurrentIsFirst()
    {
        _stockPile.Discard();

        Assert.Equal(_stockPile.First, _stockPile.Current);
    }

    [Fact]
    public void TestDiscard_TillEnd_CurrentIsLast()
    {
        DiscardTillEnd();

        Assert.Equal(_stockPile.Last, _stockPile.Current);
    }

    [Fact]
    public void TestDiscard_TillEnd_FirstIteration()
    {
        DiscardTillEnd();

        Assert.Equal(1, _stockPile.Iteration);
    }

    [Fact]
    public void TestDiscard_StockEmpty_NoCurrent()
    {
        DiscardTillEndWithExtraMoves(1);

        Assert.Null(_stockPile.Current);
    }

    [Fact]
    public void TestDiscard_StockEmpty_SecondIteration()
    {
        DiscardTillEndWithExtraMoves(1);

        Assert.Equal(2, _stockPile.Iteration);
    }

    [Fact]
    public void TestDiscard_MoveTwiceWhenStockEmpty_CurrentIsFirst()
    {
        DiscardTillEndWithExtraMoves(2);

        Assert.Equal(_stockPile.First, _stockPile.Current);
    }

    private void DiscardTillEnd()
    {
        DiscardTillEndWithExtraMoves(0);
    }

    private void DiscardTillEndWithExtraMoves(int extraMoves)
    {
        for (int i = -extraMoves; i < _stockPile.Size; i++)
        {
            _stockPile.Discard();
        }
    }

    [Fact]
    public void TestTake_DiscardEmpty_Failed()
    {
        var exception = Assert.Throws<InvalidOperationException>(() => _stockPile.Take());

        Assert.Equal("No card to select.", exception.Message);
    }

    [Fact]
    public void TestTake_OneCardInHand()
    {
        var hand = DiscardAndTake();

        Assert.Equal(1, hand.Size);
    }

    [Fact]
    public void TestTake_SizeDecreased()
    {
        const int expectedSize = InitialStockSize - 1;
        
        DiscardAndTake();

        Assert.Equal(expectedSize, _stockPile.Size);
    }

    [Fact]
    public void TestTake_FirstCard_NoCurrent()
    {
        DiscardAndTake();

        Assert.Null(_stockPile.Current);
    }

    [Fact]
    public void TestTake_SecondCard_CurrentIsFirst()
    {
        _stockPile.Discard();
        DiscardAndTake();

        Assert.Equal(_stockPile.First, _stockPile.Current);
    }

    [Fact]
    public void TestTake_FirstCard_FirstChanged()
    {
        var secondCard = _stockPile.First!.Next;
        
        DiscardAndTake();

        Assert.Equal(secondCard, _stockPile.First);
    }

    [Fact]
    public void TestTake_LastCard_LastChanged()
    {
        var penultimateCard = _stockPile.Last!.Previous;
        
        DiscardTillEnd();
        _stockPile.Take();

        Assert.Equal(penultimateCard, _stockPile.Last);
    }

    private HandPile DiscardAndTake()
    {
        _stockPile.Discard();

        return _stockPile.Take();
    }
}