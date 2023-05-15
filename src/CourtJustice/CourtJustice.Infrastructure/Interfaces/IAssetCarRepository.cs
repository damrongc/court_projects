using System;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IAssetCarRepository
	{
        Task<List<AssetCar>> GetAll();
        Task Create(AssetCar model);
        Task Update(string id, AssetCar model);
        Task Delete(string id);
        Task<AssetCar> GetByKey(string id);

        Task<List<AssetCarViewModel>> GetByCusId(string id);

       /* Task<IEnumerable<AssetCarViewModel>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);*/

        bool IsExisting(string id);
    }
}

