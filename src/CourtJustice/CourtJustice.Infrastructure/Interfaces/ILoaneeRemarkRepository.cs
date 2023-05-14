using System;
using CourtJustice.Domain;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ILoaneeRemarkRepository
	{

        Task<List<LoaneeRemark>> GetAll();
        Task Create(LoaneeRemark model);
        Task Update(int id, LoaneeRemark model);
        Task Delete(int id);
        Task<LoaneeRemark> GetByKey(int id);
        Task<List<LoaneeRemarkViewModel>> GetByCusId(int id);

        Task<IEnumerable<LoaneeRemarkViewModel>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);

        bool IsExisting(int id);
    }
}

