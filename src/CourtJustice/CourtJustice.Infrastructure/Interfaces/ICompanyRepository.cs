using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAll();
        Task Create(Company model);
        Task Update(int id, Company model);
        Task Delete(int id);
        Task<Company> GetByKey(int id);
    }
}

