namespace lab4v11
{
    // Їжа: строк придатності + м'який ПДВ
    public class Food : Product
    {
        public bool IsVegan { get; }
        public DateOnly Expiry { get; } // до якої дати придатний

        public Food(string name, decimal unitPrice, int quantity, bool isVegan, DateOnly expiry)
            : base(name, unitPrice, quantity)
        {
            IsVegan = isVegan;
            Expiry = expiry;
        }

        public bool IsExpired(DateOnly today) => today > Expiry;

        // 7% ПДВ на прикладі
        public override decimal Total() => base.Total() * 1.07m;

        public override string Info() => $"{Name} (food, vegan:{IsVegan}, до {Expiry:yyyy-MM-dd})";
    }
}