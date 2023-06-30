namespace Blackjack.CountingSystems
{
    public class HiloCountingSystem : BaseCountingSystem
    {
        public override PlayerAction GetRecommendation(IHand playerHand, ICard dealerFirstCard)
        {
            if (playerHand.CanDoubleDown)
            {
                if (playerHand.GetTotal() == 9 && dealerFirstCard.Rank >= Rank.Three && dealerFirstCard.Rank <= Rank.Six && _runningCount >= 1)
                {
                    return PlayerAction.Double;
                }
                else if (playerHand.GetTotal() == 10 && dealerFirstCard.Rank >= Rank.Two && dealerFirstCard.Rank <= Rank.Nine && _runningCount >= 1)
                {
                    return PlayerAction.Double;
                }
                else if (playerHand.GetTotal() == 11 && _runningCount >= 1)
                {
                    return PlayerAction.Double;
                }
            }

            if (playerHand.GetTotal() >= 17)
            {
                return PlayerAction.Stand;
            }
            else if (playerHand.GetTotal() == 16)
            {
                if ((dealerFirstCard.Rank >= Rank.Nine && dealerFirstCard.Rank <= Rank.Ten || dealerFirstCard.Rank == Rank.Ace) && _runningCount >= 0)
                {
                    return PlayerAction.Surrender;
                }
                else if (dealerFirstCard.Rank >= Rank.Two && dealerFirstCard.Rank <= Rank.Six && _runningCount >= 1)
                {
                    return PlayerAction.Stand;
                }
                else
                {
                    return PlayerAction.Hit;
                }
            }
            else if (playerHand.GetTotal() == 15)
            {
                if (dealerFirstCard.Rank == Rank.Ten && _runningCount >= -1)
                {
                    return PlayerAction.Surrender;
                }
                else if (dealerFirstCard.Rank >= Rank.Two && dealerFirstCard.Rank <= Rank.Six && _runningCount >= 1)
                {
                    return PlayerAction.Stand;
                }
                else
                {
                    return PlayerAction.Hit;
                }
            }
            else if (playerHand.GetTotal() == 13 || playerHand.GetTotal() == 14)
            {
                if (playerHand.GetTotal() == 14 && dealerFirstCard.Rank == Rank.Ten && _runningCount >= -1)
                {
                    return PlayerAction.Surrender;
                }
                else if (dealerFirstCard.Rank >= Rank.Two && dealerFirstCard.Rank <= Rank.Six && _runningCount >= 1)
                {
                    return PlayerAction.Stand;
                }
                else
                {
                    return PlayerAction.Hit;
                }
            }

            return PlayerAction.Hit;
        }

        protected override int GetCardValue(ICard card)
        {
            return card.Rank switch
            {
                Rank.Two or Rank.Three or Rank.Four or Rank.Five or Rank.Six => 1,
                Rank.Ten or Rank.Jack or Rank.Queen or Rank.King or Rank.Ace => -1,
                _ => 0,
            };
        }
    }
}