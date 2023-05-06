using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourtJustice.Web.Controllers
{
    public class TitlesController : BaseController<TitlesController>
    {
        private readonly ITitleRepository _titleRepository;

        public TitlesController(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        private async Task<List<Title>> GetAll()
        {
            var results = await _titleRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new Title());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Title model)
        {
         
            if (ModelState.IsValid)
            {
                if (_titleRepository.IsExisting(model.TitleCode))
                {
                    ModelState.AddModelError("TitleCode", "รหัสคำนำหน้า มีอยู่แล้วในระบบ!");
                    return View(model);
                }

                await _titleRepository.Create(model);
                _notify.Success($"{model.TitleName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _titleRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Title model)
        {
            var oldEntity = await _titleRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _titleRepository.Update(id, model);
                _notify.Success($"{model.TitleName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                //var count = await _titleRepository.CheckExistingAtUser(id);
                //if (count > 0)
                //{
                //    return new JsonResult(new { isValid = false, message = "ไม่สามารถลบข้อมูลได้ \r\nเนื่องจากมีการใช้งานแล้ว!" });
                //}
                await _titleRepository.Delete(id);
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
