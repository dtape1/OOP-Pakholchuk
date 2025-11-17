namespace lab5v11;

// Позиція в прайсі
public class PriceItem
{
    public string Code { get; }        // унікальний код
    public string Name { get; }
    public decimal Price { get; }      // ціна за одиницю

    public PriceItem(string code, string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("code empty");
        if (price <= 0) throw new ArgumentException("price <= 0");
        Code = code;
        Name = name;
        Price = price;
    }
}