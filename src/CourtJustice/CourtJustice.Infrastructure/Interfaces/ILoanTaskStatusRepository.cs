using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ILoanTaskStatusRepository
    {
        Task<List<LoanTaskStatus>> GetAll();
        Task Create(LoanTaskStatus model);
        Task Update(int id, LoanTaskStatus model);
        Task Delete(int id);
        Task<LoanTaskStatus> GetByKey(int id);

        Task<int> CheckExistingAtSub(int id);
    }
}

