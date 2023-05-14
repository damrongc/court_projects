using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Globalization;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
    public class LoaneeRepository : BaseRepository, ILoaneeRepository
    {
        public LoaneeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(Loanee model)
        {
            await Context.Loanees.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<Loanee>> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<LoaneeViewModel> GetByKey(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select * from loanee where cus_id=@cus_id";
               
                var result = await conn.QueryAsync<LoaneeViewModel>(sql, new { cus_id =id});
                return result.SingleOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<LoaneeViewModel>> GetPaging(int skip, int take, string filter)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("th-TH");
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select * from loanee";
                var sb = new StringBuilder();
                sb.Append(sql);
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (cus_id LIKE @filter");
                    sb.Append(" or name LIKE @filter");
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
                var result = await conn.QueryAsync<LoaneeViewModel>(sb.ToString(), parameters);
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

                sb.Append("select count(1) from loanee");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (cus_id LIKE @filter");
                    sb.Append(" or name LIKE @filter");
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

        public async Task Update(int id, Loanee model)
        {
            throw new NotImplementedException();
        }
    }
}

