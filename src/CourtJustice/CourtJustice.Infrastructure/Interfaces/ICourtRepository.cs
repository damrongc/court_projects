
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ICourtRepository
	{
        Task<IEnumerable<Court>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);
        Task<List<Court>> GetAll();
        Task Create(Court model);
        Task Update(string id, Court model);
        Task Delete(string id);
        Task<Court> GetByKey(string id);
    }
}

