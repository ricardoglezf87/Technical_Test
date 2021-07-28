using System.ComponentModel.DataAnnotations;

namespace Technical_Test.Identity.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
