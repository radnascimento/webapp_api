using Api.Models;

namespace Api.Services.Interface
{
    public interface IApplicationConfigService
    {
        Task<IEnumerable<ApplicationConfig>> GetAllApplicationConfigsAsync();
        Task<ApplicationConfig> GetApplicationConfigByIdAsync(int id);
        Task AddApplicationConfigAsync(ApplicationConfig applicationConfig);
        Task UpdateApplicationConfigAsync(ApplicationConfig applicationConfig);
        Task DeleteApplicationConfigAsync(int id);
    }
}
