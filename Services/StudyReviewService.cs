using Api.Models;
using Api.Repository.Interface;
using Api.Services.Interface;

namespace Api.Services
{
    public class StudyReviewService : IStudyReviewService
    {
        private readonly IStudyReviewRepository _studyReviewRepository;

        public StudyReviewService(IStudyReviewRepository studyReviewRepository)
        {
            _studyReviewRepository = studyReviewRepository;
        }


        public async Task<IEnumerable<StudyReview>> GetAllStudyReviewsAsync()
        {
            return await _studyReviewRepository.GetAllStudyReviewsAsync();
        }
    }
}
