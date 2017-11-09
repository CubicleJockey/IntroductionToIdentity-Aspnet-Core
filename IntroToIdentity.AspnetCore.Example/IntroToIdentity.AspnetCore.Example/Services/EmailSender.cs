using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IntroToIdentity.AspnetCore.Example.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    // ref: https://dotnetcoretutorials.com/2017/08/20/sending-email-net-core-2-0/
    public class EmailSender : IEmailSender
    {
        /// <exception cref="FormatException"><paramref name="address">address</paramref> is not in a recognized format.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="FormatException"><paramref name="address">address</paramref> is not in a recognized format.</exception>
        /// <exception cref="InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient"></see> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync"></see> call in progress.   -or-  <see cref="P:System.Net.Mail.MailMessage.From"></see> is null.   -or-   There are no recipients specified in <see cref="P:System.Net.Mail.MailMessage.To"></see>, <see cref="P:System.Net.Mail.MailMessage.CC"></see>, and <see cref="P:System.Net.Mail.MailMessage.Bcc"></see> properties.   -or-  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod"></see> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network"></see> and <see cref="P:System.Net.Mail.SmtpClient.Host"></see> is null.   -or-  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod"></see> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network"></see> and <see cref="P:System.Net.Mail.SmtpClient.Host"></see> is equal to the empty string ("").   -or-  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod"></see> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network"></see> and <see cref="P:System.Net.Mail.SmtpClient.Port"></see> is zero, a negative number, or greater than 65,535.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="message">message</paramref> is null.</exception>
        /// <exception cref="SmtpFailedRecipientsException">The <paramref name="message">message</paramref> could not be delivered to one or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To"></see>, <see cref="P:System.Net.Mail.MailMessage.CC"></see>, or <see cref="P:System.Net.Mail.MailMessage.Bcc"></see>.</exception>
        /// <exception cref="SmtpException">The connection to the SMTP server failed.   -or-   Authentication failed.   -or-   The operation timed out.   -or-  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl"></see> is set to true but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod"></see> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory"></see> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis"></see>.   -or-  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl"></see> is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.</exception>
        /// <exception cref="ArgumentException">Cannot be empty.</exception>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            const string ERRORMESSAGE = "Cannot be empty.";
            if(string.IsNullOrWhiteSpace(email)) { throw new ArgumentException(ERRORMESSAGE, nameof(email)); }
            if(string.IsNullOrWhiteSpace(subject)) { throw new ArgumentException(ERRORMESSAGE, nameof(subject)); }
            if(string.IsNullOrWhiteSpace(message)) { throw new ArgumentException(ERRORMESSAGE, nameof(message)); }

            var client = new SmtpClient(@"localhost\localSMTP")
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("localSMTP", "PoopyPants")
            };

            using (client)
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("sender@localSMTP.com"),
                    Subject = subject,
                    Body = message
                };
                mailMessage.To.Add(email);
                client.Send(mailMessage);
            }
            return Task.CompletedTask;
        }
    }
}
