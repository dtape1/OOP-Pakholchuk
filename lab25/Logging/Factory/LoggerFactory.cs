using lab25.Logging.Loggers;

namespace lab25.Logging.Factory;

public abstract class LoggerFactory
{
    public abstract ILogger CreateLogger();
}