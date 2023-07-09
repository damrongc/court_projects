using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IAssetImageRepository
    {
        Task<List<AssetImage>> GetAll();
        Task Create(AssetImage model);
        Task Update(int id, AssetImage model);
        Task Delete(int id);
        Task<AssetImage> GetByKey(int id);

        List<AssetImage> GetByAssetId(string id);
    }
}

