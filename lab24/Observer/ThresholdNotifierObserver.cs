using System;

namespace lab24.Observer
{
    public class ThresholdNotifierObserver
    {
        private readonly double _threshold;

        public ThresholdNotifierObserver(double threshold)
        {
            _threshold = threshold;
        }

        public void OnResultCalculated(double result, string operation)
        {
            if (result > _threshold)
            {
                Console.WriteLine($"WARNING: Result {result} exceeded threshold {_threshold}");
            }
        }
    }
}