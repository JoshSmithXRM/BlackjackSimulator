namespace Blackjack.Services
{
    public class HandEvaluator : IHandEvaluator
    {
        public HandResult EvaluateHand(IHand playerHand, IHand dealerHand)
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
            else if (playerHand.ActionsTaken.Any() && playerHand.ActionsTaken.Last() == PlayerAction.Surrender)
            {
                outcome = HandOutcome.Surrender;
                netAmountWonLost = -playerHand.BetAmount / 2;
            }
            else if (playerHand.IsBlackjack)
            {
                outcome = HandOutcome.Blackjack;
                netAmountWonLost = playerHand.BetAmount * 1.5m;
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

            return new HandResult(outcome, playerHand, dealerHand, playerHand.BetAmount, netAmountWonLost);
        }
    }
}
