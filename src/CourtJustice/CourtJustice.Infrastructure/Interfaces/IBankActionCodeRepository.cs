using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IBankActionCodeRepository
    {
        Task<List<BankActionCode>> GetAll();
        Task<List<BankActionCode>> GetByEmployer(string employerCode);
        Task Create(BankActionCode model);
        Task Update(string id, BankActionCode model);
        Task Delete(string id);
        Task<BankActionCode> GetByKey(string id);
    }
}

