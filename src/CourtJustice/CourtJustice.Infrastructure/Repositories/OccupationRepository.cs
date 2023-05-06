using System;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
	public class OccupationRepository : BaseRepository, IOccupationRepository
    {
		public OccupationRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
		}

        public async Task Create(Occupation model)
        {
            await Context.occupations.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.occupations.FindAsync(id);
            Context.occupations.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Occupation>> GetAll()
        {
            return await Context.occupations.ToListAsync();
        }

        public async Task<Occupation> GetByKey(int id)
        {
            var model = await Context.occupations.FindAsync(id);
            return model;
        }

        public async Task Update(int id, Occupation model)
        {
            var result = await Context.occupations.FindAsync(model.OccupationId);
            result.OccupationName = model.OccupationName;

            await Context.SaveChangesAsync();
        }
    }
}

