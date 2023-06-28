using System;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Enums;
using Blackjack.Interfaces;

namespace Blackjack.Models
{
    public class Shoe : IShoe
    {
        private IList<ICard> cards = new List<ICard>();

        public Shoe(int deckCount)
        {
            var suits = new[] { Suit.Spades, Suit.Hearts, Suit.Diamonds, Suit.Clubs };
            var ranks = new[]
            {
        Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven,
        Rank.Eight, Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace
    };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    for (int i = 0; i < deckCount; i++)
                    {
                        cards.Add(new Card(rank, suit));
                    }
                }
            }

            Shuffle();
        }

        public IReadOnlyList<ICard> Cards => cards.ToList().AsReadOnly();

        public ICard DrawCard()
        {
            var card = cards.First();
            cards.RemoveAt(0);
            return card;
        }

        public void Shuffle()
        {
            var random = new Random();
            cards = cards.OrderBy(x => random.Next()).ToList();
        }

        public void Reset()
        {
            cards.Clear();
        }
    }
}
