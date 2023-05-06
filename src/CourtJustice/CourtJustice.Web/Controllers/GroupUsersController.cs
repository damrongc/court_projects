using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourtJustice.Web.Controllers
{
    public class GroupUsersController : BaseController<GroupUsersController>
    {
        private readonly IGroupUserRepository _groupUserRepository;

        public GroupUsersController(IGroupUserRepository groupUserRepository)
        {
            _groupUserRepository = groupUserRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<GroupUser>> GetAll()
        {
            var results = await _groupUserRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new GroupUser { IsActive=true});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupUser model)
        {
            if (ModelState.IsValid)
            {
                await _groupUserRepository.Create(model);
                _notify.Success($"{model.GroupName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _groupUserRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GroupUser model)
        {
            var oldEntity = await _groupUserRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _groupUserRepository.Update(id, model);
                _notify.Success($"{model.GroupName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var count = await _groupUserRepository.CheckExistingAtUser(id);
                if (count > 0)
                {
                    return new JsonResult(new { isValid = false, message = "ไม่สามารถลบข้อมูลได้ \r\nเนื่องจากมีการใช้งานแล้ว!" });
                }
                await _groupUserRepository.Delete(id);
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
