namespace Api.Models.Dtos
{
    public class StudyDto
    {
        public int IdStudy { get; set; }
        public int IdTopic { get; set; }
        public string Note { get; set; }
        public DateTime OperationDate { get; set; } // Add the OperationDate field
    }
}
