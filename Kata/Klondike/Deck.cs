namespace Kata.Klondike;

public class Deck : IDisposable
{
    private readonly IEnumerator<Card> _cardsEnumerator;

    private Deck()
        : this(Card.CreateAllCards())
    {
        //
    }

    private Deck(IEnumerable<Card> cards)
    {
        _cardsEnumerator = cards.GetEnumerator();
    }

    public Card[] Take(int count)
    {
        var result = new Card[count];
        Card? previous = null;

        for (int i = 0; i < count; i++)
        {
            _cardsEnumerator.MoveNext();

            var current = _cardsEnumerator.Current;

            if (previous != null)
            {
                current.PutAt(previous);
            }

            result[i] = current;
            previous = current;
        }

        return result;
    }
    
    public static Deck Create()
    {
        return new Deck();
    }

    public static Deck Create(IEnumerable<Card> cards)
    {
        return new Deck(cards);
    }

    public void Dispose()
    {
        _cardsEnumerator.Dispose();
    }
}