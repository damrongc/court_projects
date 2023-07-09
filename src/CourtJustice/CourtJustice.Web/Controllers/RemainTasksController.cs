using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using CourtJustice.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourtJustice.Web.Controllers
{
    public class RemainTasksController : BaseController<RemainTasksController>
    {
        private readonly IRemainTaskRepository _remainTaskRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RemainTasksController(IRemainTaskRepository remainTaskRepository, IEmployeeRepository employeeRepository)
        {
            _remainTaskRepository = remainTaskRepository;
            _employeeRepository = employeeRepository;
        }

        //[HttpPost]
        //public async Task<IActionResult> GetWithPaging()
        //{
        //    try
        //    {
        //        var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
        //        //var bucketId = Request.Form["bucketId"].FirstOrDefault();
        //        //var employerCode = Request.Form["employerCode"].FirstOrDefault();
        //        var draw = Request.Form["draw"].FirstOrDefault();
        //        var start = Request.Form["start"].FirstOrDefault();
        //        var length = Request.Form["length"].FirstOrDefault();
        //        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;

        //        int recordsTotal = await _remainTaskRepository.GetRecordCount(appUser.UserId,  searchValue!);
        //        var data = await _remainTaskRepository.GetPaging(appUser.UserId, skip, pageSize, searchValue!);
        //        var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
        //        return Ok(jsonData);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private async Task<List<RemainTask>> GetAll()
        {
            var results = await _remainTaskRepository.GetAll();
            return results.ToList();
        }
        public async Task<IActionResult> Index()
        {
            var tasks = await GetAll();
            return View(tasks);
        }
        public async Task<IActionResult> Create()
        {
            var employees = await _employeeRepository.GetAll();
            List<SelectListItem> selects = new();
            foreach (var item in employees)
            {
                selects.Add(new SelectListItem
                {
                    Text = $"{item.EmployeeName}",
                    Value = item.EmployeeCode.ToString(),
                });
            }
            ViewBag.Employees = selects;
            return View(new RemainTask() );
        }
        [HttpPost]
        public async Task<IActionResult> Create(RemainTask model)
        {
            try
            {
                var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
                model.TaskDatetime= DateTime.Now;
                model.AssignFrom = appUser.UserId;
                await _remainTaskRepository.Create(model);
                _notify.Success($"ข้อความเตือนความจำ is Created.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                var employees = await _employeeRepository.GetAll();
                List<SelectListItem> selects = new();
                foreach (var item in employees)
                {
                    selects.Add(new SelectListItem
                    {
                        Text = $"{item.EmployeeName}",
                        Value = item.EmployeeCode.ToString(),
                    });
                }
                ViewBag.Employees = selects;
                return View(model);
            }
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _remainTaskRepository.Delete(id);
                var results = await GetAll();
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });

            }

        }

    }
}
