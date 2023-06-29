namespace Blackjack.Services
{
    public class CardCountingService : ICardCountingService
    {
        private readonly ICountingSystem _countingSystem;

        public int RunningCount => _countingSystem.RunningCount;

        public CountType CountType => _countingSystem.CountType;

        public CardCountingService(ICountingSystemFactory countingSystemFactory, GameConfiguration gameConfiguration)
        {
            CountingSystem configuredCountingSystem = gameConfiguration.CountingSystem;
            _countingSystem = countingSystemFactory.CreateCountingSystem(configuredCountingSystem);
        }

        public void ResetCount()
        {
            _countingSystem.ResetCount();
        }

        public Recommendation GetRecommendation(IHand playerHand, ICard dealerFirstCard)
        {
            if (playerHand.IsBlackjack)
            {
                return new Recommendation(_countingSystem.RunningCount, _countingSystem.CountType, null);
            }

            if (playerHand.IsBust)
            {
                return new Recommendation(_countingSystem.RunningCount, _countingSystem.CountType, null);
            }

            PlayerAction action = _countingSystem.GetRecommendation(playerHand, dealerFirstCard);
            return new Recommendation(_countingSystem.RunningCount, _countingSystem.CountType, action);
        }
    }
}
