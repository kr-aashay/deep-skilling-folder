namespace FactoryMethodExample;

// Product interface
public interface INotification
{
    void Send(string message);
}

// Concrete product 1
public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"[Email] {message}");
    }
}

// Concrete product 2
public class SMSNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"[SMS] {message}");
    }
}
