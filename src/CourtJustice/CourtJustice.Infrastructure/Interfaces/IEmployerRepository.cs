using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IEmployerRepository
    {
        Task<List<Employer>> GetAll();
        Task<List<Employer>> GetAllActive();
        Task Create(Employer model);
        Task Update(string id, Employer model);
        Task Delete(string id);
        Task<Employer> GetByKey(string id);
    }
}

