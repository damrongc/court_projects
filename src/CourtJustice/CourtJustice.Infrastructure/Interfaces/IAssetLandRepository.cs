using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IAssetLandRepository
    {
        Task<List<AssetLand>> GetAll();
        Task Create(AssetLand model);
        Task Update(string id, AssetLand model);
        Task Delete(string id);
        Task<AssetLand> GetByKey(string id);
        Task<List<AssetLandViewModel>> GetByCusId(string id);

        Task<IEnumerable<AssetLandViewModel>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);

        bool IsExisting(string id);
    }
}

