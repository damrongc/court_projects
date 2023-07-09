using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class GroupUserRepository : BaseRepository, IGroupUserRepository
    {
        public GroupUserRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task<int> CheckExistingAtUser(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT COUNT(1) FROM app_user WHERE group_id=@group_id";
                var count = await conn.ExecuteScalarAsync<int>(sql, new { group_id = id });
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(GroupUser model)
        {
            await Context.GroupUsers.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.GroupUsers.FindAsync(id);
            Context.GroupUsers.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<GroupUserViewModel>> GetAll()
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select group_id, group_name,group_level,is_active,user_created,created_date_time,user_updated,updated_date_time from group_user order by group_id asc";
                var groupUsers = await conn.QueryAsync<GroupUserViewModel>(sql);
                return groupUsers.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GroupUser> GetByKey(int id)
        {
            var model = await Context.GroupUsers.FindAsync(id);
            return model;
        }

        public async Task Update(int id, GroupUser model)
        {
            var result = await Context.GroupUsers.FindAsync(model.GroupId);
            result.GroupName = model.GroupName;
            //result.EmployerCode = model.EmployerCode;
            await Context.SaveChangesAsync();
        }
    }
}
