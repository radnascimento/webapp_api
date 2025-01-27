using Api.Models;
using Api.Repository.Interface;

namespace Api.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _repository;

        public TopicService(ITopicRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            return await _repository.GetAllTopicsAsync();
        }

        public async Task<Topic> GetTopicByIdAsync(int id)
        {
            return await _repository.GetTopicByIdAsync(id);
        }

        public async Task AddTopicAsync(Topic topic)
        {
            await _repository.AddTopicAsync(topic);
        }

        public async Task UpdateTopicAsync(Topic topic)
        {
            await _repository.UpdateTopicAsync(topic);
        }

        public async Task DeleteTopicAsync(int id)
        {
            await _repository.DeleteTopicAsync(id);
        }
    }
}
