using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IBankResultCodeRepository
    {
        Task<List<BankResultCode>> GetAll();
        Task<List<BankResultCode>> GetByEmployer(string employerCode);
        Task Create(BankResultCode model);
        Task Update(string id, BankResultCode model);
        Task Delete(string id);
        Task<BankResultCode> GetByKey(string id);
    }
}

