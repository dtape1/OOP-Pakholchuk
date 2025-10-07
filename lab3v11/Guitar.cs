namespace lab3v11
{
    // Гітара як похідний клас
    public class Guitar : Instrument
    {
        public Guitar(int pieces) 
            : base("Гітара", 4, pieces) // виклик базового конструктора
        {
        }

        // перевизначений метод
        public override void Play()
        {
            Console.WriteLine($"Гітара виконує {PiecesPlayed} композицій");
        }

        // реалізація абстрактного методу
        public override void Tune()
        {
            Console.WriteLine("Налаштовую гітару...");
        }
    }
}
