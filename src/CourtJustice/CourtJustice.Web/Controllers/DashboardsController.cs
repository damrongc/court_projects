using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using iText.StyledXmlParser.Jsoup.Select;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourtJustice.Web.Controllers
{
    public class DashboardsController : BaseController<DashboardsController>
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly IUserEmployerMappingRepository _userEmployerMappingRepository;
        private readonly IAppUserRepository _appUserRepository;

        public DashboardsController(IDashboardRepository dashboardRepository, IUserEmployerMappingRepository userEmployerMappingRepository, IEmployerRepository employerRepository, IAppUserRepository appUserRepository)
        {
            _dashboardRepository = dashboardRepository;
            _userEmployerMappingRepository = userEmployerMappingRepository;
            _employerRepository = employerRepository;
            _appUserRepository = appUserRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetRemainTaskPaging()
        {
            try
            {
                var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
                //var bucketId = Request.Form["bucketId"].FirstOrDefault();
                //var employerCode = Request.Form["employerCode"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = await _dashboardRepository.GetRemainTaskRecordCount(appUser.UserId, searchValue!);
                var data = await _dashboardRepository.GetRemainTaskPaging(appUser.UserId, skip, pageSize, searchValue!);
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeTodoPaging()
        {
            try
            {
                var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
                //var bucketId = Request.Form["bucketId"].FirstOrDefault();
                //var employerCode = Request.Form["employerCode"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = await _dashboardRepository.GetEmployeeTodoRecordCount(appUser.UserId, searchValue!);
                var data = await _dashboardRepository.GetEmployeeTodoPaging(appUser.UserId, skip, pageSize, searchValue!);
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetLoaneeSummary()
        {
            try
            {
                var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");

                //var employeeCode = "";
                //var employerCode = "";

                //switch (appUser.GroupId)
                //{
                //    case 1: break;
                //    case 2: 

                //        break;
                //    case 3: break;
                //    case 4:
                //        employeeCode = appUser.UserId;
                //            break;

                //}

                var results = await _dashboardRepository.GetLoaneeSummary(appUser.GroupId, appUser.UserId);
                var chart = new SummaryChartViewModel
                {
                    Title = "ข้อมูลลูกหนี้",
                    SubTitle = "รายละเอียดจำนวนลูกหนี้และยอดหนี้",
                    Categories = results.Select(p => p.EmployerName).ToArray(),
                    CountValues = results.Select(p => p.LoaneeCount).ToArray(),
                    TotalValues = results.Select(p => p.TotalAmount).ToArray(),
                };
                return new JsonResult(new { isValid = true, data = chart });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = true, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetPaymentSummary()
        {
            try
            {

                var loaneeSummaries = new List<LoaneeSummaryViewModel>();
                var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
                var mappings = new List<UserEmployerMapping?>();
                var collectors = new List<AppUserViewModel>();
                var chart = new ChartViewModel();
                switch (appUser.GroupId)
                {
                    case 1:
                        var employers = await _employerRepository.GetAllActive();
                        foreach (var employer in employers)
                        {
                            if (employer.StartDay == 0)
                            {
                                employer.StartDay = 1;
                                employer.TotalDay = 30;
                            }
                            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, employer.StartDay);
                            var endDate = startDate.AddDays(employer.TotalDay);

                            var results = await _dashboardRepository.GetPaymentSummary(string.Empty, employer.EmployerCode, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
                            foreach (var item in results)
                            {
                                item.EmployerName = employer.EmployerName;
                            }
                            loaneeSummaries.AddRange(results);
                        }

                        chart = GetPaymentSummaryByGroupUser(appUser.GroupId, 0, loaneeSummaries);
                        break;
                    case 2:
                        var managerId = appUser.UserId.ToString();
                        collectors = await _appUserRepository.GetCollectorByManager(managerId);
                        foreach (var user in collectors)
                        {
                            mappings =_userEmployerMappingRepository.GetByUser(user.UserId);
                            foreach (var mapping in mappings)
                            {
                                var employer = await _employerRepository.GetByKey(mapping.EmployerCode);
                                if (employer != null)
                                {
                                    if (employer.StartDay == 0)
                                    {
                                        employer.StartDay = 1;
                                        employer.TotalDay = 30;
                                    }
                                    var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, employer.StartDay);
                                    var endDate = startDate.AddDays(employer.TotalDay);

                                    var results = await _dashboardRepository.GetPaymentSummary(user.UserId, employer.EmployerCode, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
                                    foreach (var item in results)
                                    {
                                        item.EmployerName = employer.EmployerName;
                                    }
                                    loaneeSummaries.AddRange(results);
                                }
                            }
                        }
                        var target = await _appUserRepository.GetSumTargetByManager(managerId);
                        chart = GetPaymentSummaryByGroupUser(appUser.GroupId, target, loaneeSummaries);
                        break;
                    case 4:
                        var collectorId = appUser.UserId.ToString();
                        mappings = _userEmployerMappingRepository.GetByUser(collectorId);

                        foreach (var mapping in mappings)
                        {
                            var employer = await _employerRepository.GetByKey(mapping.EmployerCode);
                            if (employer != null)
                            {
                                if (employer.StartDay == 0)
                                {
                                    employer.StartDay = 1;
                                    employer.TotalDay = 30;
                                }
                                var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, employer.StartDay);
                                var endDate = startDate.AddDays(employer.TotalDay);

                                var results = await _dashboardRepository.GetPaymentSummary(collectorId, employer.EmployerCode, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
                                foreach (var item in results)
                                {
                                    item.EmployerName = employer.EmployerName;
                                }
                                loaneeSummaries.AddRange(results);
                            }
                        }

                        var collector = await _appUserRepository.GetByKey(appUser.UserId);
                        chart = GetPaymentSummaryByGroupUser(appUser.GroupId, collector.Target, loaneeSummaries);
                        break;
                    default:break;
                }
                
                return new JsonResult(new { isValid = true, data = chart });

                //var yearMonth = loaneeSummaries.Select(p => p.YearMonth).Distinct().ToArray();
                //var employerNames = loaneeSummaries.Select(p => p.EmployerName).ToArray();
                //var chart = new SummaryChartViewModel
                //{
                //    Title = "ข้อมูลยอดเงินที่เก็บได้เดือนปัจจุบัน",
                //    SubTitle = "รายละเอียดยอดเงินที่เก็บได้เดือนปัจจุบัน",
                //    Categories = loaneeSummaries.Select(p => p.EmployerName).ToArray(),
                //    CountValues = loaneeSummaries.Select(p => p.LoaneeCount).ToArray(),
                //    TotalValues = loaneeSummaries.Select(p => p.TotalAmount).ToArray(),
                //};



                //if (appUser.GroupId == 1)
                //{
                //    Series[] series = new Series[employerNames.Length];
                //    for (int i = 0; i < employerNames.Length; i++)
                //    {
                //        series[i] = new Series();
                //        series[i].Name = employerNames[i];
                //        var data = loaneeSummaries.
                //            Where(p => p.EmployerName == employerNames[i]).
                //            Select(p => p.TotalAmount).ToArray();

                //        series[i].Data = data;
                //        series[i].ShowInLegend = true;
                //        series[i].Visible = true;
                //    }
                //    chart.Series = series;
                //    return new JsonResult(new { isValid = true, data = chart });
                //}
                //else
                //{

                //    var collector = await _appUserRepository.GetByKey(appUser.UserId);

                //    Series[] series = new Series[employerNames.Length + 2];
                //    series[0] = new Series
                //    {
                //        Name = "เป้าหมาย",
                //        Data = new decimal[] { collector.Target },
                //        ShowInLegend = true,
                //        Visible = true,
                //        Color = "#000000"
                //    };

                //    for (int i = 0; i < employerNames.Length; i++)
                //    {
                //        series[i + 1] = new Series
                //        {
                //            Name = employerNames[i]
                //        };
                //        var data = loaneeSummaries.
                //            Where(p => p.EmployerName == employerNames[i]).
                //            Select(p => p.TotalAmount).ToArray();

                //        series[i + 1].Data = data;
                //        series[i + 1].ShowInLegend = true;
                //        series[i + 1].Visible = true;
                //        //series[i + 1].Color = "#3CB6F1";
                //    }

                //    var totalAmount = loaneeSummaries.Select(p => p.TotalAmount).Sum();

                //    var diffAmount = totalAmount - collector.Target;
                //    var color = "#FF0000";
                //    if (diffAmount > 0)
                //    {
                //        color = "#0B6623";
                //    };
                //    series[series.Length - 1] = new Series
                //    {
                //        Name = "ผลต่าง",
                //        Data = new decimal[] { diffAmount },
                //        ShowInLegend = true,
                //        Visible = true,
                //        Color = color

                //    };

                //    chart.Series = series;
                //    return new JsonResult(new { isValid = true, data = chart });
                //}
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = true, message = ex.Message });
            }
        }

        private ChartViewModel GetPaymentSummaryByGroupUser(int groupId,decimal target, List<LoaneeSummaryViewModel> loaneeSummaries)
        {
            try
            {
                var yearMonth = loaneeSummaries.Select(p => p.YearMonth).Distinct().ToArray();
                var employerNames = loaneeSummaries.Select(p => p.EmployerName).ToArray();
                var chart = new ChartViewModel
                {
                    Title = "ข้อมูลยอดเงินที่เก็บได้เดือนปัจจุบัน",
                    SubTitle = "",
                    Categories = yearMonth,
                };
                var serieLength = employerNames.Length;
                Series[] series = new Series[serieLength];
                switch (groupId)
                {
                    case 1:
                        
                        for (int i = 0; i < employerNames.Length; i++)
                        {
                            var data = loaneeSummaries
                                .Where(p => p.EmployerName == employerNames[i])
                                .Select(p => p.TotalAmount)
                                .ToArray();
                            series[i] = new Series
                            {
                                Name = employerNames[i],
                                Data = data,
                                ShowInLegend = true,
                                Visible = true
                            };
                        }
                        chart.Series = series;
                        break;
         
                    case 3:
                        break;
                    case 2:
                    case 4:
                        Array.Resize(ref series, serieLength + 2);

                        //Series[] series = new Series[employerNames.Length + 2];
                        series[0] = new Series
                        {
                            Name = "เป้าหมาย",
                            Data = new decimal[] { target },
                            ShowInLegend = true,
                            Visible = true,
                            Color = "#000000"
                        };
                        for (int i = 0; i < employerNames.Length; i++)
                        {
                            var data = loaneeSummaries
                                .Where(p => p.EmployerName == employerNames[i])
                                .Select(p => p.TotalAmount)
                                .ToArray();
                            series[i + 1] = new Series
                            {
                                Name = employerNames[i],
                                Data = data,
                                ShowInLegend = true,
                                Visible = true
                            };
                            //series[i + 1].Color = "#3CB6F1";
                        }
                        var totalAmount = loaneeSummaries.Select(p => p.TotalAmount).Sum();
                        var diffAmount = totalAmount - target;
                        var color = "#FF0000";
                        if (diffAmount > 0)
                        {
                            color = "#0B6623";
                        };
                        series[series.Length - 1] = new Series
                        {
                            Name = "ผลต่าง",
                            Data = new decimal[] { diffAmount },
                            ShowInLegend = true,
                            Visible = true,
                            Color = color

                        };

                        chart.Series = series;
                        break;

                }
                return chart;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //private ChartViewModel CreateYearlyChartData(string[] zones, string[] categories, List<LoaneeSummaryViewModel> results)
        //{
        //    var title = zones.Count() == 0 ? "No data!" : "";
        //    var chart = new ChartViewModel
        //    {
        //        Title = title,
        //        SubTitle = "",
        //        Categories = categories,
        //    };
        //    Series[] series = new Series[zones.Length];
        //    for (int i = 0; i < zones.Length; i++)
        //    {
        //        series[i] = new Series();
        //        series[i].Name = zones[i];


        //        var datas = results.Where(p => p.ZoneName == zones[i])
        //            .Select(p => p.Consumption).ToArray();


        //        series[i].Data = datas;
        //        series[i].ShowInLegend = true;
        //        series[i].Visible = true;
        //    }
        //    chart.Series = series;
        //    return chart;
        //}
    }
}
