using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
   
    public class LoanTaskStatusesController : BaseController<LoanTaskStatusesController>
    {
        private readonly ILoanTaskStatusRepository _loanTaskStatusRepository;

        public LoanTaskStatusesController(ILoanTaskStatusRepository loanTaskStatusRepository)
        {
            _loanTaskStatusRepository = loanTaskStatusRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        private async Task<List<LoanTaskStatus>> GetAll()
        {
            var results = await _loanTaskStatusRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new LoanTaskStatus());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanTaskStatus model)
        {
            if (ModelState.IsValid)
            {
                await _loanTaskStatusRepository.Create(model);
                _notify.Success($"{model.LoanTaskStatusName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _loanTaskStatusRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoanTaskStatus model)
        {
            var oldEntity = await _loanTaskStatusRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _loanTaskStatusRepository.Update(id, model);
                _notify.Success($"{model.LoanTaskStatusName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                var count = await _loanTaskStatusRepository.CheckExistingAtSub(id);
                if (count > 0)
                {
                    return new JsonResult(new { isValid = false, message = "ไม่สามารถลบข้อมูลได้ \r\nเนื่องจากมีการใช้งานแล้ว!" });
                }


                await _loanTaskStatusRepository.Delete(id);
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

