using CustomerCommLib;
using Moq;
using NUnit.Framework;

namespace CustomerComm.Tests;

// TestFixture marks this class as a test class
[TestFixture]
public class CustomerCommTests
{
    private Mock<IMailSender> _mockMailSender = null!;
    private CustomerCommLib.CustomerComm _customerComm = null!;

    // SetUp runs before EACH test — fresh mock per test prevents call count accumulation
    [SetUp]
    public void Init()
    {
        // Create mock object of IMailSender — no real SMTP server needed
        _mockMailSender = new Mock<IMailSender>();

        // Configure mock: SendMail() accepts any two strings and always returns true
        _mockMailSender
            .Setup(m => m.SendMail(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        // Inject mock via constructor (Constructor Injection)
        _customerComm = new CustomerCommLib.CustomerComm(_mockMailSender.Object);
    }

    // TestCase provides inline test data
    [TestCase]
    public void SendMailToCustomer_ShouldReturnTrue()
    {
        // Act
        var result = _customerComm.SendMailToCustomer();

        // Assert — verify return value is true
        Assert.That(result, Is.True);

        // Verify that SendMail was called exactly once with any two strings
        _mockMailSender.Verify(
            m => m.SendMail(It.IsAny<string>(), It.IsAny<string>()),
            Times.Once);
    }

    [TestCase]
    public void SendMailToCustomer_MockNeverHitsRealSmtp()
    {
        // This test proves isolation — mock intercepts the call, no real email sent
        Assert.DoesNotThrow(() => _customerComm.SendMailToCustomer());
    }
}
