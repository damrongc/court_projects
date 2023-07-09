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
    public class AssetLandRepository : BaseRepository, IAssetLandRepository
    {
        public AssetLandRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(AssetLand model)
        {
            await Context.AssetLands.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.AssetLands.FindAsync(id);
            Context.AssetLands.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<AssetLand>> GetAll()
        {
            return await Context.AssetLands.ToListAsync();
        }

        public async Task<AssetLand> GetByKey(string id)
        {
            var model = await Context.AssetLands.FindAsync(id);
            return model;
        }

        public async Task Update(string id, AssetLand model)
        {
            var result = await Context.AssetLands.FindAsync(model.AssetLandId);
            result.Address = model.Address;
            result.AddressDetail = model.AddressDetail;
            result.Position = model.Position;
            result.Gps = model.Gps;
            result.LandOfficeCode = model.LandOfficeCode;
            result.EstimatePrice = model.EstimatePrice;
            //result.AddressSet = model.AddressSet;
            //result.LandOffice = model.LandOffice;

            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetLandViewModel>> GetPaging(int skip, int take, string filter)
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
                var result = await conn.QueryAsync<AssetLandViewModel>(sb.ToString(), parameters);
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

                sb.Append("select count(1) from asset_land");
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

        public async Task<List<AssetLandViewModel>> GetByCusId(string id)
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

                var result = await conn.QueryAsync<AssetLandViewModel>(sb.ToString(), new { cus_id = id });
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsExisting(string id)
        {
            return Context.AssetLands.Any(p => p.AssetLandId == id);
        }
    }
}

