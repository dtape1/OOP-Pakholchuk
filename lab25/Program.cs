using lab25.Logging.Factory;
using lab25.Logging.Singleton;
using lab25.Observer;
using lab25.Strategy;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== SCENARIO 1 ===");

        LoggerManager.Instance.SetFactory(new ConsoleLoggerFactory());

        var context = new DataContext(new EncryptDataStrategy());
        var publisher = new DataPublisher();

        var observer = new ProcessingLoggerObserver();
        observer.Subscribe(publisher);

        var result = context.Execute("DATA1");
        publisher.Publish(result);

        Console.WriteLine("\n=== SCENARIO 2 ===");

        LoggerManager.Instance.SetFactory(new FileLoggerFactory());

        result = context.Execute("DATA2");
        publisher.Publish(result);

        Console.WriteLine("\n=== SCENARIO 3 ===");

        context.SetStrategy(new CompressDataStrategy());

        result = context.Execute("DATA3");
        publisher.Publish(result);
    }
}