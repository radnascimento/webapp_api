using Api.Models;

namespace Api.Services.Interface
{
    public interface IStudyReviewService
    {
        Task<IEnumerable<StudyReview>> GetAllStudyReviewsAsync();
    }
}
