namespace lab22;

public class Volunteer : IPerson
{
    public string Name { get; set; }

    public Volunteer(string name)
    {
        Name = name;
    }
}
