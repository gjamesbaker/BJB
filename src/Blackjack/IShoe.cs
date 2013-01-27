namespace Blackjack
{
    public interface IShoe
    {
        int CardCount();
        ICard Deal();
    }
}