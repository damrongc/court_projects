using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using CourtJustice.Web.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class CompanyResultCodesController : BaseController<CompanyResultCodesController>
    {
        private readonly ICompanyActionCodeRepository _companyActionCodeRepository;
        private readonly ICompanyResultCodeRepository _companyResultCodeRepository;
        private readonly ILoaneeRemarkRepository _loaneeRemarkRepository;


        public CompanyResultCodesController(ICompanyResultCodeRepository companyResultCodeRepository,
             ICompanyActionCodeRepository companyActionCodeRepository,
             ILoaneeRemarkRepository loaneeRemarkRepository)
        {
            _companyResultCodeRepository = companyResultCodeRepository;
            _companyActionCodeRepository = companyActionCodeRepository;
            _loaneeRemarkRepository = loaneeRemarkRepository;
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await GetAll());
        //}

        //private async Task<List<CompanyResultCodeViewModel>> GetAll()
        //{
        //    var results = await _companyResultCodeRepository.GetAll();
        //    return results.ToList();
        //}

        //private async Task<List<CompanyActionCodeViewModel>> GetAll()
        //{
        //    var companyActionCodes = await _companyActionCodeRepository.GetAll();

        //    foreach (var companyAction in companyActionCodes)
        //    {
        //        var companyResultCodes = await _companyResultCodeRepository.GetByCompanyActionId(companyAction.CompanyActionId);
        //        companyAction.CompanyResultCodes = companyResultCodes;
        //    }
        //    return companyActionCodes.ToList();
        //}
        [HttpGet]
        public async Task<JsonResult> GetCompanyResultCodes(int id)
        {
            var companyResultCodes = await _companyResultCodeRepository.GetByCompanyActionId(id);
            companyResultCodes.Insert(0, new CompanyResultCodeViewModel { CompanyResultId = 0, CompanyResultCodeName = "==กรุณาเเลือก==" });
            List<SelectListItem> selects = new();

            foreach (var item in companyResultCodes)
            {
                selects.Add(new SelectListItem
                {
                    Text = $"{item.CompanyResultCodeId}:{item.CompanyResultCodeName}",
                    Value = item.CompanyResultId.ToString(),
                });
            }
            return Json(selects);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id, int companyActionId)
        {
            try
            {
                
                var companyResultCode = new CompanyResultCodeViewModel();
                if (id == 0)
                {
                    var companyActionCode = await _companyActionCodeRepository.GetByKey(companyActionId);
                    companyResultCode = new CompanyResultCodeViewModel
                    {
                        CompanyActionId = companyActionCode.CompanyId,
                        CompanyActionCodeId = companyActionCode.CompanyActionCodeId,
                        CompanyActionCodeName = companyActionCode.CompanyActionCodeName,
                    };
                }
                else
                {
                    companyResultCode = await _companyResultCodeRepository.GetByKey(id);
                }
                return View(companyResultCode);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] CompanyResultCodeRequest request)
        {
            try
            {

                var isExits = _companyResultCodeRepository.IsExisting(request.CompanyActionId, request.CompanyResultCodeId);
                if (isExits)
                {
                    throw new Exception("Result Code มีอยู่แล้วในระบบ!");
                }
                var companyResultCode = new CompanyResultCode
                {
                    CompanyActionId = request.CompanyActionId,
                    CompanyResultCodeId = request.CompanyResultCodeId,
                    CompanyResultCodeName = request.CompanyResultCodeName,
                    IsActive = true,
                };
                if (request.CompanyResultId == 0)
                {
                    await _companyResultCodeRepository.Create(companyResultCode);

                }
                else
                {
                    await _companyResultCodeRepository.Update(request.CompanyResultId, companyResultCode);
                }
                //var results = await GetAll();
                
                //var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
                return new JsonResult(new { isValid = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });

            }
        }


        //[HttpGet]
        //public async Task<IActionResult> Create(int id)
        //{
        //    var companyActionCode = await _companyActionCodeRepository.GetByKey(id);
        //    var companyResultCode = new CompanyResultCodeViewModel
        //    {
        //        CompanyActionId = companyActionCode.CompanyId,
        //        CompanyActionCodeId = companyActionCode.CompanyActionCodeId,
        //        CompanyActionCodeName = $"{companyActionCode.CompanyActionCodeId}:{companyActionCode.CompanyActionCodeName}",

        //    };
        //    //var companies = await _companyRepository.GetAll();
        //    //List<SelectListItem> selects = new();
        //    //foreach (var item in companies)
        //    //{
        //    //    selects.Add(new SelectListItem
        //    //    {
        //    //        Text = item.CompanyName.ToString(),
        //    //        Value = item.CompanyId.ToString(),
        //    //    });
        //    //}
        //    //ViewBag.Companies = selects;
        //    return View(companyResultCode);
        //}

        //[HttpPost]
        //public async Task<JsonResult> CreateCompanyResultCode([FromBody] CompanyResultCodeRequest request)
        //{
        //    try
        //    {

        //        var isExits = _companyResultCodeRepository.IsExisting(request.CompanyActionId, request.CompanyResultCodeId);
        //        if (isExits)
        //        {
        //            throw new Exception("Result Code มีอยู่แล้วในระบบ!");
        //        }
        //        var companyResultCode = new CompanyResultCode
        //        {
        //            CompanyActionId = request.CompanyActionId,
        //            CompanyResultCodeId = request.CompanyResultCodeId,
        //            CompanyResultCodeName = request.CompanyResultCodeName,
        //            IsActive = true,
        //        };
        //        await _companyResultCodeRepository.Create(companyResultCode);
        //        var results = await GetAll();
        //        var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
        //        return new JsonResult(new { isValid = true, message = "", html });
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { isValid = false, message = ex.Message });

        //    }
        //}

        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<IActionResult> Create(CompanyResultCode model)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        await _companyResultCodeRepository.Create(model);
        ////        _notify.Success($"{model.CompanyResultCodeName} is Created.");
        ////        return RedirectToAction(nameof(Index));
        ////    }
        ////    var companies = await _companyRepository.GetAll();
        ////    List<SelectListItem> selects = new();
        ////    foreach (var item in companies)
        ////    {
        ////        selects.Add(new SelectListItem
        ////        {
        ////            Text = item.CompanyName.ToString(),
        ////            Value = item.CompanyId.ToString(),
        ////        });
        ////    }
        ////    ViewBag.Companies = selects;
        ////    return View(model);
        ////}
        //[HttpGet]

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var model = await _companyResultCodeRepository.GetByKey(id);

        //    //var companies = await _companyRepository.GetAll();
        //    //List<SelectListItem> selects = new();
        //    //foreach (var item in companies)
        //    //{
        //    //    selects.Add(new SelectListItem
        //    //    {
        //    //        Selected=item.CompanyId==model.CompanyId,
        //    //        Text = item.CompanyName.ToString(),
        //    //        Value = item.CompanyId.ToString(),
        //    //    });
        //    //}
        //    //ViewBag.Companies = selects;
        //    return View(model);
        //}
        //[HttpPost]
        //public async Task<JsonResult> EditCompanyResultCode([FromBody] CompanyResultCodeRequest request)
        //{
        //    try
        //    {
        //        var isExits = _companyResultCodeRepository.IsExisting(request.CompanyActionId, request.CompanyResultCodeId);
        //        if (isExits)
        //        {
        //            throw new Exception("Result Code มีอยู่แล้วในระบบ!");
        //        }
        //        var companyResultCode = new CompanyResultCode
        //        {
        //            CompanyActionId = request.CompanyActionId,
        //            CompanyResultCodeId = request.CompanyResultCodeId,
        //            CompanyResultCodeName = request.CompanyResultCodeName,
        //            IsActive = true,
        //        };
        //        await _companyResultCodeRepository.Update(request.CompanyResultId, companyResultCode);
        //        var results = await GetAll();
        //        var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
        //        return new JsonResult(new { isValid = true, message = "", html });
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { isValid = false, message = ex.Message });

        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, CompanyResultCode model)
        //{
        //    var oldEntity = await _companyResultCodeRepository.GetByKey(id);

        //    if (oldEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        await _companyResultCodeRepository.Update(id, model);
        //        _notify.Success($"{model.CompanyResultCodeName} is Updated");

        //        return RedirectToAction(nameof(Index));
        //    }
        //    //var companies = await _companyRepository.GetAll();
        //    //List<SelectListItem> selects = new();
        //    //foreach (var item in companies)
        //    //{
        //    //    selects.Add(new SelectListItem
        //    //    {
        //    //        Selected = item.CompanyId == oldEntity.CompanyId,
        //    //        Text = item.CompanyName.ToString(),
        //    //        Value = item.CompanyId.ToString(),
        //    //    });
        //    //}
        //    //ViewBag.Companies = selects;
        //    return View(model);
        //}

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if(id == 0)
                {
                    return new JsonResult(new { isValid = true, });

                }
                var isHaveRelation = _loaneeRemarkRepository.CompanyResultCodeIsExist(id);
                if (isHaveRelation)
                {
                    throw new Exception("ไม่สามารถลบข้อมูล เนื่องจากมีการใช้ใน รายงานการติดตาม");
                }

                await _companyResultCodeRepository.Delete(id);
                //_notify.Success($"Delete is Success.");
                //var results = await GetAll();
                //var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
                return new JsonResult(new { isValid = true, });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });

            }

        }
    }
}

