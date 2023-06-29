namespace Blackjack.Interfaces
{
    public interface IGameOutputService
    {
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
        void InvalidInput();
        void ReshuffleShoe();
        PlayerAction GetPlayerAction();
    }
}