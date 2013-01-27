namespace Blackjack
{
    public interface IShoe
    {
        int CardCount();
        ICard Deal();
        void Shuffle(IRandom random);
    }
}