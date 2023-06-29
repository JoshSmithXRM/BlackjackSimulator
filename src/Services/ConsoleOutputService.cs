namespace Blackjack.Services
{
    public class ConsoleOutputService : IOutputService
    {
        public void Clear()
        {
            Console.Clear();
        }
        
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}