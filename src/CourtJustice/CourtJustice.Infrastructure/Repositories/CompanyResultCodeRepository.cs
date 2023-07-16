using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class CompanyResultCodeRepository : BaseRepository, ICompanyResultCodeRepository
    {
        public CompanyResultCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(CompanyResultCode model)
        {
            await Context.CompanyResultCodes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.CompanyResultCodes.FindAsync(id);
            if(model is not null)
            {
                Context.CompanyResultCodes.Remove(model);
                await Context.SaveChangesAsync();
            }
            
        }

        public async Task<List<CompanyResultCodeViewModel>> GetAll()
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select 
company_result_id
,company_result_code_id
,company_result_code_name
,company_action_id
,is_active
from company_result_code";
                var result = await conn.QueryAsync<CompanyResultCodeViewModel>(sql);
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }

            //return await Context.CompanyResultCodes.ToListAsync();
        }

        public async Task<List<CompanyResultCodeViewModel>> GetByCompanyActionId(int companyActionId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select 
company_result_id
,company_result_code_id
,company_result_code_name
,company_action_id
,is_active
from company_result_code
where company_action_id=@company_action_id";
                var result = await conn.QueryAsync<CompanyResultCodeViewModel>(sql, new { company_action_id = companyActionId });
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CompanyResultCodeViewModel> GetByKey(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT 
    a.company_result_id,
    a.company_result_code_id,
    a.company_result_code_name,
    a.company_action_id,
    a.is_active,
    b.company_action_code_id,
    b.company_action_code_name
FROM
    company_result_code a,
    company_action_code b
WHERE
    a.company_action_id = b.company_action_id
        AND a.company_result_id = @id";
                var result = await conn.QueryAsync<CompanyResultCodeViewModel>(sql,new {id});
                return result.SingleOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsExisting(int companyActionId, string companyResultCodeId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select count(1) from company_result_code where  company_action_id =@company_action_id and company_result_code_id =@company_result_code_id";
                var rowCount = conn.ExecuteScalar<int>(sql, new { company_action_id = companyActionId, company_result_code_id = companyResultCodeId });
                return rowCount != 0;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsHaveActionCode(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select count(1) from company_result_code where company_action_id =@id";
                var rowCount = conn.ExecuteScalar<int>(sql, new { id });
                return rowCount != 0;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(int id, CompanyResultCode model)
        {
            var result = await Context.CompanyResultCodes.FindAsync(id);
            result.CompanyResultCodeId = model.CompanyResultCodeId;
            result.CompanyResultCodeName = model.CompanyResultCodeName;
            result.NotCallFlag = model.NotCallFlag;
            result.ShowHideFlag = model.ShowHideFlag;
            //result.CompanyActionId = model.CompanyActionId;

            await Context.SaveChangesAsync();
        }
    }
}

