namespace lab4v11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            // 10% знижка на все (приклад DI)
            var discount = new PercentageDiscount("Student -10%", 10m);
            var cart = new Cart(discount);

            // товари
            cart.Add(new Food("Хліб", 22.50m, 2, isVegan: true,  expiry: today.AddDays(2)));
            cart.Add(new Food("Сир", 79.90m, 1, isVegan: false,   expiry: today.AddDays(10)));
            cart.Add(new Clothes("Футболка", 349m, 1, size: "M"));

            Console.WriteLine("== Кошик ==");
            foreach (var i in cart.Items)
                Console.WriteLine($"{i.Info()} x{i.Quantity} -> {i.Total():0.00} грн");

            Console.WriteLine($"\nСума (з ПДВ): {cart.Sum():0.00} грн");
            Console.WriteLine($"Середня ціна за одиницю: {cart.AverageUnitPrice():0.00} грн/шт");
            Console.WriteLine($"До оплати зі знижкою \"{discount.Name}\": {cart.CheckoutTotal():0.00} грн");
        }
    }
}