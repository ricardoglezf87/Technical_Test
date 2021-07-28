using System.Threading.Tasks;

namespace Technical_Test.Mailing
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
