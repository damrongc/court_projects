using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ICompanyResultCodeRepository
    {
        Task<List<CompanyResultCodeViewModel>> GetAll();
        Task<List<CompanyResultCodeViewModel>> GetByCompanyActionId(int companyActionId);
        Task Create(CompanyResultCode model);
        Task Update(int id, CompanyResultCode model);
        Task Delete(int id);
        Task<CompanyResultCodeViewModel> GetByKey(int id);
        bool IsExisting(int companyActionId, string companyResultCodeId);
        bool IsHaveActionCode(int id);
    }
}

