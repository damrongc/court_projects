using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ICompanyActionCodeRepository
    {
        Task<List<CompanyActionCode>> GetAll();
        Task Create(CompanyActionCode model);
        Task Update(string id, CompanyActionCode model);
        Task Delete(string id);
        Task<CompanyActionCode> GetByKey(string id);
    }
}

