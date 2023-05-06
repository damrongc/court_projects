using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ILoaneeRepository
	{
        Task<IEnumerable<Loanee>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);

        Task<List<Loanee>> GetAll();
        Task Create(Loanee model);
        Task Update(int id, Loanee model);
        Task Delete(string id);
        Task<Loanee> GetByKey(string id);
    }
}

