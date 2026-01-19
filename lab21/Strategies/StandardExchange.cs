using lab21.Interfaces;

namespace lab21.Strategies
{
    // Звичайний обмін
    public class StandardExchange : IExchangeStrategy
    {
        public decimal CalculateCommission(decimal amount)
        {
            return amount * 0.02m;
        }
    }
}
