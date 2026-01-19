using lab21.Interfaces;

namespace lab21.Services
{
    // Клас працює тільки з інтерфейсом
    public class ExchangeService
    {
        public decimal CalculateFinalAmount(decimal amount, IExchangeStrategy strategy)
        {
            var commission = strategy.CalculateCommission(amount);
            return amount - commission;
        }
    }
}
