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
        private readonly ICompanyActionCodeRepository _companyActionCodeRepository;
        private readonly ICompanyResultCodeRepository _companyResultCodeRepository;
        private readonly IEmployeeRepository _employeeRepository;


        public LoaneeRemarksController(ILoaneeRemarkRepository loaneeRemarkRepository,
                   IBankActionCodeRepository bankActionCodeRepository,
                   IBankResultCodeRepository bankResultCodeRepository,
                   ICompanyActionCodeRepository companyActionCodeRepository,
                   ICompanyResultCodeRepository companyResultCodeRepository,
                   ILoaneeRepository loaneeRepository,
                   IEmployeeRepository employeeRepository)
        {
            _loaneeRemarkRepository = loaneeRemarkRepository;
            _bankActionCodeRepository = bankActionCodeRepository;
            _bankResultCodeRepository = bankResultCodeRepository;
            _companyActionCodeRepository = companyActionCodeRepository;
            _companyResultCodeRepository = companyResultCodeRepository;
            _loaneeRepository = loaneeRepository;
            _employeeRepository = employeeRepository;
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
            var loaneeRemark = new LoaneeRemarkViewModel
            {
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

        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] LoaneeRemarkViewModel model)
        {
            var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
            var loanee = await _loaneeRepository.GetByKey(model.CusId);

            var isExisting = _loaneeRemarkRepository.IsExisting(model.LoaneeRemarkId);
            var newloaneeRemark = new LoaneeRemark
            {
                Amount = model.Amount,
                TransactionDatetime = DateTime.Now,
                BankActionCodeId = model.BankActionCodeId,
                BankResultCodeId = model.BankResultCodeId,
                CompanyActionCodeId = model.CompanyActionCodeId,
                CompanyResultCodeId = model.CompanyResultCodeId,
                FollowContractNo = model.FollowContractNo,
                CusId = model.CusId,
                AppointmentDate = DateOnly.FromDateTime(model.AppointmentDate),
                AppointmentContract = appUser.UserId,
                Remark = model.Remark,
                EmployerCode = loanee.EmployerCode,
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

            var loanee = await _loaneeRepository.GetByKey(cusId);

            var backActions = await _bankActionCodeRepository.GetByEmployer(loanee.EmployerCode);
            List<SelectListItem> selects = new();
            foreach (var item in backActions)
            {
                selects.Add(new SelectListItem
                {
                    Selected = item.BankActionCodeId == loaneeRemark.BankActionCodeId ? true : false,
                    Text = $"{item.BankActionCodeId}:{item.BankActionCodeName}",
                    Value = item.BankActionCodeId.ToString(),
                });
            }
            ViewBag.BankActionCodes = selects;

            var bankResults = await _bankResultCodeRepository.GetByEmployer(loanee.EmployerCode);
            selects = new();
            foreach (var item in bankResults)
            {
                selects.Add(new SelectListItem
                {
                    Selected = item.BankResultCodeId == loaneeRemark.BankResultCodeId ? true : false,
                    Text = $"{item.BankResultCodeId}:{item.BankResultCodeName}",
                    Value = item.BankResultCodeId.ToString(),
                });
            }
            ViewBag.BankResultCodes = selects;

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
                    var messsage = "";
                    dt.Rows[0].Delete();
                    dt.AcceptChanges();

                    var compDt = dt.DefaultView.ToTable();
                    //IFormFile batchMeters
                    int columnCount = compDt.Columns.Count;
                    for (int i = 0; i < compDt.Rows.Count; i++)
                    {
                        var cusId = dt.Rows[i][2].ToString().Trim();
                        var loanee = await _loaneeRepository.GetByKey(cusId);
                        if (loanee == null)
                        {
                            throw new Exception("ไม่พบข้อมูลลูกหนี้ " + cusId);
                        }

                        var loaneeRemark = new LoaneeRemarkExcelViewModel
                        {
                            EmployerCode = loanee.EmployerCode,
                            AssignDate = dt.Rows[i][0].ToString().ConvertDateFormatTHToUS(),
                            ExpireDate = dt.Rows[i][1].ToString().ConvertDateFormatTHToUS(),
                            TransactionDatetime = DateTime.Now,
                            CusId = cusId,
                            CusName = dt.Rows[i][3].ToString().Trim(),
                            ContractNo = dt.Rows[i][4].ToString().Trim(),
                            BankActionCodeId = dt.Rows[i][5].ToString().Trim(),
                            BankResultCodeId = dt.Rows[i][7].ToString().Trim(),
                            CompanyActionCodeId = dt.Rows[i][9].ToString().Trim(),
                            CompanyResultCodeId = dt.Rows[i][11].ToString().Trim(),
                            FollowContractNo = dt.Rows[i][13].ToString().Trim(),
                            AppointmentDate = dt.Rows[i][14].ToString().ConvertDateFormatTHToUS(),
                            Amount = dt.Rows[i][15].ToString().Trim().ToDecimal(),
                            AppointmentContract = dt.Rows[i][16].ToString().Trim(),
                            Remark = dt.Rows[i][17].ToString().Trim(),
                        };
                        
                        loaneeRemarks.Add(loaneeRemark);
                        rowIndex += 1;
                        Program.Progress = (int)((float)rowIndex / (float)compDt.Rows.Count * 100.0);
                        await Task.Delay(10); // It is only to make the process slower
                    }
                    //if (!string.IsNullOrEmpty(messsage))
                    //{
                    //    ViewBag.Message = messsage;
                    //    return View();
                    //}
                    await _loaneeRemarkRepository.BulkInsertOrUpdate(loaneeRemarks);
                }
                var message = $"Import ข้อมูลการติดตาม จำนวน {loaneeRemarks.Count} ข้อมูลเรียบร้อยแล้ว.";
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

