using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class CompanyActionCodesController : BaseController<ICompanyActionCodeRepository>
    {
        private readonly ICompanyActionCodeRepository _companyActionCodeRepository;

        public CompanyActionCodesController(ICompanyActionCodeRepository companyActionCodeRepository)
        {
            _companyActionCodeRepository = companyActionCodeRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<CompanyActionCode>> GetAll()
        {
            var results = await _companyActionCodeRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new CompanyActionCode());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyActionCode model)
        {
            if (ModelState.IsValid)
            {
                await _companyActionCodeRepository.Create(model);
                _notify.Success($"{model.CompanyActionName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _companyActionCodeRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, CompanyActionCode model)
        {
            var oldEntity = await _companyActionCodeRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _companyActionCodeRepository.Update(id, model);
                _notify.Success($"{model.CompanyActionName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {


                await _companyActionCodeRepository.Delete(id);
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

