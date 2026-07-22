using MagicFilesLib;
using Moq;
using NUnit.Framework;

namespace DirectoryExplorer.Tests;

[TestFixture]
public class DirectoryExplorerTests
{
    private Mock<IDirectoryExplorer> _mockExplorer = null!;

    // Hardcoded test file names — no real file system needed
    private readonly string _file1 = "file.txt";
    private readonly string _file2 = "file2.txt";

    [OneTimeSetUp]
    public void Init()
    {
        _mockExplorer = new Mock<IDirectoryExplorer>();

        // Configure mock to return hardcoded file list for any path
        _mockExplorer
            .Setup(e => e.GetFiles(It.IsAny<string>()))
            .Returns(new List<string> { _file1, _file2 });
    }

    [TestCase]
    public void GetFiles_ShouldReturnMockedFileList()
    {
        // Act — call mock (no real disk access)
        var result = _mockExplorer.Object.GetFiles("C:\\SomePath");

        // Assert: collection is not null
        Assert.That(result, Is.Not.Null);

        // Assert: collection count equals 2
        Assert.That(result.Count, Is.EqualTo(2));

        // Assert: collection contains _file1
        Assert.That(result, Contains.Item(_file1));
    }

    [TestCase]
    public void GetFiles_ShouldContainFile2()
    {
        var result = _mockExplorer.Object.GetFiles("/any/path");
        Assert.That(result, Contains.Item(_file2));
    }
}
