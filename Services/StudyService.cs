using Api.Models;
using Api.Repository.Interface;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace Api.Services
{
    public class StudyService : IStudyService
    {
        private readonly IStudyRepository _studyRepository;


        private readonly IStudyReviewRepository _studyReviewRepository;

        public StudyService(IStudyRepository studyRepository, IStudyReviewRepository studyReviewRepository)
        {
            _studyRepository = studyRepository;
            _studyReviewRepository = studyReviewRepository;
        }

        public async Task<Study> GetStudyByIdAsync(int id)
        {
            return await _studyRepository.GetStudyByIdAsync(id);
        }

        public async Task<IEnumerable<Study>> GetAllStudiesAsync()
        {
            return await _studyRepository.GetAllStudiesAsync();
        }
        public async Task<IEnumerable<Study>> GetStudiesAsync(int? idStudy = null, int? idTopic = null, string note = null, DateTime? operationDate = null, int page = 1, int pageSize = 10, string idUser = null)
        {
            return await _studyRepository.GetStudiesAsync(idStudy, idTopic, note, operationDate, page, pageSize, idUser);
        }


        public async Task<IEnumerable<Study>> GetStudiesByTopicIdAsync(int topicId)
        {
            return await _studyRepository.GetStudiesByTopicIdAsync(topicId);
        }

        public async Task AddStudyAsync(Study study)
        {
            await _studyRepository.AddStudyAsync(study);

            var reviews = new List<StudyReview>();
            var daysToAdd = new[] { 1, 4, 7, 14, 30 };

            for (int i = 0; i < 5; i++)
            {
                reviews.Add(new StudyReview
                {
                    IdStudy = study.IdStudy,
                    OperationDate = DateTime.UtcNow.AddDays(daysToAdd[i]),
                    IdStudyPC = study.IdStudyPC,
                });

                
            }

            await _studyReviewRepository.AddStudyReviewsAsync(reviews);

        }

        public async Task UpdateStudyAsync(Study study)
        {
            await _studyRepository.UpdateStudyAsync(study);
        }

        public async Task DeleteStudyAsync(int id)
        {
            await _studyRepository.DeleteStudyAsync(id);
        }
    }
}
