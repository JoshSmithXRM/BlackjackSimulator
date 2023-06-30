namespace Blackjack.Models
{
    public interface IHandFactory
    {
        IHand CreateHand(decimal betAmount = 0);
    }
}