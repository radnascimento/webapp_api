namespace Api.Models.Filters
{
    public class StudyFilter
    {
        public int? IdStudy { get; set; }
        public int? IdTopic { get; set; }
        public string? Note { get; set; }
        public DateTime? OperationDate { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
