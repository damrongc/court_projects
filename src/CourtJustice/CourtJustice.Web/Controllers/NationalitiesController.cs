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
   
    public class NationalitiesController : BaseController<NationalitiesController>
    {
        private readonly INationalityRepository _nationalityRepository;

        public NationalitiesController(INationalityRepository nationalityRepository)
        {
            _nationalityRepository = nationalityRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<Nationality>> GetAll()
        {
            var results = await _nationalityRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new Nationality());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nationality model)
        {
            if (ModelState.IsValid)
            {
                await _nationalityRepository.Create(model);
                _notify.Success($"{model.NationalityName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _nationalityRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Nationality model)
        {
            var oldEntity = await _nationalityRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _nationalityRepository.Update(id, model);
                _notify.Success($"{model.NationalityName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {


                await _nationalityRepository.Delete(id);
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

