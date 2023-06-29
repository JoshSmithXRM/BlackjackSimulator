namespace Blackjack.Services
{
    public interface IGameOutputService
    {
        void ClearOutput();
        void NewHand();
        void PlayerHand(string hand);
        void DealerHand(string hand);
        void Recommendation(string recommendation);
        void Push();
        void PlayerWinsWithBlackjack();
        void DealerWinsWithBlackjack();
        void PlayerBusts();
        void DealerBusts();
        void PlayerWins();
        void DealerWins();
        void Tie();
        void InvalidAction();
        void ReshuffleShoe();
        void GameOver();
        void Recommendation(Recommendation recommendation);
        void ShowResult(HandResult result);
        void ShowHands(Hand playerHand, Hand dealerHand);
        void DisplayResult(HandOutcome outcome);
    }
}