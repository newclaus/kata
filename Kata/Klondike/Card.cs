namespace Kata.Klondike;

public class Card
{
    public Card(Rank rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public Rank Rank { get; }

    public Suit Suit { get; }

    public Card? Previous { get; private set; }

    public Card? Next { get; private set; }

    public void PutAt(Card card)
    {
        if (Previous != null)
        {
            throw new InvalidOperationException("The card has already put on another card");
        }

        if (card.Next != null)
        {
            throw new ArgumentOutOfRangeException(nameof(card), card, "The specified card has another card put on.");
        }

        card.Next = this;
        Previous = card;
    }

    public void Take()
    {
        if (Previous == null)
        {
            return;
        }

        Previous.Next = null;
        Previous = null;
    }

    public static IEnumerable<Card> CreateAllCards()
    {
        foreach (var suit in Enum.GetValues<Suit>())
        foreach (var rank in Enum.GetValues<Rank>())
        {
            yield return new Card(rank, suit);
        }
    }

    public override string ToString()
    {
        return $"{Rank:G} {Suit:G}";
    }
}

public enum Rank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 1
}

public enum Suit
{
    Hearts,
    Clubs,
    Diamonds,
    Spades
}