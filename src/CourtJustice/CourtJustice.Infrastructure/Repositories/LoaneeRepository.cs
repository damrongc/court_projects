using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
    public class LoaneeRepository : BaseRepository, ILoaneeRepository
    {
        public LoaneeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(Loanee model)
        {
            await Context.Loanees.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<Loanee>> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<LoaneeViewModel> GetByKey(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT loan_number,cus_id,name,phone_number,
concat(address,' ',b.sub_district_name,' ',b.district_name,' ',b.province_name,' ',b.postal_code) AS address,
concat(address1,' ',c.sub_district_name,' ',c.district_name,' ',c.province_name,' ',c.postal_code) AS address1,
concat(address2,' ',d.sub_district_name,' ',d.district_name,' ',d.province_name,' ',d.postal_code) AS address2,
occupation_name,installments_by_contract,installments_by_agree,
last_paid_date,first_paid_date,interete_rate,interete_rate_amount,overdue_amount,
due_date,follow_up_date,paid_amount,paid_in_month_amount,total_amount,
remaining_amount,overdue_day_amount

FROM loanee a
LEFT join address_set b ON a.address_id =b.address_id
LEFT JOIN address_set c ON a.address1id =c.address_id
LEFT JOIN address_set d ON a.address2id =d.address_id
LEFT JOIN occupation e ON a.occupation_id =e.occupation_id
WHERE cus_id=@cus_id";
               
                var result = await conn.QueryAsync<LoaneeViewModel>(sql, new { cus_id =id});
                return result.SingleOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<LoaneeViewModel>> GetPaging(int skip, int take, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT loan_number,cus_id,name,phone_number,
concat(address,' ',b.sub_district_name,' ',b.district_name,' ',b.province_name,' ',b.postal_code) AS address,
concat(address1,' ',c.sub_district_name,' ',c.district_name,' ',c.province_name,' ',c.postal_code) AS address1,
concat(address2,' ',d.sub_district_name,' ',d.district_name,' ',d.province_name,' ',d.postal_code) AS address2,
occupation_name,installments_by_contract,installments_by_agree,
last_paid_date,first_paid_date,interete_rate,interete_rate_amount,overdue_amount,
due_date,follow_up_date,paid_amount,paid_in_month_amount,total_amount,
remaining_amount,overdue_day_amount

FROM loanee a
LEFT join address_set b ON a.address_id =b.address_id
LEFT JOIN address_set c ON a.address1id =c.address_id
LEFT JOIN address_set d ON a.address2id =d.address_id
LEFT JOIN occupation e ON a.occupation_id =e.occupation_id";
                var sb = new StringBuilder();
                sb.Append(sql);
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (cus_id LIKE @filter");
                    sb.Append(" or name LIKE @filter");
                    sb.Append(" )");
                }
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
                var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<LoaneeViewModel>(sb.ToString(), parameters);
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetRecordCount(string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();

                sb.Append("select count(1) from loanee");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" where (cus_id LIKE @filter");
                    sb.Append(" or name LIKE @filter");
                    sb.Append(" )");
                }
                var dictionary = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
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

        public async Task Update(int id, Loanee model)
        {
            throw new NotImplementedException();
        }
    }
}

