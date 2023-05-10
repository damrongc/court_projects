using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
  
    public class LoanSubTaskStatusesController : BaseController<LoanSubTaskStatusesController>
    {
        private readonly ILoanSubTaskStatusRepository _loanSubTaskStatusRepository;
        private readonly ILoanTaskStatusRepository _loanTaskStatusRepository;

        public LoanSubTaskStatusesController(ILoanSubTaskStatusRepository loanSubTaskStatusRepository,
            ILoanTaskStatusRepository loanTaskStatusRepository)
        {
            _loanSubTaskStatusRepository = loanSubTaskStatusRepository;
            _loanTaskStatusRepository = loanTaskStatusRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<LoanSubTaskStatus>> GetAll()
        {
            var results = await _loanSubTaskStatusRepository.GetAll();
            return results.ToList();
        }


        public async Task<IActionResult> Create()
        {
            var loanTaskStatus = await _loanTaskStatusRepository.GetAll();
            List<SelectListItem> SelectloanTaskStatus  = new();
            foreach (var item in loanTaskStatus)
            {
                SelectloanTaskStatus.Add(new SelectListItem
                {
                    Text = item.LoanTaskStatusName.ToString(),
                    Value = item.LoanTaskStatusId.ToString(),
                });
            }
            ViewBag.loanTaskStatus = SelectloanTaskStatus;

            return View(new LoanSubTaskStatus());
        }


       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanSubTaskStatus model)
        {
            if (ModelState.IsValid)
            {
                await _loanSubTaskStatusRepository.Create(model);
                _notify.Success($"{model.LoanSubTaskStatusName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var loanTaskStatus = await _loanTaskStatusRepository.GetAll();
            List<SelectListItem> SelectloanTaskStatus = new();
            foreach (var item in loanTaskStatus)
            {
                SelectloanTaskStatus.Add(new SelectListItem
                {
                    Text = item.LoanTaskStatusName.ToString(),
                    Value = item.LoanTaskStatusId.ToString(),
                });
            }

            ViewBag.loanTaskStatus = SelectloanTaskStatus;

            var model = await _loanSubTaskStatusRepository.GetByKey(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoanSubTaskStatus model)
        {
            var oldEntity = await _loanSubTaskStatusRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _loanSubTaskStatusRepository.Update(id, model);
                _notify.Success($"{model.LoanSubTaskStatusName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {


                await _loanSubTaskStatusRepository.Delete(id);
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

