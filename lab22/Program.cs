using lab22;

class Program
{
    static void Main()
    {
        Employee emp = new Employee("Іван");
        Volunteer vol = new Volunteer("Петро");

        EmployeeService.PrintSalary(emp, emp.Name);

        Console.WriteLine();
        Console.WriteLine($"Імʼя: {vol.Name}");
        Console.WriteLine("Зарплата не передбачена");
    }
}
