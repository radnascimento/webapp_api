using Api.Data;
using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly AppDbContext _context;

        public MaterialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            return await _context.Materials
                .Include(m => m.Topic)
                .Include(m => m.Level)
                .OrderBy(m => m.IdTopic)
                .ToListAsync();
        }

        public async Task<Material> GetMaterialByIdAsync(int id)
        {
            return await _context.Materials
                .Include(m => m.Topic)
                .Include(m => m.Level)
                .FirstOrDefaultAsync(m => m.IdMaterial == id);
        }

        public async Task AddMaterialAsync(Material material)
        {
            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMaterialAsync(Material material)
        {
            _context.Materials.Update(material);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMaterialAsync(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material != null)
            {
                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();
            }
        }
    }
}
