namespace lab22;

public interface IPerson
{
    string Name { get; }
}

public interface IPaidEmployee
{
    decimal CalculateSalary();
}
