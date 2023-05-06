
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Web.ViewComponents
{
    public class NavigateViewComponent : ViewComponent
    {
        private readonly IMenuRepository _repo ;
        public NavigateViewComponent(IMenuRepository repo)
        {

            _repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return View(Enumerable.Empty<AppProgram>());
            //}
            //var username = User.Identity.Name;
            var user = SessionHelper.GetObjectFromJson<AppUserViewModel>(HttpContext.Session, "userObject");

            //#if DEBUG
            //            username = "admin";
            //#endif

            //            var user = _context.AppUsers.FirstOrDefault(m => m.UserName == username);

            //            var userPermissions = _context.UserPermissions.Where(m => m.GroupId == user.GroupId);
            //            var appPrograms = new List<AppProgram>();
            var programs = await _repo.GetMenu(user.GroupId);
            //foreach (var item in userPermissions)
            //{
            //    var appProgram = appProgramsAll.Find(o => o.ProgramId == item.ProgramId);
            //    appPrograms.Add(appProgram);
            //}
            var menuDisplay = new Tuple<AppUserViewModel, IEnumerable<AppProgram>>(user, programs);
            return View(menuDisplay);
      
        }
    }
}
