using Microsoft.AspNetCore.Identity;

namespace Api.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ICollection<Study> Studies { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}
