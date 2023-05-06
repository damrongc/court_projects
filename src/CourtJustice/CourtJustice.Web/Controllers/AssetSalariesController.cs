using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class AssetSalariesController : BaseController<AssetSalariesController>
    {
        private readonly IAssetSalaryRepository _assetSalaryRepository;

        public AssetSalariesController(IAssetSalaryRepository assetSalaryRepository)
        {
            _assetSalaryRepository = assetSalaryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        private async Task<List<AssetSalary>> GetAll()
        {
            var results = await _assetSalaryRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new AssetSalary() );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssetSalary model)
        {
            if (ModelState.IsValid)
            {
                await _assetSalaryRepository.Create(model);
                _notify.Success($"{model.AssetId} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _assetSalaryRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssetSalary model)
        {
            var oldEntity = await _assetSalaryRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _assetSalaryRepository.Update(id, model);
                _notify.Success($"{model.AssetId} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
              
                await _assetSalaryRepository.Delete(id);
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

