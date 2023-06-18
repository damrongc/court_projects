using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class AppProgramRepository : BaseRepository, IAppProgramRepository
    {
        public AppProgramRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task<List<AppProgram>> GetAll()
        {
            return await Context.AppPrograms.ToListAsync();
        }

        public async Task<AppProgram> GetById(int id)
        {
            return await Context.AppPrograms.FindAsync(id);
        }
    }
}
