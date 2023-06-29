namespace Blackjack.CountingSystems
{
    public abstract class BaseCountingSystem : ICountingSystem
    {
        protected int _runningCount;
        public int RunningCount => _runningCount;

        public CountType CountType => GetCountType(_runningCount);

        public void ResetCount()
        {
            _runningCount = 0;
        }

        public abstract PlayerAction GetRecommendation(IHand playerHand, ICard dealerFirstCard);

        protected void UpdateCount(ICard card)
        {
            _runningCount += GetCardValue(card);
        }

        protected abstract int GetCardValue(ICard card);

        private static CountType GetCountType(int runningCount)
        {
            if (runningCount >= 1)
            {
                return CountType.Positive;
            }
            else if (runningCount <= -1)
            {
                return CountType.Negative;
            }
            else
            {
                return CountType.Neutral;
            }
        }
    }
}