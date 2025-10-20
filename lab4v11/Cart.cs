namespace lab4v11
{
    // Кошик: містить товари, може мати стратегію знижки
    public class Cart
    {
        private readonly List<IProduct> _items = new();
        private readonly IDiscount? _discount;

        public Cart(IDiscount? discount = null) => _discount = discount;

        public void Add(IProduct item) => _items.Add(item);
        public IEnumerable<IProduct> Items => _items;

        // сума з податками (бо вони в Total() позицій)
        public decimal Sum() => _items.Sum(i => i.Total());

        // підсумок з урахуванням знижки (якщо є)
        public decimal CheckoutTotal()
        {
            var sum = Sum();
            return _discount is null ? sum : _discount.Apply(sum);
        }

        // середня ціна за одиницю
        public decimal AverageUnitPrice()
        {
            var units = _items.Sum(i => i.Quantity);
            if (units == 0) return 0m;
            var cost = _items.Sum(i => i.UnitPrice * i.Quantity);
            return cost / units;
        }
    }
}