using Api.Models;

namespace Api.Repository.Interface
{
    public interface IStudyReviewRepository
    {
        Task<StudyReview> GetStudyReviewByIdAsync(int id);
        Task<IEnumerable<StudyReview>> GetAllStudyReviewsAsync();
        Task<IEnumerable<StudyReview>> GetStudyReviewsAsync(
            int? idStudy = null,
            int? idStudyPC = null,
            DateTime? operationDate = null,
            int page = 1,
            int pageSize = 10
        );

        Task<IEnumerable<StudyReview>> GetStudyReviewsByStudyIdAsync(int studyId);
        Task AddStudyReviewAsync(StudyReview studyReview);
        Task AddStudyReviewsAsync(IEnumerable<StudyReview> studyReviews);
        Task UpdateStudyReviewAsync(StudyReview studyReview);
        Task DeleteStudyReviewAsync(int id);
    }

}
