namespace Blackjack.Interfaces
{
    public interface IGameService
    {
        public void PlayGame();
        public void PlayRound();
        public void DealCards();
        public void PlayerTurn();
        public void DealerTurn();
        public void PlayerHit();
        public void PlayerStand();
        public void PlayerDoubleDown();
        public void PlayerSplit();
        public void PlayerSurrender();
        public void PlayerInsurance();
        public void PlayerBlackjack();
        public void PlayerBust();
        public void PlayerWin();
        public void PlayerLose();
        public void PlayerPush();
        public void PlayerSurrendered();
        public void PlayerInsuranceWin();
        public void PlayerInsuranceLose();
        public void PlayerInsurancePush();
        public void PlayerSplitWin();
        public void PlayerSplitLose();
        public void PlayerSplitPush();
        public void PlayerSplitBust();
        public void PlayerSplitBlackjack();
        public void PlayerSplitSurrendered();
        public void PlayerSplitSurrender();
        public void PlayerSplitDoubleDown();
        public void PlayerSplitHit();
        public void PlayerSplitStand();
        public void PlayerSplitBustAfterAceSplit();
        public void PlayerSplitBlackjackAfterAceSplit();
        public void PlayerSplitWinAfterAceSplit();
        public void PlayerSplitLoseAfterAceSplit();
        public void PlayerSplitPushAfterAceSplit();
        public void PlayerSplitSurrenderedAfterAceSplit();
        public void PlayerSplitSurrenderAfterAceSplit();
        public void PlayerSplitDoubleDownAfterAceSplit();
        public void PlayerSplitHitAfterAceSplit();
        public void PlayerSplitStandAfterAceSplit();
    }
}
