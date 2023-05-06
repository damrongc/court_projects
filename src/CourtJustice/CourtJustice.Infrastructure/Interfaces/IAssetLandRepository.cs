using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IAssetLandRepository
	{
        Task<List<AssetLand>> GetAll();
        Task Create(AssetLand model);
        Task Update(int id, AssetLand model);
        Task Delete(int id);
        Task<AssetLand> GetByKey(int id);

        Task<IEnumerable<AssetLand>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);
    }
}

