using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ILawyerRepository
    {
        Task<List<Lawyer>> GetAll();
        Task Create(Lawyer model);
        Task Update(string id, Lawyer model);
        Task Delete(string id);
        Task<Lawyer> GetByKey(string id);
        Task<int> CheckExistingAtUser(string id);
    }
}
