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
        bool CanSurrender { get; }
        bool IsSoft { get; }
        decimal BetAmount { get; set; }
        string PartialString(bool hideSecondCard = false);
        void Clear();
        ICard GetUpCard();
        ICard GetDownCard();
        IHand Split();

        List<PlayerAction> ActionsTaken { get; }
    }
}
