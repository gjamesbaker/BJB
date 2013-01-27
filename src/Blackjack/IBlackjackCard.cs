namespace Blackjack
{
    public interface IBlackjackCard : ICard
    {
        int SoftValue { get; }
        int HardValue { get; }
    }
}