namespace PlayersManagerLib;

public class Player
{
    public string Name        { get; private set; }
    public int    Age         { get; private set; }
    public string Country     { get; private set; }
    public int    NoOfMatches { get; private set; }

    public Player(string name, int age, string country, int noOfMatches)
    {
        Name        = name;
        Age         = age;
        Country     = country;
        NoOfMatches = noOfMatches;
    }

    // Method Injection — IPlayerMapper passed as parameter (null = use real DB)
    // This pattern allows passing a mock for unit testing
    public static Player RegisterNewPlayer(string name, IPlayerMapper? playerMapper = null)
    {
        // Use real PlayerMapper if no mock is provided
        playerMapper ??= new PlayerMapper();

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Player name can't be empty.");

        // Mock will return false for this — so test proceeds past this check
        if (playerMapper.IsPlayerNameExistsInDb(name))
            throw new ArgumentException("Player name already exists.");

        // Mock will intercept this — no real DB insert
        playerMapper.AddNewPlayerIntoDb(name);

        return new Player(name, 23, "India", 30);
    }
}
