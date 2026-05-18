namespace lab28v10.Models;

public class Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
            throw new ArgumentException("Невалідний email");
        Value = value;
    }

    public override string ToString() => Value;
}