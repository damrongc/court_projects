using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class CompanyResultCodeRepository : BaseRepository, ICompanyResultCodeRepository
    {
        public CompanyResultCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(CompanyResultCode model)
        {
            await Context.CompanyResultCodes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.CompanyResultCodes.FindAsync(id);
            Context.CompanyResultCodes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<CompanyResultCode>> GetAll()
        {
            return await Context.CompanyResultCodes.Include(p => p.Company).ToListAsync();
        }

        public async Task<CompanyResultCode> GetByKey(string id)
        {
            var model = await Context.CompanyResultCodes.FindAsync(id);
            return model;
        }

        public async Task Update(string id, CompanyResultCode model)
        {
            var result = await Context.CompanyResultCodes.FindAsync(model.CompanyResultCodeId);
            result.CompanyResultCodeName = model.CompanyResultCodeName;
            result.NotCallFlag = model.NotCallFlag;
            result.ShowHideFlag = model.ShowHideFlag;
            result.CompanyId = model.CompanyId;

            await Context.SaveChangesAsync();
        }
    }
}

