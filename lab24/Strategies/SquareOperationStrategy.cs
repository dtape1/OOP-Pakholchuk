namespace lab24.Strategies
{
    public class SquareOperationStrategy : INumericOperationStrategy
    {
        public string Name => "Square";

        public double Execute(double value)
        {
            return value * value;
        }
    }
}