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

        //public async Task Delete(int id)
        //{
        //    var model = await Context.AssetCars.FindAsync(id);
        //    Context.AssetCars.Remove(model);
        //    await Context.SaveChangesAsync();
        //}

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

        public async Task<AssetCar> GetByKey(string id)
        {
            var model = await Context.AssetCars.FindAsync(id);
            return model;
        }

        public async Task Update(int id, AssetCar model)
        {
            var result = await Context.AssetCars.FindAsync(model.ChassisNumber);
            result.EngineNumber = model.EngineNumber;
            result.Brand = model.Brand;
            result.Model = model.Model;
            result.ProductionYear = model.ProductionYear;
            result.EstimatePrice = model.EstimatePrice;
            result.LicensePlate = model.LicensePlate;
            result.Owner = model.Owner;
           

            await Context.SaveChangesAsync();
        }
    }
}

