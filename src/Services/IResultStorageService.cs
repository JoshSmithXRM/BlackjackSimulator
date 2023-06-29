namespace Blackjack.Services
{
    public interface IResultStorageService
    {
        void StoreHandResult(HandResult handResult);
        void DumpRoundResults();
    }
}
