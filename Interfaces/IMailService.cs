using EmailSender.Models;

namespace EmailSender.Interfaces
{
    public interface IMailService
    {
        Task<MailResponse> SendMailToCustomers(CustomersDTO customer);
        Task<MailResponse> SendEmail(MailSenderDTO request);
    }
}

