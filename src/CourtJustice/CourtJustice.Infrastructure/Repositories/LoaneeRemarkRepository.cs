using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
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
                        TransactionDatetime= item.TransactionDatetime,
                        BankActionCodeId= item.BankActionCodeId,
                        BankResultCodeId  =item.BankResultCodeId, 
                        CompanyActionCodeId= item.CompanyActionCodeId,
                        CompanyResultCodeId= item.CompanyResultCodeId,
                        FollowContractNo= item.FollowContractNo,
                        AppointmentDate =DateOnly.FromDateTime( item.AppointmentDate),
                        AppointmentContract = item.AppointmentContract,
                        Amount=item.Amount,
                        Remark=item.Remark,
                        IsActive=true,
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

        //public async Task<List<LoaneeRemarkViewModel>> GetAll()
        //{
        //    try
        //    {
        //        using IDbConnection conn = Connection;
        //        conn.Open();
        //        var sb = new StringBuilder();
        //        sb.Append("select loanee_remark.*, " +
        //            " bank_action_code.bank_action_code_name," +
        //            " bank_result_code.bank_result_code_name, " +
        //            " company_action_code.company_action_name as company_action_code_name," +
        //            " company_result_code.company_result_code_name " +
        //            " from loanee_remark " +
        //            " left outer JOIN bank_result_code ON loanee_remark.bank_result_code_id = bank_result_code.bank_result_code_id " +
        //            " left OUTER JOIN bank_action_code ON loanee_remark.bank_action_code_id = bank_action_code.bank_action_code_id " +
        //            " left OUTER JOIN company_action_code ON loanee_remark.company_action_code_id = company_action_code.company_action_code_id " +
        //            " left OUTER JOIN company_result_code ON loanee_remark.company_result_code_id = company_result_code.company_result_code_id " +
        //            " where cus_id=@cus_id");

        //        var result = await conn.QueryAsync<LoaneeRemarkViewModel>(sb.ToString(), new { cus_id = id });
        //        return result.ToList();

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        
        public async Task<List<LoaneeRemarkViewModel>> GetByCusId(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select loanee_remark.*, " +
                    " bank_action_code.bank_action_code_name," +
                    " bank_result_code.bank_result_code_name, " +
                    " company_action_code.company_action_code_name," +
                    " company_result_code.company_result_code_name " +
                    " from loanee_remark " +
                    " left outer JOIN bank_result_code ON loanee_remark.bank_result_code_id = bank_result_code.bank_result_code_id " +
                    " left OUTER JOIN bank_action_code ON loanee_remark.bank_action_code_id = bank_action_code.bank_action_code_id " +
                    " left OUTER JOIN company_action_code ON loanee_remark.company_action_code_id = company_action_code.company_action_code_id " +
                    " left OUTER JOIN company_result_code ON loanee_remark.company_result_code_id = company_result_code.company_result_code_id " +
                    " where cus_id=@cus_id");

                var result = await conn.QueryAsync<LoaneeRemarkViewModel>(sb.ToString(), new { cus_id = id });
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
                var sb = new StringBuilder();
                sb.Append("select loanee_remark.*, " +
                    " bank_action_code.bank_action_code_name," +
                    " bank_result_code.bank_result_code_name, " +
                    " company_action_code.company_action_code_name," +
                    " company_result_code.company_result_code_name " +
                    " from loanee_remark " +
                    " left outer JOIN bank_result_code ON loanee_remark.bank_result_code_id = bank_result_code.bank_result_code_id " +
                    " left OUTER JOIN bank_action_code ON loanee_remark.bank_action_code_id = bank_action_code.bank_action_code_id " +
                    " left OUTER JOIN company_action_code ON loanee_remark.company_action_code_id = company_action_code.company_action_code_id " +
                    " left OUTER JOIN company_result_code ON loanee_remark.company_result_code_id = company_result_code.company_result_code_id " +
                    " where loanee_remark_id=@loanee_remark_id");

                var result = await conn.QueryAsync<LoaneeRemarkViewModel>(sb.ToString(), new { loanee_remark_id = id });
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
            result.CompanyActionCodeId = model.CompanyActionCodeId;
            result.CompanyResultCodeId = model.CompanyResultCodeId;
            result.TransactionDatetime = model.TransactionDatetime;
            result.CusId = model.CusId;
            result.FollowContractNo = model.FollowContractNo;

            await Context.SaveChangesAsync();
        }



    }
}

