using System.Collections.Generic;
using Blackjack.Cards;
using Blackjack.Exceptions;
using NSubstitute;
using NUnit.Framework;
using Should.Fluent;

namespace Blackjack.UnitTests
{
    [TestFixture]
    public class ShoeTests
    {
        [Test]
        public void shoe_is_initialized_with_correct_number_of_cards([Values(1,2,6)] int decks)
        {
            // Act
            IShoe shoe = new Shoe(decks);

            // Assert
            shoe.CardCount().Should().Equal(decks * 52);
        }

        [Test]
        public void unshuffled_deck_returns_expected_order()
        {
            // Arrange
            var stackedDeck = GetStackedDeck();
            
            var deck = Substitute.For<IDeck>();
            deck.GetCards().Returns(stackedDeck);

            IShoe shoe = new Shoe(deck);
            shoe.CardCount().Should().Equal(3);

            // Act

            // Assert
            for (var i = 0; i <= 2; i++)
            {
                var card = shoe.Deal();
                card.Should().Be.SameAs(stackedDeck[i]);  // 0, 1, 2
                shoe.CardCount().Should().Equal(2-i);     // 2, 1, 0
            }
        }

        [Test]
        public void shuffled_deck_returns_different_order()
        {
            // Arrange

            // Return "random" numbers 2 then 1 then 0
            // to reverse the card order
            var rng = Substitute.For<IRandom>();
            rng.Next(100).ReturnsForAnyArgs(2, 1, 0);

            var stackedDeck = GetStackedDeck();

            var deck = Substitute.For<IDeck>();
            deck.GetCards().Returns(stackedDeck);

            IShoe shoe = new Shoe(deck);
            shoe.CardCount().Should().Equal(3);

            // Act
            shoe.Shuffle(rng);

            // Assert
            for (var i = 2; i >= 0; i--)
            {
                shoe.Deal().Should().Be.SameAs(stackedDeck[i]);
                shoe.CardCount().Should().Equal(i);
            }
        }

        [Test]
        [ExpectedException(typeof(ShoeOutOfCardsException))]
        public void shoe_throws_exception_if_deal_when_empty()
        {
            // Arrange
            var card = Substitute.For<IBlackjackCard>();
            IEnumerable<ICard> cards = new List<IBlackjackCard>() { card };

            var deck = Substitute.For<IDeck>();
            deck.GetCards().Returns(cards);

            IShoe shoe = new Shoe(deck);
            shoe.CardCount().Should().Equal(1);

            shoe.Deal();
            shoe.CardCount().Should().Equal(0);

            // Act
            shoe.Deal();  // Should throw a ShoeOutOfCardsException
        }

        private IList<IBlackjackCard> GetStackedDeck()
        {
            IList<IBlackjackCard> stackedDeck = new List<IBlackjackCard>();
            stackedDeck.Add(BlackjackCardFactory.Get(Rank.Queen, Suit.Hearts));
            stackedDeck.Add(BlackjackCardFactory.Get(Rank.Seven, Suit.Spades));
            stackedDeck.Add(BlackjackCardFactory.Get(Rank.Ace, Suit.Diamonds));

            return stackedDeck;
        }
    }
}
