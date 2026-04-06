namespace lab28v10.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; }

    public Organizer Organizer { get; set; }
    public Location Location { get; set; }

    public List<Participant> Participants { get; set; } = new();
}