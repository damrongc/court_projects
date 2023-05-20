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
	public class BucketRepository :BaseRepository, IBucketRepository
	{
        public BucketRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(Bucket model)
        {
            await Context.Buckets.AddAsync(model);
            await Context.SaveChangesAsync();
        }

    

        public async Task Delete(int id)
        {
            var model = await Context.Buckets.FindAsync(id);
            Context.Buckets.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Bucket>> GetAll()
        {
            return await Context.Buckets.ToListAsync();
        }

      

      

        public async Task<Bucket> GetByKey(int id)
        {
            var model = await Context.Buckets.FindAsync(id);
            return model;
        }

        public async Task Update(int id, Bucket model)
        {
            var result = await Context.Buckets.FindAsync(model.BucketId);
            result.BucketName = model.BucketName;
            result.IsActive = model.IsActive;

            await Context.SaveChangesAsync();
        }
    }
}

