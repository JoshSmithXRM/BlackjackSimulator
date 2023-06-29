namespace Blackjack.Services
{
    public interface IShoeService
    {
        ICard DrawCard();
        void ShuffleCards();
        bool NeedsReshuffling();
    }
}