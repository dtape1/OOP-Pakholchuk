namespace lab3v11
{
    // Базовий клас для інструментів
    public abstract class Instrument
    {
        public string Name { get; }
        public int AvgPieceDurationMin { get; } // тривалість однієї пісні
        public int PiecesPlayed { get; set; }   // кількість пісень

        // конструктор
        protected Instrument(string name, int avgPieceDuration, int pieces)
        {
            Name = name;
            AvgPieceDurationMin = avgPieceDuration;
            PiecesPlayed = pieces;
        }

        // віртуальний метод
        public virtual void Play()
        {
            Console.WriteLine($"{Name} грає {PiecesPlayed} пісень");
        }

        // абстрактний метод
        public abstract void Tune();

        // підрахунок тривалості концерту
        public int ConcertDuration()
        {
            return AvgPieceDurationMin * PiecesPlayed;
        }
    }
}
