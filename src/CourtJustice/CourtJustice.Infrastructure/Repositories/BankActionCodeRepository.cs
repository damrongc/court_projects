using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class BankActionCodeRepository : BaseRepository, IBankActionCodeRepository
    {
        public BankActionCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task<int> CountByEmployerAndCode(string employerCode, string actionCodeId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select count(1) from bank_action_code 
where bank_action_code_id=@bank_action_code_id 
and employer_code=@employer_code";
                var rowCount = await conn.ExecuteScalarAsync<int>(sql, new { bank_action_code_id = actionCodeId, employer_code= employerCode });
                return rowCount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(BankActionCode model)
        {
            await Context.BankActionCodes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.BankActionCodes.FindAsync(id);
            Context.BankActionCodes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<BankActionCodeViewModel>> GetAll()
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select bank_action_id,bank_action_code_id,bank_action_code_name,a.employer_code,b.employer_name
from bank_action_code a,employer b
where a.employer_code =b.employer_code";
           
                var result = await conn.QueryAsync<BankActionCodeViewModel>(sql);
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }

            //return await Context.BankActionCodes.Include(p => p.Employer).ToListAsync();
        }

        public async Task<List<BankActionCodeViewModel>> GetByEmployer(string employerCode)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select bank_action_id,bank_action_code_id,bank_action_code_name,a.employer_code,b.employer_name
from bank_action_code a,employer b
where a.employer_code =b.employer_code";

                if (!string.IsNullOrEmpty(employerCode))
                {
                    sql += " and a.employer_code=@employer_code";
                }
                var dictionary = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(employerCode))
                {
                    dictionary.Add("@employer_code", employerCode);
                }
                var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<BankActionCodeViewModel>(sql, parameters);
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }

            //return await Context.BankActionCodes.Where(p => p.EmployerCode.Equals(employerCode)).ToListAsync();
        }

        public async Task<BankActionCodeViewModel> GetByEmployerAndCode(string employerCode, string actionCodeId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select bank_action_id,bank_action_code_id,employer_code,bank_action_code_name 
from bank_action_code 
where bank_action_code_id=@bank_action_code_id 
and employer_code=@employer_code";
                var rowCount = await conn.QueryAsync<BankActionCodeViewModel>(sql, new { 
                    bank_action_code_id = actionCodeId,
                    employer_code = employerCode });
                return rowCount.SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BankActionCode> GetByKey(int id)
        {
            var model = await Context.BankActionCodes.FindAsync(id);
            return model;
        }

        public async Task Update(BankActionCode model)
        {
            var result = await Context.BankActionCodes.FindAsync(model.BankActionId);
            result.BankActionCodeName = model.BankActionCodeName;
            await Context.SaveChangesAsync();
        }
    }
}

