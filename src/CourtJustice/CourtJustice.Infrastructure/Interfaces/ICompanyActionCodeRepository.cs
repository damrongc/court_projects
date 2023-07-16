using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ICompanyActionCodeRepository
    {
        Task<List<CompanyActionCodeViewModel>> GetAll();
        Task Create(CompanyActionCode model);
        Task Update(int id, CompanyActionCode model);
        Task Delete(int id);
        Task<CompanyActionCode> GetByKey(int id);
    }
}

