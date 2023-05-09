using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
	public class LandOfficeRepository : BaseRepository , ILandOfficeRepository
	{
		public LandOfficeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
		}

        public async Task Create(LandOffice model)
        {
            await Context.LandOffices.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.LandOffices.FindAsync(id);
            Context.LandOffices.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<LandOffice>> GetAll()
        {
            return await Context.LandOffices.ToListAsync();
        }

        public async Task<LandOffice> GetByKey(string id)
        {
            var model = await Context.LandOffices.FindAsync(id);
            return model;
        }

        public  bool IsExisting(string id)
        {
            return Context.LandOffices.Any(p=>p.LandOfficeCode== id);
        }

        public async Task Update(string id, LandOffice model)
        {
            var result = await Context.LandOffices.FindAsync(model.LandOfficeCode);
            result.LandOfficeName = model.LandOfficeName;
           

            await Context.SaveChangesAsync();
        }
    }
}

