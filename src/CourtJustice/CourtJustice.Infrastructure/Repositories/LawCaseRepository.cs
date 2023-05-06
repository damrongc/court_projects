using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
	public class LawCaseRepository : BaseRepository , ILawCaseRepository
	{
		public LawCaseRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
		}

        public async Task Create(LawCase model)
        {
            await Context.lawCases.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.lawCases.FindAsync(id);
            Context.lawCases.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<LawCase>> GetAll()
        {
            return await Context.lawCases.ToListAsync();
        }

        public async Task<LawCase> GetByKey(string id)
        {
            var model = await Context.lawCases.FindAsync(id);
            return model;
        }

        public async Task Update(string id, LawCase model)
        {
            var result = await Context.lawCases.FindAsync(model.LawCaseCode);
            result.LawCaseName = model.LawCaseName;

            await Context.SaveChangesAsync();
        }
    }
}

