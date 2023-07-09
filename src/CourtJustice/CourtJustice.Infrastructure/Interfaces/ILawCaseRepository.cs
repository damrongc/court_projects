using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ILawCaseRepository
    {
        Task<List<LawCase>> GetAll();
        Task Create(LawCase model);
        Task Update(string id, LawCase model);
        Task Delete(string id);
        Task<LawCase> GetByKey(string id);
    }
}

