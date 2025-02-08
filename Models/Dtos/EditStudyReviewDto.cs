namespace Api.Models.Dtos
{
    public class EditStudyReviewDto
    {
        public int IdStudyReview { get; set; }
        public int IdStudy { get; set; }
        public DateTime OperationDate { get; set; }
        public int IdStudyPC { get; set; }
    }
}
