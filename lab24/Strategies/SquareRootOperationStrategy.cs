using System;

namespace lab24.Strategies
{
    public class SquareRootOperationStrategy : INumericOperationStrategy
    {
        public string Name => "Square Root";

        public double Execute(double value)
        {
            return Math.Sqrt(value);
        }
    }
}