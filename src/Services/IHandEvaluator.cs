namespace Blackjack.Services
{
    public interface IHandEvaluator
    {
        HandResult EvaluateHand(IHand playerHand, IHand dealerHand);
    }
}