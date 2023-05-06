using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<GroupUser>> GetAll()
        {
            return await Context.GroupUsers.ToListAsync();
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
            await Context.SaveChangesAsync();
        }
    }
}
