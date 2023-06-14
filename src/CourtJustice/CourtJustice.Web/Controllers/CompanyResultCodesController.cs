using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
   
    public class CompanyResultCodesController : BaseController<ICompanyResultCodeRepository>
    {
        private readonly ICompanyResultCodeRepository _companyResultCodeRepository;

        public CompanyResultCodesController(ICompanyResultCodeRepository companyResultCodeRepository)
        {
            _companyResultCodeRepository = companyResultCodeRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<CompanyResultCode>> GetAll()
        {
            var results = await _companyResultCodeRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new CompanyResultCode());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyResultCode model)
        {
            if (ModelState.IsValid)
            {
                await _companyResultCodeRepository.Create(model);
                _notify.Success($"{model.CompanyResultCodeName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _companyResultCodeRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, CompanyResultCode model)
        {
            var oldEntity = await _companyResultCodeRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _companyResultCodeRepository.Update(id, model);
                _notify.Success($"{model.CompanyResultCodeName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {


                await _companyResultCodeRepository.Delete(id);
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

