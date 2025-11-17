namespace lab5v11;

// Коли товару нема в прайсі
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

// Невалідна кількість/дані
public class InvalidQuantityException : Exception
{
    public InvalidQuantityException(string message) : base(message) { }
}