using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUserViewModel> Authentication(string UserId, string Password);

        Task<List<AppUser>> GetAll();
        Task Create(AppUser model);
        Task Update(string id, AppUser model);
        Task Delete(string id);
        Task<AppUser> GetByKey(string id);

        Task<int> CheckExistingAtUser(string id);

        bool IsExisting(string id);
    }
}
