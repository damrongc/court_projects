using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Inventor.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Globalization;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
    public class LoaneeRepository : BaseRepository, ILoaneeRepository
    {
        public LoaneeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task BulkInsert(List<LoaneeViewModel> loanees)
        {
            try
            {
                foreach (var item in loanees)
                {
                    var loanee = new Loanee();
                    loanee.AssignDate = item.AssignDate.ToDateOnly();
                    loanee.ExpireDate = item.ExpireDate.ToDateOnly();
                    loanee.NationalityId = item.NationalityId;
                    loanee.BirthDate = item.BirthDate.ToDateOnly();
                    loanee.CusId = item.CusId;
                    loanee.Name = item.Name;
                    loanee.ContractNo = item.ContractNo;
                    loanee.ContractDate = item.ContractDate.ToDateOnly();
                    loanee.WODate = item.WODate.ToDateOnly();
                    loanee.Term = item.Term;
                    loanee.InstallmentsByContract = item.InstallmentsByContract;
                    loanee.LoanAmount = item.LoanAmount;
                    loanee.WOBalance = item.WOBalance;
                    loanee.OverdueAmount = item.OverdueAmount;
                    loanee.TotalPenalty = item.TotalPenalty;
                    loanee.ClosingAmount = item.ClosingAmount;
                    loanee.RcvAmtBeforeWO = item.RcvAmtBeforeWO;
                    loanee.RcvAmtAfterWO = item.RcvAmtAfterWO;
                    loanee.LastPaidAmount = item.LastPaidAmount;
                    loanee.NoOfAssignment = item.NoOfAssignment;
                    loanee.Description = item.Description;
                    loanee.HomeAddress1 = item.HomeAddress1;
                    loanee.HomeAddress2 = item.HomeAddress2;
                    loanee.HomeAddress3 = item.HomeAddress3;
                    loanee.HomeAddress4 = item.HomeAddress4;
                    loanee.TelephoneHome = item.TelephoneHome;
                    loanee.OfficeAddress1 = item.OfficeAddress1;
                    loanee.OfficeAddress2 = item.OfficeAddress2;
                    loanee.OfficeAddress3 = item.OfficeAddress3;
                    loanee.OfficeAddress4 = item.OfficeAddress4;
                    loanee.TelephoneOffice = item.TelephoneOffice;
                    loanee.IdenAddress1 = item.IdenAddress1;
                    loanee.IdenAddress2 = item.IdenAddress2;
                    loanee.IdenAddress3 = item.IdenAddress3;
                    loanee.IdenAddress4 = item.IdenAddress4;
                    loanee.MobileHome = item.MobileHome;
                    loanee.MobileOffice = item.MobileOffice;
                    loanee.MobileEmg = item.MobileEmg;
                    loanee.SpecialNote = item.SpecialNote;
                    loanee.CPCase = item.CPCase;
                    loanee.NoOfCP = item.NoOfCP;
                    loanee.BucketId = item.BucketId;
                    loanee.CPDate = item.CPDate.ToDateOnly();
                    loanee.OAFee = item.OAFee;
                    loanee.MaxOAFeeAmount = item.MaxOAFeeAmount;
                    loanee.MaxOAFeeBalance = item.MaxOAFeeBalance;
                    await Context.Loanees.AddAsync(loanee);
                }
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
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
                var sql = @"select * from loanee where cus_id=@cus_id";
               
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
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("th-TH");
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select * from loanee";
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

