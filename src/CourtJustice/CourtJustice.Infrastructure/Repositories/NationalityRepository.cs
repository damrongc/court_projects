using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class NationalityRepository : BaseRepository, INationalityRepository
    {
        public NationalityRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(Nationality model)
        {
            await Context.Nationalities.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.Nationalities.FindAsync(id);
            Context.Nationalities.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Nationality>> GetAll()
        {
            return await Context.Nationalities.ToListAsync();
        }

        public async Task<Nationality> GetByKey(string id)
        {
            var model = await Context.Nationalities.FindAsync(id);
            return model;
        }

        public async Task Update(string id, Nationality model)
        {
            var result = await Context.Nationalities.FindAsync(model.NationalityCode);
            result.NationalityName = model.NationalityName;

            await Context.SaveChangesAsync();
        }
    }
}

