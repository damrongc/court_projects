using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class BankActionCodeRepository : BaseRepository, IBankActionCodeRepository
    {
        public BankActionCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(BankActionCode model)
        {
            await Context.BankActionCodes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.BankActionCodes.FindAsync(id);
            Context.BankActionCodes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<BankActionCode>> GetAll()
        {
            return await Context.BankActionCodes.Include(p => p.Employer).ToListAsync();
        }

        public async Task<List<BankActionCode>> GetByEmployer(string employerCode)
        {
            return await Context.BankActionCodes.Where(p => p.EmployerCode.Equals(employerCode)).ToListAsync();
        }

        public async Task<BankActionCode> GetByKey(string id)
        {
            var model = await Context.BankActionCodes.FindAsync(id);
            return model;
        }

        public async Task Update(string id, BankActionCode model)
        {
            var result = await Context.BankActionCodes.FindAsync(model.BankActionCodeId);
            result.BankActionCodeName = model.BankActionCodeName;
            result.EmployerCode = model.EmployerCode;


            await Context.SaveChangesAsync();
        }
    }
}

