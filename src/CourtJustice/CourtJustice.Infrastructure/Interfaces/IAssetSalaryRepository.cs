
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IAssetSalaryRepository
	{
        Task<List<AssetSalary>> GetAll();
        Task Create(AssetSalary model);
        Task Update(int id, AssetSalary model);
        Task Delete(int id);
        Task<AssetSalary> GetByKey(int id);

        Task<IEnumerable<AssetSalary>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);
    }
}

