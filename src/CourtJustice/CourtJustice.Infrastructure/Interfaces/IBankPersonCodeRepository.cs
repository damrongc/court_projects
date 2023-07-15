using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IBankPersonCodeRepository
    {
        Task<List<BankPersonCodeViewModel>> GetByBankActionId(int bankActionId);
        Task Create(BankPersonCode model);
        Task Update(int id,BankPersonCode model);
        Task Delete(int id);
        Task<BankPersonCodeViewModel> GetByKey(int id);
        bool IsExisting(int bankActionId, string bankPersonCodeId);
    }
}
