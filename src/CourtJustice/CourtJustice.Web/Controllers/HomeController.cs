using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Web.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace CourtJustice.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppUserRepository _appUserRepository;
        private readonly ILawyerRepository _lawyerRepository;


        public HomeController(ILawyerRepository lawyerRepository, IHttpContextAccessor httpContextAccessor, IAppUserRepository appUserRepository)
        {
            _lawyerRepository = lawyerRepository;
            _httpContextAccessor = httpContextAccessor;
            _appUserRepository = appUserRepository;
        }

        [ServiceFilter(typeof(RequestAuthenticationFilter))]
        public IActionResult Index()
        {
            return View();
        }
        //public async Task< IActionResult> Index()
        //{
        //    var model = new Lawyer { LawyerName = "LawyerName" ,IsActive=true};
        //    await _lawyerRepository.Create(model);
        //    return View();
        //}

        public IActionResult Login()
        {
            //ViewBag.AppLogo = _context.Companys.FirstOrDefault().ImageName;

            var appUser = new AppUser();
            //if (_currentEnvironment.IsDevelopment())
            //{
            //    appUser.UserId = "admin";
            //    appUser.Password= "password@1";
            //}
            return View(appUser);
        }

        [HttpPost()]
        public async Task<IActionResult> Signin(AppUser user)
        {

            //var appUser = await _repository.Authentication(user.id, user.password);

            //if (appUser == null)
            //{
            //    TempData["Error"] = "Authentication Failure!";
            //    return RedirectToAction(nameof(Login));

            //}
            var appUser = await _appUserRepository.Authentication(user.UserId, user.Password);
            if (appUser == null)
            {
                TempData["Error"] = "Authentication Failure!";
                return RedirectToAction(nameof(Login));
            }

            //var appUser = new AppUser {UserId="damrong",  UserName = user.UserName };
            SessionHelper.SetObjectAsJson(HttpContext.Session, "userObject", appUser);
            _httpContextAccessor.HttpContext?.Session.SetString("UserId", user.UserId);

            //var model = new Lawyer { LawyerName = "LawyerName", IsActive = true };
            //await _lawyerRepository.Create(model);
            return RedirectToAction( nameof(Index),"Dashboards");


        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

    }
}