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
	public class BankResultCodeRepository : BaseRepository , IBankResultCodeRepository
	{
        public BankResultCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public  async Task Create(BankResultCode model)
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
            return await Context.BankResultCodes.ToListAsync();
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

