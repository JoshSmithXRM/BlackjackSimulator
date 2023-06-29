namespace Blackjack.Services
{
    public interface ICardCountingService
    {
        int RunningCount { get; }
        CountType CountType { get; }
        void ResetCount();
        Recommendation GetRecommendation(IHand playerHand, ICard dealerFirstCard);

    }
}