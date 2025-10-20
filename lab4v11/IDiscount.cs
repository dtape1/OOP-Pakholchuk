namespace lab4v11
{
    // Контракт для знижки
    public interface IDiscount
    {
        string Name { get; }
        decimal Apply(decimal sum); // повертає суму після знижки
    }
}