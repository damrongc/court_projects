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
	public class CompanyActionCodeRepository :BaseRepository , ICompanyActionCodeRepository
	{
        public CompanyActionCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(CompanyActionCode model)
        {
            await Context.CompanyActionCodes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.CompanyActionCodes.FindAsync(id);
            Context.CompanyActionCodes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<CompanyActionCode>> GetAll()
        {
            return await Context.CompanyActionCodes.ToListAsync();
        }

        public async Task<CompanyActionCode> GetByKey(string id)
        {
            var model = await Context.CompanyActionCodes.FindAsync(id);
            return model;
        }

        public async Task Update(string id, CompanyActionCode model)
        {
            var result = await Context.CompanyActionCodes.FindAsync(model.CompanyActionCodeId);
            result.CompanyActionName = model.CompanyActionName;
            result.CompanyId = model.CompanyId;
           
            await Context.SaveChangesAsync();
        }
    }
}

