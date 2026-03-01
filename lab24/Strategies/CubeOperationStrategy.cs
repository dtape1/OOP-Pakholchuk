namespace lab24.Strategies
{
    public class CubeOperationStrategy : INumericOperationStrategy
    {
        public string Name => "Cube";

        public double Execute(double value)
        {
            return value * value * value;
        }
    }
}