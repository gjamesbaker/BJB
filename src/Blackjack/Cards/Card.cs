namespace Blackjack.Cards
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
        
        public virtual int Value
        {
            get { return (int)Rank; }
        }

        public override string ToString()
        {
            return Rank + " of " + Suit;
        }

    }
}