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

        public override string ToString()
        {
            return $"{GetRankSymbol()}{GetSuitSymbol()}";
        }

        public int GetRankValue()
        {
            if (Rank == Rank.Ace)
            {
                return 11;
            }
            else if (Rank == Rank.King || Rank == Rank.Queen || Rank == Rank.Jack)
            {
                return 10;
            }
            else
            {
                return (int)Rank;
            }
        }

        public string GetRankSymbol()
        {
            return Rank switch
            {
                Rank.Ace => "A",
                Rank.King => "K",
                Rank.Queen => "Q",
                Rank.Jack => "J",
                _ => ((int)Rank).ToString(),
            };
        }

        public string GetSuitSymbol()
        {
            return Suit switch
            {
                Suit.Spades => "♠",
                Suit.Hearts => "♥",
                Suit.Diamonds => "♦",
                Suit.Clubs => "♣",
                _ => string.Empty,
            };
        }

        public int GetCardCountValue()
        {
            if (Rank == Rank.Ace || Rank == Rank.Ten || Rank == Rank.King ||
                Rank == Rank.Queen || Rank == Rank.Jack)
            {
                return -1;
            }
            else if (Rank == Rank.Two || Rank == Rank.Three || Rank == Rank.Four ||
                Rank == Rank.Five || Rank == Rank.Six)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}