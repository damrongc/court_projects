using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
    public class DashboardRepository : BaseRepository, IDashboardRepository
    {
        public DashboardRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task<IEnumerable<LoaneeRemarkViewModel>> GetEmployeeTodoPaging(string employeeCode, int skip, int take, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT  lnr.loanee_remark_id,
lnr.cus_id,
lnr.transaction_datetime,
lnr.follow_contract_no,
lnr.appointment_date,
lnr.amount,
lnr.appointment_contract,
lnr.remark,
ln.name,
ln.contract_no
FROM loanee_remark lnr,loanee ln
where ln.cus_id=lnr.cus_id
and ln.is_active =1";
                var sb = new StringBuilder();
                sb.Append(sql);
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (name LIKE @filter");
                    sb.Append(" or contract_no LIKE @filter");
                    sb.Append(" )");
                }

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    sb.Append(" and employee_code=@employeeCode");
                }
                sb.Append(" order by appointment_date asc");
                sb.Append(" Limit @skip,@take");
                var dictionary = new Dictionary<string, object>
                    {
                         { "@skip", skip },
                         { "@take", take },
                    };
                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
                }

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    dictionary.Add("@employeeCode", employeeCode);
                }

                var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<LoaneeRemarkViewModel>(sb.ToString(), parameters);
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetEmployeeTodoRecordCount(string employeeCode, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT  COUNT(1) FROM loanee_remark lnr,loanee ln WHERE ln.cus_id=lnr.cus_id AND ln.is_active =1";

                var sb = new StringBuilder();
                sb.Append(sql);
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (name LIKE @filter");
                    sb.Append(" or contract_no LIKE @filter");
                    sb.Append(" )");
                }

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    sb.Append(" and employee_code=@employeeCode");
                }
                var dictionary = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
                }

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    dictionary.Add("@employeeCode", employeeCode);
                }

                var parameters = new DynamicParameters(dictionary);
                var rowCount = await conn.ExecuteScalarAsync<int>(sb.ToString(), parameters);
                return rowCount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<LoaneeSummaryViewModel>> GetLoaneeSummary(int groupId, string employeeCode)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();

                var sb = new StringBuilder();
                sb.Append("select ln.employer_code,count(1) as loanee_count");
                sb.Append(",IFNULL(sum(wo_balance),0) as total_amount");
                sb.Append(",employer_name");
                sb.Append(" from loanee ln,employer emp");
                sb.Append(" where ln.employer_code =emp.employer_code");

                switch (groupId)
                {
                    case 2:
                        sb.Append(" and exists (select employer_code from user_employer_mapping mp where mp.employer_code=ln.employer_code and user_id=@employee_code)");
                        break;
                    case 3:
                    case 4:
                        sb.Append(" and ln.employee_code =@employee_code");
                        break;
                }
                //if (!string.IsNullOrEmpty(employerCode))
                //{
                //    sb.Append(" and ln.employer_code=@employer_code");
                //}
                //if (!string.IsNullOrEmpty(employeeCode))
                //{
                //    sb.Append(" and ln.employee_code=@employee_code");
                //}
                sb.Append(" and ln.is_active=1");
                sb.Append(" group by ln.employer_code");



                var dictionary = new Dictionary<string, object>();
                switch (groupId)
                {
                    case 2:
                        dictionary.Add("@employee_code", employeeCode);
                        break;
                    case 3:
                    case 4:
                        dictionary.Add("@employee_code", employeeCode);
                        break;
                }

                //if (!string.IsNullOrEmpty(employeeCode))
                //{
                //    dictionary.Add("@employer_code", employerCode);
                //}
                //if (!string.IsNullOrEmpty(employeeCode))
                //{
                //    dictionary.Add("@employee_code", employeeCode);
                //}

                var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<LoaneeSummaryViewModel>(sb.ToString(), parameters);
                return result.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<LoaneeSummaryViewModel>> GetPaymentSummary(string employeeCode, string employerCode, string startDate, string endDate)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();

                var sb = new StringBuilder();
                sb.Append(" select DATE_FORMAT(payment_date,'%Y-%m') as yearmonth,sum(amount) as total_amount,a.employer_code");
                sb.Append(" from payment a,loanee c");
                sb.Append(" where a.cus_id =c.cus_id");
                sb.Append(" and c.is_active=1");
                sb.Append(" and a.payment_date between @start_date and @end_date");
       

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    sb.Append(" and c.employee_code=@employee_code");
                }

                if (!string.IsNullOrEmpty(employerCode))
                {
                    sb.Append(" and a.employer_code=@employer_code");
                }
                sb.Append(" group by yearmonth,a.employer_code");

                var dictionary = new Dictionary<string, object>
                {
                    { "@start_date", startDate },
                    { "@end_date", endDate }
                };

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    dictionary.Add("@employee_code", employeeCode);
                }
                if (!string.IsNullOrEmpty(employerCode))
                {
                    dictionary.Add("@employer_code", employerCode);
                }

                var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<LoaneeSummaryViewModel>(sb.ToString(), parameters);
                return result.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<RemainTaskViewModel>> GetRemainTaskPaging(string employeeCode, int skip, int take, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT task_id,task_datetime,task_detail,assign_to,assign_from,user_name as assign_from_name
FROM remain_task a,app_user b WHERE a.assign_from =b.user_id";
                var sb = new StringBuilder();
                sb.Append(sql);
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (task_detail LIKE @filter");
                    sb.Append(" or assign_to LIKE @filter");
                    sb.Append(" )");
                }

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    sb.Append(" and assign_to=@employeeCode");
                }
                sb.Append(" order by task_datetime desc");
                sb.Append(" Limit @skip,@take");
                var dictionary = new Dictionary<string, object>
                    {
                         { "@skip", skip },
                         { "@take", take },
                    };
                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
                }

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    dictionary.Add("@employeeCode", employeeCode);
                }

                var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<RemainTaskViewModel>(sb.ToString(), parameters);
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetRemainTaskRecordCount(string employeeCode, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();

                sb.Append("SELECT count(1) FROM remain_task a,app_user b WHERE a.assign_from =b.user_id");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (task_detail LIKE @filter");
                    sb.Append(" or assign_to LIKE @filter");
                    sb.Append(" )");
                }

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    sb.Append(" and assign_to=@employeeCode");
                }


                var dictionary = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
                }

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    dictionary.Add("@employeeCode", employeeCode);
                }

                var parameters = new DynamicParameters(dictionary);
                var rowCount = await conn.ExecuteScalarAsync<int>(sb.ToString(), parameters);
                return rowCount;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
