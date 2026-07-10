namespace FactoryMethodExample;

// Factory class
public class NotificationFactory
{
    public static INotification Create(string type)
    {
        return type switch
        {
            "Email" => new EmailNotification(),
            "SMS"   => new SMSNotification(),
            _       => throw new ArgumentException($"Unknown notification type: {type}")
        };
    }
}
