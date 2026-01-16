class Program
{
    static void Main()
    {
        var validator = new OrderValidator();
        var repository = new InMemoryOrderRepository();
        var emailService = new ConsoleEmailService();

        var orderService = new OrderService(
            validator,
            repository,
            emailService
        );

        var validOrder = new Order(1, "Ivan", 500);
        var invalidOrder = new Order(2, "Petro", -100);

        Console.WriteLine("Valid order:");
        orderService.ProcessOrder(validOrder);

        Console.WriteLine();

        Console.WriteLine("Invalid order:");
        orderService.ProcessOrder(invalidOrder);
    }
}