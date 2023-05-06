using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
   
    public class LawyersController : BaseController<LawyersController>
    {
        private readonly ILawyerRepository _lawyerRepository;

        public LawyersController(ILawyerRepository lawyerRepository)
        {
            _lawyerRepository = lawyerRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        private async Task<List<Lawyer>> GetAll()
        {
            var results = await _lawyerRepository.GetAll();
            return results.ToList();
        }


        public IActionResult Create()
        {
           
            return View(new Lawyer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lawyer model)
        {
            if (ModelState.IsValid)
            {
                await _lawyerRepository.Create(model);
                _notify.Success($"{model.LawyerName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _lawyerRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lawyer model)
        {
            var oldEntity = await _lawyerRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _lawyerRepository.Update(id, model);
                _notify.Success($"{model.LawyerName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
               // var count = await _groupUserRepository.CheckExistingAtUser(id);
               // if (count > 0)
              //  {
                   // return new JsonResult(new { isValid = false, message = "ไม่สามารถลบข้อมูลได้ \r\nเนื่องจากมีการใช้งานแล้ว!" });
              //  }
                await _lawyerRepository.Delete(id);
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

