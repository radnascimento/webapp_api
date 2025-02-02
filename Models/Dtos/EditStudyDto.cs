using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Models.Dtos
{
    public class EditStudyDto
    {
        
        public int IdStudy { get; set; }
        public int IdTopic { get; set; }
        public int IdStudyPC { get; set; }
        
        public string Note { get; set; }
        public DateTime OperationDate { get; set; } // Add the OperationDate field

        [JsonIgnore]
        [BindNever]
        public string? IdUser { get; set; }

    }
}
