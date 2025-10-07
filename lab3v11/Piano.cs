namespace lab3v11
{
    // Фортепіано як похідний клас
    public class Piano : Instrument
    {
        public Piano(int pieces) 
            : base("Фортепіано", 6, pieces)
        {
        }

        public override void Play()
        {
            Console.WriteLine($"Фортепіано грає {PiecesPlayed} творів");
        }

        public override void Tune()
        {
            Console.WriteLine("Налаштовую фортепіано...");
        }
    }
}
