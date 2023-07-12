using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IBankActionCodeRepository
    {
        Task<List<BankActionCodeViewModel>> GetAll();
        Task<List<BankActionCodeViewModel>> GetByEmployer(string employerCode);
        Task<int> CountByEmployerAndCode(string employerCode, string actionCodeId);
        Task<BankActionCodeViewModel> GetByEmployerAndCode(string employerCode, string actionCodeId);
        Task Create(BankActionCode model);
        Task Update(BankActionCode model);
        Task Delete(int id);
        Task<BankActionCode> GetByKey(int id);
    }
}

