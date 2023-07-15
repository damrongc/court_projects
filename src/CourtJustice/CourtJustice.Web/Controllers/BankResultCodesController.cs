using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourtJustice.Web.Controllers
{
   
    public class BankResultCodesController : BaseController<BankResultCodesController>
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IBankResultCodeRepository _bankResultCodeRepository;
        private readonly ILoaneeRemarkRepository _loaneeRemarkRepository;

        public BankResultCodesController(IBankResultCodeRepository bankResultCodeRepository, IEmployerRepository employerRepository, ILoaneeRemarkRepository loaneeRemarkRepository)
        {
            _bankResultCodeRepository = bankResultCodeRepository;
            _employerRepository = employerRepository;
            _loaneeRemarkRepository = loaneeRemarkRepository;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var employers = await _employerRepository.GetAll();
        //    employers.Insert(0, new Employer { EmployerCode = "", EmployerName = "แสดงทั้งหมด" });
        //    List<SelectListItem> SelectEmployers = new();
        //    foreach (var item in employers)
        //    {
        //        SelectEmployers.Add(new SelectListItem
        //        {
        //            Text = item.EmployerName.ToString(),
        //            Value = item.EmployerCode.ToString(),
        //        });
        //    }
        //    ViewBag.Employers = SelectEmployers;
        //    var defaultEmployerCode = employers.FirstOrDefault().EmployerCode;
        //    return View(await GetAll(defaultEmployerCode));
        //}


        //[HttpGet]
        //public async Task<IActionResult> GetBankResult(string id)
        //{

        //    var bankActions = await GetAll(id);
        //    var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", bankActions);
        //    return new JsonResult(new { isValid = true, message = "", html });
        //}

        //private async Task<List<BankResultCodeViewModel>> GetAll()
        //{
        //    var results = await _bankResultCodeRepository.GetAll();
        //    return results.ToList();
        //}

        //private async Task<List<BankResultCodeViewModel>> GetAll(string id)
        //{
        //    var bankResults = await _bankResultCodeRepository.GetByEmployer(id);

        //    return bankResults;
        //}

        [HttpGet]
        public async Task<JsonResult> GetBankResultCodes(int id)
        {
            var bankResultCodes = await _bankResultCodeRepository.GetByBankPersonId(id);
            bankResultCodes.Insert(0, new BankResultCodeViewModel { BankResultId = 0, BankResultCodeName = "==กรุณาเเลือก==" });
            List<SelectListItem> selects = new();

            foreach (var item in bankResultCodes)
            {
                selects.Add(new SelectListItem
                {
                    Text = $"{item.BankResultCodeId}:{item.BankResultCodeName}",
                    Value = item.BankResultId.ToString(),
                });
            }
            return Json(selects);
        }


        public IActionResult Create()
        {
            //var employers = await _employerRepository.GetAll();
            //List<SelectListItem> selects = new();
            //foreach (var item in employers)
            //{
            //    selects.Add(new SelectListItem
            //    {
            //        Text = item.EmployerName.ToString(),
            //        Value = item.EmployerCode.ToString(),
            //    });
            //}
            //ViewBag.Employers = selects;
            return View(new BankResultCode());
        }

        [HttpPost]
        public IActionResult Edit([FromBody] BankResultCode model)
        {
            //var employers = await _employerRepository.GetAll();
            //List<SelectListItem> selects = new();
            //foreach (var item in employers)
            //{
            //    selects.Add(new SelectListItem
            //    {
            //        Text = item.EmployerName.ToString(),
            //        Value = item.EmployerCode.ToString(),
            //    });
            //}
            //ViewBag.Employers = selects;
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(BankResultCode model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _bankResultCodeRepository.Create(model);
        //        _notify.Success($"{model.BankResultCodeName} is Created.");
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(model);
        //}

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var model = await _bankResultCodeRepository.GetByKey(id);

        //    //var employers = await _employerRepository.GetAll();
        //    //List<SelectListItem> selects = new();
        //    //foreach (var item in employers)
        //    //{
        //    //    selects.Add(new SelectListItem
        //    //    {
        //    //        Selected = item.EmployerCode == model.EmployerCode,
        //    //        Text = item.EmployerName.ToString(),
        //    //        Value = item.EmployerCode.ToString(),
        //    //    });
        //    //}
        //    //ViewBag.Employers = selects;

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, BankResultCode model)
        //{
        //    var oldEntity = await _bankResultCodeRepository.GetByKey(id);

        //    if (oldEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        await _bankResultCodeRepository.Update(id, model);
        //        _notify.Success($"{model.BankResultCodeName} is Updated");

        //        return RedirectToAction(nameof(Index));
        //    }
        //    //var employers = await _employerRepository.GetAll();
        //    //List<SelectListItem> selects = new();
        //    //foreach (var item in employers)
        //    //{
        //    //    selects.Add(new SelectListItem
        //    //    {
        //    //        Selected = item.EmployerCode == oldEntity.EmployerCode,
        //    //        Text = item.EmployerName.ToString(),
        //    //        Value = item.EmployerCode.ToString(),
        //    //    });
        //    //}
        //    //ViewBag.Employers = selects;
        //    return View(model);
        //}

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isHaveRelation = _loaneeRemarkRepository.BankResultCodeIsExist(id);
                if (isHaveRelation)
                {
                    throw new Exception("ไม่สามารถลบข้อมูล เนื่องจากมีการใช้ใน รายงานการติดตาม");
                }

                await _bankResultCodeRepository.Delete(id);
                //var results = await GetAll("");
                //var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
                return new JsonResult(new { isValid = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });

            }

        }
    }
}

