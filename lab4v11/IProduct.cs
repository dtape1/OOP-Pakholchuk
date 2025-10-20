namespace lab4v11
{
    // Контракт для товару
    public interface IProduct
    {
        string Name { get; }
        decimal UnitPrice { get; }   // ціна за одиницю
        int Quantity { get; }        // кількість
        decimal Total();             // підсумок за позицією
        string Info();               // короткий опис
    }
}