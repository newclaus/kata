using System.Reflection;
using Kata.Klondike;
using Xunit.Sdk;

namespace Kata.Tests.Klondike;

public class TableauPileTest
{
    private const int DefaultSize = 13;
    private readonly TableauPile _validSequencePile;
    private readonly Card[] _validSequencePileCards;
    private readonly TableauPile _invalidSequencePile;

    public TableauPileTest()
    {
        var cards = Card.CreateAllCards().ToArray();

        Array.Reverse(cards);

        int cardToSelect = 14;
        for (int i = 1; i < 13; i++)
        {
            (cards[i], cards[cardToSelect]) = (cards[cardToSelect], cards[i]);
            cardToSelect += 14;

            if (cardToSelect >= 26)
            {
                cardToSelect -= 26;
            }
        }

        using var deck = Deck.Create(cards);

        _validSequencePile = TableauPile.Create(deck, DefaultSize);
        _validSequencePileCards = new Card[DefaultSize];
        Array.Copy(cards, _validSequencePileCards, DefaultSize);

        deck.Take(13);

        _invalidSequencePile = TableauPile.Create(deck, DefaultSize);
    }

    [Theory]
    [NCards]
    public void TestTake_TakeN_NCardsInHand(int takeCount)
    {
        var hand = _validSequencePile.Take(takeCount);

        Assert.Equal(takeCount, hand.Size);
    }

    [Theory]
    [NCardsAndTheRest]
    public void TestTake_TakeN_SizeReducedByN(int takeCount, int expectedSize)
    {
        _validSequencePile.Take(takeCount);

        Assert.Equal(expectedSize, _validSequencePile.Size);
    }

    [Theory]
    [NCardsAndTheRest]
    public void TestTake_TakeN_LastChanged(int takeCount, int expectedCard)
    {
        var expectedResult = expectedCard > 0 ? _validSequencePileCards[expectedCard - 1] : null;

        _validSequencePile.Take(takeCount);

        Assert.Equal(expectedResult, _validSequencePile.Last);
    }

    [Theory]
    [NCards]
    public void TestTake_TakeN_FirstInHand(int takeCount)
    {
        var expectedResult = _validSequencePileCards[^takeCount];

        var hand = _validSequencePile.Take(takeCount);

        Assert.Equal(expectedResult, hand.First);
    }

    [Theory]
    [NCards]
    public void TestTake_TakeN_HandSize(int takeCount)
    {
        var hand = _validSequencePile.Take(takeCount);

        Assert.Equal(takeCount, hand.Size);
    }

    [Theory]
    [NCards]
    public void TestTake_TakeN_LastInHand(int takeCount)
    {
        var expectedResult = _validSequencePileCards[^1];

        var hand = _validSequencePile.Take(takeCount);

        Assert.Equal(expectedResult, hand.Last);
    }

    [Fact]
    public void TestTake_TakeAll_NoFirst()
    {
        _validSequencePile.Take(_validSequencePile.Size);

        Assert.Null(_validSequencePile.First);
    }

    [Fact]
    public void TestTake_TakeAll_NoLast()
    {
        _validSequencePile.Take(_validSequencePile.Size);

        Assert.Null(_validSequencePile.Last);
    }

    [Fact]
    public void TestGetMaxCountToTake_ValidSequence_All()
    {
        int expectedResult = _validSequencePile.Size;

        var result = _validSequencePile.GetMaxCountToTake();

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [NCardsAndTheRest]
    public void TestGetMaxCountToTake_TakeNFirst_All(int takeCount, int expectedResult)
    {
        _validSequencePile.Take(takeCount);

        var result = _validSequencePile.GetMaxCountToTake();

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void TestGetMaxCountToTake_InvalidSequence_One()
    {
        const int expectedResult = 1;

        var result = _invalidSequencePile.GetMaxCountToTake();

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void TestGetMaxCountToTake_TakeOneFromInvalidSequence_One()
    {
        const int expectedResult = 1;

        var result = _invalidSequencePile.GetMaxCountToTake();

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [NCards]
    public void TestPut_TakeN_SizeIncreased(int takeCount)
    {
        var expectedResult = _validSequencePile.Size;
        TakeAndPut(takeCount);

        Assert.Equal(expectedResult, _validSequencePile.Size);
    }

    [Theory]
    [NCards]
    public void TestPut_TakeAndPutN_FirstNotChanged(int takeCount)
    {
        var expectedResult = _validSequencePile.First;
        
        TakeAndPut(takeCount);
        
        Assert.Equal(expectedResult, _validSequencePile.First);
    }

    [Theory]
    [NCards]
    public void TestPut_TakeAndPutN_LastChanged(int takeCount)
    {
        var expectedResult = _validSequencePile.Last;
        
        TakeAndPut(takeCount);
        
        Assert.Equal(expectedResult, _validSequencePile.Last);
    }

    [Theory]
    [NCards]
    public void TestPut_TakeAndPutN_ValidChain(int takeCount)
    {
        var expectedResult = _validSequencePile.Last;
        
        TakeAndPut(takeCount);

        var lastInChain = _validSequencePile.First!;

        while (lastInChain.Next != null)
        {
            lastInChain = lastInChain.Next;
        }
        
        Assert.Equal(expectedResult, lastInChain);
    }

    [Fact]
    public void TestFirstVisible()
    {
        Assert.Equal(_validSequencePile.Last, _validSequencePile.FirstVisible);
    }
    
    /*[Fact]
    public void TestFirstVisible_TakeAndPut_FirstVisibleIsPenultimateInPile()
    {
        TakeAndPut(1);
        
        Assert.Equal(_validSequencePile.Last.Previous, _validSequencePile.FirstVisible);
    }*/
    /*

    [Fact]
    [NCards]
    public void TestFirstVisible_TakeNAndPut_NPlusOneVisible(int takeCount)
    {
        for (int i = 1; i <= takeCount; i++)
        {
            TakeAndPut(i);
        }
        
        Assert.Equal(_validSequencePile.Last, _validSequencePile.FirstVisible);
    }*/

    /*[Fact]
    [NCards]
    public void TestFirstVisible_()
    {
        Assert.Equal(_validSequencePile.Last, _validSequencePile.FirstVisible);
    }*/

    private void TakeAndPut(int takeCount)
    {
        var hand = _validSequencePile.Take(takeCount);

        _validSequencePile.Put(hand);
    }

    private class NCardsAttribute : DataAttribute
    {
        private static readonly IEnumerable<object[]> Data;

        static NCardsAttribute()
        {
            Data = CreateSets().ToArray();
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return Data;
        }

        private static IEnumerable<object[]> CreateSets()
        {
            yield return CreateSet(1);
            yield return CreateSet(2);
            yield return CreateSet(3);
            yield return CreateSet(7);
            yield return CreateSet(12);
            yield return CreateSet(13);
        }

        private static object[] CreateSet(int take)
        {
            return new object[] { take };
        }
    }

    private class NCardsAndTheRest : DataAttribute
    {
        private static readonly IEnumerable<object[]> Data;

        static NCardsAndTheRest()
        {
            Data = CreateSets().ToArray();
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return Data;
        }

        private static IEnumerable<object[]> CreateSets()
        {
            yield return CreateSet(1);
            yield return CreateSet(2);
            yield return CreateSet(3);
            yield return CreateSet(7);
            yield return CreateSet(12);
            yield return CreateSet(13);
        }

        private static object[] CreateSet(int take)
        {
            return new object[] { take, DefaultSize - take };
        }
    }
    
    [Theory]
    [CardPairs]
    public void TestCanPutAt(bool expectedResult, Card topCard, Card bottomCard)
    {
        bool result = TableauPile.CanPutAt(topCard, bottomCard);

        Assert.Equal(expectedResult, result);
    }
    
    private class CardPairsAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return CreateFailureSet((Rank.King, Suit.Clubs), (Rank.King, Suit.Spades));
            yield return CreateFailureSet((Rank.Jack, Suit.Clubs), (Rank.King, Suit.Spades));
            
            yield return CreateFailureSet((Rank.Queen, Suit.Spades), (Rank.King, Suit.Spades));
            yield return CreateFailureSet((Rank.Jack, Suit.Clubs), (Rank.Queen, Suit.Spades));
            yield return CreateFailureSet((Rank.Ten, Suit.Spades), (Rank.Jack, Suit.Clubs));
            yield return CreateFailureSet((Rank.Nine, Suit.Clubs), (Rank.Ten, Suit.Clubs));
            
            yield return CreateFailureSet((Rank.King, Suit.Diamonds), (Rank.Ace, Suit.Spades));
            
            yield return CreateSuccessSet((Rank.Queen, Suit.Diamonds), (Rank.King, Suit.Spades));
            yield return CreateSuccessSet((Rank.Jack, Suit.Hearts), (Rank.Queen, Suit.Spades));
            yield return CreateSuccessSet((Rank.Ten, Suit.Diamonds), (Rank.Jack, Suit.Clubs));
            yield return CreateSuccessSet((Rank.Nine, Suit.Hearts), (Rank.Ten, Suit.Clubs));
            
            yield return CreateSuccessSet((Rank.Ace, Suit.Hearts), (Rank.Two, Suit.Clubs));
        }

        private static object[] CreateFailureSet((Rank Rank, Suit uit) topCard, (Rank Rank, Suit Suit) bottomCard)
        {
            return CreateSet(topCard, bottomCard, false);
        }

        private static object[] CreateSuccessSet((Rank Rank, Suit uit) topCard, (Rank Rank, Suit Suit) bottomCard)
        {
            return CreateSet(topCard, bottomCard, true);
        }

        private static object[] CreateSet((Rank Rank, Suit uit) topCard, (Rank Rank, Suit Suit) bottomCard, bool coupled)
        {
            return new object[] { coupled, CreateCard(topCard), CreateCard(bottomCard) };
        }

        private static Card CreateCard((Rank Rank, Suit Suit) card)
        {
            return new Card(card.Rank, card.Suit);
        }
    }
}