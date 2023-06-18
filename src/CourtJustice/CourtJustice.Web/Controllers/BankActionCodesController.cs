﻿using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers 
{
   
    public class BankActionCodesController : BaseController<IBankActionCodeRepository>
    {
        private readonly IBankActionCodeRepository _bankActionCodeRepository;
        private readonly IEmployerRepository _employerRepository;

        public BankActionCodesController(IBankActionCodeRepository bankActionCodeRepository,
            IEmployerRepository employerRepository)
        {
            _bankActionCodeRepository = bankActionCodeRepository;
            _employerRepository = employerRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<BankActionCode>> GetAll()
        {
            var results = await _bankActionCodeRepository.GetAll();
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

            return View(new BankActionCode());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankActionCode model)
        {
            if (ModelState.IsValid)
            {
                await _bankActionCodeRepository.Create(model);
                _notify.Success($"{model.BankActionCodeName} is Created.");
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

            var model = await _bankActionCodeRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BankActionCode model)
        {
            var oldEntity = await _bankActionCodeRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bankActionCodeRepository.Update(id, model);
                _notify.Success($"{model.BankActionCodeName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {


                await _bankActionCodeRepository.Delete(id);
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
