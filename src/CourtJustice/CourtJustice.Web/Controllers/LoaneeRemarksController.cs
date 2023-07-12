using CourtJustice.Domain;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using CourtJustice.Infrastructure.Utils;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class LoaneeRemarksController : BaseController<LoaneeRemarksController>
    {
        private readonly ILoaneeRepository _loaneeRepository;
        private readonly ILoaneeRemarkRepository _loaneeRemarkRepository;
        private readonly IBankActionCodeRepository _bankActionCodeRepository;
        private readonly IBankResultCodeRepository _bankResultCodeRepository;
        private readonly IBankPersonCodeRepository _bankPersonCodeRepository;
        private readonly ICompanyActionCodeRepository _companyActionCodeRepository;
        private readonly ICompanyResultCodeRepository _companyResultCodeRepository;
        private readonly IEmployeeRepository _employeeRepository;


        public LoaneeRemarksController(ILoaneeRemarkRepository loaneeRemarkRepository,
                   IBankActionCodeRepository bankActionCodeRepository,
                   IBankResultCodeRepository bankResultCodeRepository,
                   ICompanyActionCodeRepository companyActionCodeRepository,
                   ICompanyResultCodeRepository companyResultCodeRepository,
                   ILoaneeRepository loaneeRepository,
                   IEmployeeRepository employeeRepository,
                   IBankPersonCodeRepository bankPersonCodeRepository)
        {
            _loaneeRemarkRepository = loaneeRemarkRepository;
            _bankActionCodeRepository = bankActionCodeRepository;
            _bankResultCodeRepository = bankResultCodeRepository;
            _companyActionCodeRepository = companyActionCodeRepository;
            _companyResultCodeRepository = companyResultCodeRepository;
            _loaneeRepository = loaneeRepository;
            _employeeRepository = employeeRepository;
            _bankPersonCodeRepository = bankPersonCodeRepository;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}


        //private async Task<List<LoaneeRemark>> GetAll()
        //{
        //    var results = await _loaneeRemarkRepository.GetAll();
        //    return results.ToList();
        //}

        //public async Task<IActionResult> Create()
        //{

        //    await GetViewBag();

        //    return View(new LoaneeRemark());
        //}

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, string cusId)
        {
            try
            {
                var loaneeRemark = await _loaneeRemarkRepository.GetByKey(id);
                if (loaneeRemark != null)
                {
                    var remain = (loaneeRemark.TransactionDatetime.Date - DateTime.Now.Date).TotalDays;
                    if (remain < 0)
                    {
                        throw new Exception("ไม่สามารถลบข้อมูล วันที่ย้อนหลังได้!");
                    }
                }


                await _loaneeRemarkRepository.Delete(id);
                var loaneeRemarks = await _loaneeRemarkRepository.GetByCusId(cusId);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_LoaneeRemarkCard", loaneeRemarks);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id, string cusId)
        {
            try
            {
                var loanee = await _loaneeRepository.GetByKey(cusId);
                var loaneeRemark = new LoaneeRemarkViewModel
                {
                    EmployerCode = loanee.EmployerCode,
                    CusId = cusId,
                    AppointmentDate = DateTime.Now,
                };
                if (id == 0)
                {
                    await GetViewBag(cusId, loaneeRemark);
                    return View(loaneeRemark);
                }
                else
                {
                    loaneeRemark = await _loaneeRemarkRepository.GetByKey(id);
                    await GetViewBag(cusId, loaneeRemark);
                    return View(loaneeRemark);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] LoaneeRemarkViewModel model)
        {
            try
            {
                var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
                var bankAction = await _bankActionCodeRepository.GetByKey(model.BankActionId);
                var bankResult = await _bankResultCodeRepository.GetByKey(model.BankResultId);
                var bankPerson = await _bankPersonCodeRepository.GetByKey(model.BankPersonId);

                var isExisting = _loaneeRemarkRepository.IsExisting(model.LoaneeRemarkId);
                var newloaneeRemark = new LoaneeRemark
                {
                    Amount = model.Amount,
                    TransactionDatetime = DateTime.Now,
               
                    CompanyActionCodeId = model.CompanyActionCodeId,
                    CompanyResultCodeId = model.CompanyResultCodeId,
                    FollowContractNo = model.FollowContractNo,
                    CusId = model.CusId,
                    AppointmentDate = DateOnly.FromDateTime(model.AppointmentDate),
                    AppointmentContract = appUser.UserId,
                    Remark = model.Remark,
                    EmployerCode = model.EmployerCode,

                    BankActionCodeId = bankAction.BankActionCodeId,
                    BankResultCodeId = bankResult.BankResultCodeId,
                    BankPersonCodeId = bankPerson.BankPersonCodeId,

                    BankActionId = model.BankActionId,
                    BankResultId= model.BankResultId,
                    BankPersonId= model.BankPersonId,
                    IsActive=true,
                };

                if (isExisting)
                {
                    await _loaneeRemarkRepository.Update(model.LoaneeRemarkId, newloaneeRemark);
                }
                else
                {
                    await _loaneeRemarkRepository.Create(newloaneeRemark);
                }
                await _loaneeRepository.UpdateContractNo(model.CusId, model.FollowContractNo);
                ViewBag.CusId = model.CusId;
                var loaneeRemark = await _loaneeRemarkRepository.GetByCusId(model.CusId);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_LoaneeRemarkCard", loaneeRemark);
                return new JsonResult(new { isValid = true, html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = true, message = ex.Message });

            }

        }

        [HttpGet]
        public async Task<JsonResult> GetByCusId(string id = "")
        {
            var loaneeRemark = await _loaneeRemarkRepository.GetByCusId(id);
            ViewBag.CusId = id;
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_LoaneeRemarkCard", loaneeRemark);
            return new JsonResult(new { isValid = true, message = "", html });
        }

        private async Task GetViewBag(string cusId, LoaneeRemarkViewModel loaneeRemark)
        {
            try
            {
                var backActions = await _bankActionCodeRepository.GetByEmployer(loaneeRemark.EmployerCode);
                List<SelectListItem> selects = new();
                foreach (var item in backActions)
                {
                    selects.Add(new SelectListItem
                    {
                        Selected = item.BankActionId == loaneeRemark.BankActionId ? true : false,
                        Text = $"{item.BankActionCodeId}:{item.BankActionCodeName}",
                        Value = item.BankActionId.ToString(),
                    });
                }
                ViewBag.BankActionCodes = selects;


                var bankResults = await _bankResultCodeRepository.GetByEmployer(loaneeRemark.EmployerCode);
                selects = new();
                foreach (var item in bankResults)
                {
                    selects.Add(new SelectListItem
                    {
                        Selected = item.BankResultId == loaneeRemark.BankResultId ? true : false,
                        Text = $"{item.BankResultCodeId}:{item.BankResultCodeName}",
                        Value = item.BankResultId.ToString(),
                    });
                }
                ViewBag.BankResultCodes = selects;

                //var bankPersonCodes = await _bankPersonCodeRepository.GetAll( loaneeRemark.BankActionId);
                //selects = new();
                //foreach (var item in bankPersonCodes)
                //{
                //    selects.Add(new SelectListItem
                //    {
                //        Selected = item.BankPersonId == loaneeRemark.BankPersonId,
                //        Text = $"{item.BankPersonCodeId}:{item.BankPersonCodeName}",
                //        Value = item.BankPersonId.ToString(),
                //    });
                //}
                //ViewBag.BankPersonCodes = selects;

                if (loaneeRemark.BankActionId > 0)
                {
                    var bankPersonCodes = await _bankPersonCodeRepository.GetAll(loaneeRemark.BankActionId);
                    selects = new();
                    foreach (var item in bankPersonCodes)
                    {
                        selects.Add(new SelectListItem
                        {
                            Selected = item.BankPersonId == loaneeRemark.BankPersonId,
                            Text = $"{item.BankPersonCodeId}:{item.BankPersonCodeName}",
                            Value = item.BankPersonId.ToString(),
                        });
                    }
                    ViewBag.BankPersonCodes = selects;
                }
                else
                {
                    if (backActions.Count > 0)
                    {
                        var bankActionId = backActions.FirstOrDefault().BankActionId;
                        var bankPersonCodes = await _bankPersonCodeRepository.GetAll( bankActionId);
                        selects = new();
                        foreach (var item in bankPersonCodes)
                        {
                            selects.Add(new SelectListItem
                            {
                                Selected = item.BankPersonId == loaneeRemark.BankPersonId,
                                Text = $"{item.BankPersonCodeId}:{item.BankPersonCodeName}",
                                Value = item.BankPersonId.ToString(),
                            });
                        }
                        ViewBag.BankPersonCodes = selects;
                    }
                    else
                    {
                        selects = new();
                        ViewBag.BankPersonCodes = selects;
                    }
                }


                var companyActions = await _companyActionCodeRepository.GetAll();
                selects = new();
                foreach (var item in companyActions)
                {
                    selects.Add(new SelectListItem
                    {
                        Selected = item.CompanyActionCodeId == loaneeRemark.CompanyActionCodeId ? true : false,
                        Text = $"{item.CompanyActionCodeId}:{item.CompanyActionCodeName}",
                        Value = item.CompanyActionCodeId.ToString(),
                    });
                }
                ViewBag.CompanyActionCodes = selects;

                var companyResults = await _companyResultCodeRepository.GetAll();
                selects = new();
                foreach (var item in companyResults)
                {
                    selects.Add(new SelectListItem
                    {
                        Selected = item.CompanyResultCodeId == loaneeRemark.CompanyResultCodeId ? true : false,
                        Text = $"{item.CompanyResultCodeId}:{item.CompanyResultCodeName}",
                        Value = item.CompanyResultCodeId.ToString(),
                    });
                }
                ViewBag.CompanyResultCodes = selects;
            }
            catch (Exception)
            {

                throw;
            }



            //var employees = await _employeeRepository.GetAll();
            //selects = new();
            //foreach (var item in employees)
            //{
            //    selects.Add(new SelectListItem
            //    {
            //        Text = $"{item.EmployeeName}",
            //        Value = item.EmployeeCode.ToString(),
            //    });
            //}
            //ViewBag.Employees = selects;
        }

        [HttpGet]
        public IActionResult IndexImport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmImport(IList<IFormFile> files)
        {
            var rowCount = 0;
            var rowIndex = 0;
            Program.Progress = 0;
            var message = "";
            List<LoaneeRemarkExcelViewModel> loaneeRemarks = new();
            var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            try
            {

                if (files[0]?.Length != 0)
                {
                    var stream = files[0].OpenReadStream();
                    using var reader = ExcelReaderFactory.CreateReader(stream);
                    var result = reader.AsDataSet();
                    var dt = result.Tables[0];

                    dt.Rows[0].Delete();
                    dt.AcceptChanges();

                    var compDt = dt.DefaultView.ToTable();
                    //IFormFile batchMeters
                    int columnCount = compDt.Columns.Count;
                    for (int i = 0; i < compDt.Rows.Count; i++)
                    {
                        var loaneeRemark = new LoaneeRemarkExcelViewModel
                        {
                            TransactionDatetime = DateTime.Now,
                            EmployerCode = dt.Rows[i][0].ToString().Trim(),
                            CusId = dt.Rows[i][1].ToString().Trim(),
                            NationalityId = dt.Rows[i][2].ToString().Trim(),
                            CusName = dt.Rows[i][3].ToString().Trim(),
                            ContractNo = dt.Rows[i][4].ToString().Trim(),
                            BankActionCodeId = dt.Rows[i][5].ToString().Trim(),
                            BankResultCodeId = dt.Rows[i][7].ToString().Trim(),
                            BankPersonCodeId = dt.Rows[i][9].ToString().Trim(),
                            CompanyActionCodeId = dt.Rows[i][11].ToString().Trim(),
                            CompanyResultCodeId = dt.Rows[i][13].ToString().Trim(),
                            FollowContractNo = dt.Rows[i][15].ToString().Trim(),
                            AppointmentDate = DateTime.Parse(dt.Rows[i][16].ToString()),
                            Amount = dt.Rows[i][17].ToString().Trim().ToDecimal(),
                            AppointmentContract = dt.Rows[i][18].ToString().Trim(),
                            Remark = dt.Rows[i][19].ToString().Trim(),
                        };
                        loaneeRemarks.Add(loaneeRemark);
                        rowIndex += 1;
                        Program.Progress = (int)((float)rowIndex / (float)compDt.Rows.Count * 100.0);
                        await Task.Delay(10); // It is only to make the process slower
                    }
                    //Validate
                    foreach (var item in loaneeRemarks)
                    {
                        var loanee = await _loaneeRepository.GetByKey(item.CusId);
                        if (loanee == null)
                        {
                            message += $"ไม่พบข้อมูล ลูกหนี้ {item.CusId}{Environment.NewLine}";
                        }
                        else
                        {
                            item.EmployerCode = loanee.EmployerCode;
                        }
                    }
                    var gEmployers = loaneeRemarks.Select(p => p.EmployerCode).Distinct().ToList();
                    var gBankActions = loaneeRemarks.Select(p => p.BankActionCodeId).Distinct().ToList();
                    var gBankResults = loaneeRemarks.Select(p => p.BankResultCodeId).Distinct().ToList();
                    var gBankPersonCodes = loaneeRemarks.Select(p => p.BankPersonCodeId).Distinct().ToList();
                    var gCompanyActions = loaneeRemarks.Select(p => p.CompanyActionCodeId).Distinct().ToList();
                    var gCompanyResults = loaneeRemarks.Select(p => p.CompanyResultCodeId).Distinct().ToList();

                    foreach (var employerCode in gEmployers)
                    {
                        foreach (var bankActionCodeId in gBankActions)
                        {
                            var count = await _bankActionCodeRepository.CountByEmployerAndCode(employerCode, bankActionCodeId);
                            if (count == 0)
                            {
                                message += $"ไม่พบข้อมูล Action Code[ธนาคาร] {employerCode} {bankActionCodeId}{Environment.NewLine}";
                            }
                            else
                            {
                                //foreach (var bankPersonCode in gBankPersonCodes)
                                //{
                                //    var personCode = await _bankPersonCodeRepository.GetByKey(employerCode, bankActionCodeId, bankPersonCode);
                                //    if (personCode == null)
                                //    {
                                //        message += $"ไม่พบข้อมูล Person Code[ธนาคาร] {employerCode},{bankActionCodeId},{bankPersonCode}{Environment.NewLine}";
                                //    }
                                //}
                            }
                        }
                        foreach (var bankResultCodeId in gBankResults)
                        {
                            var count = await _bankResultCodeRepository.CountByEmployerAndCode(employerCode, bankResultCodeId);
                            if (count == 0)
                            {
                                message += $"ไม่พบข้อมูล Result Code[ธนาคาร] {employerCode} {bankResultCodeId}{Environment.NewLine}";
                            }
                        }
                    }

                    foreach (var item in gCompanyActions)
                    {
                        var companyAction = await _companyActionCodeRepository.GetByKey(item);
                        if (companyAction == null)
                        {
                            message += $"ไม่พบข้อมูล Action Code[บริษัท] {item}{Environment.NewLine}";
                        }
                    }
                    foreach (var item in gCompanyResults)
                    {
                        var companyResult = await _companyResultCodeRepository.GetByKey(item);
                        if (companyResult == null)
                        {
                            message += $"ไม่พบข้อมูล Result Code[บริษัท] {item}{Environment.NewLine}";
                        }
                    }
                    if (!string.IsNullOrEmpty(message))
                    {
                        return Json(new { isvalid = false, message });
                    }
                    foreach (var loaneeRemarkExcel in loaneeRemarks)
                    {
                        var bankActionCode = await _bankActionCodeRepository.GetByEmployerAndCode(loaneeRemarkExcel.EmployerCode
                            , loaneeRemarkExcel.BankActionCodeId);
                        loaneeRemarkExcel.BankActionId = bankActionCode.BankActionId;

                        var bankResultCode = await _bankResultCodeRepository.GetByEmployerAndCode(loaneeRemarkExcel.EmployerCode
                            , loaneeRemarkExcel.BankResultCodeId);
                        loaneeRemarkExcel.BankResultId = bankResultCode.BankResultId;

                        //var bankPersonCode = await _bankPersonCodeRepository.GetByKey(loaneeRemarkExcel.EmployerCode
                        //    , loaneeRemarkExcel.BankActionCodeId
                        //    , loaneeRemarkExcel.BankPersonCodeId);
                        //loaneeRemarkExcel.BankPersonCodeId = bankPersonCode.BankPersonCodeId;
                    }
                    await _loaneeRemarkRepository.BulkInsertOrUpdate(loaneeRemarks);
                }
                message = $"Import ข้อมูลการติดตาม จำนวน {loaneeRemarks.Count} ข้อมูลเรียบร้อยแล้ว.";
                _notify.Success(message);
                ViewBag.Message = message;
                return Json(new { isvalid = true, message });
            }
            catch (Exception err)
            {
                string msgError = err.Message;
                Program.Progress = (int)((float)rowIndex / (float)rowCount * 100.0);
                return Json(new { isvalid = false, message = msgError });
            }
        }

        [HttpPost]
        public IActionResult Progress()
        {
            return this.Content(Program.Progress.ToString());
        }

        //private DateTime ConvertDateFormatTHToUS(string rawDate)
        //{
        //    const string DATE_FORMAT = "dd/MM/yyyy";
        //    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //    CultureInfo culture = new("en-US");
        //    var convertdDate = DateTime.ParseExact(rawDate.Trim(), DATE_FORMAT, culture);
        //    if (convertdDate.Year > 2500) convertdDate = convertdDate.AddYears(-543);
        //    return convertdDate;
        //}



    }
}

