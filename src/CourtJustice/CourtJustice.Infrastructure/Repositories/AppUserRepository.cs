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
                var models = await conn.QueryAsync<AppUserViewModel>(sql, new { UserId, Password = encryptPassword });
                return models.FirstOrDefault();
            }
            catch
            {

                throw;
            }
        }



        public async Task ChangePassword(AppUserViewModel model)
        {
            var result = await Context.AppUsers.FindAsync(model.UserId);
            var encryptPassword = SecurityHelper.EncryptText(model.NewPassword);
            result.Password=encryptPassword;
            Context.AppUsers.Update(result);
            await Context.SaveChangesAsync();
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

        public async Task<List<AppUser>> GetActiveUser()
        {
            return await Context.AppUsers.Where(p=>p.IsActive).ToListAsync();
        }

        public async Task<List<AppUserViewModel>> GetAll()
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT user_id,user_name,email,phone_number,is_active,target
,(select group_name from group_user where group_user.group_id=app_user.group_id) as group_name
,(select user_name from app_user x where x.user_id =app_user.manager_id  )as manager_name
FROM app_user";
                var appUser = await conn.QueryAsync<AppUserViewModel>(sql);
                return appUser.ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<AppUserViewModel> GetByKey(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT * FROM app_user WHERE user_id=@id";
                var appUser = await conn.QueryAsync<AppUserViewModel>(sql,new {id});
                return appUser.SingleOrDefault();
            }
            catch
            {
                throw;
            }
            //return await Context.AppUsers.FindAsync(id);
        }

        public async Task<List<AppUserViewModel>> GetCollectorByManager(string managerId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT user_id,user_name,email,phone_number,target
FROM app_user
where manager_id=@manager_id
and is_active=1";
                var appUser = await conn.QueryAsync<AppUserViewModel>(sql, new { manager_id = managerId });
                return appUser.ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<decimal> GetSumTargetByManager(string managerId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT SUM(target) FROM app_user WHERE manager_id=@manager_id AND is_active=1";
                var count = await conn.ExecuteScalarAsync<int>(sql, new { manager_id = managerId });
                return count;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<AppUserViewModel>> GetUserByGroup(int groupId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select user_id,user_name from app_user a,group_user b where a.group_id =b.group_id and a.group_id=@group_id";
                var appUser = await conn.QueryAsync<AppUserViewModel>(sql, new { group_id = groupId });
                return appUser.ToList();
            }
            catch
            {
                throw;
            }
        }

        public bool IsExisting(string id)
        {
            return Context.AppUsers.Any(e => e.UserId == id);
        }

        public async Task Update(string id, AppUser model)
        {
            var result = await Context.AppUsers.FindAsync(model.UserId);
            result.UserName = model.UserName;
            result.Email = model.Email;
            result.PhoneNumber = model.PhoneNumber;
            result.GroupId = model.GroupId;
            result.IsActive = model.IsActive;
            result.Target = model.Target;
            result.ManagerId= model.ManagerId;
            //result.UserUpdated = model.UserUpdated;
            //result.UpdatedDateTime = model.UpdatedDateTime;
            Context.AppUsers.Update(result);
            await Context.SaveChangesAsync();
        }
    }
}
