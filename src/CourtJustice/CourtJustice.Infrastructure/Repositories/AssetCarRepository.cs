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
            var model = await Context.AssetCars.FindAsync(id);
            Context.AssetCars.Remove(model);
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
                sb.Append("select chassis_number, engine_number, brand, model, production_year, estimate_price, license_plate, owner," +
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
            result.CarTypeCode = model.CarTypeCode;

            await Context.SaveChangesAsync();
        }

       
    }
}

