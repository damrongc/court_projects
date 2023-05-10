using System;
using System.Data;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace CourtJustice.Infrastructure.Repositories
{
	public class LoanTaskStatusRepository : BaseRepository, ILoanTaskStatusRepository
	{
		public LoanTaskStatusRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
		}

        public async Task<int> CheckExistingAtSub(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT COUNT(1) FROM loan_sub_task_status WHERE loan_task_status_id=@loan_task_status_id";
                var count = await conn.ExecuteScalarAsync<int>(sql, new { loan_task_status_id = id });
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public  async Task Create(LoanTaskStatus model)
        {
            await Context.LoanTaskStatuses.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.LoanTaskStatuses.FindAsync(id);
            Context.LoanTaskStatuses.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async  Task<List<LoanTaskStatus>> GetAll()
        {
            return await Context.LoanTaskStatuses.ToListAsync();
        }

        public async Task<LoanTaskStatus> GetByKey(int id)
        {
            var model = await Context.LoanTaskStatuses.FindAsync(id);
            return model;
        }

        public async Task Update(int id, LoanTaskStatus model)
        {
            var result = await Context.LoanTaskStatuses.FindAsync(model.LoanTaskStatusId);
            result.LoanTaskStatusName = model.LoanTaskStatusName;


            await Context.SaveChangesAsync();
        }
    }
}

