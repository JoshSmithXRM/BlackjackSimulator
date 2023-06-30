namespace Blackjack.Models
{
    public interface ICard : IRankable, ISuitable
    {
        public Rank Rank { get; }
        public Suit Suit { get; }
        public int GetCardCountValue();
    }
}
