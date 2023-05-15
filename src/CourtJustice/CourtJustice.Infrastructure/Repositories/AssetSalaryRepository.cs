using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

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

        public async Task<IEnumerable<AssetSalary>> GetPaging(int skip, int take, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select * from asset_salary");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (asset_id LIKE @filter");
                    sb.Append(" or name company @filter");
                    sb.Append(" )");
                }
                sb.Append(" Limit @skip,@take");
                var dictionary = new Dictionary<string, object>
                    {
                         { "@skip", skip },
                         { "@take", take },
                    };
                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
                }
                var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<AssetSalary>(sb.ToString(), parameters);
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<int> GetRecordCount(string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();

                sb.Append("select count(1) from asset_salary");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (asset_id LIKE @filter");
                    sb.Append(" or company LIKE @filter");
                    sb.Append(" )");
                }
                var dictionary = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
                }
                var parameters = new DynamicParameters(dictionary);

                var rowCount = await conn.ExecuteScalarAsync<int>(sb.ToString(), parameters);
                return rowCount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(int id, AssetSalary model)
        {
            var result = await Context.AssetSalaries.FindAsync(model.AssetId);
            result.Company = model.Company;
            result.Address = model.Address;
            result.Salary = model.Salary;
            result.SalaryDate = model.SalaryDate;
          

            await Context.SaveChangesAsync();
        }
    }
}

