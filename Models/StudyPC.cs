using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class StudyPC
    {
        [Key]
        public int IdStudyPC { get; set; }
        public string Name { get; set; }

        public ICollection<Study> Studies { get; set; }

        public ICollection<StudyReview> StudyReviews { get; set; }


    }
}
