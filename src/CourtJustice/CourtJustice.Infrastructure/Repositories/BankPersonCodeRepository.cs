using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class BankPersonCodeRepository : BaseRepository, IBankPersonCodeRepository
    {
        public BankPersonCodeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(BankPersonCode model)
        {
            await Context.BankPersonCodes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"delete from bank_person_code where bank_person_id=@bank_person_id";

                await conn.ExecuteAsync(sql, new { bank_person_id = id});

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BankPersonCodeViewModel>> GetByBankActionId(int bankActionId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select bank_person_id,bank_person_code_id,bank_person_code_name,bank_action_id,is_active
,(select count(1) from bank_result_code where bank_result_code.bank_person_id=bank_person_code.bank_person_id) as bank_result_code_count
from bank_person_code 
where bank_action_id =@bank_action_id";

                var result = await conn.QueryAsync<BankPersonCodeViewModel>(sql, new { bank_action_id = bankActionId});
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BankPersonCodeViewModel> GetByKey(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select * from bank_person_code 
where bank_person_id=@bank_person_id";
                //                var sql = @"select * from bank_person_code 
                //where employer_code =@employer_code
                //and bank_action_code_id =@bank_action_code_id
                //and bank_person_code_id =@bank_person_code_id";

                var result = await conn.QueryAsync<BankPersonCodeViewModel>(sql, new { bank_person_id=id });
                return result.SingleOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsExisting(int bankActionId, string bankPersonCodeId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select count(1) from bank_person_code 
where  bank_action_id =@bank_action_id
and bank_person_code_id =@bank_person_code_id";
                //                var sql = @"select count(1) from bank_person_code 
                //where employer_code =@employer_code
                //and bank_action_code_id =@bank_action_code_id
                //and bank_person_code_id =@bank_person_code_id";

                var rowCount = conn.ExecuteScalar<int>(sql, new { bank_action_id = bankActionId, bank_person_code_id = bankPersonCodeId });

                return rowCount == 0 ? false : true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(int id, BankPersonCode model)
        {
            var bankPersonCode = await Context.BankPersonCodes.FindAsync(id);
            if (bankPersonCode is not null)
            {
                bankPersonCode.BankPersonCodeId = model.BankPersonCodeId;
                bankPersonCode.BankPersonCodeName = model.BankPersonCodeName;
                await Context.SaveChangesAsync();
            }

        }
    }
}
