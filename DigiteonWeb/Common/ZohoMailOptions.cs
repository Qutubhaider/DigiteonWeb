using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public class ZohoMailOptions
{
    public string FromName { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Host { get; set; } = "smtp.zoho.com";
    public int Port { get; set; } = 587;
    public bool UseSsl { get; set; } = false;
    public bool UseStartTls { get; set; } = true;
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
        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress(_opts.FromName, _opts.FromEmail));
        msg.To.Add(MailboxAddress.Parse(toEmail));
        msg.Subject = subject;
        msg.Body = new BodyBuilder { HtmlBody = htmlBody, TextBody = HtmlToPlain(htmlBody) }.ToMessageBody();
        if (!string.IsNullOrWhiteSpace(replyTo)) msg.ReplyTo.Add(MailboxAddress.Parse(replyTo));

        // Protocol log will be created in the app working directory: smtp.log
        using var smtp = new SmtpClient(new ProtocolLogger("smtp.log")) { Timeout = 30000 };

        // Don't allow XOAUTH2 (Zoho authenticates with username/password)
        smtp.AuthenticationMechanisms.Remove("XOAUTH2");

        // Optional: helpful for debugging TLS cert problems (keep strict in prod)
        smtp.ServerCertificateValidationCallback = (object sender, X509Certificate? certificate,
            X509Chain? chain, SslPolicyErrors sslPolicyErrors) =>
        {
            Console.WriteLine($"Server cert validation: {sslPolicyErrors}");
            return sslPolicyErrors == SslPolicyErrors.None;
        };

        try
        {
            //var secureOpt = _opts.UseSsl
            //    ? SecureSocketOptions.SslOnConnect      // for port 465
            //    : (_opts.UseStartTls ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto); // for 587

            // Connect
           // await smtp.ConnectAsync(_opts.Host, _opts.Port, secureOpt);

            // Authenticate with username (full email) and the Zoho App Password (if 2FA enabled)

            var secure = SecureSocketOptions.StartTls; // for port 587
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            await smtp.ConnectAsync("smtp.zoho.in", 587, secure);

            await smtp.AuthenticateAsync(_opts.Username, _opts.Password);

            await smtp.SendAsync(msg);
        }
        catch (MailKit.Security.SslHandshakeException ex)
        {
            throw new Exception("TLS handshake failed — try switching ports (587 vs 465) or check SSL inspection. See smtp.log", ex);
        }
        catch (MailKit.ServiceNotAuthenticatedException ex)
        {
            throw new Exception("Authentication failed — check username/password or use a Zoho App Password if 2FA is enabled. See smtp.log", ex);
        }
        catch (MailKit.CommandException ex)
        {
            throw new Exception("SMTP command rejected by server. Check the From email and SMTP permissions. See smtp.log", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("SMTP send failed — check network/firewall and smtp.log for details.", ex);
        }
        finally
        {
            if (smtp.IsConnected)
                await smtp.DisconnectAsync(true);
        }
    }

    static string HtmlToPlain(string html) =>
        System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", string.Empty).Trim();
}
