using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class UserPermissionRepository : BaseRepository, IUserPermissionRepository
    {
        public UserPermissionRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(UserPermission model)
        {
            await Context.UserPermissions.AddAsync(model);
            await Context.SaveChangesAsync();
     
        }

        public async Task DeleteByGroupId(int id)
        {
            var oldPermission = Context.UserPermissions.Where(p => p.GroupId == id);
            Context.UserPermissions.RemoveRange(oldPermission);
            await Context.SaveChangesAsync();
        }

        public async Task<List<UserPermission>> GetByGroupId(int id)
        {
            return await Context.UserPermissions.Where(p => p.GroupId == id).ToListAsync();
        }

        public async Task<UserPermission> GetRootMenu(int parentProgramId, int groupId)
        {
            return await Context.UserPermissions.Where(p => p.ProgramId == parentProgramId
            && p.GroupId == groupId).FirstOrDefaultAsync();
        }

        //public async Task SetPermission(List<UserPermissionViewModel> userPermissions)
        //{
        //    var oldPermission = Context.UserPermissions.Where(p => p.GroupId == userPermissions[0].GroupId);
        //    Context.UserPermissions.RemoveRange(oldPermission);
        //    await Context.SaveChangesAsync();

        //    foreach (var item in userPermissions)
        //    {
        //        var userPermission = new UserPermission();

        //    }
        //        Task.CompletedTask;
        //}
    }
}
