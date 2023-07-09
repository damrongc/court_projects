using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class BankResultCodeRepository : BaseRepository, IBankResultCodeRepository
    {
        public BankResultCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(BankResultCode model)
        {
            await Context.BankResultCodes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.BankResultCodes.FindAsync(id);
            Context.BankResultCodes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<BankResultCode>> GetAll()
        {
            return await Context.BankResultCodes.Include(p => p.Employer).ToListAsync();
        }

        public async Task<List<BankResultCode>> GetByEmployer(string employerCode)
        {
            return await Context.BankResultCodes.Where(p => p.EmployerCode.Equals(employerCode)).ToListAsync();
        }

        public async Task<BankResultCode> GetByKey(string id)
        {
            var model = await Context.BankResultCodes.FindAsync(id);
            return model;
        }

        public async Task Update(string id, BankResultCode model)
        {
            var result = await Context.BankResultCodes.FindAsync(model.BankResultCodeId);
            result.BankResultCodeName = model.BankResultCodeName;
            result.EmployerCode = model.EmployerCode;


            await Context.SaveChangesAsync();
        }
    }
}

