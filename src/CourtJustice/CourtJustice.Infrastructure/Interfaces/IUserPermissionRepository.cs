using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IUserPermissionRepository
    {
        Task Create(UserPermission model);
        Task<List<UserPermission>> GetByGroupId(int id);
        Task DeleteByGroupId(int id);

        Task<UserPermission> GetRootMenu(int parentProgramId, int groupId);
        //Task SetPermission(List<UserPermissionViewModel> userPermissions);
    }
}
