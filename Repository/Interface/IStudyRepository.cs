using Api.Models;

namespace Api.Repository.Interface
{
    public interface IStudyRepository
    {
        Task<Study> GetStudyByIdAsync(int id);
        Task<IEnumerable<Study>> GetAllStudiesAsync();
        Task<IEnumerable<Study>> GetStudiesAsync(int? idStudy = null, int? idTopic = null, string note = null, DateTime? operationDate = null, int page = 1, int pageSize = 10, string idUser = null);

        Task<IEnumerable<Study>> GetStudiesByTopicIdAsync(int topicId);
        Task AddStudyAsync(Study study);
        Task UpdateStudyAsync(Study study);
        Task DeleteStudyAsync(int id);
    }
}
