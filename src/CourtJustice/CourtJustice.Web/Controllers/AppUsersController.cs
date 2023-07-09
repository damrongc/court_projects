using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CourtJustice.Web.Controllers
{
   
    public class AppUsersController : BaseController<AppUsersController>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IGroupUserRepository _groupUserRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILawyerRepository _lawyerRepositor;
        private readonly IEmployerRepository _employerRepository;
        private readonly IUserEmployerMappingRepository _userEmployerMappingRepository;

        public AppUsersController(IAppUserRepository appUserRepository,
            IGroupUserRepository groupUserRepository,
            IEmployeeRepository employeeRepository,
            ILawyerRepository lawyerRepositor,
            IEmployerRepository employerRepository,
            IUserEmployerMappingRepository userEmployerMappingRepository)
        {
            _appUserRepository = appUserRepository;
            _groupUserRepository = groupUserRepository;
            _employeeRepository = employeeRepository;
            _lawyerRepositor = lawyerRepositor;
            _employerRepository = employerRepository;
            _userEmployerMappingRepository = userEmployerMappingRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        private async Task<List<AppUserViewModel>> GetAll()
        {
            var result = await _appUserRepository.GetAll();
            return result.ToList();
        }

        public async Task<IActionResult> Create()
        {
            var appUser = new AppUserViewModel
            {
                IsActive = true
            };
            var groups = await _groupUserRepository.GetAll();
            groups.Insert(0, new GroupUserViewModel{GroupId = 0,GroupName = "==เลือกข้อมูล=="});
            ViewBag.GroupUsers = new SelectList(groups, nameof(GroupUser.GroupId), nameof(GroupUser.GroupName), null, null);

            var employers = await _employerRepository.GetAll();
            employers.Insert(0, new Employer { EmployerCode = "", EmployerName = "==เลือกข้อมูล==" });
            ViewBag.Employers = new SelectList(employers, nameof(Employer.EmployerCode), nameof(Employer.EmployerName), null, null);

            var managers = await _appUserRepository.GetActiveUser();
            managers.Insert(0, new AppUser { UserId = "", UserName = "==เลือกข้อมูล==" });
            ViewBag.Managers = new SelectList(managers, nameof(AppUserViewModel.UserId), nameof(AppUserViewModel.UserName), null, null);


            //List<SelectListItem> selects = new();
            //foreach (var item in employers)
            //{
            //    selects.Add(new SelectListItem
            //    {
            //        Text = $"{item.EmployerName}",
            //        Value = item.EmployerCode.ToString(),
            //    });
            //}
            //ViewBag.Employers = selects;

            return View(appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUserViewModel appUser)
        {
            if (!_appUserRepository.IsExisting(appUser.UserId))
            {
                //var user = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
                //var currentUsername = User?.FindFirst(ClaimTypes.NameIdentifier) == null ?
                //  "Anonymous"
                //  : User?.FindFirst(ClaimTypes.NameIdentifier).Value;

                //appUser.Password = SecurityHelper.EncryptText(appUser.Password);
                //appUser.UserCreated = currentUsername!;
                //appUser.CreatedDateTime = DateTime.Now;

                var model = new AppUser
                {
                    UserId = appUser.UserId,
                    UserName = appUser.UserName,
                    Password = SecurityHelper.EncryptText(appUser.Password),
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber,
                    GroupId= appUser.GroupId,
                    IsActive = appUser.IsActive,
                    Target =appUser.Target,
                    ManagerId= appUser.ManagerId,
                    //CreatedDateTime = DateTime.Now,
                    //UserCreated = user.UserId
                };

                await _appUserRepository.Create(model);

                //switch (appUser.GroupId)
                //{
                //    case 3:
                //        var lawyer = new Lawyer
                //        {
                //            LawyerCode = appUser.UserId,
                //            LawyerName = appUser.UserName,
                //            Email = appUser.Email,
                //            PhoneNumber = appUser.PhoneNumber,
                //            IsActive = appUser.IsActive,

                //        };
                //        await _lawyerRepositor.Create(lawyer);
                //        break;
                //    case 4:
                //        var employee = new Employee
                //        {
                //            EmployeeCode = appUser.UserId,
                //            EmployeeName = appUser.UserName,
                //            Email = appUser.Email,
                //            PhoneNumber = appUser.PhoneNumber,
                //            //HireDate   =DateOnly.FromDateTime(DateTime.Today),
                //            Target = 0,
                //            IsActive = appUser.IsActive,
                //        };
                //        await _employeeRepository.Create(employee);
                //        break;
                //}

                if (appUser.GroupId != 1)
                {
                    var mapping = new UserEmployerMapping
                    {
                        UserId = appUser.UserId,
                        EmployerCode = appUser.EmployerCode
                    };

                    await _userEmployerMappingRepository.Create(mapping);
                }

        
                _notify.Success($"User: {appUser.UserName} is Created");
            }
            else
            {
                _notify.Error("User Id is already exist!");
            }
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{

            //}
            //var groups = await _groupUserRepository.GetAll();
            //groups.Insert(0, new GroupUserViewModel
            //{
            //    GroupId = 0,
            //    GroupName = "==เลือกข้อมูล=="

            //});
            //ViewBag.GroupUsers = new SelectList(groups, nameof(GroupUser.GroupId), nameof(GroupUser.GroupName), null, null);

            //var employers = await _employerRepository.GetAll();
            //employers.Insert(0, new Employer { EmployerCode = "", EmployerName = "==เลือกข้อมูล==" });
            //ViewBag.Employers = new SelectList(employers, nameof(Employer.EmployerCode), nameof(Employer.EmployerName), null, null);


            //return View(appUser);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var appUser = await _appUserRepository.GetByKey(id);

            if (appUser == null)
            {
                return NotFound();
            }
            var groups = await _groupUserRepository.GetAll();
            ViewBag.GroupUsers = new SelectList(groups, nameof(GroupUser.GroupId), nameof(GroupUser.GroupName), appUser.GroupId, null);

            var employerCode = "";
            if (appUser.GroupId != 1)
            {
                var mappings = _userEmployerMappingRepository.GetByUser(appUser.UserId);

                if (mappings != null)
                {
                    employerCode = mappings[0].EmployerCode;
                }
            }
         
            var employers = await _employerRepository.GetAll();
            employers.Insert(0, new Employer { EmployerCode = "", EmployerName = "==เลือกข้อมูล==" });
            ViewBag.Employers = new SelectList(employers, nameof(Employer.EmployerCode), nameof(Employer.EmployerName), employerCode, null);

            var managers = await _appUserRepository.GetActiveUser();
            managers.Insert(0, new AppUser { UserId = "", UserName = "==เลือกข้อมูล==" });
            ViewBag.Managers = new SelectList(managers, nameof(AppUserViewModel.UserId), nameof(AppUserViewModel.UserName), appUser.ManagerId, null);

            //var viewModel = new AppUserViewModel
            //{
            //    UserId = appUser.UserId,
            //    UserName = appUser.UserName,
            //    Email = appUser.Email,
            //    PhoneNumber = appUser.PhoneNumber,
            //    IsActive = appUser.IsActive,
            //};

            return View(appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, AppUserViewModel appUser)
        {
            var oldEntity = await _appUserRepository.GetByKey(id);
            if (id != appUser.UserId)
            {
                return NotFound();
            }
            try
            {
                var model = new AppUser
                {
                    UserId = appUser.UserId,
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber,
                    IsActive = appUser.IsActive,
                    GroupId= appUser.GroupId,
                    Target =appUser.Target,
                    ManagerId= appUser.ManagerId,
                };
                await _appUserRepository.Update(id, model);

                if (!string.IsNullOrEmpty(appUser.EmployerCode))
                {
                    _userEmployerMappingRepository.DeleteByUser(appUser.UserId);
                    var mapping = new UserEmployerMapping
                    {
                        UserId = appUser.UserId,
                        EmployerCode = appUser.EmployerCode
                    };

                    await _userEmployerMappingRepository.Create(mapping);
                }
 

                _notify.Success($"User: {appUser.UserName} is Updated");
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{

            //}
            //var groups = await _groupUserRepository.GetAll();
            //ViewBag.GroupUsers = new SelectList(groups, nameof(GroupUser.GroupId), nameof(GroupUser.GroupName), appUser.GroupId, null);
            //return View(appUser);
        }

        public async Task<IActionResult> ChangePasswordPopup()
        {
            var user = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");

            var appUser = await _appUserRepository.GetByKey(user.UserId); 

            if (appUser == null)
            {
                return NotFound();
            }
            var model = new AppUserViewModel
            {
                UserId = appUser.UserId,
                UserName = appUser.UserName

            };
            TempData["Error"] = string.Empty;
            return View(model);
        }


        [HttpPost]
        public async Task<JsonResult> ConfirmChangePassword([FromBody] AppUserViewModel model)
        {
            await _appUserRepository.ChangePassword(model);
            //var oldEntity = await _appUserRepository.GetByKey(model.UserId);
            //var appUser = SessionHelper.GetObjectFromJson<AppUser>(HttpContext.Session, "userObject");
            //oldEntity.Password = SecurityHelper.EncryptText(model.NewPassword);
            //oldEntity.LastModifiedBy = appUser.UserName;
            //oldEntity.LastModifiedOn = DateTime.Now;

            //await _context.SaveChangesAsync(appUser.UserName);

            return new JsonResult(new { isValid = true });

        }
    }
}

