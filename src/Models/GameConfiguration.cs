namespace Blackjack.Models
{
    public class GameConfiguration
    {
        public int DecksInShoe { get; set; } = 6;
        public decimal MinimumBet { get; set; } = 5;
        public decimal MaximumBet { get; set; } = 500;
        public int MinimumHands { get; set; } = 1;
        public int MaximumHands { get; set; } = 7;
        public int MinimumRounds { get; set; } = 1;
        public int MaximumRounds { get; set; } = 1000000;
        public (double Min, double Max) PenetrationRateRange { get; set; } = (0.5, 0.75);
        public Strategy Strategy { get; set; } = Strategy.Basic;
        public CountingSystem CountingSystem { get; set; } = CountingSystem.HiLo;
        public bool InsuranceOption { get; set; } = false;
        public bool DealerHitsOnSoft17 { get; set; } = true;
    }
}