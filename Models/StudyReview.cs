using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class StudyReview
    {
        [Key]
        public int IdStudyReview { get; set; }
        public int IdStudy { get; set; }
        
        public DateTime OperationDate { get; set; }

        public int IdStudyPC { get; set; }
        public StudyPC StudyPC { get; set; }
        public Study Study { get; set; }

        
    }
}
