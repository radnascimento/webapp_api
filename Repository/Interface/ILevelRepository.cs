using Api.Models;

namespace Api.Repository.Interface
{
    public interface ILevelRepository
    {
        Task<IEnumerable<Level>> GetAllLevelsAsync();
        Task<Level> GetLevelByIdAsync(int id);
        Task AddLevelAsync(Level level);
        Task UpdateLevelAsync(Level level);
        Task DeleteLevelAsync(int id);
    }
}
