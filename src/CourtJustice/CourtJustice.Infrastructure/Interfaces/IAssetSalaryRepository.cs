
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IAssetSalaryRepository
	{
        Task<List<AssetSalary>> GetAll();
        Task Create(AssetSalary model);
        Task Update(int id, AssetSalary model);
        Task Delete(int id);

        Task<AssetSalary> GetByKey(int id);
        Task<List<AssetSalaryViewModel>> GetByCusId(string id);
        bool IsExisting(int id);

        Task<IEnumerable<AssetSalaryViewModel>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);
    }
}

