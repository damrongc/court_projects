using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
    
    public class LawOfficesController : BaseController<LawOfficesController>
    {
        private readonly ILawOfficeRepository _lawOfficeRepository;

        public LawOfficesController(ILawOfficeRepository lawOfficeRepository)
        {
            _lawOfficeRepository = lawOfficeRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        private async Task<List<LawOffice>> GetAll()
        {
            var results = await _lawOfficeRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new LawOffice());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LawOffice model)
        {
            if (ModelState.IsValid)
            {
                await _lawOfficeRepository.Create(model);
                _notify.Success($"{model.LawOfficeName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _lawOfficeRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, LawOffice model)
        {
            var oldEntity = await _lawOfficeRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _lawOfficeRepository.Update(id, model);
                _notify.Success($"{model.LawOfficeName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {


                await _lawOfficeRepository.Delete(id);
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

