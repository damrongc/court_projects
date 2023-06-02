using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class JusticeCaseLawyerRepository : BaseRepository, IJusticeCaseLawyerRepository
    {
        public JusticeCaseLawyerRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(JusticeCaseLawyer model)
        {
            await Context.JusticeCaseLawyers.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<JusticeCaseLawyerViewModel>> GetByCaseNo(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select * from justice_case_lawyer_id where black_case_no=@black_case_no";
                var result = await conn.QueryAsync<JusticeCaseLawyerViewModel>(sql, new { black_case_no = id });
                return result.ToList()!;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
