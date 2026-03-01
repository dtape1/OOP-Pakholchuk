using System.Collections.Generic;

namespace lab24.Observer
{
    public class HistoryLoggerObserver
    {
        public List<string> History { get; } = new();

        public void OnResultCalculated(double result, string operation)
        {
            History.Add($"{operation}: {result}");
        }
    }
}