using System.Collections.Generic;
using System.Linq;
using Blackjack.Bets;
using Blackjack.Cards;
using Blackjack.Exceptions;
using Blackjack.Hands;
using NSubstitute;
using NUnit.Framework;
using Should.Fluent;

namespace Blackjack.UnitTests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void place_bet_reduces_balance_by_ante()
        {
            // Arrange
            IBlackjackPlayer player = new BlackjackPlayer {Balance = 1000, Ante = 50};

            // Act
            player.PlaceBet();

            // Assert
            player.Balance.Should().Equal(950.0);
        }

        [Test]
        public void default_player_accepts_hit_below_17()
        {
            // Arrange
            IBlackjackPlayer player = new BlackjackPlayer();
            var dealerFaceUpCard = Substitute.For<IBlackjackCard>();
            var hand = Substitute.For<IPlayerHand>();
            var bet = Substitute.For<AnteBet>(0.0);

            hand.Value().Returns(16);
            hand.Bet.Returns(bet);

            // Act
            var acceptHit = player.Hit(hand, dealerFaceUpCard);

            // Assert
            acceptHit.Should().Be.True();
        }

        [Test]
        public void default_player_rejects_hit_above_16()
        {
            // Arrange
            IBlackjackPlayer player = new BlackjackPlayer();
            var dealerFaceUpCard = Substitute.For<IBlackjackCard>();
            var hand = Substitute.For<IPlayerHand>();
            hand.Value().Returns(17);

            // Act
            var acceptHit = player.Hit(hand, dealerFaceUpCard);

            // Assert
            acceptHit.Should().Be.False();
        }

        [Test]
        public void start_new_game_resets_hands_to_empty()
        {
            // Arrange
            IBlackjackPlayer player = new BlackjackPlayer();
            player.PlaceBet();
            player.Hands.Count().Should().Equal(1);

            // Act
            player.StartNewGame();

            // Assert
            player.Hands.Count().Should().Equal(0);
        }

        [Test]
        public void settle_bet_on_busted_hand_returns_lost_amount()
        {
            // Arrange
            IBlackjackPlayer player = new BlackjackPlayer();
            IBlackjackBet bet = new AnteBet(100);

            var dealerHand = Substitute.For<IDealerHand>();
            var playerHand = Substitute.For<IPlayerHand>();
            playerHand.Busted.Returns(true);
            playerHand.Bet.Returns(bet);

            // Act
            var amount = player.SettleBet(playerHand, dealerHand);

            // Assert
            amount.Should().Equal(100.0);
        }

        [Test]
        public void settle_bet_when_only_dealer_busted_returns_negative_win_amount()
        {
            // Arrange
            IBlackjackPlayer player = new BlackjackPlayer();
            IBlackjackBet bet = new AnteBet(100);

            var dealerHand = Substitute.For<IDealerHand>();
            var playerHand = Substitute.For<IPlayerHand>();
            playerHand.Busted.Returns(false);
            dealerHand.Busted.Returns(true);

            playerHand.Bet.Returns(bet);

            // Act
            var amount = player.SettleBet(playerHand, dealerHand);

            // Assert
            amount.Should().Equal(-100.0);
        }

        [Test]
        public void settle_bet_when_only_dealer_busted_credits_players_balance()
        {
            // Arrange
            IBlackjackBet bet = new AnteBet(100);
            IBlackjackPlayer player = new BlackjackPlayer {Balance = 900};

            var dealerHand = Substitute.For<IDealerHand>();
            var playerHand = Substitute.For<IPlayerHand>();
            playerHand.Busted.Returns(false);
            dealerHand.Busted.Returns(true);

            playerHand.Bet.Returns(bet);

            // Act
            player.SettleBet(playerHand, dealerHand);
            
            // Assert
            player.Balance.Should().Equal(1100.0);
        }

        [Test]
        public void settle_bet_with_equal_hands_returns_zero()
        {
            // Arrange
            IBlackjackBet bet = new AnteBet(100);
            IBlackjackPlayer player = new BlackjackPlayer { Balance = 900 };

            var dealerHand = Substitute.For<IDealerHand>();
            var playerHand = Substitute.For<IPlayerHand>();
            playerHand.Busted.Returns(false);
            dealerHand.Busted.Returns(false);
            playerHand.Value().Returns(20);
            dealerHand.Value().Returns(20);

            playerHand.Bet.Returns(bet);

            // Act
            var amount = player.SettleBet(playerHand, dealerHand);

            // Assert
            amount.Should().Equal(0.0);
        }

        [Test]
        public void settle_bet_with_equal_hands_changes_bet_to_push_bet()
        {
            // Arrange
            IBlackjackBet bet = new AnteBet(100);
            IBlackjackPlayer player = new BlackjackPlayer { Balance = 900 };

            var dealerHand = Substitute.For<IDealerHand>();
            var playerHand = Substitute.For<IPlayerHand>();
            playerHand.Busted.Returns(false);
            dealerHand.Busted.Returns(false);
            playerHand.Value().Returns(20);
            dealerHand.Value().Returns(20);

            playerHand.Bet.Returns(bet);

            // Act
            player.SettleBet(playerHand, dealerHand);

            // Assert
            playerHand.Bet.Should().Be.OfType<PushBet>();
        }

        [Test]
        public void settle_bet_with_equal_hands_credits_players_balance_with_bet_amount()
        {
            // Arrange
            IBlackjackBet bet = new AnteBet(100);
            IBlackjackPlayer player = new BlackjackPlayer { Balance = 900 };

            var dealerHand = Substitute.For<IDealerHand>();
            var playerHand = Substitute.For<IPlayerHand>();
            playerHand.Busted.Returns(false);
            dealerHand.Busted.Returns(false);
            playerHand.Value().Returns(20);
            dealerHand.Value().Returns(20);

            playerHand.Bet.Returns(bet);

            // Act
            player.SettleBet(playerHand, dealerHand);

            // Assert
            player.Balance.Should().Equal(1000.0);
        }

        [Test]
        public void settle_bet_on_losing_hand_returns_lost_amount()
        {
            // Arrange
            IBlackjackBet bet = new AnteBet(100);
            IBlackjackPlayer player = new BlackjackPlayer { Balance = 900 };

            var dealerHand = Substitute.For<IDealerHand>();
            var playerHand = Substitute.For<IPlayerHand>();
            playerHand.Busted.Returns(false);
            dealerHand.Busted.Returns(false);
            playerHand.Value().Returns(18);
            dealerHand.Value().Returns(20);

            playerHand.Bet.Returns(bet);

            // Act
            var amount = player.SettleBet(playerHand, dealerHand);

            // Assert
            amount.Should().Equal(100.0);
        }

        [Test]
        public void settle_bet_on_winning_hand_returns_credits_player_balance()
        {
            // Arrange
            IBlackjackBet bet = new AnteBet(100);
            IBlackjackPlayer player = new BlackjackPlayer { Balance = 900 };

            var dealerHand = Substitute.For<IDealerHand>();
            var playerHand = Substitute.For<IPlayerHand>();
            playerHand.Busted.Returns(false);
            dealerHand.Busted.Returns(false);
            playerHand.Value().Returns(20);
            dealerHand.Value().Returns(18);

            playerHand.Bet.Returns(bet);

            // Act
            player.SettleBet(playerHand, dealerHand);

            // Assert
            player.Balance.Should().Equal(1100.0);
        }

        [Test]
        public void settle_bet_on_winning_hand_returns_negative_win_amount()
        {
            // Arrange
            IBlackjackBet bet = new AnteBet(100);
            IBlackjackPlayer player = new BlackjackPlayer { Balance = 900 };

            var dealerHand = Substitute.For<IDealerHand>();
            var playerHand = Substitute.For<IPlayerHand>();
            playerHand.Busted.Returns(false);
            dealerHand.Busted.Returns(false);
            playerHand.Value().Returns(20);
            dealerHand.Value().Returns(18);

            playerHand.Bet.Returns(bet);

            // Act
            var amount = player.SettleBet(playerHand, dealerHand);

            // Assert
            amount.Should().Equal(-100.0);
        }

        [Test]
        public void offer_to_split_on_eligible_hand_results_in_two_hands()
        {
            // Arrange
            var player = new BlackjackPlayer();
            var dealerFaceUpCard = Substitute.For<IBlackjackCard>();

            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();
            var cards = new List<IBlackjackCard> {card1, card2};

            var hand1 = Substitute.For<IPlayerHand>();
            hand1.EligibleForSplit.Returns(true);
            hand1.GetCards().Returns(cards);

            var playerHands = (List<IPlayerHand>) player.Hands;

            playerHands.Add(hand1);

            // Act
            player.OfferSplit(dealerFaceUpCard);

            // Assert
            var actualHands = player.Hands.ToList();

            actualHands.Should().Not.Be.Null();
            actualHands.Count.Should().Equal(2);

            hand1.ReceivedWithAnyArgs().SplitInto(hand1);
        }

        [Test]
        public void offer_to_split_on_ineligible_hand_returns_false()
        {
            // Arrange
            var player = new BlackjackPlayer();
            var dealerFaceUpCard = Substitute.For<IBlackjackCard>();

            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();
            var cards = new List<IBlackjackCard> { card1, card2 };

            var hand1 = Substitute.For<IPlayerHand>();
            hand1.EligibleForSplit.Returns(false);

            var playerHands = (List<IPlayerHand>)player.Hands;
            playerHands.Add(hand1);

            // Act
            var response = player.OfferSplit(dealerFaceUpCard);

            // Assert
            response.Should().Be.False();
        }

        [Test]
        public void offer_to_double_down_on_ineligible_hand_returns_false()
        {
            // Arrange
            var player = new BlackjackPlayer();
            var dealerFaceUpCard = Substitute.For<IBlackjackCard>();

            var hand1 = Substitute.For<IPlayerHand>();
            hand1.EligibleForDoubleDown.Returns(false);

            var playerHands = (List<IPlayerHand>)player.Hands;
            playerHands.Add(hand1);

            // Act
            var response = player.OfferDoubleDown(hand1, dealerFaceUpCard);

            // Assert
            response.Should().Be.False();
        }

        [Test]
        public void offer_to_double_down_on_eligible_hand_returns_true()
        {
            // Arrange
            var player = new BlackjackPlayer();
            var dealerFaceUpCard = Substitute.For<IBlackjackCard>();

            var hand1 = Substitute.For<IPlayerHand>();
            hand1.EligibleForDoubleDown.Returns(true);

            // Act
            var response = player.OfferDoubleDown(hand1, dealerFaceUpCard);

            // Assert
            response.Should().Be.True();
        }

        [Test]
        public void offer_to_double_down_on_eligible_hand_decreases_players_balance()
        {
            // Arrange
            var player = new BlackjackPlayer {Balance = 100.0};
            var bet = new AnteBet(20.0);

            var dealerFaceUpCard = Substitute.For<IBlackjackCard>();

            var hand1 = Substitute.For<IPlayerHand>();
            hand1.EligibleForDoubleDown.Returns(true);
            hand1.Bet = bet;

            // Act
            player.OfferDoubleDown(hand1, dealerFaceUpCard);

            // Assert
            player.Balance.Should().Equal(80.0);
        }

        [Test]
        public void offer_to_double_down_on_eligible_hand_doubles_amount_of_bet()
        {
            // Arrange
            var player = new BlackjackPlayer { Balance = 100.0 };
            var bet = new AnteBet(20.0);

            var dealerFaceUpCard = Substitute.For<IBlackjackCard>();

            var hand1 = Substitute.For<IPlayerHand>();
            hand1.EligibleForDoubleDown.Returns(true);
            hand1.Bet = bet;

            // Act
            player.OfferDoubleDown(hand1, dealerFaceUpCard);

            // Assert
            hand1.Bet.Amount.Should().Equal(40.0);
            hand1.Bet.Should().Be.OfType<DoubleDownBet>();
        }

        [Test, ExpectedException(typeof(MissingBetException))]
        public void calling_get_initial_hand_before_placing_ante_throws_exception()
        {
            // Arrange
            var player = new BlackjackPlayer();

            // Act
            player.GetInitialHand();
        }

        [Test]
        public void calling_get_initial_hand_returns_first_hand()
        {
            // Arrange
            var player = new BlackjackPlayer();
            player.PlaceBet();
            player.PlaceBet();

            var hands = (List<IPlayerHand>)player.Hands;
            hands.Count.Should().Equal(2);
            var hand1 = hands[0];

            // Act
            var hand = player.GetInitialHand();

            // Assert
            hand.Should().Not.Be.Null();
            hand.Should().Be.SameAs(hand1);
        }

    }
}
