﻿using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class CarTypeRepository : BaseRepository, ICarTypeRepository
    {
        public CarTypeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {

        }

        public async Task Create(CarType model)
        {
            await Context.CarTypes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.CarTypes.FindAsync(id);
            Context.CarTypes.Remove(model);
            await Context.SaveChangesAsync();
        }



        public async Task<CarType> GetByKey(int id)
        {
            var model = await Context.CarTypes.FindAsync(id);
            return model;
        }

        public async Task Update(int id, CarType model)
        {
            var result = await Context.CarTypes.FindAsync(model.CarTypeCode);
            result.CarTypeName = model.CarTypeName;
            result.IsActive = model.IsActive;
            await Context.SaveChangesAsync();
        }


        public async Task<List<CarType>> GetAll()
        {
            return await Context.CarTypes.ToListAsync();
        }

        public bool IsExisting(int id)
        {
            return Context.CarTypes.Any(c => c.CarTypeCode == id);
        }
    }
}

