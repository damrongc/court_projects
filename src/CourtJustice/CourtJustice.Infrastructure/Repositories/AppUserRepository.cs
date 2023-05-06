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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> CheckExistingAtUser(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT COUNT(1) FROM app_user WHERE user_id=@user_id";
                var count = await conn.ExecuteScalarAsync<int>(sql, new { user_id = id });
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task Create(AppUser model)
        {
            await Context.AppUsers.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.AppUsers.FindAsync(id);
            Context.AppUsers.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await Context.AppUsers.ToListAsync();
        }

        public Task<AppUser> GetByKey(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, AppUser model)
        {
            var result = await Context.AppUsers.FindAsync(model.UserId);
            Context.AppUsers.Update(result);
            await Context.SaveChangesAsync();
        }
    }
}
