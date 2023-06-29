namespace Blackjack.Services
{
    public class SimpleHandEvaluator : IHandEvaluator
    {
        public HandResult EvaluateHand(Hand playerHand, Hand dealerHand, int runningCount, CountType countType, List<PlayerAction> playerActionsTaken)
        {
            bool playerWins = !playerHand.IsBust && (dealerHand.IsBust || playerHand.GetTotal() > dealerHand.GetTotal());
            bool tie = !playerHand.IsBust && playerHand.GetTotal() == dealerHand.GetTotal();

            HandOutcome outcome;
            decimal netAmountWonLost;

            if (playerHand.IsBust)
            {
                outcome = HandOutcome.Loss;
                netAmountWonLost = -playerHand.BetAmount;
            }
            else if (playerWins)
            {
                outcome = HandOutcome.Win;
                netAmountWonLost = playerHand.BetAmount;
            }
            else if (tie)
            {
                outcome = HandOutcome.Push;
                netAmountWonLost = 0;
            }
            else
            {
                outcome = HandOutcome.Loss;
                netAmountWonLost = -playerHand.BetAmount;
            }

            return new HandResult(outcome, playerHand.Cards, dealerHand.Cards, playerHand.BetAmount, netAmountWonLost, runningCount, countType, playerActionsTaken);
        }
    }
}
