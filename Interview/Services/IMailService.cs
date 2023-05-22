using Interview.Models;

namespace Interview.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
     
    }
}
