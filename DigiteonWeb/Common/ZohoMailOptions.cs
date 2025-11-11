using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DigiteonWeb.Common
{
    public class ZohoMailOptions
    {
        public string FromName { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = "smtp.zoho.com";
        public int Port { get; set; } = 587;
        public bool UseSsl { get; set; } = false;       // for 465
        public bool UseStartTls { get; set; } = true;   // for 587
    }

    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string htmlBody, string? replyTo = null);
    }

    public class ZohoEmailService : IEmailService
    {
        private readonly ZohoMailOptions _opts;

        public ZohoEmailService(IOptions<ZohoMailOptions> opts)
        {
            _opts = opts.Value;
        }

        public async Task SendAsync(string toEmail, string subject, string htmlBody, string? replyTo = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_opts.FromName, _opts.FromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = htmlBody };
            message.Body = builder.ToMessageBody();

            if (!string.IsNullOrWhiteSpace(replyTo))
            {
                message.ReplyTo.Add(MailboxAddress.Parse(replyTo));
            }

            using var smtp = new SmtpClient();
            try
            {
                var secure = _opts.UseSsl
                    ? SecureSocketOptions.SslOnConnect
                    : (_opts.UseStartTls ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);

                //await smtp.ConnectAsync(_opts.Host, _opts.Port, secure);
                //await smtp.AuthenticateAsync(_opts.Username, _opts.Password);
                //await smtp.SendAsync(message);
            }
            finally
            {
                //await smtp.DisconnectAsync(true);
            }
        }
    }
}
