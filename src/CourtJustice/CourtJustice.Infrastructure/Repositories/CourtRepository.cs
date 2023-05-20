using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
	public class CourtRepository : BaseRepository, ICourtRepository
	{
		public CourtRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
		}

        public async Task Create(Court model)
        {
            await Context.Courts.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.Courts.FindAsync(id);
            Context.Courts.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Court>> GetAll()
        {
            return await Context.Courts.ToListAsync();
        }

        public async Task<Court> GetByKey(int id)
        {
            var model = await Context.Courts.FindAsync(id);
            return model;
        }

        public async Task<IEnumerable<Court>> GetPaging(int skip, int take, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select * from court");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (court_id LIKE @filter");
                    sb.Append(" or court_name LIKE @filter");
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
                var result = await conn.QueryAsync<Court>(sb.ToString(), parameters);
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

                sb.Append("select count(1) from court");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (court_id LIKE @filter");
                    sb.Append(" or court_name LIKE @filter");
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

        public async Task Update(int id, Court model)
        {
            var result = await Context.Courts.FindAsync(model.CourtId);
            result.CourtName = model.CourtName;
            result.Address = model.Address;
            result.AddressDetail = model.AddressDetail;
            result.IsActive = model.IsActive;

            await Context.SaveChangesAsync();
        }
    }
}

