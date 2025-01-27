using Api.Models;

namespace Api.Repository.Interface
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> GetAllMaterialsAsync();
        Task<Material> GetMaterialByIdAsync(int id);
        Task AddMaterialAsync(Material material);
        Task UpdateMaterialAsync(Material material);
        Task DeleteMaterialAsync(int id);
    }
}
