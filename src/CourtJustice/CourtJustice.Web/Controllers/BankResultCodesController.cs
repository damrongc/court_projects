using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
   
    public class BankResultCodesController : BaseController<IBankResultCodeRepository>
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IBankResultCodeRepository _bankResultCodeRepository;

        public BankResultCodesController(IBankResultCodeRepository bankResultCodeRepository, IEmployerRepository employerRepository)
        {
            _bankResultCodeRepository = bankResultCodeRepository;
            _employerRepository = employerRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<BankResultCode>> GetAll()
        {
            var results = await _bankResultCodeRepository.GetAll();
            return results.ToList();
        }

        public async Task<IActionResult> Create()
        {
            var employers = await _employerRepository.GetAll();
            List<SelectListItem> selects = new();
            foreach (var item in employers)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.EmployerName.ToString(),
                    Value = item.EmployerCode.ToString(),
                });
            }
            ViewBag.Employers = selects;
            return View(new BankResultCode());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankResultCode model)
        {
            if (ModelState.IsValid)
            {
                await _bankResultCodeRepository.Create(model);
                _notify.Success($"{model.BankResultCodeName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var employers = await _employerRepository.GetAll();
            List<SelectListItem> selects = new();
            foreach (var item in employers)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.EmployerName.ToString(),
                    Value = item.EmployerCode.ToString(),
                });
            }
            ViewBag.Employers = selects;

            var model = await _bankResultCodeRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BankResultCode model)
        {
            var oldEntity = await _bankResultCodeRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bankResultCodeRepository.Update(id, model);
                _notify.Success($"{model.BankResultCodeName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {


                await _bankResultCodeRepository.Delete(id);
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

