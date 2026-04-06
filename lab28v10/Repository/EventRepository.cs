using System.Text.Json;
using lab28v10.Models;

namespace lab28v10.Repository;

public class EventRepository
{
    private List<Event> _events = new();

    public void Add(Event ev)
    {
        _events.Add(ev);
    }

    public List<Event> GetAll()
    {
        return _events;
    }

    public Event? GetById(int id)
    {
        return _events.FirstOrDefault(e => e.Id == id);
    }

    public async Task SaveToFileAsync(string filename)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        using FileStream fs = new FileStream(filename, FileMode.Create);
        await JsonSerializer.SerializeAsync(fs, _events, options);
    }

    public async Task LoadFromFileAsync(string filename)
    {
        if (!File.Exists(filename))
            return;

        using FileStream fs = new FileStream(filename, FileMode.Open);
        var data = await JsonSerializer.DeserializeAsync<List<Event>>(fs);

        if (data != null)
            _events = data;
    }
}