using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
	public class CaseResultRepository : BaseRepository , ICaseResultRepository
	{
		public CaseResultRepository(IConfiguration config, ApplicationDbContext context) : base (config, context)
		{
		}

        public async Task Create(CaseResult model)
        {
            await Context.CaseResults.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.CaseResults.FindAsync(id);
            Context.CaseResults.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<CaseResult>> GetAll()
        {
            return await Context.CaseResults.ToListAsync();
        }

        public async Task<CaseResult> GetByKey(int id)
        {
            var model = await Context.CaseResults.FindAsync(id);
            return model;
        }

        public async Task Update(int id, CaseResult model)
        {
            var result = await Context.CaseResults.FindAsync(model.CaseResultId);
            result.CaseResultName = model.CaseResultName;
            result.IsActive = model.IsActive;
            await Context.SaveChangesAsync();
        }

       
    }
}

