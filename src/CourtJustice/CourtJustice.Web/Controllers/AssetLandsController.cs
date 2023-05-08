using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
 
    public class AssetLandsController : BaseController<AssetLandsController>
    {
        private readonly IAssetLandRepository _assetLandRepository;
        private readonly ILandOfficeRepository _landOfficeRepository;

        public AssetLandsController(IAssetLandRepository assetLandRepository,
            ILandOfficeRepository landOfficeRepository)
        {
            _assetLandRepository = assetLandRepository;
            _landOfficeRepository = landOfficeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }



        private async Task<List<AssetLand>> GetAll()
        {
            var results = await _assetLandRepository.GetAll();
            return results.ToList();
        }

        public async Task<IActionResult> Create()
        {
            var landOffices = await _landOfficeRepository.GetAll();
            List<SelectListItem> SelectLandOffice = new();
            foreach (var item in landOffices)
            {
                SelectLandOffice.Add(new SelectListItem
                {
                    Text = item.LandOfficeName.ToString(),
                    Value = item.LandOfficeCode.ToString(),
                });
            }
            ViewBag.LandOffices = SelectLandOffice;
            return View(new AssetLand());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssetLand model)
        {
            if (ModelState.IsValid)
            {
                await _assetLandRepository.Create(model);
                _notify.Success($"{model.AssetLandId} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _assetLandRepository.GetByKey(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssetLand model)
        {
            var oldEntity = await _assetLandRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _assetLandRepository.Update(id, model);
                _notify.Success($"{model.AssetLandId} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                await _assetLandRepository.Delete(id);
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
        [HttpGet]
        public IActionResult GetAssetLandByCusId(string id)
        {
            //var assetLands = await _assetLandRepository.GetByKey(id);
            var assetLands = new List<AssetLandViewModel>
            {
                new AssetLandViewModel
                {
                    AssetLandId = "01",
                    Position = "asas",
                    EstimatePrice = 200000

                }
            };
            return PartialView("~/Views/AssetLands/_AssetLandCard.cshtml", assetLands);
            //var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetLandCard", assetLands);
            //return new JsonResult(new { isValid = true, message = "", html });
        }



        [HttpPost]
        public async Task<IActionResult> GetWithPaging()
        {
            try
            {
                //var productGroupCode = Request.Form["productGroupCode"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = await _assetLandRepository.GetRecordCount(searchValue);

                var data = await _assetLandRepository.GetPaging(skip, pageSize, searchValue);
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch
            {
                throw;
            }
        }

    }
}

