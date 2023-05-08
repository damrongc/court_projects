using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

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

        public LoaneesController(ILoaneeRepository loaneeRepository,
            IOccupationRepository occupationRepository,
            ILoanTypeRepository loanTypeRepository,
            IBucketRepository bucketRepository,
            IEmployerRepository employerRepository,
            IEmployeeRepository employeeRepository)
        {
            _loaneeRepository = loaneeRepository;
            _occupationRepository = occupationRepository;
            _loanTypeRepository = loanTypeRepository;
            _bucketRepository = bucketRepository;
            _employerRepository = employerRepository;
            _employeeRepository = employeeRepository;
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

            return View();
        }

        private async  Task ListOfViewBag()
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

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                if (files[0]?.Length != 0)
                {
                    var stream = files[0].OpenReadStream();
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        var dt = result.Tables[0];
                        var messsage = "";
                        dt.Rows[0].Delete();
                        dt.AcceptChanges();
                        //dt.DefaultView.Sort = "Column11,Column2,Column3 ASC";

                        var compDt = dt.DefaultView.ToTable();
                        columnCount = compDt.Columns.Count;
                        var loanee = new LoaneeViewModel();
                        for (int i = 0; i < compDt.Rows.Count; i++)
                        {
                            loanee.Name = dt.Rows[i][2].ToString().Trim();
                            loanee.CusId = dt.Rows[i][3].ToString().Trim();
                            loanee.LoanNumber = dt.Rows[i][4].ToString().Trim();
                            loanee.Address = dt.Rows[i][5].ToString().Trim();
               
                            //var meterId = dt.Rows[i][0].ToString().Trim();
                            //var address = dt.Rows[i][2].ToString().Trim();
                            //var subAddress = dt.Rows[i][3].ToString().Trim();
                            //var circuit = dt.Rows[i][4].ToString().Trim();
                            //var siteName = dt.Rows[i][5].ToString().Trim();
                            //var buildingName = dt.Rows[i][6].ToString().Trim();
                            //var zoneName = dt.Rows[i][7].ToString().Trim();

                            ////var meterType = dt.Rows[i][8].ToString().Trim();
                            //var meterCode = dt.Rows[i][9].ToString().Trim();
                            //var meterName = dt.Rows[i][10].ToString().Trim();
                            //var locationCode = dt.Rows[i][11].ToString().Trim();
                            //var locationName = dt.Rows[i][12].ToString().Trim();

                            //var loopId = dt.Rows[i][13].ToString().Trim();
                            //var meterModel = dt.Rows[i][14].ToString().Trim();
                            //var portNo = dt.Rows[i][15].ToString().Trim();
                            //var ipAddress = dt.Rows[i][16].ToString().Trim();
                            //var ipPort = dt.Rows[i][17].ToString().Trim();
                            //var phase = dt.Rows[i][18].ToString().Trim();
                            //var floor = dt.Rows[i][19].ToString().Trim();
                            //var multiply = dt.Rows[i][20].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(messsage))
                        {
                            ViewBag.Message = messsage;
                            return View();
                        }

                        
                    }
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
                int recordsTotal = await _loaneeRepository.GetRecordCount(searchValue);

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
