using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ILoaneeRemarkRepository
    {

        //Task<List<LoaneeRemarkViewModel>> GetAll();
        Task Create(LoaneeRemark model);
        Task Update(int id, LoaneeRemark model);
        Task Delete(int id);
        Task<LoaneeRemarkViewModel> GetByKey(int id);
        Task<List<LoaneeRemarkViewModel>> GetByCusId(string id);
        bool IsExisting(int id);

        Task BulkInsertOrUpdate(List<LoaneeRemarkExcelViewModel> loaneeRemarks);
    }
}

