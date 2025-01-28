using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Models.Dtos
{
    public class TopicDto
    {
        [JsonIgnore]
        [BindNever]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        [JsonIgnore]
        [BindNever]
        public string? IdUser { get; set; }

        public DateTime OperationDate { get; set; } // Add the OperationDate field

    }
}
