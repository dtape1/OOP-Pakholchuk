namespace lab25.Observer;

public class DataPublisher
{
    public event Action<string>? DataProcessed;

    public void Publish(string result)
    {
        DataProcessed?.Invoke(result);
    }
}