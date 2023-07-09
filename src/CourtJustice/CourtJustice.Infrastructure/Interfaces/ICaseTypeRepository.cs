using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ICaseTypeRepository
    {
        Task<List<CaseType>> GetAll();
        Task Create(CaseType model);
        Task Update(string id, CaseType model);
        Task Delete(string id);
        Task<CaseType> GetByKey(string id);
    }
}

