using Api.Enums;

namespace Api.Models
{
    public class Study
    {
        public int IdStudy { get; set; }
        public int IdTopic { get; set; }  
        public string Note { get; set; }

        public Topic Topic { get; set; }

        public DateTime OperationDate { get; set; } // Add the OperationDate field
    }
}
