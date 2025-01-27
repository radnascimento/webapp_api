using Api.Models;
using Api.Repository.Interface;

namespace Api.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _repository;

        public MaterialService(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            return await _repository.GetAllMaterialsAsync();
        }

        public async Task<Material> GetMaterialByIdAsync(int id)
        {
            return await _repository.GetMaterialByIdAsync(id);
        }

        public async Task AddMaterialAsync(Material material)
        {
            await _repository.AddMaterialAsync(material);
        }

        public async Task UpdateMaterialAsync(Material material)
        {
            await _repository.UpdateMaterialAsync(material);
        }

        public async Task DeleteMaterialAsync(int id)
        {
            await _repository.DeleteMaterialAsync(id);
        }
    }
}
