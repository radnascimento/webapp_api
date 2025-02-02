using Api.Data;
using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class ApplicationConfigRepository : IApplicationConfigRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationConfigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationConfig>> GetAllApplicationConfigsAsync()
        {
            return await _context.ApplicationConfig.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<ApplicationConfig> GetApplicationConfigByIdAsync(int id)
        {
            return await _context.ApplicationConfig.FindAsync(id);
        }

        public async Task AddApplicationConfigAsync(ApplicationConfig applicationConfig)
        {
            await _context.ApplicationConfig.AddAsync(applicationConfig);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateApplicationConfigAsync(ApplicationConfig applicationConfig)
        {
            _context.ApplicationConfig.Update(applicationConfig);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteApplicationConfigAsync(int id)
        {
            var applicationConfig = await _context.ApplicationConfig.FindAsync(id);
            if (applicationConfig != null)
            {
                _context.ApplicationConfig.Remove(applicationConfig);
                await _context.SaveChangesAsync();
            }
        }
    }
}
