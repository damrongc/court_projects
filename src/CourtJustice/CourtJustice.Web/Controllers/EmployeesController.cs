using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
    
    public class EmployeesController : BaseController<EmployeesController>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAppUserRepository _appUserRepository;

        public EmployeesController(IEmployeeRepository employeeRepository, IAppUserRepository appUserRepository)
        {
            _employeeRepository = employeeRepository;
            _appUserRepository = appUserRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        private async Task<List<EmployeeViewModel>> GetAll()
        {
            var result = await _employeeRepository.GetAll();
            return result.ToList();
        }

        public IActionResult Create()
        {
            return View(new Employee { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.Create(model);
                _notify.Success($"{model.EmployeeName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var managers = await _appUserRepository.GetUserByGroup(2);
            managers.Insert(0, new AppUserViewModel { UserId="",UserName="ไม่ระบุ"});
            List<SelectListItem> selects = new();
            foreach (var item in managers)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.UserName,
                    Value = item.UserId,
                });
            }
            ViewBag.Managers = selects;

            var model = await _employeeRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Employee model)
        {
            var oldEntity = await _employeeRepository.GetByKey(id);
            if (oldEntity == null)
            {
                return NotFound();
            }
            //if (string.IsNullOrEmpty(model.ManagerCode))
            //{
            //    ModelState.AddModelError("ManagerCode", "กรุณาเลือก ผู้จัดการ");
            //}
            if (ModelState.IsValid)
            {
                await _employeeRepository.Update(id, model);
                var appUser = new AppUser
                {
                    UserId=model.EmployeeCode,
                    UserName = model.EmployeeName,
                    Email = model!.Email,
                    PhoneNumber = model!.PhoneNumber,
                    IsActive = model.IsActive
                };
                await _appUserRepository.Update(id, appUser);
                _notify.Success($"{model.EmployeeName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            var managers = await _appUserRepository.GetUserByGroup(2);
            managers.Insert(0, new AppUserViewModel { UserId = "", UserName = "ไม่ระบุ" });
            List<SelectListItem> selects = new();
            foreach (var item in managers)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.UserName,
                    Value = item.UserId,
                });
            }
            ViewBag.Managers = selects;
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
               
                await _employeeRepository.Delete(id);
                //_notify.Success($"Delete is Success.");
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

