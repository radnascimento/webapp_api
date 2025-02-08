using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dtos
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Url { get; set; }

    }

}
