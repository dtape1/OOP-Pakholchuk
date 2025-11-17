namespace lab4v11
{
    // Абстрактний базовий товар
    public abstract class Product : IProduct
    {
        public string Name { get; }
        public decimal UnitPrice { get; }
        public int Quantity { get; }

        protected Product(string name, decimal unitPrice, int quantity)
        {
            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        // спільна формула
        public virtual decimal Total() => UnitPrice * Quantity;

        // кожен підклас дає свій короткий опис
        public abstract string Info();
    }
}