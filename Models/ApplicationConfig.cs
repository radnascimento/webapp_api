using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class ApplicationConfig
    {
        [Key]
        public int IdApplicationConfig { get; set; }
        public string Name { get; set; }
        public string JsonContent { get; set; }

        public DateTime OperationDate { get; set; }
    }
}
