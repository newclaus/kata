namespace Kata.Klondike;

public class FoundationPile : IPile
{
    public FoundationPile(Suit suit)
    {
        Suit = suit;
    }


    public Card? First { get; private set; }

    public Card? Last { get; private set; }

    public int Size { get; private set; }

    public Suit Suit { get; }

    public void Put(HandPile hand)
    {
        if (hand.Size > 1)
        {
            throw new InvalidOperationException("Multiple cards cannot be put at foundation.");
        }

        if (hand.First.Suit != Suit)
        {
            throw new InvalidOperationException($"Card suit is not supported, {Suit} is expected.");
        }

        if (Last != null)
        {
            if (hand.First.Rank != Last.Rank + 1)
            {
                throw new InvalidOperationException($"Card {hand.First.Rank} cannot be put at {Last.Rank}.");
            }
        }
        else
        {
            if (hand.First.Rank != Rank.Ace)
            {
                throw new InvalidOperationException($"Card {hand.First.Rank} cannot be put at empty pile, {Rank.Ace} is expected.");
            }
        }
        
        if (Size == 0)
        {
            First = hand.First;
        }
        else
        {
            hand.First.PutAt(Last);
        }
        
        Last = hand.Last;
        
        Size++;
    }
}