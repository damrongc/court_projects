using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ILoaneeRemarkRepository
    {
        Task Create(LoaneeRemark model);
        Task Update(int id, LoaneeRemark model);
        Task Delete(int id);
        Task<LoaneeRemarkViewModel> GetByKey(int id);
        Task<List<LoaneeRemarkViewModel>> GetByCusId(string id);
        bool IsExisting(int id);
        //Task<int> CountByPersonCode(string personCode);
        Task BulkInsertOrUpdate(List<LoaneeRemarkExcelViewModel> loaneeRemarks);
        bool BankPersonCodeIsExist(int id);
        bool BankActionCodeIsExist(int id);
        bool BankResultCodeIsExist(int id);
        bool CompanyActionCodeIsExist(int id);
        bool CompanyResultCodeIsExist(int id);

    }
}

