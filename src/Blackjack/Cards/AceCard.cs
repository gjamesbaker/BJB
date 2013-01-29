namespace Blackjack.Cards
{
    public class AceCard : Card, IAceCard
    {
        protected internal AceCard(Suit suit) : base(Rank.Ace, suit)
        {}

        public override int Value
        {
            get { return 11; }
        }

    }

    public interface IAceCard : IBlackjackCard
    {
    }
}