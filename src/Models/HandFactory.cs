namespace Blackjack.Models
{
    public class HandFactory : IHandFactory
    {
        public IHand CreateHand(decimal betAmount = 0) => new Hand(this)
        {
            BetAmount = betAmount
        };
    }
}