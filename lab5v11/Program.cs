using lab5v11;

namespace lab5v11;

internal class Program
{
    static void Main(string[] args)
    {
        IRepository<PriceItem> priceRepo = new Repository<PriceItem>();
        priceRepo.Add(new PriceItem("BRD", "Хліб", 22.50m));
        priceRepo.Add(new PriceItem("CHS", "Сир", 79.90m));
        priceRepo.Add(new PriceItem("TSH", "Футболка", 349m));

        var cart = new List<CartItem>
        {
            new CartItem("BRD", 2),
            new CartItem("CHS", 1),
            new CartItem("TSH", 1),
            // new CartItem("BAD", 1) // розкоментуй для перевірки винятку NotFound
        };

        var service = new CartService(priceRepo);

        try
        {
            var sum = service.Sum(cart);
            var avg = service.AveragePerPosition(cart);
            var totalWithCoupon = service.ApplyCoupon(sum, percent: 7m);

            Console.WriteLine("== Кошик ==");
            foreach (var (price, item) in service.Resolve(cart))
                Console.WriteLine($"{price.Name} [{price.Code}] x{item.Quantity} -> {(price.Price * item.Quantity):0.00} грн");

            Console.WriteLine($"\nСума: {sum:0.00} грн");
            Console.WriteLine($"Середня ціна за позицію: {avg:0.00} грн");
            Console.WriteLine($"Купон -7%: {totalWithCoupon:0.00} грн");

            var top = CartService.MaxBy(service.Resolve(cart), x => x.price.Price);
            Console.WriteLine($"Найдорожча позиція: {top.price.Name} {top.price.Price:0.00} грн");
        }
        catch (InvalidQuantityException ex)
        {
            Console.WriteLine($"Помилка кількості: {ex.Message}");
        }
        catch (NotFoundException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непередбачена помилка: {ex.Message}");
        }
    }
}
