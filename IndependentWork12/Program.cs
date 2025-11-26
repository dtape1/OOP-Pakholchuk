using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace IndependentWork12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" IndependentWork12 | PLINQ дослідження \n");

            // Розміри даних для тестів
            int[] sizes = { 1_000_000, 5_000_000 };

            foreach (var size in sizes)
            {
                RunPerformanceTest(size);
            }

            Console.WriteLine();
            RunSideEffectProblemDemo();

            Console.WriteLine("\n Кінець роботи ");
        }

        // -----------------------------------------------------
        // 1. Функція для перевірки числа на простоту (важка)
        // -----------------------------------------------------
        static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        // -----------------------------------------------------
        // 2. Порівняння LINQ і PLINQ
        // -----------------------------------------------------
        static void RunPerformanceTest(int size)
        {
            Console.WriteLine($" Тест продуктивності ({size} елементів) ");

            Random rnd = new Random();
            List<int> numbers = Enumerable.Range(0, size)
                                          .Select(_ => rnd.Next(1, 2_000_000))
                                          .ToList();

            // Звичайний LINQ
            Stopwatch sw1 = Stopwatch.StartNew();
            var primesLinq = numbers
                .Where(IsPrime)
                .Count();
            sw1.Stop();

            // PLINQ
            Stopwatch sw2 = Stopwatch.StartNew();
            var primesPlinq = numbers
                .AsParallel()               // Паралельна обробка
                .Where(IsPrime)
                .Count();
            sw2.Stop();

            Console.WriteLine($"LINQ:   {sw1.ElapsedMilliseconds} ms");
            Console.WriteLine($"PLINQ:  {sw2.ElapsedMilliseconds} ms");
            Console.WriteLine($"Результат однаковий: {primesLinq == primesPlinq}");
            Console.WriteLine();
        }

        // -----------------------------------------------------
        // 3. Демонстрація проблеми потокобезпеки (side effects)
        // -----------------------------------------------------
        static void RunSideEffectProblemDemo()
        {
            Console.WriteLine(" Побічні ефекти в PLINQ ");

            List<int> data = Enumerable.Range(1, 500_000).ToList();

            int unsafeCounter = 0;

            // Неправильно: одночасний запис у змінну
            data.AsParallel().ForAll(n =>
            {
                if (n % 2 == 0)
                {
                    unsafeCounter++;     // НЕБЕЗПЕЧНО
                }
            });

            Console.WriteLine($"Небезпечний результат (непередбачуваний): {unsafeCounter}");

            // Виправлений варіант
            int safeCounter = 0;
            object locker = new object();

            data.AsParallel().ForAll(n =>
            {
                if (n % 2 == 0)
                {
                    lock (locker)
                    {
                        safeCounter++;
                    }
                }
            });

            Console.WriteLine($"Безпечний результат: {safeCounter}");
        }
    }
}
