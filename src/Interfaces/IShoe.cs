using System.Collections.Generic;

namespace Blackjack.Interfaces
{
    public interface IShoe
    {
        IReadOnlyList<ICard> Cards { get; }
        ICard DrawCard();
        void Shuffle();
        void Reset();
    }
}
