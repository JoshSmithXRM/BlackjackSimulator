namespace Blackjack.Services
{
    public interface IOutputService
    {
        void Clear();
        void WriteLine(string message);
        void Write(string message);
        void WriteLine();
    }
}