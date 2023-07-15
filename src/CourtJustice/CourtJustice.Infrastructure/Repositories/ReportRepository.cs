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
                sb.Append(" ,ln.assign_date,ln.expire_date,ln.nationality_id,ln.cus_id,ln.name as cus_name,ln.contract_no");
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
                var sql = @"SELECT 
    lr.loanee_remark_id,
    lr.cus_id,
    lr.transaction_datetime,
    -- lr.bank_action_code_id,
    -- lr.bank_result_code_id,
    -- lr.company_action_code_id,
    -- lr.company_result_code_id,
    -- lr.bank_person_code_id,
    lr.follow_contract_no,
    lr.appointment_date,
    lr.amount,
    lr.appointment_contract,
    lr.remark,
    lr.employer_code,
    
    (SELECT user_name FROM app_user WHERE app_user.user_id = ln.employee_code) AS collector,
    ln.assign_date,
    ln.expire_date,
    ln.nationality_id,
    ln.name AS cus_name,
    ln.contract_no,
    ln.employer_work_group,
	(select bank_action_code_id  from bank_action_code ba where lr.bank_action_id = ba.bank_action_id and lr.employer_code =ba.employer_code) as bank_action_code_id,
    (select bank_result_code_id from bank_result_code br where lr.bank_result_id = br.bank_result_id and lr.bank_person_id =br.bank_person_id) as bank_result_code_id,
    (select company_action_code_id from company_action_code ca where lr.company_action_code_id = ca.company_action_code_id ) as company_action_code_id,
    (select company_result_code_id from company_result_code cr where lr.company_result_code_id = cr.company_result_code_id ) as company_result_code_id,
    (select bank_person_code_id from bank_person_code bp where lr.bank_person_id = bp.bank_person_id) as bank_person_code_id,
    (select bank_action_code_name  from bank_action_code ba where lr.bank_action_id = ba.bank_action_id and lr.employer_code =ba.employer_code) as bank_action_code_name,
    (select bank_result_code_name from bank_result_code br where lr.bank_result_id = br.bank_result_id and lr.bank_person_id =br.bank_person_id) as bank_result_code_name,
    (select company_action_code_name from company_action_code ca where lr.company_action_code_id = ca.company_action_code_id ) as company_action_code_name,
    (select company_result_code_name from company_result_code cr where lr.company_result_code_id = cr.company_result_code_id ) as company_result_code_name,
    (select bank_person_code_name from bank_person_code bp where lr.bank_person_id = bp.bank_person_id) as bank_person_code_name
FROM loanee_remark lr,loanee ln
WHERE lr.cus_id = ln.cus_id
AND ln.is_active=1
        AND DATE(lr.transaction_datetime) BETWEEN @start_date AND @end_date";
                var sb = new StringBuilder();
                sb.Append(sql);
                if (!string.IsNullOrEmpty(employerCode))
                {
                    sb.Append(" AND lr.employer_code =@employer_code");
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
