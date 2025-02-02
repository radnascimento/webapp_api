using Api.Models;

namespace Api.Repository.Interface
{
    public interface IApplicationConfigRepository
    {
        Task<IEnumerable<ApplicationConfig>> GetAllApplicationConfigsAsync();
        Task<ApplicationConfig> GetApplicationConfigByIdAsync(int id);
        Task AddApplicationConfigAsync(ApplicationConfig applicationConfig);
        Task UpdateApplicationConfigAsync(ApplicationConfig applicationConfig);
        Task DeleteApplicationConfigAsync(int id);
    }
}
