using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Infrastructure.Repositories
{
    public class LawyerRepository : BaseRepository, ILawyerRepository
    {
        public LawyerRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public Task<int> CheckExistingAtUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Create(Lawyer model)
        {
         
            await Context.Lawyers.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.Lawyers.FindAsync(id);
            Context.Lawyers.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Lawyer>> GetAll()
        {
            return await Context.Lawyers.ToListAsync();
        }

        public async Task<Lawyer> GetByKey(int id)
        {
            var model = await Context.Lawyers.FindAsync(id);
            return model;
        }

        public async Task Update(int id, Lawyer model)
        {
            var result = await Context.Lawyers.FindAsync(model.LawyerId);
            result.LawyerName = model.LawyerName;
            result.PhoneNumber = model.PhoneNumber;
            result.Address = model.Address;
            result.AddressNo = model.AddressNo;
            result.Building = model.Building;
            result.Email = model.Email;
            result.Floor = model.Floor;
            result.Village = model.Village;
            result.Substreet = model.Substreet;
            result.Street = model.Street;
            result.ProvinceId = model.ProvinceId;
            result.DistrictId = model.DistrictId;
            result.SubDistrictId = model.SubDistrictId;
            await Context.SaveChangesAsync();
        }
    }
}
