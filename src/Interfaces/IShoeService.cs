namespace Blackjack.Interfaces
{
    public interface IShoeService
    {
        IReadOnlyList<ICard> Cards { get; }
        ICard DrawCard();
        void InitializeShoe();
        void Reshuffle();
        bool NeedsReshuffling();
    }
}