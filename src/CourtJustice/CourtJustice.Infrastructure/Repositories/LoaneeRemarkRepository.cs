using CourtJustice.Domain;
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
    public class LoaneeRemarkRepository : BaseRepository, ILoaneeRemarkRepository
    {
        public LoaneeRemarkRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(LoaneeRemark model)
        {
            try
            {

                await Context.LoaneeRemarks.AddAsync(model);
                await Context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task Delete(int id)
        {
            var model = await Context.LoaneeRemarks.FindAsync(id);
            Context.LoaneeRemarks.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<LoaneeRemark>> GetAll()
        {
            return await Context.LoaneeRemarks.ToListAsync();
        }

        public async Task<List<LoaneeRemarkViewModel>> GetByCusId(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select * from loanee_remark where cus_id=@cus_id");

                var result = await conn.QueryAsync<LoaneeRemarkViewModel>(sb.ToString(), new { cus_id = id });
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<LoaneeRemark> GetByKey(int id)
        {
            var model = await Context.LoaneeRemarks.FindAsync(id);
            return model;
        }



        public bool IsExisting(int id)
        {
            return Context.LoaneeRemarks.Any(p => p.LoaneeRemarkId == id);
        }

        public async Task Update(int id, LoaneeRemark model)
        {
            var result = await Context.LoaneeRemarks.FindAsync(model.LoaneeRemarkId);
            result.Remark = model.Remark;

            await Context.SaveChangesAsync();
        }



    }
}

