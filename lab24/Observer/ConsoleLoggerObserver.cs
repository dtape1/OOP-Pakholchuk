using System;

namespace lab24.Observer
{
    public class ConsoleLoggerObserver
    {
        public void OnResultCalculated(double result, string operation)
        {
            Console.WriteLine($"Operation: {operation} | Result: {result}");
        }
    }
}