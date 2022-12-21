namespace Kata.Klondike;

public class StockPile : IPile
{
    private StockPile(Card first, Card last, int size)
    {
        First = first;
        Last = last;
        Size = size;
    }

    public Card? First { get; private set; }

    public Card? Last { get; private set; }

    public int Size { get; private set; }

    public int Iteration { get; private set; } = 1;

    public Card? Current { get; private set; }
    
    public bool Discard()
    {
        if (Current != null)
        {
            Current = Current.Next;
        }
        else if (First == null)
        {
            return false;
        }
        else
        {
            Current = First;
        }

        if (Current == null)
        {
            Iteration++;
        }
        
        return true;
    }

    public HandPile Take()
    {
        var current = Current;

        if (current == null)
        {
            throw new InvalidOperationException("No card to select.");
        }

        Current = current.Previous;
        if (current == First)
        {
            First = First.Next;
        }
        else if (current == Last)
        {
            Last = Last.Previous;
        }
        
        current.Take();

        Size--;

        return new HandPile(current, current, 1);
    }

    public static StockPile Create(Deck deck)
    {
        const int count = 24;
        var cards = deck.Take(count);

        return new StockPile(cards[0], cards[^1], cards.Length);
    }
}