using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using FastReport.Web;
using FastReport.Export.PdfSimple;
using CourtJustice.Web.Utils;
using Inventor.Infrastructure.Utils;

namespace CourtJustice.Web.Controllers
{
    public class LoaneesController : BaseController<LoaneesController>
    {
        private readonly ILoaneeRepository _loaneeRepository;
        private readonly IOccupationRepository _occupationRepository;
        private readonly ILoanTypeRepository _loanTypeRepository;
        private readonly IBucketRepository _bucketRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHost;
        private readonly IConfiguration _configuration;

        public LoaneesController(ILoaneeRepository loaneeRepository,
            IOccupationRepository occupationRepository,
            ILoanTypeRepository loanTypeRepository,
            IBucketRepository bucketRepository,
            IEmployerRepository employerRepository,
            IEmployeeRepository employeeRepository,
            IWebHostEnvironment webHost,
            IConfiguration configuration)
        {
            _loaneeRepository = loaneeRepository;
            _occupationRepository = occupationRepository;
            _loanTypeRepository = loanTypeRepository;
            _bucketRepository = bucketRepository;
            _employerRepository = employerRepository;
            _employeeRepository = employeeRepository;
            _webHost = webHost;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            //var occupations = await _occupationRepository.GetAll();
            List<SelectListItem> SelectEmployers = new();
            //foreach (var item in occupations)
            //{
            //    SelectOccupations.Add(new SelectListItem
            //    {
            //        Text = item.OccupationName.ToString(),
            //        Value = item.OccupationId.ToString(),
            //    });
            //}

            SelectEmployers.Add(new SelectListItem { Text = "Easy buy", Value = "1" });
            SelectEmployers.Add(new SelectListItem { Text = "กรุงเทพ", Value = "2" });
            ViewBag.Employers = SelectEmployers;

            List<SelectListItem> SelectLoanTaks = new();
            SelectLoanTaks.Add(new SelectListItem { Text = "ติดต่อได้-นัดชำระ", Value = "1" });
            SelectLoanTaks.Add(new SelectListItem { Text = "ติดต่อได้-ไม่นัดชำระ", Value = "2" });
            ViewBag.LoanTaskStatus = SelectLoanTaks;


            //dynamic mymodel = new ExpandoObject();
            //mymodel.Lonees = new List<LoaneeViewModel>();
            //mymodel.AssetLands = new List<AssetLandViewModel>();


            var tupleModel = new Tuple<LoaneeViewModel, IEnumerable<AssetLandViewModel>>(new LoaneeViewModel(), Enumerable.Empty<AssetLandViewModel>());

            return View(tupleModel);
        }

        private async Task ListOfViewBag()
        {
            var occupations = await _occupationRepository.GetAll();
            List<SelectListItem> SelectOccupations = new();
            foreach (var item in occupations)
            {
                SelectOccupations.Add(new SelectListItem
                {
                    Text = item.OccupationName.ToString(),
                    Value = item.OccupationId.ToString(),
                });
            }
            ViewBag.Occupations = SelectOccupations;

            //Loan Type
            var loanTypes = await _loanTypeRepository.GetAll();
            List<SelectListItem> SelectLoanTypes = new();
            foreach (var item in loanTypes)
            {
                SelectLoanTypes.Add(new SelectListItem
                {
                    Text = item.LoanTypeName.ToString(),
                    Value = item.LoanTypeCode.ToString(),
                });
            }
            ViewBag.LoanTypes = SelectLoanTypes;

            //Bucket
            var buckets = await _bucketRepository.GetAll();
            List<SelectListItem> SelectBuckets = new();
            foreach (var item in buckets)
            {
                SelectBuckets.Add(new SelectListItem
                {
                    Text = item.BucketName.ToString(),
                    Value = item.BucketId.ToString(),
                });
            }
            ViewBag.Buckets = SelectBuckets;

            //Employer
            var employers = await _employerRepository.GetAll();
            List<SelectListItem> SelectEmployers = new();
            foreach (var item in employers)
            {
                SelectEmployers.Add(new SelectListItem
                {
                    Text = item.EmployerName.ToString(),
                    Value = item.EmployerCode.ToString(),
                });
            }
            ViewBag.Employers = SelectEmployers;
            //Employee
            var employees = await _employeeRepository.GetAll();
            List<SelectListItem> SelectEmployees = new();
            foreach (var item in employees)
            {
                SelectEmployees.Add(new SelectListItem
                {
                    Text = item.EmployeeName.ToString(),
                    Value = item.EmployeeCode.ToString(),
                });
            }
            ViewBag.Employees = SelectEmployees;
        }

        public async Task<IActionResult> Create()
        {

            await ListOfViewBag();
            return View(new Loanee
            {
                IsActive = true,
                LastPaidDate = DateOnly.FromDateTime(DateTime.Today),
                FirstPaidDate = DateOnly.FromDateTime(DateTime.Today),
                DueDate = DateOnly.FromDateTime(DateTime.Today),
                FollowUpDate = DateOnly.FromDateTime(DateTime.Today)
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Loanee model)
        {

            if (ModelState.IsValid)
            {
                await _loaneeRepository.Create(model);
                _notify.Success($"{model.Name} is Created.");
                return RedirectToAction(nameof(Index));
            }
            await ListOfViewBag();
            return View(model);
        }

        [HttpGet]
        public IActionResult IndexImport()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetLoaneeByKey(string id)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("th-TH");
            var loanee = await _loaneeRepository.GetByKey(id);

            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_LoaneeCard", loanee);
            return new JsonResult(new { isValid = true, message = "", html });
        }

        //[HttpGet]
        //public async Task<JsonResult> GetAssetLandByCusId(string id)
        //{
        //    //var assetLands = await _assetLandRepository.GetByKey(id);
        //    var assetLands = new List<AssetLandViewModel>
        //    {
        //        new AssetLandViewModel
        //        {
        //            AssetLandId = "01",
        //            Position = "asas",
        //            EstimatePrice = 200000

        //        }
        //    };
        //    var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetLandCard", assetLands);
        //    return new JsonResult(new { isValid = true, message = "", html });
        //}

        //[HttpGet]
        //public async Task<JsonResult> GetAssetCarByCusId(string id)
        //{
        //    //var assetLands = await _assetLandRepository.GetByKey(id);
        //    var assetLands = new List<AssetLandViewModel>
        //    {
        //        new AssetLandViewModel
        //        {
        //            AssetLandId = "01",
        //            Position = "asas",
        //            EstimatePrice = 200000

        //        }
        //    };
        //    var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetCarCard", assetLands);
        //    return new JsonResult(new { isValid = true, message = "", html });
        //}
        [HttpGet]
        public IActionResult ShowNotice(string id)
        {
            var webReport = new WebReport();
            //var conn = new MySqlDataConnection();
            //conn.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            //webReport.Report.Dictionary.Connections.Add(conn);

            var path = System.IO.Path.Combine(ReportUtils.DesignerPath(_webHost), "notice.frx");
            var xmlPath = System.IO.Path.Combine(ReportUtils.DesignerPath(_webHost), "notice.xml");
            webReport.Report.Load(path);
            //var dataSet = new DataSet();

            CultureInfo cultureInfo = new CultureInfo("th-TH");
            var notices = new List<LoaneeNoticeViewModel>();
            var notice = new LoaneeNoticeViewModel
            {
                NoticeDate = DateTime.Today.ToString("dd MMMM yyyy", cultureInfo),
                CompanyName = "Company",
                BankName = "Bank Name",
                Address = "Address",
                LoaneeName = "ขวัญเรือน บุญมา",
                ContractDate = DateTime.Now.ToString("dd MMMM yyyy", cultureInfo),
                LoaneeNumber = "630227",
                Amount = 1000,
                DebtAmount = 1000,
                Fee = 20,
                Rate = 25.25,
                TotalAmount = 100000
            };


            notices.Add(notice);
            var ds = notices.ConvertToDataSet("Notice");
            //dataSet.ReadXml(cc);
            //cc.WriteXml(xmlPath, XmlWriteMode.WriteSchema);

            //dataSet.ReadXml(webRootPath + "/reports/nwind.xml"); // Open the xml database

            webReport.Report.RegisterData(ds, "Notice");

            ////var fastReportGenerator = new FastReportGenerator<LoaneeNoticeViewModel>(path, "test.frx");

            ////var report = fastReportGenerator.GeneratePdfFromHtml(data, PageSize.A4);
            //var exportPath = System.IO.Path.Combine(_webHost.WebRootPath, "notices");
            ////ExportToFile(report, exportPath, "testWithoutPdfSimple");

            //var pdfExport = new PDFSimpleExport();
            //var stream = new MemoryStream();
            //webReport.Report.Export(pdfExport, stream);
            //pdfExport.Dispose();
            //stream.Position = 0;
            //stream.ToArray();


            //ExportToFile(stream.ToArray(), exportPath, $"notices_{id}");

            webReport.Report.Prepare();

            using (MemoryStream ms = new MemoryStream())
            {
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(webReport.Report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf", "notices.pdf");
            }

            return View(webReport);
        }

        //public IActionResult Pdf()
        //{
        //    var webReport = GetReport();
        //    webReport.Report.Prepare();

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        PDFSimpleExport pdfExport = new PDFSimpleExport();
        //        pdfExport.Export(webReport.Report, ms);
        //        ms.Flush();
        //        return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension("Master-Detail") + ".pdf");
        //    }
        //}

        static void ExportToFile(byte[] report, string exportPath, string fileName)
        {

            fileName = System.IO.Path.Combine(exportPath, string.Format("{0}.pdf", fileName));
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
            System.IO.File.WriteAllBytes(fileName, report);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmImport(IList<IFormFile> files)
        {
            //IFormFile batchMeters
            var columnCount = 0;
            var rowCount = 0;
            var rowIndex = 0;
            //var addressAndSubAddress = "";
            Program.Progress = 0;
            List<LoaneeViewModel> loanees = new List<LoaneeViewModel>();
            const string DATE_FORMAT = "dd/MM/yyyy";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            CultureInfo culture = new CultureInfo("en-US");
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
                    dt.Rows[1].Delete();
                    dt.Rows[2].Delete();
                    dt.AcceptChanges();
                    //dt.DefaultView.Sort = "Column11,Column2,Column3 ASC";

                    var compDt = dt.DefaultView.ToTable();
                    columnCount = compDt.Columns.Count;
                    var loanee = new LoaneeViewModel();
                    for (int i = 0; i < compDt.Rows.Count; i++)
                    {
                        var assignDate = DateTime.ParseExact(dt.Rows[i][0].ToString().Trim(), DATE_FORMAT, culture);
                        if (assignDate.Year > 2500) assignDate = assignDate.AddYears(-543);
                        loanee.AssignDate = assignDate;

                        var expireDate = DateTime.ParseExact(dt.Rows[i][1].ToString().Trim(), DATE_FORMAT, culture);
                        if (expireDate.Year > 2500) expireDate = expireDate.AddYears(-543);

                        loanee.ExpireDate = expireDate;
                        loanee.NationalityId = dt.Rows[i][2].ToString().Trim();
                        var birthDate = DateTime.ParseExact(dt.Rows[i][3].ToString().Trim(), DATE_FORMAT, culture);
                        if (birthDate.Year > 2500) birthDate = birthDate.AddYears(-543);
                        loanee.BirthDate = birthDate;
                        loanee.CusId = dt.Rows[i][4].ToString().Trim();
                        loanee.Name = dt.Rows[i][5].ToString().Trim();
                        loanee.ContractNo = dt.Rows[i][6].ToString().Trim();

                        var contractDate = DateTime.ParseExact(dt.Rows[i][7].ToString().Trim(), DATE_FORMAT, culture);
                        if (contractDate.Year > 2500) contractDate = contractDate.AddYears(-543);
                        loanee.ContractDate = contractDate;

                        var woDate = DateTime.ParseExact(dt.Rows[i][8].ToString().Trim(), DATE_FORMAT, culture);
                        if (woDate.Year > 2500) woDate = woDate.AddYears(-543);

                        loanee.WODate = woDate;
                        loanee.Term = dt.Rows[i][9].ToString().Trim() == "" ? 0 : dt.Rows[i][9].ToString().Trim().ToInt16();
                        loanee.InstallmentsByContract = dt.Rows[i][10].ToString().Trim().ToDecimal();
                        loanee.LoanAmount = dt.Rows[i][12].ToString().Trim().ToDecimal();
                        loanee.WOBalance = dt.Rows[i][13].ToString().Trim().ToDecimal();
                        loanee.OverdueAmount = dt.Rows[i][14].ToString().Trim().ToDecimal();
                        loanee.TotalPenalty = dt.Rows[i][15].ToString().Trim().ToDecimal();
                        loanee.ClosingAmount = dt.Rows[i][16].ToString().Trim().ToDecimal();
                        //loanee.RcvAmtStatus = dt.Rows[i][17].ToString().Trim();
                        loanee.RcvAmtBeforeWO = dt.Rows[i][18].ToString().Trim().ToDecimal();
                        loanee.RcvAmtAfterWO = dt.Rows[i][19].ToString().Trim().ToDecimal();
                        loanee.LastPaidAmount = dt.Rows[i][20].ToString().Trim().ToDecimal();
                        loanee.NoOfAssignment = dt.Rows[i][22].ToString().Trim().ToInt16();
                        loanee.Description = dt.Rows[i][23].ToString().Trim();
                        loanee.HomeAddress1 = dt.Rows[i][24].ToString().Trim();
                        loanee.HomeAddress2 = dt.Rows[i][25].ToString().Trim();
                        loanee.HomeAddress3 = dt.Rows[i][26].ToString().Trim();
                        loanee.HomeAddress4 = dt.Rows[i][27].ToString().Trim();
                        loanee.TelephoneHome = dt.Rows[i][28].ToString().Trim();
                        loanee.OfficeAddress1 = dt.Rows[i][29].ToString().Trim();
                        loanee.OfficeAddress2 = dt.Rows[i][30].ToString().Trim();
                        loanee.OfficeAddress3 = dt.Rows[i][31].ToString().Trim();
                        loanee.OfficeAddress4 = dt.Rows[i][32].ToString().Trim();
                        loanee.TelephoneOffice = dt.Rows[i][33].ToString().Trim();
                        loanee.IdenAddress1 = dt.Rows[i][34].ToString().Trim();
                        loanee.IdenAddress2 = dt.Rows[i][35].ToString().Trim();
                        loanee.IdenAddress3 = dt.Rows[i][36].ToString().Trim();
                        loanee.IdenAddress4 = dt.Rows[i][37].ToString().Trim();
                        loanee.MobileHome = dt.Rows[i][50].ToString().Trim();
                        loanee.MobileOffice = dt.Rows[i][51].ToString().Trim();
                        loanee.MobileEmg = dt.Rows[i][53].ToString().Trim();
                        loanee.SpecialNote = dt.Rows[i][54].ToString().Trim();
                        loanee.CPCase = dt.Rows[i][56].ToString().Trim();
                        loanee.NoOfCP = dt.Rows[i][57].ToString().Trim().ToInt16();

                        var cpDate = DateTime.ParseExact(dt.Rows[i][58].ToString().Trim(), DATE_FORMAT, culture);
                        if (cpDate.Year > 2500) cpDate = cpDate.AddYears(-543);

                        loanee.CPDate = cpDate;
                        loanee.OAFee = dt.Rows[i][59].ToString().Trim().ToDecimal();
                        loanee.MaxOAFeeAmount = dt.Rows[i][60].ToString().Trim().ToDecimal();
                        loanee.MaxOAFeeBalance = dt.Rows[i][61].ToString().Trim().ToDecimal();
                        loanees.Add(loanee);
                    }


                    if (!string.IsNullOrEmpty(messsage))
                    {
                        ViewBag.Message = messsage;
                        return View();
                    }

                    //_loaneeRepository.Create()
                }
                var message = $"Loanee {rowCount} records is imported.";
                _notify.Success(message);
                ViewBag.Message = message;
                return Json(new { isvalid = true, message = $"Data {rowCount} Uploaded Successfully!" });
            }
            catch (Exception err)
            {
                string msgError = "";
                msgError = err.ToString();
                Program.Progress = (int)((float)rowIndex / (float)rowCount * 100.0);
                return Json(new { isvalid = false, message = msgError });
            }
        }

        

        [HttpPost]
        public async Task<IActionResult> GetWithPaging()
        {
            try
            {
                //var productGroupCode = Request.Form["productGroupCode"].FirstOrDefault();


                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = await _loaneeRepository.GetRecordCount(searchValue!);


                var data = await _loaneeRepository.GetPaging(skip, pageSize, searchValue);
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Progress()
        {
            return this.Content(Program.Progress.ToString());
        }
    }
}
