using Kata.Klondike;

namespace Kata.Tests.Klondike;

public class DeckTest : IDisposable
{
    private readonly Deck _pile;

    public DeckTest()
    {
        _pile = Deck.Create();
    }

    [Fact]
    public void TestCreateDeck_52Cards()
    {
        Assert.Equal(52, TakeAll().Length);
    }

    [Fact]
    public void TestCreateDeck_AllChained()
    {
        var cards = TakeAll();
        
        var current = cards[0];
        int count = 1;

        while (current.Next != null)
        {
            current = current.Next;
            count++;
        }
        
        Assert.Equal(current, cards[^1]);
        Assert.Equal(52, count);
    }

    private Card[] TakeAll()
    {
        return _pile.Take(52);
    }

    public void Dispose()
    {
        _pile.Dispose();
    }
}