using Api.Models;
using Api.Repository.Interface;
using Api.Services.Interface;

namespace Api.Services
{
    public class ApplicationConfigService : IApplicationConfigService
    {
        private readonly IApplicationConfigRepository _repository;

        // Inject the repository via constructor
        public ApplicationConfigService(IApplicationConfigRepository repository)
        {
            _repository = repository;
        }

        // Get all application configurations
        public async Task<IEnumerable<ApplicationConfig>> GetAllApplicationConfigsAsync()
        {
            return await _repository.GetAllApplicationConfigsAsync();
        }

        // Get a specific application configuration by ID
        public async Task<ApplicationConfig> GetApplicationConfigByIdAsync(int id)
        {
            return await _repository.GetApplicationConfigByIdAsync(id);
        }

        // Add a new application configuration
        public async Task AddApplicationConfigAsync(ApplicationConfig applicationConfig)
        {
            await _repository.AddApplicationConfigAsync(applicationConfig);
        }

        // Update an existing application configuration
        public async Task UpdateApplicationConfigAsync(ApplicationConfig applicationConfig)
        {
            await _repository.UpdateApplicationConfigAsync(applicationConfig);
        }

        // Delete an application configuration by ID
        public async Task DeleteApplicationConfigAsync(int id)
        {
            await _repository.DeleteApplicationConfigAsync(id);
        }
    }
}