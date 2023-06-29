using System.Collections.Generic;

namespace Blackjack.Models
{
    public interface IHand
    {
        IReadOnlyList<ICard> Cards { get; }
        void AddCard(ICard card);
        int GetTotal();
        bool IsBust { get; }
        bool IsBlackjack { get; }
        bool CanDoubleDown { get; }
        bool CanSplit { get; }
        bool IsSoft { get; }
        decimal BetAmount { get; set; }
        string PartialString(bool hideSecondCard = false);
        void Clear();
        ICard GetUpCard();
        IHand Split();
        List<PlayerAction> ActionsTaken { get; }
    }
}
