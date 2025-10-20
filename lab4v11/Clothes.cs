namespace lab4v11
{
    // Одяг: додаємо розмір + ПДВ
    public class Clothes : Product
    {
        public string Size { get; }

        public Clothes(string name, decimal unitPrice, int quantity, string size)
            : base(name, unitPrice, quantity)
        {
            Size = size;
        }

        // 20% ПДВ для одягу
        public override decimal Total() => base.Total() * 1.20m;

        public override string Info() => $"{Name} (clothes, size:{Size})";
    }
}