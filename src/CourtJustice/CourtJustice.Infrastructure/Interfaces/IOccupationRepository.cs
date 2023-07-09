using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IOccupationRepository
    {
        Task<List<Occupation>> GetAll();
        Task Create(Occupation model);
        Task Update(int id, Occupation model);
        Task Delete(int id);
        Task<Occupation> GetByKey(int id);
    }
}

