namespace Blackjack.CountingSystems
{
    public abstract class BaseCountingSystem : ICountingSystem
    {
        protected int _runningCount;
        public int RunningCount => _runningCount;

        public CountType CountType => GetCountType(_runningCount);

        public void ResetCount() => _runningCount = 0;

        public abstract PlayerAction GetRecommendation(IHand playerHand, ICard dealerFirstCard);

        public void UpdateCount(ICard card) => _runningCount += GetCardValue(card);

        protected abstract int GetCardValue(ICard card);

        private static CountType GetCountType(int runningCount) => runningCount >= 1 ? CountType.Positive : runningCount <= -1 ? CountType.Negative : CountType.Neutral;
    }
}