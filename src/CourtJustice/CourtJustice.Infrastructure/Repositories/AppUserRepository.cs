using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository, IAppUserRepository
    {
        public AppUserRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task<AppUserViewModel> Authentication(string UserId, string Password)
        {
            try
            {
                var encryptPassword = SecurityHelper.EncryptText(Password);


                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT a.user_id,a.user_name,a.group_id,group_name
FROM app_user a,group_user b
WHERE a.group_id =b.group_id
AND a.user_id=@UserId
AND a.password=@Password";
                var models = await conn.QueryAsync<AppUserViewModel>(sql, new { UserId = UserId, Password = encryptPassword });
                return models.FirstOrDefault();
            }
            catch
            {

                throw;
            }
        }

        public async Task<int> CheckExistingAtUser(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT COUNT(1) FROM app_user WHERE user_id=@user_id";
                var count = await conn.ExecuteScalarAsync<int>(sql, new { user_id = id });
                return count;
            }
            catch 
            {
                throw;
            }
        }


        public async Task Create(AppUser model)
        {
            await Context.AppUsers.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.AppUsers.FindAsync(id);
            Context.AppUsers.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await Context.AppUsers.Include(p=>p.GroupUser).ToListAsync();
        }

        public async Task<AppUser> GetByKey(string id)
        {
            return await Context.AppUsers.FindAsync(id);
        }

        public bool IsExisting(string id)
        {
            return Context.AppUsers.Any(e => e.UserId == id);
        }

        public async Task Update(string id, AppUser model)
        {
            var result = await Context.AppUsers.FindAsync(model.UserId);
            result.UserName =model.UserName;
            result.Email =model.Email;
            result.IsActive =model.IsActive;
            result.UserUpdated =model.UserUpdated;
            result.UpdatedDateTime = model.UpdatedDateTime;
            Context.AppUsers.Update(result);
            await Context.SaveChangesAsync();
        }
    }
}
