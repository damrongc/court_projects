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
	public class AssetSalaryRepository : BaseRepository , IAssetSalaryRepository
	{
		public AssetSalaryRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
		{
		}

        public async Task Create(AssetSalary model)
        {
            await Context.AssetSalaries.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.AssetSalaries.FindAsync(id);
            Context.AssetSalaries.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<AssetSalary>> GetAll()
        {
            return await Context.AssetSalaries.ToListAsync();
        }

        public async Task<AssetSalary> GetByKey(int id)
        {
            var model = await Context.AssetSalaries.FindAsync(id);
            return model;
        }

        public async Task Update(int id, AssetSalary model)
        {
            var result = await Context.AssetSalaries.FindAsync(model.AssetId);
            result.Company = model.Company;
            result.Address = model.Address;
            result.Salary = model.Salary;
            result.SalaryDate = model.SalaryDate;
            result.AddressSet = model.AddressSet;

            await Context.SaveChangesAsync();
        }
    }
}

