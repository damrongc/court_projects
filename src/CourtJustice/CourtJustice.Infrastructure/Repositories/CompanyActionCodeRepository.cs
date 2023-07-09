using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class CompanyActionCodeRepository : BaseRepository, ICompanyActionCodeRepository
    {
        public CompanyActionCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(CompanyActionCode model)
        {
            await Context.CompanyActionCodes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.CompanyActionCodes.FindAsync(id);
            Context.CompanyActionCodes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<CompanyActionCode>> GetAll()
        {
            return await Context.CompanyActionCodes.Include(p => p.Company).ToListAsync();
        }

        public async Task<CompanyActionCode> GetByKey(string id)
        {
            var model = await Context.CompanyActionCodes.FindAsync(id);
            return model;
        }

        public async Task Update(string id, CompanyActionCode model)
        {
            var result = await Context.CompanyActionCodes.FindAsync(model.CompanyActionCodeId);
            result.CompanyActionCodeName = model.CompanyActionCodeName;
            result.CompanyId = model.CompanyId;

            await Context.SaveChangesAsync();
        }
    }
}

