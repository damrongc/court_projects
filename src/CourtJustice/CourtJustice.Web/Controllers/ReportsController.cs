using ClosedXML.Excel;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using CourtJustice.Infrastructure.Utils;
using CourtJustice.Web.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace CourtJustice.Web.Controllers
{
    public class ReportsController : BaseController<ReportsController>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUserEmployerMappingRepository _userEmployerMappingRepository;
        private readonly IEmployerRepository _employerRepository;

        public ReportsController(IReportRepository reportRepository, IUserEmployerMappingRepository userEmployerMappingRepository, IEmployerRepository employerRepository)
        {
            _reportRepository = reportRepository;
            _userEmployerMappingRepository = userEmployerMappingRepository;
            _employerRepository = employerRepository;
        }

        public async Task<IActionResult> IndexReceipt()
        {
            var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
            var mappings = _userEmployerMappingRepository.GetByUser(appUser.UserId);
            var employers = await _employerRepository.GetAll();
            employers.Insert(0, new Employer { EmployerCode = "", EmployerName = "แสดงทั้งหมด" });

            List<SelectListItem> SelectEmployers = new();
            foreach (var item in employers)
            {
                switch (appUser.GroupId)
                {
                    case 2:
                    case 3:
                    case 4:
                        if (mappings.Any(p => p.EmployerCode == item.EmployerCode))
                        {
                            SelectEmployers.Add(new SelectListItem
                            {
                                Text = item.EmployerName.ToString(),
                                Value = item.EmployerCode.ToString(),
                            });
                        }
                        break;
                    default:
                        SelectEmployers.Add(new SelectListItem
                        {
                            Text = item.EmployerName.ToString(),
                            Value = item.EmployerCode.ToString(),
                        });
                        break;
                }

            }
            ViewBag.Employers = SelectEmployers;
            var payments = new List<PaymentExcelViewModel>();
            return View(payments);
        }

        [HttpPost]
        public async Task<JsonResult> GetReceiptReport([FromBody] ReceiptReportRequest request)
        {
            try
            {
                var startDate = request.StartDate;
                var endDate = request.EndDate;
                var results = await _reportRepository.GetLoaneeReceipt(request.EmployerCode, startDate, endDate);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTableReceipt", results);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.ToString() });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReceiptReport([FromBody] ReceiptReportRequest request)
        {
            try
            {
                var startDate = request.StartDate;
                var endDate = request.EndDate;
                var results = await _reportRepository.GetLoaneeReceipt(request.EmployerCode, startDate, endDate);

                return CreateXlsxReceiptReport($"Receipt_{DateTime.Now.ToUnixTimestamp()}.xlsx", results);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult CreateXlsxReceiptReport(string fileName, List<PaymentExcelViewModel> data)
        {

            try
            {
                var cultureInfor = new CultureInfo("th-TH");
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheetProductTypeY = workbook.Worksheets.Add("Receipt");
                    worksheetProductTypeY.Cell(1, 1).Value = "ASSIGN DATE";
                    worksheetProductTypeY.Cell(1, 2).Value = "EXPIRE DATE";
                    worksheetProductTypeY.Cell(1, 3).Value = "ID";
                    worksheetProductTypeY.Cell(1, 4).Value = "CUSTOMER CODE";
                    worksheetProductTypeY.Cell(1, 5).Value = "CUSTOMER NAME";
                    worksheetProductTypeY.Cell(1, 6).Value = "CONTRACT NO";
                    worksheetProductTypeY.Cell(1, 7).Value = "RECEIPT DATE";
                    worksheetProductTypeY.Cell(1, 8).Value = "BOOKING DATE";
                    worksheetProductTypeY.Cell(1, 9).Value = "TOTAL RECEIVED";
                    worksheetProductTypeY.Cell(1, 10).Value = "W/O BALANCE";
                    worksheetProductTypeY.Cell(1, 11).Value = "START OD STATUS";
                    worksheetProductTypeY.Cell(1, 12).Value = "END OD STATUS";
                    worksheetProductTypeY.Cell(1, 13).Value = "COLLECTOR";

                    for (int index = 0; index < data.Count; index++)
                    {
                        worksheetProductTypeY.Cell(index + 2, 1).Value = data[index].AssignDate.ToString("dd-MM-yyyy", cultureInfor);
                        worksheetProductTypeY.Cell(index + 2, 2).Value = data[index].ExpireDate.ToString("dd-MM-yyyy", cultureInfor);
                        worksheetProductTypeY.Cell(index + 2, 3).Value = data[index].CusId;
                        worksheetProductTypeY.Cell(index + 2, 4).Value = data[index].NationalityId;
                        worksheetProductTypeY.Cell(index + 2, 5).Value = data[index].CusName;
                        worksheetProductTypeY.Cell(index + 2, 6).Value = data[index].ContractNo;
                        worksheetProductTypeY.Cell(index + 2, 7).Value = data[index].ReceiptDate.ToString("dd-MM-yyyy", cultureInfor);
                        worksheetProductTypeY.Cell(index + 2, 8).Value = data[index].BookingDate.ToString("dd-MM-yyyy", cultureInfor);
                        worksheetProductTypeY.Cell(index + 2, 9).Value = data[index].TotalReceived.ToFormat2Decimal();
                        worksheetProductTypeY.Cell(index + 2, 10).Value = data[index].WOBalance.ToFormat2Decimal();
                        worksheetProductTypeY.Cell(index + 2, 11).Value = data[index].StartOverdueStatus;
                        worksheetProductTypeY.Cell(index + 2, 12).Value = data[index].EndOverdueStatus;
                        worksheetProductTypeY.Cell(index + 2, 13).Value = data[index].UserCreated;

                    }

                    var _filePath = string.Format("{0}/{1}", _hostEnvironment.WebRootPath, fileName);

                    using (FileStream fileStream = new FileStream(_filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        workbook.SaveAs(fileStream);
                    }

                    var errorMessage = "you can return the errors here!";
                    return Json(new { fileName, errorMessage });


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> IndexRemark()
        {
            var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
            var mappings = _userEmployerMappingRepository.GetByUser(appUser.UserId);
            var employers = await _employerRepository.GetAll();
            employers.Insert(0, new Employer { EmployerCode = "", EmployerName = "แสดงทั้งหมด" });

            List<SelectListItem> SelectEmployers = new();
            foreach (var item in employers)
            {
                switch (appUser.GroupId)
                {
                    case 2:
                    case 3:
                    case 4:
                        if (mappings.Any(p => p.EmployerCode == item.EmployerCode))
                        {
                            SelectEmployers.Add(new SelectListItem
                            {
                                Text = item.EmployerName.ToString(),
                                Value = item.EmployerCode.ToString(),
                            });
                        }
                        break;
                    default:
                        SelectEmployers.Add(new SelectListItem
                        {
                            Text = item.EmployerName.ToString(),
                            Value = item.EmployerCode.ToString(),
                        });
                        break;
                }

            }
            ViewBag.Employers = SelectEmployers;
            var remarks = new List<LoaneeRemarkExcelViewModel>();
            return View(remarks);
        }

        [HttpPost]
        public async Task<JsonResult> GetRemarkReport([FromBody] RemarkReportRequest request)
        {
            try
            {
                var startDate = request.StartDate;
                var endDate = request.EndDate;
                var results = await _reportRepository.GetLoaneeRemark(request.EmployerCode, startDate, endDate);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTableRemark", results);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.ToString() });
            }
        }


        [HttpPost]
        public async Task<IActionResult> RemarkReport([FromBody] RemarkReportRequest request)
        {
            try
            {
                var startDate = request.StartDate;
                var endDate = request.EndDate;
                var results = await _reportRepository.GetLoaneeRemark(request.EmployerCode, startDate, endDate);

                return CreateXlsxRemarkReport($"Remark_{DateTime.Now.ToUnixTimestamp()}.xlsx", results);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult CreateXlsxRemarkReport(string fileName, List<LoaneeRemarkExcelViewModel> data)
        {

            try
            {
                var cultureInfor = new CultureInfo("th-TH");
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheetProductTypeY = workbook.Worksheets.Add("Remark");
                    //worksheetProductTypeY.Cell(1, 1).Value = "ASSIGN DATE";
                    //worksheetProductTypeY.Cell(1, 2).Value = "EXPIRE DATE";
                    worksheetProductTypeY.Cell(1, 1).Value = "CUSTOMER CODE";
                    worksheetProductTypeY.Cell(1, 2).Value = "ID";
                    worksheetProductTypeY.Cell(1, 3).Value = "CUSTOMER NAME";
                    worksheetProductTypeY.Cell(1, 4).Value = "CONTRACT NO";
                    worksheetProductTypeY.Cell(1, 5).Value = "BANK ACTION CODE";
                    worksheetProductTypeY.Cell(1, 6).Value = "BANK ACTION NAME";
                    worksheetProductTypeY.Cell(1, 7).Value = "BANK RESULT CODE ";
                    worksheetProductTypeY.Cell(1, 8).Value = "BANK RESULT NAME";

                    worksheetProductTypeY.Cell(1, 9).Value = "PERSON CODE ";
                    worksheetProductTypeY.Cell(1, 10).Value = "PERSON CODE NAME";

                    worksheetProductTypeY.Cell(1, 11).Value = "COMPANY ACTION CODE";
                    worksheetProductTypeY.Cell(1, 12).Value = "COMPANY ACTION NAME";
                    worksheetProductTypeY.Cell(1, 13).Value = "COMPANY RESULT CODE";
                    worksheetProductTypeY.Cell(1, 14).Value = "COMPANY RESULT NAME";
                    worksheetProductTypeY.Cell(1, 15).Value = "FOLLOW CONTRACT NO";
                    worksheetProductTypeY.Cell(1, 16).Value = "APPOINTMENT DATE";
                    worksheetProductTypeY.Cell(1, 17).Value = "AMOUNT";
                    worksheetProductTypeY.Cell(1, 18).Value = "APPOINTMENT CONTRACT";
                    worksheetProductTypeY.Cell(1, 19).Value = "REMARK";
                    worksheetProductTypeY.Cell(1, 20).Value = "EMPLOYER WORK GROUP";
                    worksheetProductTypeY.Cell(1, 21).Value = "COLLECTOR";

                    for (int index = 0; index < data.Count; index++)
                    {
                        //worksheetProductTypeY.Cell(index + 2, 1).Value = data[index].AssignDate;
                        //worksheetProductTypeY.Cell(index + 2, 2).Value = data[index].ExpireDate;
                        worksheetProductTypeY.Cell(index + 2, 1).Value = data[index].CusId;
                        worksheetProductTypeY.Cell(index + 2, 2).Value = data[index].NationalityId;
                        worksheetProductTypeY.Cell(index + 2, 3).Value = data[index].CusName;
                        worksheetProductTypeY.Cell(index + 2, 4).Value = data[index].ContractNo;
                        worksheetProductTypeY.Cell(index + 2, 5).Value = data[index].BankActionCodeId;
                        worksheetProductTypeY.Cell(index + 2, 6).Value = data[index].BankActionCodeName;
                        worksheetProductTypeY.Cell(index + 2, 7).Value = data[index].BankResultCodeId;
                        worksheetProductTypeY.Cell(index + 2, 8).Value = data[index].BankResultCodeName;

                        worksheetProductTypeY.Cell(index + 2, 9).Value = data[index].BankPersonCodeId;
                        worksheetProductTypeY.Cell(index + 2, 10).Value = data[index].BankPersonCodeName;

                        worksheetProductTypeY.Cell(index + 2, 11).Value = data[index].CompanyActionCodeId;
                        worksheetProductTypeY.Cell(index + 2, 12).Value = data[index].CompanyActionCodeName;
                        worksheetProductTypeY.Cell(index + 2, 13).Value = data[index].CompanyResultCodeId;
                        worksheetProductTypeY.Cell(index + 2, 14).Value = data[index].CompanyResultCodeName;
                        worksheetProductTypeY.Cell(index + 2, 15).Value = data[index].FollowContractNo;
                        worksheetProductTypeY.Cell(index + 2, 16).Value = data[index].AppointmentDate.ToString("dd-MM-yyyy", cultureInfor);
                        worksheetProductTypeY.Cell(index + 2, 17).Value = data[index].Amount.ToFormat2Decimal();
                        worksheetProductTypeY.Cell(index + 2, 18).Value = "'" + data[index].AppointmentContract;
                        worksheetProductTypeY.Cell(index + 2, 19).Value = data[index].Remark;
                        worksheetProductTypeY.Cell(index + 2, 20).Value = data[index].EmployerWorkGroup;
                        worksheetProductTypeY.Cell(index + 2, 21).Value = data[index].Collector;

                    }

                    var _filePath = string.Format("{0}/{1}", _hostEnvironment.WebRootPath, fileName);

                    using (FileStream fileStream = new FileStream(_filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        workbook.SaveAs(fileStream);
                    }

                    var errorMessage = "you can return the errors here!";
                    return Json(new { fileName, errorMessage });


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        public ActionResult DownloadExcel(string fileName)
        {
            var _filePath = string.Format("{0}/{1}", _hostEnvironment.WebRootPath, fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(_filePath);
            System.IO.File.Delete(_filePath);
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
        }
    }
}
