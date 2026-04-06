using lab28v10.Models;
using lab28v10.Repository;

class Program
{
    static async Task Main()
    {
        var repo = new EventRepository();

        // створення організаторів
        var organizer1 = new Organizer { Id = 1, Name = "Tech Corp" };
        var organizer2 = new Organizer { Id = 2, Name = "Music Group" };

        // створення подій
        var event1 = new Event
        {
            Id = 1,
            Title = "Tech Conference",
            Organizer = organizer1,
            Location = new Location { City = "Kyiv", Address = "Main Street 1" },
            Participants = new List<Participant>
            {
                new Participant { Id = 1, Name = "Ivan", Email = "ivan@gmail.com" },
                new Participant { Id = 2, Name = "Olena", Email = "olena@gmail.com" }
            }
        };

        var event2 = new Event
        {
            Id = 2,
            Title = "Music Festival",
            Organizer = organizer2,
            Location = new Location { City = "Lviv", Address = "Center Square 5" },
            Participants = new List<Participant>
            {
                new Participant { Id = 3, Name = "Andrii", Email = "andrii@gmail.com" }
            }
        };

        // додавання
        repo.Add(event1);
        repo.Add(event2);

        // збереження
        await repo.SaveToFileAsync("events.json");
        Console.WriteLine("Saved to JSON\n");

        // новий репозиторій
        var repo2 = new EventRepository();

        // завантаження
        await repo2.LoadFromFileAsync("events.json");

        Console.WriteLine("Loaded from JSON:\n");

        foreach (var ev in repo2.GetAll())
        {
            Console.WriteLine($"Event: {ev.Title}");
            Console.WriteLine($"Organizer: {ev.Organizer.Name}");
            Console.WriteLine($"Location: {ev.Location.City}, {ev.Location.Address}");

            Console.WriteLine("Participants:");
            foreach (var p in ev.Participants)
            {
                Console.WriteLine($" - {p.Name} ({p.Email})");
            }

            Console.WriteLine();
        }
    }
}