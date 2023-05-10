using System;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace CourtJustice.Infrastructure.Repositories
{
	public class LoanSubTaskStatusRepository : BaseRepository,ILoanSubTaskStatusRepository

    {
		public LoanSubTaskStatusRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
		}

        public async Task Create(LoanSubTaskStatus model)
        {
            await Context.LoanSubTaskStatuses.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.LoanSubTaskStatuses.FindAsync(id);
            Context.LoanSubTaskStatuses.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<LoanSubTaskStatus>> GetAll()
        {
            return await Context.LoanSubTaskStatuses.ToListAsync();
        }

        public async Task<LoanSubTaskStatus> GetByKey(int id)
        {
            var model = await Context.LoanSubTaskStatuses.FindAsync(id);
            return model;
        }

        public async Task Update(int id, LoanSubTaskStatus model)
        {
            var result = await Context.LoanSubTaskStatuses.FindAsync(model.LoanSubTaskStatusId);
            result.LoanSubTaskStatusName = model.LoanSubTaskStatusName;
            result.LoanTaskStatusId = model.LoanTaskStatusId;


            await Context.SaveChangesAsync();
        }
    }
}

