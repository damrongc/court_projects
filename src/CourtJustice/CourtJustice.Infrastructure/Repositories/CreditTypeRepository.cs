using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class CreditTypeRepository : BaseRepository, ICreditTypeRepository
    {
        public CreditTypeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(CreditType model)
        {
            await Context.CreditTypes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.CreditTypes.FindAsync(id);
            Context.CreditTypes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<CreditType>> GetAll()
        {
            return await Context.CreditTypes.ToListAsync();
        }

        public async Task<CreditType> GetByKey(string id)
        {
            var model = await Context.CreditTypes.FindAsync(id);
            return model;
        }

        public async Task Update(string id, CreditType model)
        {
            var result = await Context.CreditTypes.FindAsync(model.CreditTypeCode);
            result.CreditTypeName = model.CreditTypeName;
            result.IsActive = model.IsActive;

            await Context.SaveChangesAsync();
        }
    }
}

