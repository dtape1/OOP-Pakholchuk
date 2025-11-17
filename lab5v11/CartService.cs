namespace lab5v11;

// Сервіс розрахунків по кошику
public class CartService
{
    private readonly IRepository<PriceItem> _prices;

    public CartService(IRepository<PriceItem> prices)
    {
        _prices = prices;
    }

    // Зліплюємо кошик з прайсом (join)
    public IEnumerable<(PriceItem price, CartItem item)> Resolve(IEnumerable<CartItem> cart)
    {
        foreach (var ci in cart)
        {
            var price = _prices.FirstOrDefault(p => p.Code == ci.Code)
                        ?? throw new NotFoundException($"Товар з кодом '{ci.Code}' не знайдено");
            yield return (price, ci);
        }
    }

    // Підсумок без знижки
    public decimal Sum(IEnumerable<CartItem> cart)
        => Resolve(cart).Sum(x => x.price.Price * x.item.Quantity);

    // Середня ціна за позицію (по прайсу)
    public decimal AveragePerPosition(IEnumerable<CartItem> cart)
        => Resolve(cart).Average(x => x.price.Price);

    // Купон (наприклад, 7%)
    public decimal ApplyCoupon(decimal sum, decimal percent) => sum * (100m - percent) / 100m;

    // Узагальнена MaxBy як бонус (Generics + Func)
    public static T MaxBy<T>(IEnumerable<T> src, Func<T, decimal> metric)
        => src.OrderByDescending(metric).First();
}