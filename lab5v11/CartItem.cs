namespace lab5v11;

// Елемент кошика (посилається на прайс за Code)
public class CartItem
{
    public string Code { get; }    // код з прайсу
    public int Quantity { get; }   // кількість

    public CartItem(string code, int quantity)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("code empty");
        if (quantity <= 0) throw new InvalidQuantityException("Кількість має бути > 0");
        Code = code;
        Quantity = quantity;
    }
}