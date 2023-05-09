using System;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class ReferencerRepository : BaseRepository, IReferencerRepository
    {
        public ReferencerRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {

        }

        public async Task Create(Referencer model)
        {
            await Context.Referencers.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.Referencers.FindAsync(id);
            Context.Referencers.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Referencer>> GetAll()
        {
            return await Context.Referencers.ToListAsync();
        }

        public async Task<Referencer> GetByKey(string id)
        {
            var model = await Context.Referencers.FindAsync(id);
            return model;
        }

        public async Task Update(string id, Referencer model)
        {
            var result = await Context.Referencers.FindAsync(model.ReferencerCode);
            result.FullName = model.FullName;
            result.PhoneNumber = model.PhoneNumber;
            result.Address = model.Address;
            result.AddressDetail = model.AddressDetail;

            await Context.SaveChangesAsync();
        }
    }
}


