using System.Collections.Generic;

namespace Blackjack.Models
{
    public interface IHand
    {
        public IReadOnlyList<ICard> Cards { get; }
        public void AddCard(ICard card);
        public int GetTotal();
        public bool IsBust { get; }
        public bool IsBlackjack { get; }
        public bool CanDoubleDown { get; }
        public string PartialString(bool hideSecondCard);
        public void Clear();
    }
}
