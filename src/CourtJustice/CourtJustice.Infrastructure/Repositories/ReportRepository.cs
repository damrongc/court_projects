using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public ReportRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task<List<PaymentExcelViewModel>> GetLoaneeReceipt(string employerCode, string startDate, string endDate)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select pm.payment_date  as receipt_date,pm.booking_date,pm.amount as total_received,pm.wo_balance,pm.start_overdue_status,pm.end_overdue_status");
                sb.Append(" ,ln.assign_date,ln.expire_date,ln.cus_id,ln.name as cus_name,ln.contract_no");
                sb.Append(" ,user_name as user_created");
                sb.Append(" from payment pm, loanee ln,app_user");
                sb.Append(" where pm.cus_id = ln.cus_id");
                sb.Append(" and app_user.user_id = ln.employee_code");
                sb.Append(" and pm.payment_date between @start_date and @end_date");

                if (!string.IsNullOrEmpty(employerCode))
                {
                    sb.Append(" and pm.employer_code =@employer_code");
                }

                var dictionary = new Dictionary<string, object>
                {
                    { "@start_date", startDate },
                    { "@end_date", endDate }
                };
                if (!string.IsNullOrEmpty(employerCode))
                {
                    dictionary.Add("@employer_code", employerCode);
                }
                    var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<PaymentExcelViewModel>(sb.ToString(), parameters);
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<LoaneeRemarkExcelViewModel>> GetLoaneeRemark(string employerCode, string startDate, string endDate)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();
                sb.Append("select pm.cus_id,transaction_datetime");
                sb.Append(" ,pm.bank_action_code_id");
                sb.Append(" ,bank_action_code_name");
                sb.Append(" ,pm.bank_result_code_id");
                sb.Append(" ,bank_result_code_name");
                sb.Append(" ,pm.company_action_code_id");
                sb.Append(" ,company_action_code_name");
                sb.Append(" ,pm.company_result_code_id");
                sb.Append(" ,company_result_code_name");
                sb.Append(" ,pm.follow_contract_no");
                sb.Append(" ,pm.appointment_date");
                sb.Append(" ,pm.amount");
                sb.Append(" ,pm.appointment_contract");
                sb.Append(" ,pm.remark");
                sb.Append(" ,ln.assign_date,ln.expire_date,ln.name as cus_name,ln.contract_no");
                sb.Append(" ,user_name as collector");
                sb.Append(" from loanee_remark pm");
                sb.Append(" ,loanee ln");
                sb.Append(" ,app_user");
                sb.Append(" ,bank_result_code bankr");
                sb.Append(" ,bank_action_code banka");
                sb.Append(" ,company_action_code coma");
                sb.Append(" ,company_result_code comr");
                sb.Append(" where pm.cus_id = ln.cus_id");
                sb.Append(" and app_user.user_id = ln.employee_code");
                sb.Append(" and pm.bank_result_code_id =bankr.bank_result_code_id");
                sb.Append(" and pm.bank_action_code_id =banka.bank_action_code_id");
                sb.Append(" and pm.company_result_code_id =comr.company_result_code_id");
                sb.Append(" and pm.company_action_code_id =coma.company_action_code_id");
                sb.Append(" and date(pm.transaction_datetime) between @start_date and @end_date");
                if (!string.IsNullOrEmpty(employerCode))
                {
                    sb.Append(" and pm.employer_code =@employer_code");
                }
                var dictionary = new Dictionary<string, object>
                {
                    { "@start_date", startDate },
                    { "@end_date", endDate }
                };
                if (!string.IsNullOrEmpty(employerCode))
                {
                    dictionary.Add("@employer_code", employerCode);
                }
                var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<LoaneeRemarkExcelViewModel>(sb.ToString(), parameters);
                return result.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<LoaneeNoticeViewModel> GetNotice(string cusId)
        {
            throw new NotImplementedException();
        }
    }
}
