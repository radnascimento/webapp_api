using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Api.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Study> Studies { get; set; }

    }
}
