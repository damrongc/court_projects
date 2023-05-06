using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ICourtRepository
	{
        Task<IEnumerable<Court>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);

        Task<List<Court>> GetAll();
        Task Create(Court model);
        Task Update(int id, Court model);
        Task Delete(int id);
        Task<Court> GetByKey(int id);
    }
}

