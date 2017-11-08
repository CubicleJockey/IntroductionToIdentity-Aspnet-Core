using System.ComponentModel.DataAnnotations;

namespace IntroToIdentity.AspnetCore.Example.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
