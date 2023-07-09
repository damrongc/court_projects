using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class UserEmployerMappingRepository : BaseRepository, IUserEmployerMappingRepository
    {
        public UserEmployerMappingRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(UserEmployerMapping model)
        {
            await Context.UserEmployerMappings.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public void DeleteByUser(string userId)
        {
            var mapping = Context.UserEmployerMappings.Where(p => p.UserId == userId).SingleOrDefault();
            Context.UserEmployerMappings.Remove(mapping);

        }

        public List<UserEmployerMapping?> GetByEmployer(string employerCode)
        {
            return Context.UserEmployerMappings.Where(p => p.EmployerCode == employerCode).ToList();
        }


        public List<UserEmployerMapping?> GetByUser(string userId)
        {
            return Context.UserEmployerMappings.Where(p => p.UserId == userId).ToList();
        }
    }
}
