using lab21.Interfaces;

namespace lab21.Strategies
{
    // Обмін через банківську карту
    public class CardExchange : IExchangeStrategy
    {
        public decimal CalculateCommission(decimal amount)
        {
            return amount * 0.025m + 10;
        }
    }
}
