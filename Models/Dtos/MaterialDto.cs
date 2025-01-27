namespace Api.Models.Dtos
{
    public class MaterialDto
    {
        public int IdMaterial { get; set; }
        public int IdTopic { get; set; }
        public int IdLevel { get; set; }
        public string Url { get; set; }

        public DateTime OperationDate { get; set; } // Add the OperationDate field
    }
}
