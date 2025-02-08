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

        public async Task AddStudyReviewAsync(StudyReview studyReview)
        {
            throw new NotImplementedException();
        }

        public async Task AddStudyReviewsAsync(IEnumerable<StudyReview> studyReviews)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteStudyReviewAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<StudyReview> GetStudyReviewByIdAsync(int id)
        {
            return await _studyReviewRepository.GetStudyReviewByIdAsync(id);
        }

        public async Task<IEnumerable<StudyReview>> GetStudyReviewsAsync(int? idStudy, int? idStudyPC, DateTime? operationDate, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StudyReview>> GetStudyReviewsByStudyIdAsync(int studyId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateStudyReviewAsync(StudyReview studyReview)
        {
            await _studyReviewRepository.UpdateStudyReviewAsync(studyReview);
        }
    }
}
