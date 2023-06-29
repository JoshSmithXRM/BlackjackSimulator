namespace Blackjack.Models
{
    public class HandResult
    {
        public HandOutcome Outcome { get; }
        public IHand PlayerHand { get; }
        public IHand DealerHand { get; }
        public decimal BetAmount { get; }
        public decimal NetAmountWonLost { get; }
        public decimal TotalAmount => BetAmount + NetAmountWonLost;
        public int RunningCount { get; }
        public CountType CountType { get; }
        public List<PlayerAction> PlayerActions { get; }

        public HandResult(HandOutcome outcome, IHand playerHand, IHand dealerHand, decimal betAmount, decimal netAmountWonLost)
        {
            Outcome = outcome;
            PlayerHand = playerHand;
            DealerHand = dealerHand;
            BetAmount = betAmount;
            NetAmountWonLost = netAmountWonLost;
            PlayerActions = playerHand.ActionsTaken;
        }
    }
}
