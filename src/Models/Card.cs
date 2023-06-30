namespace Blackjack.Models
{
    public class Card : ICard
    {
        public Rank Rank { get; }
        public Suit Suit { get; }

        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString() => $"{GetRankSymbol()}{GetSuitSymbol()}";

        public int GetRankValue() => Rank == Rank.Ace ? 11 : Rank is Rank.King or Rank.Queen or Rank.Jack ? 10 : (int)Rank;

        public string GetRankSymbol() => Rank switch
        {
            Rank.Ace => "A",
            Rank.King => "K",
            Rank.Queen => "Q",
            Rank.Jack => "J",
            _ => ((int)Rank).ToString(),
        };

        public string GetSuitSymbol() => Suit switch
        {
            Suit.Spades => "♠",
            Suit.Hearts => "♥",
            Suit.Diamonds => "♦",
            Suit.Clubs => "♣",
            _ => string.Empty,
        };

        public int GetCardCountValue() => Rank is Rank.Ace or Rank.Ten or Rank.King or
                Rank.Queen or Rank.Jack
                ? -1
                : Rank is Rank.Two or Rank.Three or Rank.Four or
                                Rank.Five or Rank.Six
                    ? 1
                    : 0;

    }
}