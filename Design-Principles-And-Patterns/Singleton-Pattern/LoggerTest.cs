namespace SingletonPatternExample;

public class LoggerTest
{
    public static void Run()
    {
        Logger logger1 = Logger.GetInstance();
        Logger logger2 = Logger.GetInstance();

        logger1.Log("Message from logger1");
        logger2.Log("Message from logger2");

        Console.WriteLine($"Same instance? {ReferenceEquals(logger1, logger2)}");
    }
}
