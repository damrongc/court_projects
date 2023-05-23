using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
	public class LawOfficeRepository : BaseRepository, ILawOfficeRepository
    {
		public LawOfficeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
		}

        public async Task Create(LawOffice model)
        {
            await Context.LawOffices.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.LawOffices.FindAsync(id);
            Context.LawOffices.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<LawOffice>> GetAll()
        {
            return await Context.LawOffices.ToListAsync();
        }

        public async Task<LawOffice> GetByKey(string id)
        {
            var model = await Context.LawOffices.FindAsync(id);
            return model;
        }

        public async Task Update(string id, LawOffice model)
        {
            var result = await Context.LawOffices.FindAsync(model.LawOfficeCode);
            result.LawOfficeName = model.LawOfficeName;
            result.IsActive = model.IsActive;
         
            await Context.SaveChangesAsync();
        }
    }
}

