using Api.Models;

namespace Api.Repository.Interface
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Topic>> GetAllTopicsAsync();
        Task<Topic> GetTopicByIdAsync(int id);
        Task AddTopicAsync(Topic topic);
        Task UpdateTopicAsync(Topic topic);
        Task DeleteTopicAsync(int id);
    }
}
