using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(Company model)
        {
            await Context.Companys.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.Companys.FindAsync(id);
            Context.Companys.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Company>> GetAll()
        {
            return await Context.Companys.ToListAsync();
        }

        public async Task<Company> GetByKey(int id)
        {
            var model = await Context.Companys.FindAsync(id);
            return model;
        }

        public async Task Update(int id, Company model)
        {
            var result = await Context.Companys.FindAsync(model.CompanyId);
            result.CompanyName = model.CompanyName;
            result.IsActive = model.IsActive;
            result.Address = model.Address;
            result.PhoneNumber = model.PhoneNumber;
            result.LogoPath = model.LogoPath;

            await Context.SaveChangesAsync();
        }
    }
}

