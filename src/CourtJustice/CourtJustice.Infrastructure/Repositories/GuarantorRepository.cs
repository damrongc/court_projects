using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace CourtJustice.Infrastructure.Repositories
{
    public class GuarantorRepository : BaseRepository, IGuarantorRepository
    {
        public GuarantorRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(Guarantor model)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Guarantor>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Guarantor> GetByKey(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, Guarantor model)
        {
            throw new NotImplementedException();
        }
    }
}

