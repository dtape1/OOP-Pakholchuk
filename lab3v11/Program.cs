namespace lab3v11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // список інструментів (поліморфізм)
            List<Instrument> instruments = new List<Instrument>
            {
                new Guitar(5),
                new Piano(3)
            };

            // показ роботи методів
            foreach (var inst in instruments)
            {
                inst.Tune();
                inst.Play();
                Console.WriteLine($"Тривалість концерту: {inst.ConcertDuration()} хв.\n");
            }
        }
    }
}