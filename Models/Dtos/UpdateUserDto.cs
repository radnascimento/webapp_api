namespace Api.Models.Dtos
{
    public class UpdateUserDto
    {
        public string UserName { get; set; } // Required to find the user
        public string Email { get; set; }
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
