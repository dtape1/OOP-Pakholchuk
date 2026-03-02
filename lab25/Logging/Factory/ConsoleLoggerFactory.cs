using lab25.Logging.Loggers;

namespace lab25.Logging.Factory;

public class ConsoleLoggerFactory : LoggerFactory
{
    public override ILogger CreateLogger()
    {
        return new ConsoleLogger();
    }
}