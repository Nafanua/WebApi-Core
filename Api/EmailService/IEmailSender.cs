using RssCrawleraApi.EmailService;
using System.Threading.Tasks;

namespace RssCrawleraApi.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string recepientName, string recepientEmail, string subject, string body, SmtpConfig config = null, bool isHtml = true);
    }
}
