using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
    public class AddressSetRepository : BaseRepository, IAddressSetRepository
    {
        public AddressSetRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task<AddressSet> GetById(int id)
        {
            return await Context.AddressSets.FindAsync(id);
        }

        public async Task<IEnumerable<AddressSet>> GetPaging(int skip, int take, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select address_id,province_name,district_name,sub_district_name,postal_code from address_set");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where postal_code LIKE @filter");
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
                var result = await conn.QueryAsync<AddressSet>(sb.ToString(), parameters);
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public  async Task<int> GetRecordCount(string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();

                sb.Append("select count(1) from address_set");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where postal_code LIKE @filter");
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
    }
}
