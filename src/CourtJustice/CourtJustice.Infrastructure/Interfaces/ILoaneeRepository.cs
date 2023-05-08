using System;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ILoaneeRepository
	{
        Task<IEnumerable<LoaneeViewModel>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);

        //Task<List<Loanee>> GetAll();
        Task Create(Loanee model);
        Task Update(int id, Loanee model);
        Task Delete(string id);
        Task<LoaneeViewModel> GetByKey(string id);
    }
}

