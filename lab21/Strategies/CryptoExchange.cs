using lab21.Interfaces;

namespace lab21.Strategies
{
    // Обмін криптовалюти
    public class CryptoExchange : IExchangeStrategy
    {
        public decimal CalculateCommission(decimal amount)
        {
            return amount * 0.04m;
        }
    }
}
