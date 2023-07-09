using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUserViewModel> Authentication(string UserId, string Password);
        Task<List<AppUserViewModel>> GetAll();
        Task Create(AppUser model);
        Task Update(string id, AppUser model);
        Task Delete(string id);
        Task<AppUserViewModel> GetByKey(string id);
        Task<int> CheckExistingAtUser(string id);
        bool IsExisting(string id);
        Task<List<AppUserViewModel>> GetUserByGroup(int groupId);
        Task<List<AppUser>> GetActiveUser();
        Task<List<AppUserViewModel>> GetCollectorByManager(string managerId);
        Task<decimal> GetSumTargetByManager(string managerId);

        Task ChangePassword(AppUserViewModel model);


    }
}
