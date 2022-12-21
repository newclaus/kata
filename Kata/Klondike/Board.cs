using System.Collections.ObjectModel;

namespace Kata.Klondike;

public class Board
{
    private Board()
    {
        //
    }

    public IReadOnlyList<TableauPile> Tableau { get; init; } = null!;

    public StockPile StockPile { get; init; } = null!;
    
    public IReadOnlyDictionary<Suit, FoundationPile> FoundationPiles { get; init; } = null!;

    public static Board CreateNew()
    {
        using var deck = Deck.Create();

        var foundationPiles = Enum.GetValues<Suit>()
            .ToDictionary(suit => suit, suit => new FoundationPile(suit));
        
        return new Board
        {
            Tableau = CreateTableau(deck),
            StockPile = CreateStock(deck),
            FoundationPiles = new ReadOnlyDictionary<Suit, FoundationPile>(foundationPiles)
        };
    }

    private static TableauPile[] CreateTableau(Deck deck)
    {
        var tableau = new TableauPile[7];

        for (int i = 0; i < 7; i++)
        {
            var tableauPile = TableauPile.Create(deck, i + 1);
            
            tableau[i] = tableauPile;
        }

        return tableau;
    }

    private static StockPile CreateStock(Deck deck)
    {
        return StockPile.Create(deck);
    }
}