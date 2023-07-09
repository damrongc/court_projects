﻿using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using CourtJustice.Infrastructure.Utils;
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

                        existingLoanee.AssignDate = item.AssignDate;
                        existingLoanee.ExpireDate = item.ExpireDate;
                        existingLoanee.ContractDate = item.ContractDate;
                        existingLoanee.BirthDate = item.BirthDate;
                        existingLoanee.WODate = item.WODate;

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
                        existingLoanee.CompanyName = item.CompanyName;
                        existingLoanee.OccupationName = item.OccupationName;
                        existingLoanee.OfficeAddress1 = item.OfficeAddress1;
                        existingLoanee.OfficeAddress2 = item.OfficeAddress2;
                        existingLoanee.OfficeAddress3 = item.OfficeAddress3;
                        existingLoanee.OfficeAddress4 = item.OfficeAddress4;
                        existingLoanee.TelephoneOffice = item.TelephoneOffice;
                        existingLoanee.IdenAddress1 = item.IdenAddress1;
                        existingLoanee.IdenAddress2 = item.IdenAddress2;
                        existingLoanee.IdenAddress3 = item.IdenAddress3;
                        existingLoanee.IdenAddress4 = item.IdenAddress4;

                        existingLoanee.EmergencyContract1 = item.EmergencyContract1;
                        existingLoanee.EmergencyPhone1 = item.IdenAddress1;
                        existingLoanee.EmergencyExt1 = item.EmergencyExt1;

                        existingLoanee.EmergencyContract2 = item.EmergencyContract2;
                        existingLoanee.EmergencyPhone2 = item.IdenAddress2;
                        existingLoanee.EmergencyExt2 = item.EmergencyExt2;

                        existingLoanee.EmergencyContract3 = item.EmergencyContract3;
                        existingLoanee.EmergencyPhone3 = item.IdenAddress3;
                        existingLoanee.EmergencyExt3 = item.EmergencyExt3;

                        existingLoanee.EmergencyContract4 = item.EmergencyContract4;
                        existingLoanee.EmergencyPhone4 = item.IdenAddress4;
                        existingLoanee.EmergencyExt4 = item.EmergencyExt4;


                        existingLoanee.MobileHome = item.MobileHome;
                        existingLoanee.MobileOffice = item.MobileOffice;
                        existingLoanee.MobileCont = item.MobileCont;
                        existingLoanee.MobileEmg = item.MobileEmg;
                        existingLoanee.SpecialNote = item.SpecialNote;
                        existingLoanee.CPCase = item.CPCase;
                        existingLoanee.NoOfCP = item.NoOfCP;
                        existingLoanee.CPDate = item.CPDate;
                        existingLoanee.OAFee = item.OAFee;
                        existingLoanee.MaxOAFeeAmount = item.MaxOAFeeAmount;
                        existingLoanee.MaxOAFeeBalance = item.MaxOAFeeBalance;

                        existingLoanee.Gender = item.Gender;
                        existingLoanee.MaritalStatus = item.MaritalStatus;
                        existingLoanee.ProductCode = item.ProductCode;
                        existingLoanee.DebtAge = item.DebtAge;
                        existingLoanee.TotalPayment = item.TotalPayment;
                        existingLoanee.EmployerWorkGroup = item.EmployerWorkGroup;
                        existingLoanee.Salary = item.Salary;

                    }
                    else
                    {
                        var loanee = new Loanee
                        {
                            AssignDate = item.AssignDate,
                            ExpireDate = item.ExpireDate,
                            NationalityId = item.NationalityId,
                            BirthDate = item.BirthDate,
                            CusId = item.CusId,
                            Name = item.Name,
                            ContractNo = item.ContractNo,
                            ContractDate = item.ContractDate,
                            WODate = item.WODate,
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
                            CompanyName = item.CompanyName,
                            OccupationName = item.OccupationName,
                            OfficeAddress1 = item.OfficeAddress1,
                            OfficeAddress2 = item.OfficeAddress2,
                            OfficeAddress3 = item.OfficeAddress3,
                            OfficeAddress4 = item.OfficeAddress4,
                            TelephoneOffice = item.TelephoneOffice,
                            IdenAddress1 = item.IdenAddress1,
                            IdenAddress2 = item.IdenAddress2,
                            IdenAddress3 = item.IdenAddress3,
                            IdenAddress4 = item.IdenAddress4,
                            EmergencyContract1 = item.EmergencyContract1,
                            EmergencyPhone1 = item.IdenAddress1,
                            EmergencyExt1 = item.EmergencyExt1,

                            EmergencyContract2 = item.EmergencyContract2,
                            EmergencyPhone2 = item.IdenAddress2,
                            EmergencyExt2 = item.EmergencyExt2,

                            EmergencyContract3 = item.EmergencyContract3,
                            EmergencyPhone3 = item.IdenAddress3,
                            EmergencyExt3 = item.EmergencyExt3,

                            EmergencyContract4 = item.EmergencyContract4,
                            EmergencyPhone4 = item.IdenAddress4,
                            EmergencyExt4 = item.EmergencyExt4,

                            MobileHome = item.MobileHome,
                            MobileOffice = item.MobileOffice,
                            MobileCont = item.MobileCont,
                            MobileEmg = item.MobileEmg,
                            SpecialNote = item.SpecialNote,
                            CPCase = item.CPCase,
                            NoOfCP = item.NoOfCP,
                            CPDate = item.CPDate,
                            OAFee = item.OAFee,
                            MaxOAFeeAmount = item.MaxOAFeeAmount,
                            MaxOAFeeBalance = item.MaxOAFeeBalance,
                            BucketId = item.BucketId,
                            EmployeeCode = item.EmployeeCode,
                            //OccupationId = item.OccupationId,
                            EmployerCode = item.EmployerCode,
                            LoanTypeCode = item.LoanTypeCode,

                            Gender = item.Gender,
                            MaritalStatus = item.MaritalStatus,
                            ProductCode = item.ProductCode,
                            DebtAge = item.DebtAge,
                            TotalPayment = item.TotalPayment,
                            EmployerWorkGroup = item.EmployerWorkGroup,
                            Salary = item.Salary,
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

        public async Task DeActivate(List<LoaneeViewModel> loanees)
        {
            try
            {
                foreach (var item in loanees)
                {
                    var loanee = await Context.Loanees.FindAsync(item.CusId);
                    if (loanee != null)
                    {
                        loanee.IsActive = false;
                        await Context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
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
                var sql = @"SELECT loanee.cus_id,
    loanee.assign_date,
    loanee.expire_date,
    loanee.birth_date,
    loanee.nationality_id,
    loanee.name,
    loanee.telephone_home,
    loanee.contract_no,
    loanee.contract_date,
    loanee.wo_date,
    loanee.term,
    loanee.loan_amount,
    loanee.wo_balance,
    loanee.overdue_amount,
    loanee.total_penalty,
    loanee.closing_amount,
    loanee.rcv_amt_status,
    loanee.rcv_amt_before_wo,
    loanee.rcv_amt_after_wo,
    loanee.last_paid_amount,
    loanee.no_of_assignment,
    loanee.description,
    loanee.loan_type_code,
    loanee.installments_by_contract,
    loanee.installments_by_agree,
    loanee.last_paid_date,
    loanee.bucket_id,
    loanee.first_paid_date,
    loanee.interete_rate,
    loanee.interete_rate_amount,
    loanee.due_date,
    loanee.follow_up_date,
    loanee.paid_amount,
    loanee.paid_in_month_amount,
    loanee.total_amount,
    loanee.employee_code,
    loanee.remaining_amount,
    loanee.overdue_day_amount,
    loanee.employer_code,
    loanee.loan_task_status_id,
    loanee.home_address1,
    loanee.home_address2,
    loanee.home_address3,
    loanee.home_address4,
    loanee.company_name,
    loanee.occupation_name,
    loanee.office_address1,
    loanee.office_address2,
    loanee.office_address3,
    loanee.office_address4,
    loanee.telephone_office,
    loanee.iden_address1,
    loanee.iden_address2,
    loanee.iden_address3,
    loanee.iden_address4,
    loanee.mobile_home,
    loanee.mobile_office,
    loanee.mobile_cont,
    loanee.mobile_emg,
    loanee.special_note,
    loanee.cp_case,
    loanee.no_of_cp,
    loanee.cp_date,
    loanee.oa_fee,
    loanee.max_oa_fee_amount,
    loanee.max_oa_fee_balance,
    loanee.oa_flag,
    loanee.sending_address,
    loanee.follow_contract_no,
    loanee.debt_age,
    loanee.employer_work_group,
    loanee.gender,
    loanee.marital_status,
    loanee.product_code,
    loanee.total_payment,
    loanee.salary,
    loanee.is_active,
    loanee.user_created,
    loanee.created_date_time,
    loanee.user_updated,
    loanee.updated_date_time,
    employer.employer_name
from loanee,employer 
where loanee.employer_code=employer.employer_code 
and cus_id=@cus_id";
                var result = await conn.QueryAsync<LoaneeViewModel>(sql, new { cus_id = id });
                return result.FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<LoaneeViewModel>> GetPaging(int loanTaskStatusId, string employerCode, List<string> employeeCodes, int skip, int take, string filter)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("th-TH");
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"SELECT loanee.cus_id,
    loanee.assign_date,
    loanee.expire_date,
    loanee.birth_date,
    loanee.nationality_id,
    loanee.name,
    loanee.telephone_home,
    loanee.contract_no,
    loanee.contract_date,
    loanee.wo_date,
    loanee.term,
    loanee.loan_amount,
    loanee.wo_balance,
    loanee.overdue_amount,
    loanee.total_penalty,
    loanee.closing_amount,
    loanee.rcv_amt_status,
    loanee.rcv_amt_before_wo,
    loanee.rcv_amt_after_wo,
    loanee.last_paid_amount,
    loanee.no_of_assignment,
    loanee.description,
    loanee.loan_type_code,
    loanee.installments_by_contract,
    loanee.installments_by_agree,
    loanee.last_paid_date,
    loanee.bucket_id,
    loanee.first_paid_date,
    loanee.interete_rate,
    loanee.interete_rate_amount,
    loanee.due_date,
    loanee.follow_up_date,
    loanee.paid_amount,
    loanee.paid_in_month_amount,
    loanee.total_amount,
    loanee.employee_code,
    loanee.remaining_amount,
    loanee.overdue_day_amount,
    loanee.employer_code,
    loanee.loan_task_status_id,
    loanee.home_address1,
    loanee.home_address2,
    loanee.home_address3,
    loanee.home_address4,
    loanee.company_name,
    loanee.occupation_name,
    loanee.office_address1,
    loanee.office_address2,
    loanee.office_address3,
    loanee.office_address4,
    loanee.telephone_office,
    loanee.iden_address1,
    loanee.iden_address2,
    loanee.iden_address3,
    loanee.iden_address4,
    loanee.mobile_home,
    loanee.mobile_office,
    loanee.mobile_cont,
    loanee.mobile_emg,
    loanee.special_note,
    loanee.cp_case,
    loanee.no_of_cp,
    loanee.cp_date,
    loanee.oa_fee,
    loanee.max_oa_fee_amount,
    loanee.max_oa_fee_balance,
    loanee.oa_flag,
    loanee.sending_address,
    loanee.follow_contract_no,
    loanee.debt_age,
    loanee.employer_work_group,
    loanee.gender,
    loanee.marital_status,
    loanee.product_code,
    loanee.total_payment,
    loanee.salary,
    loanee.is_active,
    loanee.user_created,
    loanee.created_date_time,
    loanee.user_updated,
    loanee.updated_date_time,
    employer.employer_name
from loanee,employer 
where loanee.employer_code=employer.employer_code and loanee.is_active=1";
                var sb = new StringBuilder();
                sb.Append(sql);
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (cus_id LIKE @filter");
                    sb.Append(" or name LIKE @filter");
                    sb.Append(" or contract_no LIKE @filter");
                    sb.Append(" or telephone_home LIKE @filter");
                    sb.Append(" or mobile_home LIKE @filter");
                    sb.Append(" or mobile_office LIKE @filter");
                    sb.Append(" or telephone_office LIKE @filter");
                    sb.Append(" or follow_contract_no LIKE @filter");
                    sb.Append(" or nationality_id LIKE @filter");
                    sb.Append(" or product_code LIKE @filter");
                    sb.Append(" or employer_work_group LIKE @filter");
                    sb.Append(" or employee_code LIKE @filter");
                    sb.Append(" )");
                }
                if (loanTaskStatusId > 0)
                {
                    sb.Append(" and loan_task_status_id=@loanTaskStatusId");
                }
                if (!string.IsNullOrEmpty(employerCode))
                {
                    sb.Append(" and loanee.employer_code=@employerCode");
                }
                if (employeeCodes.Count > 0)
                {
                    if (employeeCodes.Count == 1)
                    {
                        sb.AppendFormat(" and loanee.employee_code = '{0}'", employeeCodes[0]);
                    }
                    else
                    {
                        string delimiter = ",";
                        var listEmployeeCode = "";
                        foreach (var item in employeeCodes)
                        {
                            listEmployeeCode += "'" + item + "'" + delimiter;
                        }
                        listEmployeeCode = listEmployeeCode.Remove(listEmployeeCode.Length - 1);
                        sb.AppendFormat(" and loanee.employee_code in ({0})", listEmployeeCode);
                    }

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
                if (loanTaskStatusId > 0)
                {
                    dictionary.Add("@loanTaskStatusId", loanTaskStatusId);
                }
                if (!string.IsNullOrEmpty(employerCode))
                {
                    dictionary.Add("@employerCode", employerCode);
                }
                //if (!string.IsNullOrEmpty(employeeCode))
                //{
                //    dictionary.Add("@employeeCode", employeeCode);
                //}
                var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<LoaneeViewModel>(sb.ToString(), parameters);
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetRecordCount(int loanTaskStatusId, string employerCode, List<string> employeeCodes, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();

                sb.Append("select count(1) from loanee,employer where loanee.employer_code=employer.employer_code  and loanee.is_active=1");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (cus_id LIKE @filter");
                    sb.Append(" or name LIKE @filter");
                    sb.Append(" or contract_no LIKE @filter");
                    sb.Append(" or telephone_home LIKE @filter");
                    sb.Append(" or mobile_home LIKE @filter");
                    sb.Append(" or mobile_office LIKE @filter");
                    sb.Append(" or telephone_office LIKE @filter");
                    sb.Append(" or follow_contract_no LIKE @filter");
                    sb.Append(" or nationality_id LIKE @filter");
                    sb.Append(" or product_code LIKE @filter");
                    sb.Append(" or employer_work_group LIKE @filter");
                    sb.Append(" or employee_code LIKE @filter");
                    sb.Append(" )");
                }
                if (loanTaskStatusId > 0)
                {
                    sb.Append(" and loan_task_status_id=@loanTaskStatusId");
                }
                if (!string.IsNullOrEmpty(employerCode))
                {
                    sb.Append(" and loanee.employer_code=@employerCode");
                }
                if (employeeCodes.Count > 0)
                {
                    if (employeeCodes.Count == 1)
                    {
                        sb.AppendFormat(" and loanee.employee_code = '{0}'", employeeCodes[0]);
                    }
                    else
                    {
                        string delimiter = ",";
                        var listEmployeeCode = "";
                        foreach (var item in employeeCodes)
                        {
                            listEmployeeCode += "'" + item + "'" + delimiter;
                        }
                        listEmployeeCode = listEmployeeCode.Remove(listEmployeeCode.Length - 1);
                        sb.AppendFormat(" and loanee.employee_code in ({0})", listEmployeeCode);
                    }

                }

                var dictionary = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
                }
                if (loanTaskStatusId > 0)
                {
                    dictionary.Add("@loanTaskStatusId", loanTaskStatusId);
                }
                if (!string.IsNullOrEmpty(employerCode))
                {
                    dictionary.Add("@employerCode", employerCode);
                }
                //if (employeeCode.Count > 0)
                //{
                //    dictionary.Add("@employeeCode", employeeCode);
                //}
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

        public async Task UpdateContractNo(string id, string followContractNo)
        {
            try
            {
                var loanee = await Context.Loanees.FindAsync(id);
                if (loanee != null)
                {
                    loanee.FollowContractNo = followContractNo;
                    await Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateOrAssign(LoaneeViewModel model)
        {
            try
            {
                var loanee = await Context.Loanees.FindAsync(model.CusId);
                if (loanee != null)
                {
                    loanee.Name = model.Name;
                    loanee.HomeAddress1 = model.HomeAddress1;
                    loanee.HomeAddress2 = model.HomeAddress2;
                    loanee.HomeAddress3 = model.HomeAddress3;
                    loanee.HomeAddress4 = model.HomeAddress4;
                    loanee.TelephoneHome = model.TelephoneHome;
                    loanee.MobileHome = model.MobileHome;

                    loanee.IdenAddress1 = model.IdenAddress1;
                    loanee.IdenAddress2 = model.IdenAddress2;
                    loanee.IdenAddress3 = model.IdenAddress3;
                    loanee.IdenAddress4 = model.IdenAddress4;
                    loanee.MobileEmg = model.MobileEmg;

                    loanee.CompanyName = model.CompanyName;
                    loanee.OccupationName = model.OccupationName;
                    loanee.OfficeAddress1 = model.OfficeAddress1;
                    loanee.OfficeAddress2 = model.OfficeAddress2;
                    loanee.OfficeAddress3 = model.OfficeAddress3;
                    loanee.OfficeAddress4 = model.OfficeAddress4;
                    loanee.TelephoneOffice = model.TelephoneOffice;
                    loanee.MobileOffice = model.MobileOffice;
                    loanee.LoanTypeCode = model.LoanTypeCode;
                    loanee.LoanTaskStatusId = model.LoanTaskStatusId;
                    loanee.BucketId = model.BucketId;
                    await Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

