using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ICaseResultRepository
    {

        Task<List<CaseResult>> GetAll();
        Task Create(CaseResult model);
        Task Update(int id, CaseResult model);
        Task Delete(int id);
        Task<CaseResult> GetByKey(int id);
    }
}

