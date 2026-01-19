using lab21.Interfaces;

namespace lab21.Strategies
{
    // VIP обмін з мінімальною комісією
    public class VipExchange : IExchangeStrategy
    {
        public decimal CalculateCommission(decimal amount)
        {
            return amount * 0.01m;
        }
    }
}
