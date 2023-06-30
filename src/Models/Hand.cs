namespace Blackjack.Models
{
    public class Hand : IHand
    {
        private readonly List<ICard> cards = new();
        private readonly IHandFactory _handFactory;

        public IReadOnlyList<ICard> Cards => cards;

        public void AddCard(ICard card) => cards.Add(card);

        public int GetTotal()
        {
            var total = cards.Sum(card => card.GetRankValue());

            // Adjust for aces
            var aceCount = cards.Count(card => card.Rank == Rank.Ace);
            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }

        public bool IsBust => GetTotal() > 21;

        public bool IsBlackjack => cards.Count == 2 && GetTotal() == 21;

        public bool IsSoft => cards.Any(card => card.Rank == Rank.Ace) && GetTotal() <= 21;

        public bool CanDoubleDown => cards.Count == 2;

        public bool CanSplit => cards.Count == 2 && cards[0].Rank == cards[1].Rank;

        public decimal BetAmount { get; set; }

        public List<PlayerAction> ActionsTaken { get; }

        public bool CanSurrender => cards.Count == 2 && !IsBlackjack;

        public Hand(IHandFactory handFactory)
        {
            _handFactory = handFactory;
            ActionsTaken = new List<PlayerAction>();
        }

        public override string ToString() => string.Join(", ", cards);

        public string PartialString(bool hideSecondCard = false) => cards.Count > 1
                ? hideSecondCard ? $"{cards.First()}, *" : string.Join(", ", cards)
                : cards.Count == 1 ? $"{cards.First()}" : "No cards";

        public void Clear() => cards.Clear();

        public ICard GetUpCard() => cards[0];

        public ICard GetDownCard() => cards[1];

        public IHand Split()
        {
            var splitHand = _handFactory.CreateHand(BetAmount);
            splitHand.AddCard(cards[1]);
            cards.RemoveAt(1);
            return splitHand;
        }
    }
}