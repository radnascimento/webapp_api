using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime OperationDate { get; set; } // Add the OperationDate field

        public ICollection<Material> Materials { get; set; }
        public ICollection<Study> Studies { get; set; }

        public string IdUser { get; set; }
        public ApplicationUser User { get; set; }


    }
}
