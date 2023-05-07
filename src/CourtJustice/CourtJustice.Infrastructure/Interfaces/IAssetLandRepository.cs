using System;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IAssetLandRepository
	{
        Task<List<AssetLand>> GetAll();
        Task Create(AssetLand model);
        Task Update(int id, AssetLand model);
        Task Delete(int id);
        Task<AssetLand> GetByKey(int id);

        Task<IEnumerable<AssetLandViewModel>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);
    }
}

