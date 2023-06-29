namespace Blackjack.Models
{
    public class HandResult
    {
        public HandOutcome Outcome { get; }
        public IReadOnlyList<ICard> PlayerCards { get; }
        public IReadOnlyList<ICard> DealerCards { get; }
        public decimal BetAmount { get; }
        public decimal NetAmountWonLost { get; }
        public decimal TotalAmount => BetAmount + NetAmountWonLost;
        public int RunningCount { get; }
        public CountType CountType { get; }
        public List<PlayerAction> PlayerActions { get; }

        public HandResult(HandOutcome outcome, IReadOnlyList<ICard> playerCards, IReadOnlyList<ICard> dealerCards, decimal betAmount, decimal netAmountWonLost, int runningCount, CountType countType, List<PlayerAction> playerActions)
        {
            Outcome = outcome;
            PlayerCards = playerCards;
            DealerCards = dealerCards;
            BetAmount = betAmount;
            NetAmountWonLost = netAmountWonLost;
            RunningCount = runningCount;
            CountType = countType;
            PlayerActions = playerActions;
        }
    }
}
