namespace lab22;

public class EmployeeService
{
    public static void PrintSalary(IPaidEmployee employee, string name)
    {
        Console.WriteLine($"Імʼя: {name}");
        Console.WriteLine($"Зарплата: {employee.CalculateSalary()}");
    }
}
