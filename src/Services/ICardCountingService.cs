namespace Blackjack.Services
{
    public interface ICardCountingService
    {
        int RunningCount { get; }
        CountType CountType { get; }
        void ResetCount();
        void UpdateCount(ICard card);
        Recommendation GetRecommendation(IHand playerHand, ICard dealerFirstCard);

    }
}