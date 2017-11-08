using System.ComponentModel.DataAnnotations;

namespace IntroToIdentity.AspnetCore.Example.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
