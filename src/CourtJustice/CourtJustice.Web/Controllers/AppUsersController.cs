using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;




namespace CourtJustice.Web.Controllers
{
   
    public class AppUsersController : BaseController<AppUsersController>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IGroupUserRepository _groupUserRepository;

        public AppUsersController(IAppUserRepository appUserRepository, 
            IGroupUserRepository groupUserRepository)
        {
            _appUserRepository = appUserRepository;
            _groupUserRepository = groupUserRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        private async Task<List<AppUser>> GetAll()
        {
            var result = await _appUserRepository.GetAll();
            return result.ToList();
        }

        public async Task<IActionResult> Create()
        {
            var appUser = new AppUser
            {
                IsActive = true
            };
            var groups = await _groupUserRepository.GetAll();
            groups.Insert(0, new GroupUser
            {
                GroupId = 0,
                GroupName = "==เลือกข้อมูล=="

            });
            ViewBag.GroupUsers = new SelectList(groups, nameof(GroupUser.GroupId), nameof(GroupUser.GroupName), null, null);


            return View(appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                if (!_appUserRepository.IsExisting(appUser.UserId))
                {

                    var currentUsername = User?.FindFirst(ClaimTypes.NameIdentifier) == null ?
                      "Anonymous"
                      : User?.FindFirst(ClaimTypes.NameIdentifier).Value;


                    appUser.Password = SecurityHelper.EncryptText(appUser.Password);
                    appUser.UserCreated = currentUsername!;
                    appUser.CreatedDateTime = DateTime.Now;
                    await _appUserRepository.Create(appUser);
                    _notify.Success($"User: {appUser.UserName} is Created");
                }
                else
                {
                    _notify.Error("User Id is already exist!");
                }

                return RedirectToAction(nameof(Index));
            }
            var groups = await _groupUserRepository.GetAll();
            groups.Insert(0, new GroupUser
            {
                GroupId = 0,
                GroupName = "==เลือกข้อมูล=="

            });
            ViewBag.GroupUsers = new SelectList(groups, nameof(GroupUser.GroupId), nameof(GroupUser.GroupName), null, null);
            return View(appUser);
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
            return View(appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, AppUser appUser)
        {
            var oldEntity = await _appUserRepository.GetByKey(id);
            if (id != appUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUsername = User?.FindFirst(ClaimTypes.NameIdentifier) == null ?
                      "Anonymous"
                      : User?.FindFirst(ClaimTypes.NameIdentifier).Value;
                    //appUser.Password = SecurityHelper.EncryptText(appUser.Password);
                    appUser.UserUpdated = currentUsername;
                    appUser.UpdatedDateTime = DateTime.Now;

                    //_context.Entry(oldEntity).CurrentValues.SetValues(appUser);
                    //await _context.SaveChangesAsync(currentUsername);

                    await _appUserRepository.Update(id, appUser);
                    _notify.Success($"User: {appUser.UserName} is Updated");
                    //_context.Update(appUser);
                    //await _context.SaveChangesAsync();
                }
                catch
                {
                    return BadRequest();
                }
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!AppUserExists(appUser.UserId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }
            var groups = await _groupUserRepository.GetAll();
            ViewBag.GroupUsers = new SelectList(groups, nameof(GroupUser.GroupId), nameof(GroupUser.GroupName), appUser.GroupId, null);
            return View(appUser);
        }
    }
}

