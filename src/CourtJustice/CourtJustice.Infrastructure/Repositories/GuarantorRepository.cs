using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
    public class GuarantorRepository : BaseRepository, IGuarantorRepository
    {
        public GuarantorRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(Guarantor model)
        {
            await Context.Guarantors.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.Guarantors.FindAsync(id);
            Context.Guarantors.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Guarantor>> GetAll()
        {
            return await Context.Guarantors.ToListAsync();
        }

        public async Task<List<GuarantorViewModel>> GetByCusId(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select * from guarantor where cus_id=@cus_id");


                var result = await conn.QueryAsync<GuarantorViewModel>(sb.ToString(), new { cus_id = id });
                return result.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guarantor> GetByKey(int id)
        {
            var model = await Context.Guarantors.FindAsync(id);
            return model;
        }

        public bool IsExisting(int id)
        {
            return Context.Guarantors.Any(p => p.GuarantorCode == id);
        }

        public async Task Update(int id, Guarantor model)
        {
            var result = await Context.Guarantors.FindAsync(model.GuarantorCode);
            result.Address = model.Address;
            result.AddressDetail = model.AddressDetail;
            result.FullName = model.FullName;
            result.PhoneNumber = model.PhoneNumber;
            await Context.SaveChangesAsync();

        }
    }
}

