﻿using CourtJustice.Domain.Models;
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

        public async Task Delete(int id)
        {
            var model = await Context.AssetLands.FindAsync(id);
            Context.AssetLands.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<AssetLand>> GetAll()
        {
            return await Context.AssetLands.ToListAsync();
        }

        public async Task<AssetLand> GetByKey(int id)
        {
            var model = await Context.AssetLands.FindAsync(id);
            return model;
        }

        public async Task Update(int id, AssetLand model)
        {
            var result = await Context.AssetLands.FindAsync(model.AssetLandId);
            result.Address = model.Address;
            result.Position = model.Position;
            result.Gps = model.Gps;
            result.LandOfficeCode = model.LandOfficeCode;
            result.EstimatePrice = model.EstimatePrice;
            result.AddressSet = model.AddressSet;
            result.LandOffice = model.LandOffice;

            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetLand>> GetPaging(int skip, int take, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select * from asset_land");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (chassis_number LIKE @filter");
                    sb.Append(" or name engine_number @filter");
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
                var result = await conn.QueryAsync<AssetLand>(sb.ToString(), parameters);
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
                    sb.Append(" where (chassis_number LIKE @filter");
                    sb.Append(" or engine_number LIKE @filter");
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
    }
}

