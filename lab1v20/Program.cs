using System;

class Figure
{
    private string name;
    public double Area { get; set; }

    public Figure(string name, double area)
    {
        this.name = name;
        Area = area;
        Console.WriteLine($"Створено фігуру: {name}");
    }

    ~Figure()
    {
        Console.WriteLine($"Фігуру {name} видалено");
    }

    public string GetFigure()
    {
        return $"Назва: {name}, Площа: {Area}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Figure f1 = new Figure("Коло", 78.5);
        Figure f2 = new Figure("Квадрат", 64);
        Figure f3 = new Figure("Трикутник", 36.7);

        Console.WriteLine(f1.GetFigure());
        Console.WriteLine(f2.GetFigure());
        Console.WriteLine(f3.GetFigure());
    }
}