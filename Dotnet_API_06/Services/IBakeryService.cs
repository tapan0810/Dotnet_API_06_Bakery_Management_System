using Dotnet_API_06.Entities.Dtos;
using Dotnet_API_06.Entities.Models;

namespace Dotnet_API_06.Services
{
    public interface IBakeryService
    {
        public Task<List<GetAllBakeryDto>> GetallBackery();
        public Task<GetBakeryByIdDto?> GetBackeryById(int id);
        public Task<GetBakeryByIdDto?> CreateBackery(CreateBakeryDto createBakery);
        public Task<bool> UpdateBakery(int id, UpdateBakeryDto updateBakery);
        public Task<bool> DeleteBakery(int id);
            
    }
}
