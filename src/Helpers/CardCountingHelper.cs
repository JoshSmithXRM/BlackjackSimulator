namespace Blackjack.Helpers
{
    public static class CardCountingHelper
    {
        public static CountType GetCountType(int runningCount)
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

        public static string GetRecommendation(int runningCount, Hand playerHand, ICard dealerFirstCard)
        {
            CountType countType = GetCountType(runningCount);

            if (playerHand.IsBlackjack)
            {
                return $"Count: {runningCount} ({countType}). Recommendation: Blackjack!";
            }
            else if (playerHand.CanDoubleDown)
            {
                return $"Count: {runningCount} ({countType}). Recommendation: Double down!";
            }
            else if (playerHand.GetTotal() >= 19)
            {
                return $"Count: {runningCount} ({countType}). Recommendation: Stand.";
            }
            else if (playerHand.GetTotal() == 18)
            {
                if (dealerFirstCard.Rank >= Rank.Nine && dealerFirstCard.Rank <= Rank.Ace)
                {
                    return $"Count: {runningCount} ({countType}). Recommendation: Hit.";
                }
                else
                {
                    return $"Count: {runningCount} ({countType}). Recommendation: Stand.";
                }
            }
            else if (playerHand.GetTotal() == 17)
            {
                if (dealerFirstCard.Rank >= Rank.Ten && dealerFirstCard.Rank <= Rank.Ace)
                {
                    return $"Count: {runningCount} ({countType}). Recommendation: Hit.";
                }
                else
                {
                    return $"Count: {runningCount} ({countType}). Recommendation: Stand.";
                }
            }
            else if (playerHand.GetTotal() == 16)
            {
                if (dealerFirstCard.Rank >= Rank.Nine && dealerFirstCard.Rank <= Rank.Ace)
                {
                    return $"Count: {runningCount} ({countType}). Recommendation: Hit. Consider surrender against Ace.";
                }
                else
                {
                    return $"Count: {runningCount} ({countType}). Recommendation: Stand.";
                }
            }
            else if (playerHand.GetTotal() == 15)
            {
                if (dealerFirstCard.Rank >= Rank.Ten && dealerFirstCard.Rank <= Rank.Ace)
                {
                    return $"Count: {runningCount} ({countType}). Recommendation: Hit. Consider surrender against 10.";
                }
                else
                {
                    return $"Count: {runningCount} ({countType}). Recommendation: Stand.";
                }
            }
            else if ((playerHand.GetTotal() == 13 || playerHand.GetTotal() == 14) && dealerFirstCard.Rank >= Rank.Two && dealerFirstCard.Rank <= Rank.Six)
            {
                return $"Count: {runningCount} ({countType}). Recommendation: Stand.";
            }
            else if (playerHand.GetTotal() == 12 && dealerFirstCard.Rank >= Rank.Four && dealerFirstCard.Rank <= Rank.Six)
            {
                return $"Count: {runningCount} ({countType}). Recommendation: Stand.";
            }
            else
            {
                return $"Count: {runningCount} ({countType}). Recommendation: Hit.";
            }
        }
    }
}