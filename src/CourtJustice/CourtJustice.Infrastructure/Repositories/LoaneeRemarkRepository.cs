using CourtJustice.Domain;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
	public class LoaneeRemarkRepository :BaseRepository, ILoaneeRemarkRepository
	{
        public LoaneeRemarkRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(LoaneeRemark model)
        {
            await Context.LoaneeRemarks.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.LoaneeRemarks.FindAsync(id);
            Context.LoaneeRemarks.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<LoaneeRemark>> GetAll()
        {
            return await Context.LoaneeRemarks.ToListAsync();
        }

        public async Task<List<LoaneeRemarkViewModel>> GetByCusId(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select asset_land_id, position, gps,address,address_detail,cus_id,estimate_price, a.land_office_code, land_office_name" +
                    " from asset_land a,land_office b" +
                    " where a.land_office_code = b.land_office_code" +
                    " and cus_id=@cus_id");

                var result = await conn.QueryAsync<LoaneeRemarkViewModel>(sb.ToString(), new { cus_id = id });
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<LoaneeRemark> GetByKey(int id)
        {
            var model = await Context.LoaneeRemarks.FindAsync(id);
            return model;
        }

        public async Task<IEnumerable<LoaneeRemarkViewModel>> GetPaging(int skip, int take, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select asset_land_id, position, gps,land_office_name" +
                    " from asset_land a,land_office b" +
                    " where a.land_office_code = b.land_office_code");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (asset_land_id LIKE @filter");
                    sb.Append(" or position  @filter");
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
                var result = await conn.QueryAsync<LoaneeRemarkViewModel>(sb.ToString(), parameters);
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

                sb.Append("select count(1) from lone");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (asset_land_id LIKE @filter");
                    sb.Append(" or position LIKE @filter");
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

        public bool IsExisting(int id)
        {
            return Context.LoaneeRemarks.Any(p => p.LoaneeRemarkId == id);
        }

        public async Task Update(int id, LoaneeRemark model)
        {
            var result = await Context.LoaneeRemarks.FindAsync(model.LoaneeRemarkId);
            result.Remark = model.Remark;
            result.CusId = model.CusId;
          
       

            await Context.SaveChangesAsync();
        }
    }
}

