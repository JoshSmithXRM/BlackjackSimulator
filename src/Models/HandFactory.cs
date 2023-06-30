namespace Blackjack.Models
{
    public class HandFactory : IHandFactory
    {
        public IHand CreateHand(decimal betAmount = 0)
        {
            return new Hand(this)
            {
                BetAmount = betAmount
            };
        }
    }
}