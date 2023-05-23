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
	public class AssetImageRepository : BaseRepository, IAssetImageRepository
	{
        public AssetImageRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(AssetImage model)
        {
            await Context.AssetImages.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.AssetImages.FindAsync(id);
            Context.AssetImages.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<AssetImage>> GetAll()
        {
            return await Context.AssetImages.ToListAsync();
        }

        public  List<AssetImage> GetByAssetId(string id)
        {
           return Context.AssetImages.Where(x=>x.AssetId==id).ToList(); 
        }

        public async Task<AssetImage> GetByKey(int id)
        {
            var model = await Context.AssetImages.FindAsync(id);
            return model;
        }

        public async Task Update(int id, AssetImage model)
        {
            var result = await Context.AssetImages.FindAsync(model.ImageId);
            result.AssetId = model.AssetId;
            result.ImagePath = model.ImagePath;
            await Context.SaveChangesAsync();
        }
    }
}

