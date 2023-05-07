﻿using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class AssetCarsController : BaseController<AssetCarsController>
    {
        private readonly IAssetCarRepository _assetCarRepository;
        private readonly ICarTypeRepository _carTypeRepository;

        public AssetCarsController(IAssetCarRepository assetCarRepository,ICarTypeRepository carTypeRepository)
        {
            _assetCarRepository = assetCarRepository;
            _carTypeRepository = carTypeRepository;
        }


        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        private async Task<List<AssetCar>> GetAll()
        {
            var results = await _assetCarRepository.GetAll();
            return results.ToList();
        }

        public async Task<IActionResult> Create()
        {
            var cartype = await _carTypeRepository.GetAll();
            List<SelectListItem> SelectCarType = new();
            foreach (var item in cartype)
            {
                SelectCarType.Add(new SelectListItem
                {
                    Text = item.CarTypeName.ToString(),
                    Value = item.CarTypeCode.ToString(),
                });
            }
            ViewBag.CarTypes = SelectCarType;

            return View(new AssetCar());
        }




    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssetCar model)
        {
            if (ModelState.IsValid)
            {
                await _assetCarRepository.Create(model);
                _notify.Success($"{model.ChassisNumber} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _assetCarRepository.GetByKey(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssetCar model)
        {
            var oldEntity = await _assetCarRepository.GetByKey(model.ChassisNumber);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _assetCarRepository.Update(id, model);
                _notify.Success($"{model.ChassisNumber} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {

                await _assetCarRepository.Delete(id);
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

