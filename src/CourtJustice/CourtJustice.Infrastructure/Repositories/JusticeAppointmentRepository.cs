using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class JusticeAppointmentRepository : BaseRepository, IJusticeAppointmentRepository
    {
        public JusticeAppointmentRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(JusticeAppointment model)
        {
            await Context.JusticeAppointments.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<JusticeAppointmentViewModel>> GetByCaseNo(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select * from justice_appointment where black_case_no=@black_case_no";
                var result = await conn.QueryAsync<JusticeAppointmentViewModel>(sql, new { black_case_no = id });
                return result.ToList()!;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<JusticeAppointmentViewModel> GetLastByCaseNo(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select * from justice_appointment where black_case_no=@black_case_no order by justice_appointment_id desc limit 1";
                var result = await conn.QueryAsync<JusticeAppointmentViewModel>(sql, new { black_case_no = id });
                return result.FirstOrDefault()!;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
