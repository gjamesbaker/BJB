using System.Text;

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
        
        public string ToLongString()
        {
            return Rank + " of " + Suit;
        }

        public virtual int Value
        {
            get { return (int)Rank; }
        }

        public override string ToString()
        {
            return ToLongString();
        }
    }
}