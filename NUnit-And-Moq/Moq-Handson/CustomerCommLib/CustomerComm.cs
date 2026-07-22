namespace CustomerCommLib;

// Class Under Test
// IMailSender injected via constructor — enables passing a mock in unit tests
public class CustomerComm
{
    private readonly IMailSender _mailSender;

    // Constructor Injection — dependency injected at creation time
    public CustomerComm(IMailSender mailSender)
    {
        _mailSender = mailSender;
    }

    public bool SendMailToCustomer()
    {
        // Actual logic — calls injected IMailSender
        // In unit tests, this calls the mock (no real email sent)
        _mailSender.SendMail("cust123@abc.com", "Some Message");
        return true;
    }
}
