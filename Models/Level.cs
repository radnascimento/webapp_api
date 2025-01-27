using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Level
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Material> Materials { get; set; }
    }
}
