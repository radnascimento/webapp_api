using System.ComponentModel.DataAnnotations;
using Api.Enums;

namespace Api.Models
{
    public class Material
    {
        [Key]
        public int IdMaterial { get; set; }
        public int IdTopic { get; set; }  
        public int IdLevel { get; set; }  
        public string Url { get; set; }

        public Topic Topic { get; set; }
        public Level Level { get; set; }

        public DateTime OperationDate { get; set; } // Add the OperationDate field
    }
}
