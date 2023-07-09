using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IGroupUserRepository
    {
        Task<List<GroupUserViewModel>> GetAll();
        Task Create(GroupUser model);
        Task Update(int id, GroupUser model);
        Task Delete(int id);
        Task<GroupUser> GetByKey(int id);

        Task<int> CheckExistingAtUser(int id);
    }
}
