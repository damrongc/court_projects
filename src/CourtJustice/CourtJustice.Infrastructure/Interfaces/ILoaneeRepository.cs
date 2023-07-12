using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ILoaneeRepository
    {
        Task<IEnumerable<LoaneeViewModel>> GetPaging(int loanTaskStatusId, string employerCode, List<string> employeeCodes, int skip, int take, string filter);
        Task<int> GetRecordCount(int loanTaskStatusId, string employerCode, List<string> employeeCodes, string filter);
        Task Create(Loanee model);
        Task Update(int id, Loanee model);
        Task Delete(string id);
        Task<LoaneeViewModel> GetByKey(string id);
        Task BulkInsertOrUpdate(List<LoaneeViewModel> loanees);
        bool IsExisting(string id);
        Task UpdateOrAssign(LoaneeViewModel model);
        Task UpdateLoaneeByCollector(LoaneeViewModel model);
        Task UpdateContractNo(string id ,string followContractNo);

        Task DeActivate(List<LoaneeViewModel> loanees);
    }
}

