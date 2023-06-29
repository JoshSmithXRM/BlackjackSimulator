namespace Blackjack.Services
{
    public interface IHandEvaluator
    {
        HandResult EvaluateHand(Hand playerHand, Hand dealerHand, int runningCount, CountType countType, List<PlayerAction> playerActionsTaken);
    }
}