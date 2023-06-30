namespace Blackjack.Models
{
    public class GameConfiguration
    {
        public int DecksInShoe { get; set; } = 6;
        public decimal MinimumBet { get; set; } = 5;
        public decimal MaximumBet { get; set; } = 500;
        public decimal MinimumHands { get; set; } = 1;
        public decimal MaximumHands { get; set; } = 7;
        public (double Min, double Max) PenetrationRateRange { get; set; } = (0.5, 0.75);
        public Strategy Strategy { get; set; } = Strategy.Basic;
        public CountingSystem CountingSystem { get; set; } = CountingSystem.HiLo;
        public bool InsuranceOption { get; set; } = false;
        public bool DealerHitsOnSoft17 { get; set; } = true;
    }
}