using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Inventor.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
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

        public async Task BulkInsertOrUpdate(List<LoaneeViewModel> loanees)
        {
            try
            {
                foreach (var item in loanees)
                {

                    var existingLoanee = await Context.Loanees.FindAsync(item.CusId);
                    if (existingLoanee != null)
                    {
                        existingLoanee.Term = item.Term;
                        existingLoanee.InstallmentsByContract = item.InstallmentsByContract;
                        existingLoanee.LoanAmount = item.LoanAmount;
                        existingLoanee.WOBalance = item.WOBalance;
                        existingLoanee.OverdueAmount = item.OverdueAmount;
                        existingLoanee.TotalPenalty = item.TotalPenalty;
                        existingLoanee.ClosingAmount = item.ClosingAmount;
                        existingLoanee.RcvAmtBeforeWO = item.RcvAmtBeforeWO;
                        existingLoanee.RcvAmtAfterWO = item.RcvAmtAfterWO;
                        existingLoanee.LastPaidAmount = item.LastPaidAmount;
                        existingLoanee.NoOfAssignment = item.NoOfAssignment;
                        existingLoanee.Description = item.Description;
                        existingLoanee.HomeAddress1 = item.HomeAddress1;
                        existingLoanee.HomeAddress2 = item.HomeAddress2;
                        existingLoanee.HomeAddress3 = item.HomeAddress3;
                        existingLoanee.HomeAddress4 = item.HomeAddress4;
                        existingLoanee.TelephoneHome = item.TelephoneHome;
                        existingLoanee.OfficeAddress1 = item.OfficeAddress1;
                        existingLoanee.OfficeAddress2 = item.OfficeAddress2;
                        existingLoanee.OfficeAddress3 = item.OfficeAddress3;
                        existingLoanee.OfficeAddress4 = item.OfficeAddress4;
                        existingLoanee.TelephoneOffice = item.TelephoneOffice;
                        existingLoanee.IdenAddress1 = item.IdenAddress1;
                        existingLoanee.IdenAddress2 = item.IdenAddress2;
                        existingLoanee.IdenAddress3 = item.IdenAddress3;
                        existingLoanee.IdenAddress4 = item.IdenAddress4;
                        existingLoanee.MobileHome = item.MobileHome;
                        existingLoanee.MobileOffice = item.MobileOffice;
                        existingLoanee.MobileEmg = item.MobileEmg;
                        existingLoanee.SpecialNote = item.SpecialNote;
                        existingLoanee.CPCase = item.CPCase;
                        existingLoanee.NoOfCP = item.NoOfCP;
                        existingLoanee.CPDate = item.CPDate.ToDateOnly();
                        existingLoanee.OAFee = item.OAFee;
                        existingLoanee.MaxOAFeeAmount = item.MaxOAFeeAmount;
                        existingLoanee.MaxOAFeeBalance = item.MaxOAFeeBalance;
                    }
                    else
                    {
                        var loanee = new Loanee
                        {
                            AssignDate = item.AssignDate.ToDateOnly(),
                            ExpireDate = item.ExpireDate.ToDateOnly(),
                            NationalityId = item.NationalityId,
                            BirthDate = item.BirthDate.ToDateOnly(),
                            CusId = item.CusId,
                            Name = item.Name,
                            ContractNo = item.ContractNo,
                            ContractDate = item.ContractDate.ToDateOnly(),
                            WODate = item.WODate.ToDateOnly(),
                            Term = item.Term,
                            InstallmentsByContract = item.InstallmentsByContract,
                            LoanAmount = item.LoanAmount,
                            WOBalance = item.WOBalance,
                            OverdueAmount = item.OverdueAmount,
                            TotalPenalty = item.TotalPenalty,
                            ClosingAmount = item.ClosingAmount,
                            RcvAmtBeforeWO = item.RcvAmtBeforeWO,
                            RcvAmtAfterWO = item.RcvAmtAfterWO,
                            LastPaidAmount = item.LastPaidAmount,
                            NoOfAssignment = item.NoOfAssignment,
                            Description = item.Description,
                            HomeAddress1 = item.HomeAddress1,
                            HomeAddress2 = item.HomeAddress2,
                            HomeAddress3 = item.HomeAddress3,
                            HomeAddress4 = item.HomeAddress4,
                            TelephoneHome = item.TelephoneHome,
                            OfficeAddress1 = item.OfficeAddress1,
                            OfficeAddress2 = item.OfficeAddress2,
                            OfficeAddress3 = item.OfficeAddress3,
                            OfficeAddress4 = item.OfficeAddress4,
                            TelephoneOffice = item.TelephoneOffice,
                            IdenAddress1 = item.IdenAddress1,
                            IdenAddress2 = item.IdenAddress2,
                            IdenAddress3 = item.IdenAddress3,
                            IdenAddress4 = item.IdenAddress4,
                            MobileHome = item.MobileHome,
                            MobileOffice = item.MobileOffice,
                            MobileEmg = item.MobileEmg,
                            SpecialNote = item.SpecialNote,
                            CPCase = item.CPCase,
                            NoOfCP = item.NoOfCP,
                            CPDate = item.CPDate.ToDateOnly(),
                            OAFee = item.OAFee,
                            MaxOAFeeAmount = item.MaxOAFeeAmount,
                            MaxOAFeeBalance = item.MaxOAFeeBalance,
                            BucketId = item.BucketId,
                            EmployeeCode = item.EmployeeCode,
                            OccupationId = item.OccupationId,
                        };
                        await Context.Loanees.AddAsync(loanee);
                    }
                    
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
                return result.FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<LoaneeViewModel>> GetPaging(int bucketId, string employerCode,int skip, int take, string filter)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("th-TH");
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select * from loanee where 1=1";
                var sb = new StringBuilder();
                sb.Append(sql);
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (cus_id LIKE @filter");
                    sb.Append(" or name LIKE @filter");
                    sb.Append(" or contract_no LIKE @filter");
                    sb.Append(" or nationality_id LIKE @filter");
                    sb.Append(" )");
                }
                if (bucketId > 0)
                {
                    sb.Append(" and bucket_id=@bucketId");
                }
                if (!string.IsNullOrEmpty(employerCode))
                {
                    sb.Append(" and employer_code=@employerCode");
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
                if (bucketId > 0)
                {
                    dictionary.Add("@bucketId", bucketId);
                }
                if (!string.IsNullOrEmpty(employerCode))
                {
                    dictionary.Add("@employerCode", employerCode);
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

        public async Task<int> GetRecordCount(int bucketId, string employerCode, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();

                sb.Append("select count(1) from loanee where 1=1");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (cus_id LIKE @filter");
                    sb.Append(" or name LIKE @filter");
                    sb.Append(" or contract_no LIKE @filter");
                    sb.Append(" or nationality_id LIKE @filter");
                    sb.Append(" )");
                }
                if (bucketId > 0)
                {
                    sb.Append(" and bucket_id=@bucketId");
                }
                if (!string.IsNullOrEmpty(employerCode))
                {
                    sb.Append(" and employer_code=@employerCode");
                }
                var dictionary = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
                }
                if (bucketId > 0)
                {
                    dictionary.Add("@bucketId", bucketId);
                }
                if (!string.IsNullOrEmpty(employerCode))
                {
                    dictionary.Add("@employerCode", employerCode);
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

        public bool IsExisting(string id)
        {
            return Context.Loanees.Any(p => p.CusId == id);
        }

        public async Task Update(int id, Loanee model)
        {
            throw new NotImplementedException();
        }
    }
}

