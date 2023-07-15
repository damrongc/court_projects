using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class BankResultCodeRepository : BaseRepository, IBankResultCodeRepository
    {
        public BankResultCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task<int> CountByEmployerAndCode(string employerCode, string resultCodeId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select count(1) from bank_result_code 
where bank_result_code_id=@bank_result_code_id 
and employer_code=@employer_code";
                var rowCount = await conn.ExecuteScalarAsync<int>(sql, new { bank_result_code_id = resultCodeId, employer_code = employerCode });
                return rowCount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(BankResultCode model)
        {
            await Context.BankResultCodes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.BankResultCodes.FindAsync(id);
            Context.BankResultCodes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteByBankPersonId(int bankPersonId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"delete from bank_result_code where bank_person_id =@bank_person_id";
                await conn.ExecuteAsync(sql, new { bank_person_id = bankPersonId });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BankResultCodeViewModel>> GetAll()
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select bank_result_id,bank_result_code_id,bank_result_code_name,a.employer_code,b.employer_name
from bank_result_code a,employer b
where a.employer_code =b.employer_code";
                var result = await conn.QueryAsync<BankResultCodeViewModel>(sql);
                return result.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BankResultCodeViewModel>> GetByBankPersonId(int bankPersonId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select bank_result_id
,bank_result_code_id
,bank_result_code_name
,bank_person_id
,is_active
from bank_result_code
where bank_person_id =@bank_person_id";

                var result = await conn.QueryAsync<BankResultCodeViewModel>(sql,new { bank_person_id  =bankPersonId});
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BankResultCodeViewModel>> GetByEmployer(string employerCode)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select bank_result_id,bank_result_code_id,bank_result_code_name,a.employer_code,b.employer_name
from bank_result_code a,employer b
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
                var result = await conn.QueryAsync<BankResultCodeViewModel>(sql, parameters);
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }

            //return await Context.BankResultCodes.Where(p => p.EmployerCode.Equals(employerCode)).ToListAsync();
        }

        public async Task<BankResultCodeViewModel> GetByEmployerAndCode(string employerCode, string resultCodeId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select bank_result_id,bank_result_code_id,employer_code,bank_result_code_name
from bank_result_code 
where bank_result_code_id=@bank_result_code_id 
and employer_code=@employer_code";
                var bankResult = await conn.QueryAsync<BankResultCodeViewModel>(sql
                    , new { bank_result_code_id = resultCodeId, employer_code = employerCode });
                return bankResult.SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BankResultCode> GetByKey(int id)
        {
            var model = await Context.BankResultCodes.FindAsync(id);
            return model;
        }

        public async Task Update(int id, BankResultCode model)
        {
            var result = await Context.BankResultCodes.FindAsync(id);
            result.BankResultCodeId = model.BankResultCodeId;
            result.BankResultCodeName = model.BankResultCodeName;
            await Context.SaveChangesAsync();
        }
    }
}

