using Api.Data;
using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class LevelRepository : ILevelRepository
    {
        private readonly AppDbContext _context;

        public LevelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Level>> GetAllLevelsAsync()
        {
            return await _context.Levels.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<Level> GetLevelByIdAsync(int id)
        {
            return await _context.Levels.FindAsync(id);
        }

        public async Task AddLevelAsync(Level level)
        {
            await _context.Levels.AddAsync(level);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLevelAsync(Level level)
        {
            _context.Levels.Update(level);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLevelAsync(int id)
        {
            var level = await _context.Levels.FindAsync(id);
            if (level != null)
            {
                _context.Levels.Remove(level);
                await _context.SaveChangesAsync();
            }
        }
    }
}
