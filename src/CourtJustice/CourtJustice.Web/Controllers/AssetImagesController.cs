﻿using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class AssetImagesController : BaseController<AssetImagesController>
    {

        private readonly IAssetImageRepository _assetImageRepository;

        public AssetImagesController(IAssetImageRepository assetImageRepository)
        {
            _assetImageRepository = assetImageRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<AssetImage>> GetAll()
        {
            var results = await _assetImageRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new AssetImage());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssetImage model)
        {
            if (ModelState.IsValid)
            {
                await _assetImageRepository.Create(model);
                _notify.Success($"{model.ImageId} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _assetImageRepository.GetByKey(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssetImage model)
        {
            var oldEntity = await _assetImageRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _assetImageRepository.Update(id, model);
                _notify.Success($"{model.ImageId} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                await _assetImageRepository.Delete(id);
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

