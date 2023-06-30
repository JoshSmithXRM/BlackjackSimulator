namespace Blackjack.Models
{
    public interface IPlayer
    {
        string Name { get; }
        decimal Balance { get; }
        decimal CurrentBet { get; }
        void PlaceBet(decimal amount);
        void Win(decimal amount);
        void Lose(decimal amount);
        void Push();
        void ClearHand();
    }
}