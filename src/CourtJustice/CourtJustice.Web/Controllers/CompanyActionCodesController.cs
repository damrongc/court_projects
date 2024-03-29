﻿using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourtJustice.Web.Controllers
{
    public class CompanyActionCodesController : BaseController<CompanyActionCodesController>
    {
        private readonly ICompanyActionCodeRepository _companyActionCodeRepository;
        private readonly ICompanyResultCodeRepository _companyResultCodeRepository;
        private readonly ICompanyRepository _companyRepository;


        public CompanyActionCodesController(ICompanyActionCodeRepository companyActionCodeRepository,
            ICompanyRepository companyRepository,
            ICompanyResultCodeRepository companyResultCodeRepository)
        {
            _companyRepository = companyRepository;
            _companyActionCodeRepository = companyActionCodeRepository;
            _companyResultCodeRepository = companyResultCodeRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        [HttpGet]
        public async Task<JsonResult> GetAllCompanyAction()
        {
            var results = await GetAll();
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
            return new JsonResult(new { isValid = true ,html});
        }

        private async Task<List<CompanyActionCodeViewModel>> GetAll()
        {
            var companyActionCodes = await _companyActionCodeRepository.GetAll();

            foreach (var companyAction in companyActionCodes)
            {
                var companyResultCodes = await _companyResultCodeRepository.GetByCompanyActionId(companyAction.CompanyActionId);
                companyAction.CompanyResultCodes = companyResultCodes;
            }
            return companyActionCodes.ToList();
        }

        public async Task<IActionResult> Create()
        {

            var companies = await _companyRepository.GetAll();
            List<SelectListItem> selects = new();
            foreach (var item in companies)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.CompanyName.ToString(),
                    Value = item.CompanyId.ToString(),
                });
            }
            ViewBag.Companies = selects;
            return View(new CompanyActionCode());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyActionCode model)
        {
            if (ModelState.IsValid)
            {
                await _companyActionCodeRepository.Create(model);
                _notify.Success($"{model.CompanyActionCodeName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            var companies = await _companyRepository.GetAll();
            List<SelectListItem> selects = new();
            foreach (var item in companies)
            {
                selects.Add(new SelectListItem
                {
                    Text = item.CompanyName.ToString(),
                    Value = item.CompanyId.ToString(),
                });
            }
            ViewBag.Companies = selects;
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _companyActionCodeRepository.GetByKey(id);

            var companies = await _companyRepository.GetAll();
            List<SelectListItem> selects = new();
            foreach (var item in companies)
            {
                selects.Add(new SelectListItem
                {
                    Selected =item.CompanyId==model.CompanyId,
                    Text = item.CompanyName.ToString(),
                    Value = item.CompanyId.ToString(),
                });
            }
            ViewBag.Companies = selects;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyActionCode model)
        {
            var oldEntity = await _companyActionCodeRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _companyActionCodeRepository.Update(id, model);
                _notify.Success($"{model.CompanyActionCodeName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            var companies = await _companyRepository.GetAll();
            List<SelectListItem> selects = new();
            foreach (var item in companies)
            {
                selects.Add(new SelectListItem
                {
                    Selected = item.CompanyId == oldEntity.CompanyId,
                    Text = item.CompanyName.ToString(),
                    Value = item.CompanyId.ToString(),
                });
            }
            ViewBag.Companies = selects;
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var isHaveActionCode = _companyResultCodeRepository.IsHaveActionCode(id);
                if (isHaveActionCode)
                {
                    throw new Exception("ไม่สามารถลบข้อมูลได้ เนื่องจากมี Result Code!");
                }

                await _companyActionCodeRepository.Delete(id);
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

