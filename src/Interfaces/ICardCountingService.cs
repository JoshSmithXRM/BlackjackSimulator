namespace Blackjack.Interfaces
{
    public interface ICardCountingService
    {
        public CountType GetCountType(int runningCount);
        public string GetRecommendation(int runningCount, Hand playerHand, ICard dealerFirstCard);
    }
}