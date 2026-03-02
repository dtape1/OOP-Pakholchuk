using lab25.Logging.Factory;
using lab25.Logging.Loggers;

namespace lab25.Logging.Singleton;

public class LoggerManager
{
    private static LoggerManager? _instance;
    private LoggerFactory? _factory;

    private LoggerManager() {}

    public static LoggerManager Instance
        => _instance ??= new LoggerManager();

    public void SetFactory(LoggerFactory factory)
    {
        _factory = factory;
    }

    public ILogger GetLogger()
    {
        return _factory!.CreateLogger();
    }
}