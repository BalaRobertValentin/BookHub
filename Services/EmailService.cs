using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("BookRental SRL", "gowpiratu2015@gmail.com")); // replace with your sending email
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls); // replace with your SMTP server details
            await client.AuthenticateAsync("balaaurora1975@gmail.com", "mkzcrnfbonkzelle"); // replace with your email and password
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}