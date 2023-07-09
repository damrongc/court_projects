using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class AssetTypeRepository : BaseRepository, IAssetTypeRepository
    {
        public AssetTypeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(AssetType model)
        {
            await Context.AssetTypes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.AssetTypes.FindAsync(id);
            Context.AssetTypes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<AssetType>> GetAll()
        {
            return await Context.AssetTypes.ToListAsync();
        }

        public async Task<AssetType> GetByKey(int id)
        {
            var model = await Context.AssetTypes.FindAsync(id);
            return model;
        }

        public async Task Update(int id, AssetType model)
        {
            var result = await Context.AssetTypes.FindAsync(model.AssetTypeId);
            result.AssetTypeName = model.AssetTypeName;

            await Context.SaveChangesAsync();
        }
    }
}

