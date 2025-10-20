namespace lab4v11
{
    // Відсоткова знижка
    public class PercentageDiscount : IDiscount
    {
        public string Name { get; }
        public decimal Percent { get; } // напр. 10 => мінус 10%

        public PercentageDiscount(string name, decimal percent)
        {
            Name = name;
            Percent = percent;
        }

        public decimal Apply(decimal sum)
        {
            if (Percent <= 0) return sum;
            return sum * (100m - Percent) / 100m;
        }
    }
}