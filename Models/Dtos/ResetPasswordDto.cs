using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        //[Required]
        //[MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
