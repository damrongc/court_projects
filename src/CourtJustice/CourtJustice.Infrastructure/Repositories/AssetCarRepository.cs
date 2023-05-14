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
    public class AssetCarRepository : BaseRepository, IAssetCarRepository
    {
        public AssetCarRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(AssetCar model)
        {
            await Context.AssetCars.AddAsync(model);
            await Context.SaveChangesAsync();
        }

   

        public async Task Delete(string id)
        {
            var model = await Context.AssetImages.FindAsync(id);
            Context.AssetImages.Remove(model);
            await Context.SaveChangesAsync();
        }
    

        public async Task<List<AssetCar>> GetAll()
        {
            return await Context.AssetCars.ToListAsync();
        }

        public async Task<List<AssetCarViewModel>> GetByCusId(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select chassis_number, engine_number, brand, model, production_year, estimate_price, " +
                     " a.car_type_code, b.car_type_name, cus_id" +
                     " from asset_car a,car_type b" +
                     " where a.car_type_code = b.car_type_code" +
                      " and cus_id=@cus_id");

                var result = await conn.QueryAsync<AssetCarViewModel>(sb.ToString(), new { cus_id = id });
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AssetCar> GetByKey(string id)
        {
            var model = await Context.AssetCars.FindAsync(id);
            return model;
        }

        public async Task<IEnumerable<AssetCarViewModel>> GetPaging(int skip, int take, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select chassis_number, engine_number, brand, model, production_year, estimate_price, license_plate_owner, " +
                    " car_type_code, b.car_type_name, cus_id" +
                    " from asset_car a,car_type b" +
                    " where a.car_type_code = b.car_type_code");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (chassis_number LIKE @filter");
                    sb.Append(" or engine_number  @filter");
                    sb.Append(" or brand  @filter");
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
                var result = await conn.QueryAsync<AssetCarViewModel>(sb.ToString(), parameters);
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

                sb.Append("select count(1) from asset_car");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (chassis_number LIKE @filter");
                    sb.Append(" or engine_number  @filter");
                    sb.Append(" or brand  @filter");
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

        public bool IsExisting(string id)
        {
            return Context.AssetCars.Any(p => p.ChassisNumber == id);
        }

        public async Task Update(string id, AssetCar model)
        {
            var result = await Context.AssetCars.FindAsync(model.ChassisNumber);
            result.EngineNumber = model.EngineNumber;
            result.Brand = model.Brand;
            result.Model = model.Model;
            result.ProductionYear = model.ProductionYear;
            result.EstimatePrice = model.EstimatePrice;
            result.LicensePlate = model.LicensePlate;
            result.Owner = model.Owner;
            result.CarType = model.CarType;

            await Context.SaveChangesAsync();
        }

       
    }
}

