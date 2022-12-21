namespace Kata.Klondike;

public class TableauPile : IPile
{
    private TableauPile(Card first, Card last, int size)
    {
        First = first;
        Last = last;
        Size = size;
        FirstVisible = Last;
    }

    public Card? First { get; private set; }

    public Card? Last { get; private set; }

    public int Size { get; private set; }

    public Card? FirstVisible { get; private set; }

    public HandPile Take(int count)
    {
        Card last = Last;
        Card current = last;

        for (int i = 1; i < count; i++)
        {
            current = current.Previous;
        }

        Last = current.Previous;
        if (Last == null)
        {
            First = null;
        }

        Size -= count;

        current.Take();

        return new HandPile(current, last, count);
    }

    public void Put(HandPile hand)
    {
        if (Size > 0)
        {
            hand.First.PutAt(Last);
        }
        else
        {
            First = hand.First;
        }
        
        Last = hand.Last;
        
        Size += hand.Size;
    }

    public int GetMaxCountToTake()
    {
        if (Size == 0)
        {
            return 0;
        }
        
        int maxCount = 1;
        var current = Last;
        var previous = current.Previous;

        while (previous != null && CanPutAt(current, previous))
        {
            maxCount++;
            current = previous;
            previous = current.Previous;
        }
        
        return maxCount;
    }

    public static bool CanPutAt(Card topCard, Card bottomCard)
    {
        return topCard.Rank == bottomCard.Rank - 1 &&
               (topCard.Suit, bottomCard.Suit) is
               (Suit.Hearts or Suit.Diamonds, Suit.Clubs or Suit.Spades) or
               (Suit.Clubs or Suit.Spades, Suit.Hearts or Suit.Diamonds);
    }

    public static TableauPile Create(Deck deck, int count)
    {
        var cards = deck.Take(count);

        return new TableauPile(cards[0], cards[^1], cards.Length);
    }
}

public class HandPile : IPile
{
    public HandPile(Card first, Card last, int count)
    {
        First = first;
        Last = last;
        Size = count;
    }
    
    public Card First { get; }
    
    public Card Last { get; }
    
    public int Size { get; }
}