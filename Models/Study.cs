using Api.Enums;
using Microsoft.AspNetCore.Identity;

namespace Api.Models
{
    public class Study
    {
        public int IdStudy { get; set; }
        public int IdTopic { get; set; }
        public string Note { get; set; }
        
        public Topic Topic { get; set; }
        public DateTime OperationDate { get; set; }
        public string IdUser { get; set; }
        public ApplicationUser User { get; set; }

        public int IdStudyPC { get; set; }
        public StudyPC StudyPC { get; set; }
                       
        public ICollection<StudyReview> StudyReviews { get; set; }



    }
}
