using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class LoanTypeRepository : BaseRepository, ILoanTypeRepository
    {
        public LoanTypeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(LoanType model)
        {
            await Context.LoanTypes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.LoanTypes.FindAsync(id);
            Context.LoanTypes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<LoanType>> GetAll()
        {

            return await Context.LoanTypes.ToListAsync();
        }

        public async Task<LoanType> GetByKey(string id)
        {
            var model = await Context.LoanTypes.FindAsync(id);
            return model;
        }

        public async Task Update(string id, LoanType model)
        {
            var result = await Context.LoanTypes.FindAsync(model.LoanTypeCode);
            result.LoanTypeName = model.LoanTypeName;
            result.IsActive = model.IsActive;

            await Context.SaveChangesAsync();
        }
    }
}
