using System.Reflection;
using Kata.Klondike;
using Xunit.Sdk;

namespace Kata.Tests.Klondike;

public class FoundationPileTest
{
    private const Suit PileSuit = Suit.Hearts;
    
    private readonly FoundationPile _foundationPile;

    public FoundationPileTest()
    {
        _foundationPile = new FoundationPile(PileSuit);
    }
    
    [Fact]
    public void TestPut_AceAtEmptyPile()
    {
        PutCard(Rank.Ace);
    }

    [Theory]
    [InlineData(Rank.Ace, 1)]
    [InlineData(Rank.Five, 5)]
    [InlineData(Rank.Jack, 11)]
    [InlineData(Rank.King, 13)]
    public void TestPut_SizeIncreased(Rank upToRank, int expectedSize)
    {
        PutCards(upToRank);

        Assert.Equal(expectedSize, _foundationPile.Size);
    }

    [Theory]
    [AllRanks]
    public void TestPut_FirstCard(Rank upToRank)
    {
        PutCards(upToRank);
        
        Assert.Equal(Rank.Ace, _foundationPile.First!.Rank);
    }

    [Theory]
    [AllRanks]
    public void TestPut_LastCard(Rank upToRank)
    {
        PutCards(upToRank);
        
        Assert.Equal(upToRank, _foundationPile.Last!.Rank);
    }

    [Theory]
    [AllRanks]
    public void TestPut_ValidChain(Rank upToRank)
    {
        PutCards(upToRank);
        
        var expectedResult = _foundationPile.Last;
        
        var lastInChain = _foundationPile.First!;

        while (lastInChain.Next != null)
        {
            lastInChain = lastInChain.Next;
        }
        
        Assert.Equal(expectedResult, lastInChain);
    }

    [Fact]
    public void TestPut_MultipleCardsInHand_Exception()
    {
        var kingSpades = new Card(Rank.King, Suit.Spades);
        var queenHearts = new Card(Rank.Queen, Suit.Hearts);
        
        queenHearts.PutAt(kingSpades);

        var hand = new HandPile(queenHearts, kingSpades, 2);

        var exception = Assert.Throws<InvalidOperationException>(() => _foundationPile.Put(hand));
        
        Assert.Equal("Multiple cards cannot be put at foundation.", exception.Message);
    }

    [Theory]
    [InlineData(null, Rank.Two, "Card Two cannot be put at empty pile, Ace is expected.")]
    [InlineData(Rank.Ace, Rank.Three, "Card Three cannot be put at Ace.")]
    [InlineData(Rank.Five, Rank.Four, "Card Four cannot be put at Five.")]
    [InlineData(Rank.Jack, Rank.King, "Card King cannot be put at Jack.")]
    public void TestPut_InvalidRank_Exception(Rank? upToRank, Rank rank, string expectedMessage)
    {
        PutCards(upToRank);

        var exception = Assert.Throws<InvalidOperationException>(() => PutCard(rank));
        
        Assert.Equal(expectedMessage, exception.Message);
    }
    
    [Theory]
    [InlineData(Suit.Spades)]
    [InlineData(Suit.Clubs)]
    [InlineData(Suit.Diamonds)]
    public void TestPut_InvalidSuit_Exception(Suit suit)
    {
        var card = new Card(Rank.Ace, suit);

        var hand = new HandPile(card, card, 1);
        
        var exception = Assert.Throws<InvalidOperationException>(() => _foundationPile.Put(hand));
        
        Assert.Equal("Card suit is not supported, Hearts is expected.", exception.Message);
    }

    private void PutCards(Rank? upToRank)
    {
        if (upToRank == null)
        {
            return;
        }
        
        for (Rank rank = Rank.Ace; rank <= upToRank; rank++)
        {
            PutCard(rank);
        }
    }

    private void PutCard(Rank rank)
    {
        var hand = CreateHand(rank);

        _foundationPile.Put(hand);
    }

    private HandPile CreateHand(Rank rank)
    {
        var card = new Card(rank, PileSuit);

        return new HandPile(card, card, 1);
    }

    private class AllRanksAttribute : DataAttribute
    {
        private static readonly IEnumerable<object[]> Sets;

        static AllRanksAttribute()
        {
            Sets = Enum.GetValues<Rank>().Select(CreateSet).ToArray();
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return Sets;
        }

        private static object[] CreateSet(Rank rank)
        {
            return new object[] { rank };
        }
    }
}