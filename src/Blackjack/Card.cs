namespace Blackjack
{
    public class Card : IBlackjackCard
    {
        protected internal Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }

        public virtual int SoftValue
        {
            get { return (int)Rank; }
        }

        public virtual int HardValue
        {
            get { return (int)Rank; }
        }
    }
}