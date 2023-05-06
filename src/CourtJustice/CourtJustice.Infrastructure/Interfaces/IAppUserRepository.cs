using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUserViewModel> Authentication(string UserId, string Password);

        Task<List<AppUser>> GetAll();
        Task Create(AppUser model);
        Task Update(int id, AppUser model);
        Task Delete(int id);
        Task<AppUser> GetByKey(int id);

        Task<int> CheckExistingAtUser(int id);
    }
}
