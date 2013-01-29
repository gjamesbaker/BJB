namespace Blackjack.Cards
{
    public interface IShoe
    {
        int CardCount();
        IBlackjackCard Deal();
        void Shuffle();
        void Shuffle(IRandom random);
    }

}