using System.Data.SqlClient;

namespace PlayersManagerLib;

// Interface — allows mocking DB calls in unit tests
public interface IPlayerMapper
{
    bool IsPlayerNameExistsInDb(string name);
    void AddNewPlayerIntoDb(string name);
}

// Real implementation — hits SQL Server (cannot be unit tested directly)
// Mocked in tests via IPlayerMapper
public class PlayerMapper : IPlayerMapper
{
    private readonly string _connectionString =
        "Data Source=(local);Initial Catalog=GameDB;Integrated Security=True";

    public bool IsPlayerNameExistsInDb(string name)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT count(*) FROM Player WHERE [Name] = @name";
        command.Parameters.AddWithValue("@name", name);
        var existingPlayersCount = (int)command.ExecuteScalar();
        return existingPlayersCount > 0;
    }

    public void AddNewPlayerIntoDb(string name)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Player ([Name]) VALUES (@name)";
        command.Parameters.AddWithValue("@name", name);
        command.ExecuteNonQuery();
    }
}
