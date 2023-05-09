using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class LoanTypesController : BaseController<LoanTypesController>
    {
        private readonly ILoanTypeRepository _loanTypeRepository;

        public LoanTypesController(ILoanTypeRepository loanTypeRepository)
        {
            _loanTypeRepository = loanTypeRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<LoanType>> GetAll()
        {
            var results = await _loanTypeRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new LoanType());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanType model)
        {
            if (ModelState.IsValid)
            {
                await _loanTypeRepository.Create(model);
                _notify.Success($"{model.LoanTypeName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _loanTypeRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, LoanType model)
        {
            var oldEntity = await _loanTypeRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _loanTypeRepository.Update(id, model);
                _notify.Success($"{model.LoanTypeName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {


                await _loanTypeRepository.Delete(id);
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

