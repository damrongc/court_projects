using CourtJustice.Domain.Models;
namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ICompanyResultCodeRepository
    {
        Task<List<CompanyResultCode>> GetAll();
        Task Create(CompanyResultCode model);
        Task Update(string id, CompanyResultCode model);
        Task Delete(string id);
        Task<CompanyResultCode> GetByKey(string id);
    }
}

