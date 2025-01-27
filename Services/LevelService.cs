using Api.Models;
using Api.Repository.Interface;

namespace Api.Services
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _repository;

        public LevelService(ILevelRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Level>> GetAllLevelsAsync()
        {
            return await _repository.GetAllLevelsAsync();
        }

        public async Task<Level> GetLevelByIdAsync(int id)
        {
            return await _repository.GetLevelByIdAsync(id);
        }

        public async Task AddLevelAsync(Level level)
        {
            await _repository.AddLevelAsync(level);
        }

        public async Task UpdateLevelAsync(Level level)
        {
            await _repository.UpdateLevelAsync(level);
        }

        public async Task DeleteLevelAsync(int id)
        {
            await _repository.DeleteLevelAsync(id);
        }
    }
}
