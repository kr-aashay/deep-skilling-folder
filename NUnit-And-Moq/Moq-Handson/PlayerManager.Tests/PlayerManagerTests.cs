using Moq;
using NUnit.Framework;
using PlayersManagerLib;

namespace PlayerManager.Tests;

[TestFixture]
public class PlayerManagerTests
{
    private Mock<IPlayerMapper> _mockPlayerMapper = null!;

    [OneTimeSetUp]
    public void Init()
    {
        _mockPlayerMapper = new Mock<IPlayerMapper>();

        // Configure mock: IsPlayerNameExistsInDb returns false
        // so RegisterNewPlayer proceeds past the duplicate name check
        _mockPlayerMapper
            .Setup(m => m.IsPlayerNameExistsInDb(It.IsAny<string>()))
            .Returns(false);

        // Configure mock: AddNewPlayerIntoDb does nothing (no real DB)
        _mockPlayerMapper
            .Setup(m => m.AddNewPlayerIntoDb(It.IsAny<string>()));
    }

    [TestCase]
    public void RegisterNewPlayer_ShouldReturnPlayerWithCorrectAttributes()
    {
        // Act — pass mock so no real DB is accessed
        var player = Player.RegisterNewPlayer("Sachin", _mockPlayerMapper.Object);

        // Assert player attributes
        Assert.That(player,            Is.Not.Null);
        Assert.That(player.Name,       Is.EqualTo("Sachin"));
        Assert.That(player.Age,        Is.EqualTo(23));
        Assert.That(player.Country,    Is.EqualTo("India"));
        Assert.That(player.NoOfMatches, Is.EqualTo(30));
    }

    [TestCase]
    public void RegisterNewPlayer_ShouldCallIsPlayerNameExistsInDb()
    {
        Player.RegisterNewPlayer("Virat", _mockPlayerMapper.Object);

        // Verify DB check was called once
        _mockPlayerMapper.Verify(
            m => m.IsPlayerNameExistsInDb("Virat"),
            Times.Once);
    }

    [TestCase]
    public void RegisterNewPlayer_ShouldCallAddNewPlayerIntoDb()
    {
        Player.RegisterNewPlayer("Rohit", _mockPlayerMapper.Object);

        // Verify DB insert was called once
        _mockPlayerMapper.Verify(
            m => m.AddNewPlayerIntoDb("Rohit"),
            Times.Once);
    }

    // ExpectedException equivalent in NUnit — use Assert.Throws
    [TestCase]
    public void RegisterNewPlayer_EmptyName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Player.RegisterNewPlayer("", _mockPlayerMapper.Object));
    }

    [TestCase]
    public void RegisterNewPlayer_DuplicateName_ShouldThrowArgumentException()
    {
        // Configure mock to return true — simulates player already exists in DB
        _mockPlayerMapper
            .Setup(m => m.IsPlayerNameExistsInDb("Duplicate"))
            .Returns(true);

        Assert.Throws<ArgumentException>(() =>
            Player.RegisterNewPlayer("Duplicate", _mockPlayerMapper.Object));
    }
}
