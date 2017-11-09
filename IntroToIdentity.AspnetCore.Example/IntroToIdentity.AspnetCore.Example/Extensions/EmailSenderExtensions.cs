using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace IntroToIdentity.AspnetCore.Example.Services
{
    public static class EmailSenderExtensions
    {
        /// <exception cref="ArgumentNullException"><paramref name="value">value</paramref> is null.</exception>
        /// <exception cref="ArgumentException">The <see cref="M:System.Text.Encodings.Web.TextEncoder.TryEncodeUnicodeScalar(System.Int32,System.Char*,System.Int32,System.Int32@)"></see> method failed. The encoder does not implement <see cref="P:System.Text.Encodings.Web.TextEncoder.MaxOutputCharactersPerInputCharacter"></see> correctly.</exception>
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
