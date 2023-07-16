using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class CompanyActionCodeRepository : BaseRepository, ICompanyActionCodeRepository
    {
        public CompanyActionCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(CompanyActionCode model)
        {
            await Context.CompanyActionCodes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.CompanyActionCodes.FindAsync(id);
            Context.CompanyActionCodes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<CompanyActionCodeViewModel>> GetAll()
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select 
company_action_id
,company_action_code_id
,company_action_code_name
,company_id
,is_active
from company_action_code
order by company_action_code_id asc";
                var result = await conn.QueryAsync<CompanyActionCodeViewModel>(sql);
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
            //return await Context.CompanyActionCodes.ToListAsync();
        }

        public async Task<CompanyActionCode> GetByKey(int id)
        {
//            try
//            {
//                using IDbConnection conn = Connection;
//                conn.Open();
//                var sql = @"select 
//company_action_id
//,company_action_code_id
//,company_action_code_name
//,company_id
//,is_active
//from company_action_code
//where company_action_id=@id";
//                var result = await conn.QueryAsync<CompanyActionCodeViewModel>(sql,new { id });
//                return result.SingleOrDefault();

//            }
//            catch (Exception)
//            {

//                throw;
//            }
            var model = await Context.CompanyActionCodes.FindAsync(id);
            return model;
        }

        public async Task Update(int id, CompanyActionCode model)
        {
            var result = await Context.CompanyActionCodes.FindAsync(id);
            result.CompanyActionCodeId = model.CompanyActionCodeId;
            result.CompanyActionCodeName = model.CompanyActionCodeName;
            //result.CompanyId = model.CompanyId;

            await Context.SaveChangesAsync();
        }
    }
}

