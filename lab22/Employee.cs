namespace lab22;

public class Employee : IPerson, IPaidEmployee
{
    public string Name { get; set; }

    public Employee(string name)
    {
        Name = name;
    }

    public decimal CalculateSalary()
    {
        return 1000;
    }
}
