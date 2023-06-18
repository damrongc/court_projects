using System;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ILoaneeRepository
	{
        Task<IEnumerable<LoaneeViewModel>> GetPaging(int bucketId,string employerCode, int skip, int take, string filter);
        Task<int> GetRecordCount(int bucketId, string employerCode,string filter);

        //Task<List<Loanee>> GetAll();
        Task Create(Loanee model);
        Task Update(int id, Loanee model);
        Task Delete(string id);
        Task<LoaneeViewModel> GetByKey(string id);
        Task BulkInsertOrUpdate(List<LoaneeViewModel> loanees);

        bool IsExisting(string id);
    }
}

