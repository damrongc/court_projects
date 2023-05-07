using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ILoanTypeRepository
    {
        Task<List<LoanType>> GetAll();
        Task Create(LoanType model);
        Task Update(string id, LoanType model);
        Task Delete(string id);
        Task<LoanType> GetByKey(string id);
    }
}
