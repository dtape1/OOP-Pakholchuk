public class InMemoryOrderRepository : IOrderRepository
{
    public void Save(Order order)
    {
        Console.WriteLine($"Order {order.Id} saved");
    }

    public Order GetById(int id)
    {
        return null;
    }
}