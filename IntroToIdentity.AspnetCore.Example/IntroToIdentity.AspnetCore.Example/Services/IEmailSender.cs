using System.Threading.Tasks;

namespace IntroToIdentity.AspnetCore.Example.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
