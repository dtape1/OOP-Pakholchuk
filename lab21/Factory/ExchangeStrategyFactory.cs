using lab21.Interfaces;
using lab21.Strategies;

namespace lab21.Factory
{
    public static class ExchangeStrategyFactory
    {
        public static IExchangeStrategy CreateStrategy(string type)
        {
            return type.ToLower() switch
            {
                "standard" => new StandardExchange(),
                "card" => new CardExchange(),
                "crypto" => new CryptoExchange(),
                "vip" => new VipExchange(),
                _ => throw new ArgumentException("Невідомий тип обміну")
            };
        }
    }
}
