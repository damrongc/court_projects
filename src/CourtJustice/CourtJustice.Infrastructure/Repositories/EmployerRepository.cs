using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class EmployerRepository : BaseRepository, IEmployerRepository
    {
        public EmployerRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(Employer model)
        {
            await Context.Employers.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.Employers.FindAsync(id);
            Context.Employers.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Employer>> GetAll()
        {
            return await Context.Employers.ToListAsync();
        }


        public async Task<List<Employer>> GetAllActive()
        {
            return await Context.Employers.Where(p=>p.IsActive).ToListAsync();
        }
        public async Task<Employer> GetByKey(string id)
        {
            var model = await Context.Employers.FindAsync(id);
            return model;
        }

        public async Task Update(string id, Employer model)
        {
            var result = await Context.Employers.FindAsync(model.EmployerCode);
            result.EmployerName = model.EmployerName;
            result.StartDay = model.StartDay;
            result.TotalDay = model.TotalDay;
            result.IsActive = model.IsActive;
            await Context.SaveChangesAsync();
        }
    }
}

