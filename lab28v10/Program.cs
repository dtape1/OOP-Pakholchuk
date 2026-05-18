using lab28v10.Models;
using lab28v10.Repository;

class Program
{
    static async Task Main()
    {
        var repo = CreateAndPopulateRepository();
        await repo.SaveToFileAsync("events.json");
        Console.WriteLine("Saved to JSON\n");

        var repo2 = new EventRepository();
        await repo2.LoadFromFileAsync("events.json");
        Console.WriteLine("Loaded from JSON:\n");

        foreach (var ev in repo2.GetAll())
            PrintEvent(ev);
    }

    static EventRepository CreateAndPopulateRepository()
    {
        var repo = new EventRepository();

        var organizer1 = new Organizer { Id = 1, Name = "Tech Corp" };
        var organizer2 = new Organizer { Id = 2, Name = "Music Group" };

        repo.Add(new Event
        {
            Id = 1,
            Title = "Tech Conference",
            Organizer = organizer1,
            Location = new Location { City = "Kyiv", Address = "Main Street 1" },
            Participants = new List<Participant>
            {
                new() { Id = 1, Name = "Ivan",  Email = new Email("ivan@gmail.com") },
                new() { Id = 2, Name = "Olena", Email = new Email("olena@gmail.com") }
            }
        });

        repo.Add(new Event
        {
            Id = 2,
            Title = "Music Festival",
            Organizer = organizer2,
            Location = new Location { City = "Lviv", Address = "Center Square 5" },
            Participants = new List<Participant>
            {
                new() { Id = 3, Name = "Andrii", Email = new Email("andrii@gmail.com") }
            }
        });

        return repo;
    }

    static void PrintEvent(Event ev)
    {
        Console.WriteLine($"Event: {ev.Title}");
        Console.WriteLine($"Organizer: {ev.Organizer.Name}");
        Console.WriteLine($"Location: {ev.Location.City}, {ev.Location.Address}");
        Console.WriteLine("Participants:");
        foreach (var p in ev.Participants)
            Console.WriteLine($" - {p.Name} ({p.Email})");
        Console.WriteLine();
    }
}