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
using CourtJustice.Infrastructure.Utils;
using CourtJustice.Web.Requests;

namespace CourtJustice.Web.Controllers
{
    public class LoaneesController : BaseController<LoaneesController>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly ILoaneeRepository _loaneeRepository;
        private readonly IOccupationRepository _occupationRepository;
        private readonly ILoanTypeRepository _loanTypeRepository;
        private readonly IBucketRepository _bucketRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoanTaskStatusRepository _loanTaskStatusRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IWebHostEnvironment _webHost;
        private readonly IConfiguration _configuration;
        private readonly IUserEmployerMappingRepository _userEmployerMappingRepository;


        public LoaneesController(ILoaneeRepository loaneeRepository,
            IOccupationRepository occupationRepository,
            ILoanTypeRepository loanTypeRepository,
            IBucketRepository bucketRepository,
            IEmployerRepository employerRepository,
            IEmployeeRepository employeeRepository,
            IWebHostEnvironment webHost,
            IConfiguration configuration,
            ILoanTaskStatusRepository loanTaskStatusRepository,
            ICompanyRepository companyRepository,
            IAppUserRepository appUserRepository,
            IUserEmployerMappingRepository userEmployerMappingRepository)
        {
            _loaneeRepository = loaneeRepository;
            _occupationRepository = occupationRepository;
            _loanTypeRepository = loanTypeRepository;
            _bucketRepository = bucketRepository;
            _employerRepository = employerRepository;
            _employeeRepository = employeeRepository;
            _webHost = webHost;
            _configuration = configuration;
            _loanTaskStatusRepository = loanTaskStatusRepository;
            _companyRepository = companyRepository;
            _appUserRepository = appUserRepository;
            _userEmployerMappingRepository = userEmployerMappingRepository;
        }

        public async Task<IActionResult> Index()
        {
            var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
            switch (appUser.GroupId)
            {
                case 1:
                    ViewBag.IsEditable = true;
                    break;
                case 2:

                    ViewBag.IsEditable = false;
                    break;
                case 3:
                    ViewBag.IsEditable = false;
                    break;
                case 4:
                    ViewBag.IsEditable = false;
                    break;
            }
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

            var loanTaskStatus = await _loanTaskStatusRepository.GetAll();
            loanTaskStatus.Insert(0, new LoanTaskStatus { LoanTaskStatusId = 0, LoanTaskStatusName = "แสดงทั้งหมด" });
            List<SelectListItem> SelectLoanTaks = new();
            foreach (var item in loanTaskStatus)
            {
                SelectLoanTaks.Add(new SelectListItem
                {
                    Text = item.LoanTaskStatusName,
                    Value = item.LoanTaskStatusId.ToString()
                });
            }
            ViewBag.LoanTaskStatus = SelectLoanTaks;
            return View(new LoaneeViewModel());
            //var tupleModel = new Tuple<LoaneeViewModel, IEnumerable<AssetLandViewModel>>(new LoaneeViewModel(), Enumerable.Empty<AssetLandViewModel>());
            //return View(tupleModel);
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

        //public async Task<IActionResult> Create()
        //{

        //    await ListOfViewBag();
        //    return View(new Loanee
        //    {
        //        IsActive = true,
        //        LastPaidDate = DateOnly.FromDateTime(DateTime.Today),
        //        FirstPaidDate = DateOnly.FromDateTime(DateTime.Today),
        //        DueDate = DateOnly.FromDateTime(DateTime.Today),
        //        FollowUpDate = DateOnly.FromDateTime(DateTime.Today)
        //    });
        //}
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

        [HttpPost]
        public async Task<JsonResult> UpdateLoanee([FromBody] UpdateLoaneeRequest request)
        {
            try
            {
                var viewModel = new LoaneeViewModel
                {
                    CusId = request.CusId,
                    Name = request.Name,
                    HomeAddress1 = request.HomeAddress1,
                    HomeAddress2 = request.HomeAddress2,
                    HomeAddress3 = request.HomeAddress3,
                    HomeAddress4 = request.HomeAddress4,
                    TelephoneHome = request.TelephoneHome,
                    MobileHome = request.MobileHome,
                    IdenAddress1 = request.IdenAddress1,
                    IdenAddress2 = request.IdenAddress2,
                    IdenAddress3 = request.IdenAddress3,
                    IdenAddress4 = request.IdenAddress4,
                    MobileEmg = request.MobileEmg,
                    OfficeAddress1 = request.OfficeAddress1,
                    CompanyName = request.CompanyName,
                    OccupationName = request.OccupationName,
                    OfficeAddress2 = request.OfficeAddress2,
                    OfficeAddress3 = request.OfficeAddress3,
                    OfficeAddress4 = request.OfficeAddress4,
                    TelephoneOffice = request.TelephoneHome,
                    MobileOffice = request.MobileOffice,
                    LoanTypeCode = request.LoanTypeCode,
                    LoanTaskStatusId = request.LoanTaskStatusId,
                };

                await _loaneeRepository.UpdateOrAssign(viewModel);
                return new JsonResult(new { isValid = true, message = "" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateLoaneeByCollector([FromBody] UpdateLoaneeRequest request)
        {
            try
            {
                var viewModel = new LoaneeViewModel
                {
                    CusId = request.CusId,
                    TelephoneHome = request.TelephoneHome,
                    MobileHome = request.MobileHome,
                    MobileEmg = request.MobileEmg,
                    TelephoneOffice = request.TelephoneHome,
                    MobileOffice = request.MobileOffice,
                    LoanTaskStatusId = request.LoanTaskStatusId,
                };

                await _loaneeRepository.UpdateLoaneeByCollector(viewModel);
                return new JsonResult(new { isValid = true, message = "" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> IndexImport()
        {

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
            return View();
        }

        [HttpGet]
        public IActionResult IndexCancelImport()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetLoaneeByKey(string id)
        {
            var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("th-TH");
            var loanee = await _loaneeRepository.GetByKey(id);

            var loanTaskStatus = await _loanTaskStatusRepository.GetAll();
            loanTaskStatus.Insert(0, new LoanTaskStatus { LoanTaskStatusId = 0, LoanTaskStatusName = "ไม่ระบุ" });
            List<SelectListItem> SelectLoanTaks = new();
            foreach (var item in loanTaskStatus)
            {
                SelectLoanTaks.Add(new SelectListItem
                {
                    Selected = item.LoanTaskStatusId == loanee.LoanTaskStatusId,
                    Text = item.LoanTaskStatusName,
                    Value = item.LoanTaskStatusId.ToString()
                });
            }
            ViewBag.LoanTaskStatus = SelectLoanTaks;


            var employers = await _employerRepository.GetAll();
            List<SelectListItem> SelectEmployers = new();
            foreach (var item in employers)
            {
                SelectEmployers.Add(new SelectListItem
                {
                    Selected = item.EmployerCode == loanee.EmployerCode,
                    Text = item.EmployerName.ToString(),
                    Value = item.EmployerCode.ToString(),
                });
            }
            ViewBag.Employers = SelectEmployers;

            var buckets = await _bucketRepository.GetAll();
            List<SelectListItem> SelectBuckets = new();
            foreach (var item in buckets)
            {
                SelectBuckets.Add(new SelectListItem
                {
                    Selected = item.BucketId == loanee.BucketId,
                    Text = item.BucketName.ToString(),
                    Value = item.BucketId.ToString(),
                });
            }
            ViewBag.Buckets = SelectBuckets;

            //var occupations = await _occupationRepository.GetAll();
            //List<SelectListItem> SelectOccupations = new();
            //foreach (var item in occupations)
            //{
            //    SelectOccupations.Add(new SelectListItem
            //    {
            //        Selected = item.OccupationId == loanee.OccupationId,
            //        Text = item.OccupationName.ToString(),
            //        Value = item.OccupationId.ToString(),
            //    });
            //}
            //ViewBag.Occupations = SelectOccupations;

            var loanTypes = await _loanTypeRepository.GetAll();
            List<SelectListItem> SelectLoanTypes = new();
            foreach (var item in loanTypes)
            {
                SelectLoanTypes.Add(new SelectListItem
                {
                    Selected = item.LoanTypeCode == loanee.LoanTypeCode,
                    Text = item.LoanTypeName.ToString(),
                    Value = item.LoanTypeCode.ToString(),
                });
            }
            ViewBag.LoanTypes = SelectLoanTypes;


            var employees = new List<EmployeeViewModel>();
            List<SelectListItem> SelectEmployees = new();
            ViewBag.GroupId = appUser.GroupId;
            switch (appUser.GroupId)
            {
                case 1:
                    employees = await _employeeRepository.GetAll();
                    employees.Insert(0, new EmployeeViewModel { EmployeeCode = "0", EmployeeName = "ไม่ระบุ" });
                    //ViewBag.IsEditable = true;
                    break;
                case 2:
                    employees = await _employeeRepository.GetEmployeeByManager(appUser.UserId);
                    //ViewBag.IsEditable = true;
                    break;
                case 3:
                case 4:
                    employees = await _employeeRepository.GetEmployeeByCode(appUser.UserId);
                    //ViewBag.IsEditable = false;
                    break;
                default: 
                    //ViewBag.IsEditable = false; 
                    break;
            }

            foreach (var item in employees)
            {
                SelectEmployees.Add(new SelectListItem
                {
                    Selected = item.EmployeeCode == loanee.EmployerCode,
                    Text = item.EmployeeName.ToString(),
                    Value = item.EmployeeCode.ToString(),
                });
            }
            ViewBag.Employees = SelectEmployees;


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
        public async Task<IActionResult> ShowNotice(string id)
        {
            var webReport = new WebReport();
            //var conn = new MySqlDataConnection();
            //conn.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            //webReport.Report.Dictionary.Connections.Add(conn);

            var company = await _companyRepository.GetByKey(1);
            var loanee = await _loaneeRepository.GetByKey(id);
            var employer = await _employerRepository.GetByKey(loanee.EmployerCode);
            var path = Path.Combine(ReportUtils.DesignerPath(_webHost), "notice.frx");
            var xmlPath = Path.Combine(ReportUtils.DesignerPath(_webHost), "notice.xml");
            webReport.Report.Load(path);
            //var dataSet = new DataSet();

            CultureInfo cultureInfo = new("th-TH");
            var notices = new List<LoaneeNoticeViewModel>();
            var notice = new LoaneeNoticeViewModel
            {
                NoticeDate = DateTime.Today.ToString("dd MMMM yyyy", cultureInfo),
                CompanyName = company.CompanyName,
                BankName = employer.EmployerName,
                Address = company.Address,
                LoaneeName = loanee.Name,
                ContractDate = loanee.ContractDate,
                LoaneeNumber = loanee.ContractNo,
                Amount = loanee.LoanAmount.ToFormat2Decimal(),
                DebtAmount = Convert.ToDecimal(99970.94).ToFormat2Decimal(),
                Fee = Convert.ToDecimal(20318.89).ToFormat2Decimal(),
                Rate = loanee.IntereteRate.ToFormat2Decimal(),
                TotalAmount = Convert.ToDecimal(12289.93).ToFormat2Decimal()
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

            using (MemoryStream ms = new())
            {
                PDFSimpleExport pdfExport = new();
                pdfExport.Export(webReport.Report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf", loanee.ContractNo + ".pdf");
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
        public async Task<IActionResult> ConfirmImport(IList<IFormFile> files, string employerCode)
        {
            var rowCount = 0;
            var rowIndex = 0;
            //var addressAndSubAddress = "";
            Program.Progress = 0;
            List<LoaneeViewModel> loanees = new();
            //const string DATE_FORMAT = "dd/MM/yyyy";
            //var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");


            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //CultureInfo culture = new("th-TH");
            var cuurent_cus_id = "";
            int current_idx = 0;
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
                    //IFormFile batchMeters
                    int columnCount = compDt.Columns.Count;

                    for (int i = 0; i < compDt.Rows.Count; i++)
                    {
                        var loanee = new LoaneeViewModel();
                        loanee.EmployeeCode = dt.Rows[i][0].ToString().Trim();
                        var cusId = dt.Rows[i][5].ToString().Trim();
                        cuurent_cus_id = cusId.Trim();
                        current_idx = i + 1;

                        //var isExisting = _loaneeRepository.IsExisting(cusId);
                        //if (isExisting)
                        //    continue;
                        //var assignDate = DateTime.ParseExact(dt.Rows[i][1].ToString().Trim(), DATE_FORMAT, culture);
                        //if (assignDate.Year > 2500) assignDate = assignDate.AddYears(-543);
                        if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString()))
                        {
                            loanee.AssignDate = dt.Rows[i][1].ToString().Substring(0,10).Trim();
                        }
                        //var expireDate = DateTime.ParseExact(dt.Rows[i][2].ToString().Trim(), DATE_FORMAT, culture);
                        //if (expireDate.Year > 2500) expireDate = expireDate.AddYears(-543);
                        if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                        {
                            loanee.ExpireDate = dt.Rows[i][2].ToString();

                        }
                        loanee.NationalityId = dt.Rows[i][3].ToString().Trim();
                        //var birthDate = DateTime.ParseExact(dt.Rows[i][4].ToString().Trim(), DATE_FORMAT, culture);
                        //if (birthDate.Year > 2500) birthDate = birthDate.AddYears(-543);

                        if (!string.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                        {
                            loanee.BirthDate = dt.Rows[i][4].ToString();

                        }
                        loanee.CusId = cusId;
                        loanee.Name = dt.Rows[i][6].ToString().Trim();
                        loanee.ContractNo = dt.Rows[i][7].ToString().Trim();

                        //var contractDate = DateTime.ParseExact(dt.Rows[i][8].ToString().Trim(), DATE_FORMAT, culture);
                        //if (contractDate.Year > 2500) contractDate = contractDate.AddYears(-543);

                        if (!string.IsNullOrEmpty(dt.Rows[i][8].ToString()))
                        {
                            loanee.ContractDate = dt.Rows[i][8].ToString();
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[i][9].ToString()))
                        {
                            loanee.WODate = dt.Rows[i][9].ToString();
                        }
                        //var woDate = DateTime.ParseExact(dt.Rows[i][9].ToString().Trim(), DATE_FORMAT, culture);
                        //if (woDate.Year > 2500) woDate = woDate.AddYears(-543);
                        //if (!string.IsNullOrEmpty(dt.Rows[i][9].ToString()))
                        //{
                        //    loanee.WODate = DateTime.Parse(dt.Rows[i][9].ToString());

                        //}
                        loanee.Term = dt.Rows[i][10].ToString().Trim() == "" ? 0 : dt.Rows[i][10].ToString().Trim().ToInt16();
                        loanee.InstallmentsByContract = dt.Rows[i][11].ToString().Trim() == "" ? 0 : dt.Rows[i][11].ToString().Trim().ToDecimal();
                        loanee.LoanAmount = dt.Rows[i][13].ToString().Trim() == "" ? 0 : dt.Rows[i][13].ToString().Trim().ToDecimal();
                        loanee.WOBalance = dt.Rows[i][14].ToString().Trim() == "" ? 0 : dt.Rows[i][14].ToString().Trim().ToDecimal();
                        loanee.OverdueAmount = dt.Rows[i][15].ToString().Trim() == "" ? 0 : dt.Rows[i][15].ToString().Trim().ToDecimal();
                        loanee.TotalPenalty = dt.Rows[i][16].ToString().Trim() == "" ? 0 : dt.Rows[i][16].ToString().Trim().ToDecimal();
                        loanee.ClosingAmount = dt.Rows[i][17].ToString().Trim() == "" ? 0 : dt.Rows[i][17].ToString().Trim().ToDecimal();
                        loanee.RcvAmtStatus = dt.Rows[i][18].ToString().Trim();
                        loanee.RcvAmtBeforeWO = dt.Rows[i][19].ToString().Trim() == "" ? 0 : dt.Rows[i][19].ToString().Trim().ToDecimal();
                        loanee.RcvAmtAfterWO = dt.Rows[i][20].ToString().Trim() == "" ? 0 : dt.Rows[i][20].ToString().Trim().ToDecimal();
                        loanee.LastPaidAmount = dt.Rows[i][21].ToString().Trim() == "" ? 0 : dt.Rows[i][21].ToString().Trim().ToDecimal();
                        loanee.NoOfAssignment = dt.Rows[i][23].ToString().Trim() == "" ? 0 : dt.Rows[i][23].ToString().Trim().ToInt16();
                        loanee.Description = dt.Rows[i][24].ToString().Trim();
                        loanee.HomeAddress1 = dt.Rows[i][25].ToString().Trim();
                        loanee.HomeAddress2 = dt.Rows[i][26].ToString().Trim();
                        loanee.HomeAddress3 = dt.Rows[i][27].ToString().Trim();
                        loanee.HomeAddress4 = dt.Rows[i][28].ToString().Trim();
                        loanee.TelephoneHome = dt.Rows[i][29].ToString().Trim();
                        loanee.CompanyName = dt.Rows[i][30].ToString().Trim();
                        loanee.OfficeAddress1 = dt.Rows[i][31].ToString().Trim();
                        loanee.OfficeAddress2 = dt.Rows[i][32].ToString().Trim();
                        loanee.OfficeAddress3 = dt.Rows[i][33].ToString().Trim();
                        loanee.OfficeAddress4 = dt.Rows[i][34].ToString().Trim();
                        loanee.TelephoneOffice = dt.Rows[i][35].ToString().Trim();
                        loanee.IdenAddress1 = dt.Rows[i][36].ToString().Trim();
                        loanee.IdenAddress2 = dt.Rows[i][37].ToString().Trim();
                        loanee.IdenAddress3 = dt.Rows[i][38].ToString().Trim();
                        loanee.IdenAddress4 = dt.Rows[i][39].ToString().Trim();
                        loanee.EmergencyContract1 = dt.Rows[i][40].ToString().Trim();
                        loanee.EmergencyPhone1 = dt.Rows[i][41].ToString().Trim();
                        loanee.EmergencyExt1 = dt.Rows[i][42].ToString().Trim();
                        loanee.EmergencyContract2 = dt.Rows[i][43].ToString().Trim();
                        loanee.EmergencyPhone2 = dt.Rows[i][44].ToString().Trim();
                        loanee.EmergencyExt2 = dt.Rows[i][45].ToString().Trim();
                        loanee.EmergencyContract3 = dt.Rows[i][46].ToString().Trim();
                        loanee.EmergencyPhone3 = dt.Rows[i][47].ToString().Trim();
                        loanee.EmergencyExt3 = dt.Rows[i][48].ToString().Trim();
                        loanee.EmergencyContract4 = dt.Rows[i][49].ToString().Trim();
                        loanee.EmergencyPhone4 = dt.Rows[i][50].ToString().Trim();
                        loanee.EmergencyExt4 = dt.Rows[i][51].ToString().Trim();
                        loanee.MobileHome = dt.Rows[i][52].ToString().Trim();
                        loanee.MobileOffice = dt.Rows[i][53].ToString().Trim();
                        loanee.MobileCont = dt.Rows[i][54].ToString().Trim();
                        loanee.MobileEmg = dt.Rows[i][55].ToString().Trim();
                        loanee.SpecialNote = dt.Rows[i][56].ToString().Trim();
                        loanee.LoanTypeCode = "00";
                        loanee.CPCase = dt.Rows[i][58].ToString().Trim();
                        loanee.NoOfCP = dt.Rows[i][59].ToString().Trim() == "" ? 0 : dt.Rows[i][59].ToString().Trim().ToInt16();
                        //loanee.OccupationId = 1;
                        //var cpDate = DateTime.ParseExact(dt.Rows[i][59].ToString().Trim(), DATE_FORMAT, culture);
                        //if (cpDate.Year > 2500) cpDate = cpDate.AddYears(-543);

                        if (!string.IsNullOrEmpty(dt.Rows[i][60].ToString()))
                        {
                            loanee.CPDate = dt.Rows[i][60].ToString();
                        }

                        loanee.OAFee = dt.Rows[i][61].ToString().Trim() == "" ? 0 : dt.Rows[i][61].ToString().Trim().ToDecimal();
                        loanee.MaxOAFeeAmount = dt.Rows[i][62].ToString().Trim() == "" ? 0 : dt.Rows[i][62].ToString().Trim().ToDecimal();
                        loanee.MaxOAFeeBalance = dt.Rows[i][63].ToString().Trim() == "" ? 0 : dt.Rows[i][63].ToString().Trim().ToDecimal();
                        loanee.OAFlag = dt.Rows[i][64].ToString().Trim();
                        loanee.SendingAddress = dt.Rows[i][65].ToString().Trim();
                        loanee.Gender = dt.Rows[i][66].ToString().Trim();
                        loanee.OccupationName = dt.Rows[i][67].ToString().Trim();
                        loanee.MaritalStatus = dt.Rows[i][68].ToString().Trim();
                        loanee.ProductCode = dt.Rows[i][69].ToString().Trim();
                        loanee.DebtAge = dt.Rows[i][70].ToString().Trim();
                        loanee.TotalPayment = dt.Rows[i][71].ToString().Trim() == "" ? 0 : dt.Rows[i][71].ToString().Trim().ToDecimal();
                        loanee.EmployerWorkGroup = dt.Rows[i][72].ToString().Trim();
                        loanee.Salary = dt.Rows[i][73].ToString().Trim() == "" ? 0 : dt.Rows[i][73].ToString().Trim().ToDecimal();
                        loanee.LoanTaskStatusId = dt.Rows[i][74].ToString().Trim() == "" ? 99 : dt.Rows[i][74].ToString().Trim().ToInt16();
                        loanee.BucketId = 1;
                        loanee.EmployerCode = employerCode;
                        loanees.Add(loanee);
                        rowIndex += 1;
                        Program.Progress = (int)((float)rowIndex / (float)compDt.Rows.Count * 100.0);
                        await Task.Delay(10); // It is only to make the process slower
                    }


                    if (!string.IsNullOrEmpty(messsage))
                    {
                        ViewBag.Message = messsage;
                        return View();
                    }
                    await _loaneeRepository.BulkInsertOrUpdate(loanees);
                }
                var message = $"Loanee {loanees.Count} records is imported.";
                _notify.Success(message);
                ViewBag.Message = message;
                return Json(new { isvalid = true, message });
            }
            catch (Exception err)
            {
                string msgError = string.Format("ROW: {0} {1} is {2}", current_idx, cuurent_cus_id, err.Message);
                Program.Progress = (int)((float)rowIndex / (float)rowCount * 100.0);
                return Json(new { isvalid = false, message = msgError });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmCancelImport(IList<IFormFile> files)
        {
            var rowCount = 0;
            var rowIndex = 0;
            Program.Progress = 0;
            List<LoaneeViewModel> loanees = new();
            var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            CultureInfo culture = new("th-TH");
            var cuurent_cus_id = "";
            int current_idx = 0;
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
                    //IFormFile batchMeters
                    int columnCount = compDt.Columns.Count;

                    for (int i = 0; i < compDt.Rows.Count; i++)
                    {
                        var loanee = new LoaneeViewModel();
                        var cusId = dt.Rows[i][5].ToString().Trim();
                        cuurent_cus_id = cusId.Trim();
                        current_idx = i + 1;
                        loanee.CusId= cusId;
                        loanees.Add(loanee);
                        rowIndex += 1;
                        Program.Progress = (int)((float)rowIndex / (float)compDt.Rows.Count * 100.0);
                        await Task.Delay(10); // It is only to make the process slower
                    }


                    if (!string.IsNullOrEmpty(messsage))
                    {
                        ViewBag.Message = messsage;
                        return View();
                    }
                    await _loaneeRepository.DeActivate(loanees);
                }
                var message = $"ยกเลิกการ import {loanees.Count} records";
                _notify.Success(message);
                ViewBag.Message = message;
                return Json(new { isvalid = true, message });
            }
            catch (Exception err)
            {
                string msgError = string.Format("ROW: {0} {1} is {2}", current_idx, cuurent_cus_id, err.Message);
                Program.Progress = (int)((float)rowIndex / (float)rowCount * 100.0);
                return Json(new { isvalid = false, message = msgError });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetWithPaging()
        {
            try
            {
                var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
                var employeeCodes = new List<string>();

                switch (appUser.GroupId)
                {
                    case 2:
                        var employees = await _employeeRepository.GetEmployeeByManager(appUser.UserId);
                        foreach (var employee in employees)
                        {
                            employeeCodes.Add(employee.EmployeeCode);
                        }
                        break;
                    case 4:
                        employeeCodes.Add(appUser.UserId);
                        break;
                }

                var loanTaskStatusId = Request.Form["loanTaskStatusId"].FirstOrDefault();
                var employerCode = Request.Form["employerCode"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                employerCode = employerCode == "0" ? "" : employerCode;
                int recordsTotal = await _loaneeRepository.GetRecordCount(loanTaskStatusId!.ToInt16(), employerCode, employeeCodes, searchValue!);
                var data = await _loaneeRepository.GetPaging(loanTaskStatusId!.ToInt16(), employerCode!, employeeCodes, skip, pageSize, searchValue!);
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
