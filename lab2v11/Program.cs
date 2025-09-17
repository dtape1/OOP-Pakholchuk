using System;

namespace lab2v11
{
    class Program
    {
        static void Main(string[] args)
        {
            Temperature t1 = new Temperature();
            t1.Celsius = 25;

            Temperature t2 = new Temperature();
            t2.Celsius = 30;

            Console.WriteLine($"t1: {t1.Celsius} °C = {t1.Fahrenheit} °F");
            Console.WriteLine($"t2: {t2.Celsius} °C = {t2.Fahrenheit} °F");

            Console.WriteLine($"t1 > t2: {t1 > t2}");
            Console.WriteLine($"t1 < t2: {t1 < t2}");
            Console.WriteLine($"t1 == t2: {t1 == t2}");

            t1[0] = t1.Celsius;
            t2[0] = t2.Celsius;

            Console.WriteLine($"Історія t1[0]: {t1[0]}");
            Console.WriteLine($"Історія t2[0]: {t2[0]}");
        }
    }
}