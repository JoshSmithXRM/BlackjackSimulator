namespace Blackjack.CountingSystems
{
    public interface ICountingSystem
    {
        int RunningCount { get; }
        CountType CountType { get; }
        void ResetCount();
        PlayerAction GetRecommendation(IHand playerHand, ICard dealerFirstCard);
        abstract void UpdateCount(ICard card);
    }
}