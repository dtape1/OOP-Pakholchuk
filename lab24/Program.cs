using lab24.Strategies;
using lab24.Processor;
using lab24.Observer;

class Program
{
    static void Main()
    {
        // Strategy
        var processor = new NumericProcessor(new SquareOperationStrategy());

        // Observer
        var publisher = new ResultPublisher();

        var consoleObserver = new ConsoleLoggerObserver();
        var historyObserver = new HistoryLoggerObserver();
        var thresholdObserver = new ThresholdNotifierObserver(20);

        // Підписка на подію
        publisher.ResultCalculated += consoleObserver.OnResultCalculated;
        publisher.ResultCalculated += historyObserver.OnResultCalculated;
        publisher.ResultCalculated += thresholdObserver.OnResultCalculated;

        double[] numbers = { 4, 9, 16 };

        // Square
        foreach (var number in numbers)
        {
            var result = processor.Process(number);
            publisher.PublishResult(result, processor.CurrentOperationName());
        }

        // Change Strategy → Cube
        processor.SetStrategy(new CubeOperationStrategy());

        foreach (var number in numbers)
        {
            var result = processor.Process(number);
            publisher.PublishResult(result, processor.CurrentOperationName());
        }

        // Change Strategy → Square Root
        processor.SetStrategy(new SquareRootOperationStrategy());

        foreach (var number in numbers)
        {
            var result = processor.Process(number);
            publisher.PublishResult(result, processor.CurrentOperationName());
        }
    }
}