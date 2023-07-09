using CourtJustice.Domain.Models;


namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IAssetTypeRepository
    {
        Task<List<AssetType>> GetAll();
        Task Create(AssetType model);
        Task Update(int id, AssetType model);
        Task Delete(int id);
        Task<AssetType> GetByKey(int id);
    }
}

