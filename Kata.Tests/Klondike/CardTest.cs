using Kata.Klondike;

namespace Kata.Tests.Klondike;

public class CardTest
{
    private readonly Card[] _deck;

    public CardTest()
    {
        _deck = Card.CreateAllCards().ToArray();
    }

    [Fact]
    public void Test_CreateAll_52Cards()
    {
        Assert.Equal(52, _deck.Length);
    }

    [Fact]
    public void Test_PutAt_Successful()
    {
        var card1 = _deck[0];
        var card2 = _deck[1];

        card2.PutAt(card1);

        Assert.Equal(card1, card2.Previous);
        Assert.Equal(card2, card1.Next);
    }

    [Fact]
    public void Test_PutAtAlreadyChained_ArgumentOutOfRangeException()
    {
        var card1 = _deck[0];
        var card2 = _deck[1];
        var card3 = _deck[2];

        card2.PutAt(card1);

        Assert.Throws<ArgumentOutOfRangeException>(() => card3.PutAt(card1));
    }

    [Fact]
    public void Test_PutAt_CurrentAlreadyChained_InvalidOperationException()
    {
        var card1 = _deck[0];
        var card2 = _deck[1];
        var card3 = _deck[2];

        card2.PutAt(card1);

        Assert.Throws<InvalidOperationException>(() => card2.PutAt(card3));
    }

    [Fact]
    public void Test_Take()
    {
        var card1 = _deck[0];
        var card2 = _deck[1];

        card2.PutAt(card1);

        card2.Take();

        Assert.Null(card1.Next);
        Assert.Null(card2.Previous);
    }
}