using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IBankResultCodeRepository
    {
        Task<List<BankResultCodeViewModel>> GetAll();
        Task<List<BankResultCodeViewModel>> GetByBankPersonId(int bankPersonId);
        //Task<List<BankResultCodeViewModel>> GetByEmployer(string employerCode);
        //Task<int> CountByEmployerAndCode(string employerCode, string resultCodeId);
        //Task<BankResultCodeViewModel> GetByEmployerAndCode(string employerCode, string resultCodeId);
        Task Create(BankResultCode model);
        Task Update(int id,BankResultCode model);
        Task Delete(int id);
        Task<BankResultCode> GetByKey(int id);
        Task DeleteByBankPersonId(int bankPersonId);

    }
}

