using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class LoaneeRemarkRepository : BaseRepository, ILoaneeRemarkRepository
    {
        public LoaneeRemarkRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public bool BankActionCodeIsExist(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select count(1) from loanee_remark where bank_action_id=@bank_action_id";
                var rowCount = conn.ExecuteScalar<int>(sql, new
                {
                    bank_action_id = id,
                });
                return rowCount != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool BankPersonCodeIsExist(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select count(1) from loanee_remark where bank_person_id=@bank_person_id";
                var rowCount = conn.ExecuteScalar<int>(sql, new { bank_person_id =id});
                return rowCount != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool BankResultCodeIsExist(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select count(1) from loanee_remark where bank_result_id=@bank_result_id";
                var rowCount = conn.ExecuteScalar<int>(sql, new
                {
                    bank_result_id = id,
                });
                return rowCount != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task BulkInsertOrUpdate(List<LoaneeRemarkExcelViewModel> loaneeRemarks)
        {
            try
            {
                foreach (var item in loaneeRemarks)
                {
                    var loaneeRemark = new LoaneeRemark
                    {
                        CusId = item.CusId,
                        EmployerCode = item.EmployerCode,
                        TransactionDatetime = item.TransactionDatetime,
                        
                        FollowContractNo = item.FollowContractNo,
                        AppointmentDate = DateOnly.FromDateTime(item.AppointmentDate),
                        AppointmentContract = item.AppointmentContract,
                        Amount = item.Amount,
                        Remark = item.Remark,

                        BankActionCodeId = item.BankActionCodeId,
                        BankResultCodeId = item.BankResultCodeId,
                        BankPersonCodeId = item.BankPersonCodeId,
                        CompanyActionCodeId = item.CompanyActionCodeId,
                        CompanyResultCodeId = item.CompanyResultCodeId,

                        BankActionId = item.BankActionId,
                        BankResultId = item.BankResultId,
                        BankPersonId = item.BankPersonId,
                        IsActive = true,
                    };
                    await Context.LoaneeRemarks.AddAsync(loaneeRemark);
                }
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<int> CountByPersonCode(string personCode)
        //{
        //    try
        //    {
        //        using IDbConnection conn = Connection;
        //        conn.Open();
        //        var sql = @"select count(1) from loanee_remark where person_code_id=@person_code_id";
        //        var rowCount = await conn.ExecuteScalarAsync<int>(sql, new { person_code_id  =personCode});
        //        return rowCount;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

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

        public async Task<List<LoaneeRemarkViewModel>> GetByCusId(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT 
    lr.loanee_remark_id,
    lr.cus_id,
    lr.transaction_datetime,
    -- lr.bank_action_code_id,
    -- lr.bank_result_code_id,
    -- lr.company_action_code_id,
    -- lr.company_result_code_id,
    lr.follow_contract_no,
    lr.appointment_date,
    lr.amount,
    lr.appointment_contract,
    lr.remark,
    lr.employer_code,
    -- lr.bank_person_code_id,
    lr.bank_action_id,
    lr.bank_result_id,
    lr.bank_person_id,
    (select concat( ba.bank_action_code_id,':',bank_action_code_name ) from bank_action_code ba where lr.bank_action_id = ba.bank_action_id and lr.employer_code =ba.employer_code) as bank_action_code_name,
    (select concat( br.bank_result_code_id,':',bank_result_code_name) from bank_result_code br where lr.bank_result_id = br.bank_result_id and lr.bank_person_id =br.bank_person_id) as bank_result_code_name,
    (select concat( ca.company_action_code_id,':',company_action_code_name) from company_action_code ca where lr.company_action_code_id = ca.company_action_code_id ) as company_action_code_name,
    (select concat( cr.company_result_code_id,':',company_result_code_name) from company_result_code cr where lr.company_result_code_id = cr.company_result_code_id ) as company_result_code_name,
    (select concat( bp.bank_person_code_id,':',bank_person_code_name) from bank_person_code bp where lr.bank_person_id = bp.bank_person_id) as bank_person_code_name
FROM loanee_remark lr
WHERE cus_id = @cus_id
ORDER BY transaction_datetime DESC";
                var result = await conn.QueryAsync<LoaneeRemarkViewModel>(sql, new { cus_id = id });
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<LoaneeRemarkViewModel> GetByKey(int id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT 
    lr.loanee_remark_id,
    lr.cus_id,
    lr.transaction_datetime,
    -- lr.bank_action_code_id,
    -- lr.bank_result_code_id,
    -- lr.company_action_code_id,
    -- lr.company_result_code_id,
    lr.follow_contract_no,
    lr.appointment_date,
    lr.amount,
    lr.appointment_contract,
    lr.remark,
    lr.employer_code,
    -- lr.bank_person_code_id,
    lr.bank_action_id,
    lr.bank_result_id,
    lr.bank_person_id,
    (select concat( ba.bank_action_code_id,':',bank_action_code_name ) from bank_action_code ba where lr.bank_action_id = ba.bank_action_id and lr.employer_code =ba.employer_code) as bank_action_code_name,
    (select concat( br.bank_result_code_id,':',bank_result_code_name) from bank_result_code br where lr.bank_result_id = br.bank_result_id and lr.bank_person_id =br.bank_person_id) as bank_result_code_name,
    (select concat( ca.company_action_code_id,':',company_action_code_name) from company_action_code ca where lr.company_action_code_id = ca.company_action_code_id ) as company_action_code_name,
    (select concat( cr.company_result_code_id,':',company_result_code_name) from company_result_code cr where lr.company_result_code_id = cr.company_result_code_id ) as company_result_code_name,
    (select concat( bp.bank_person_code_id,':',bank_person_code_name) from bank_person_code bp where lr.bank_person_id = bp.bank_person_id) as bank_person_code_name
FROM loanee_remark lr
WHERE loanee_remark_id = @loanee_remark_id";
                var result = await conn.QueryAsync<LoaneeRemarkViewModel>(sql, new { loanee_remark_id = id });
                return result.SingleOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsExisting(int id)
        {
            return Context.LoaneeRemarks.Any(p => p.LoaneeRemarkId == id);
        }

        public async Task Update(int id, LoaneeRemark model)
        {
            var result = await Context.LoaneeRemarks.FindAsync(id);
            result.Remark = model.Remark;
            result.AppointmentContract = model.AppointmentContract;
            result.Amount = model.Amount;
            result.AppointmentDate = model.AppointmentDate;
            result.BankActionCodeId = model.BankActionCodeId;
            result.BankResultCodeId = model.BankResultCodeId;
            result.BankPersonCodeId = model.BankPersonCodeId;
            result.CompanyActionCodeId = model.CompanyActionCodeId;
            result.CompanyResultCodeId = model.CompanyResultCodeId;
            //result.TransactionDatetime = model.TransactionDatetime;
            result.CusId = model.CusId;
            result.FollowContractNo = model.FollowContractNo;
            result.BankActionId = model.BankActionId;
            result.BankPersonId = model.BankPersonId;
            result.BankResultId = model.BankResultId;
            await Context.SaveChangesAsync();
        }
    }
}

