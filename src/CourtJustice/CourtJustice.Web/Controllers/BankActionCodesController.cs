using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Web.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourtJustice.Web.Controllers
{

    public class BankActionCodesController : BaseController<BankActionCodesController>
    {
        private readonly IBankActionCodeRepository _bankActionCodeRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly ILoaneeRemarkRepository _loaneeRemarkRepository;
        private readonly IBankPersonCodeRepository _bankPersonCodeRepository;

        public BankActionCodesController(IBankActionCodeRepository bankActionCodeRepository,
            IEmployerRepository employerRepository,
            ILoaneeRemarkRepository loaneeRemarkRepository,
            IBankPersonCodeRepository bankPersonCodeRepository)
        {
            _bankActionCodeRepository = bankActionCodeRepository;
            _employerRepository = employerRepository;
            _loaneeRemarkRepository = loaneeRemarkRepository;
            _bankPersonCodeRepository = bankPersonCodeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var employers = await _employerRepository.GetAll();
            employers.Insert(0, new Employer { EmployerCode = "", EmployerName = "แสดงทั้งหมด" });
            List<SelectListItem> SelectEmployers = new();
            foreach (var item in employers)
            {
                SelectEmployers.Add(new SelectListItem
                {
                    Text = item.EmployerName.ToString(),
                    Value = item.EmployerCode.ToString(),
                });
            }
            ViewBag.Employers = SelectEmployers;
            var defaultEmployerCode = employers.FirstOrDefault().EmployerCode;
            return View(await GetAll(defaultEmployerCode));
        }

        [HttpGet]
        public async Task<IActionResult> GetBankAction(string id)
        {

            var bankActions = await GetAll(id);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", bankActions);
            return new JsonResult(new { isValid = true, message = "", html });
        }

        [HttpGet]
        public async Task<IActionResult> CreatePersonCode(int id)
        {
            var bankActionCode =await _bankActionCodeRepository.GetByKey(id);

            return View(new BankPersonCodeViewModel
            {
                BankActionId=id,
                BankActionCodeId = bankActionCode.BankActionCodeId,
                BankActionCodeName = $"{bankActionCode.BankActionCodeId}:{bankActionCode.BankActionCodeName}" 
            });
        }

        [HttpPost]
        public async Task<JsonResult> CreatePersonCode([FromBody] BankPersonCodeRequest request)
        {
            try
            {

                var isExits = _bankPersonCodeRepository.IsExisting(request.BankActionId,request.BankPersonCodeId);
                if (isExits)
                {
                    throw new Exception("Person Code มีอยู่แล้วในระบบ!");
                }
                var bankPersonCode = new BankPersonCode
                {
                    BankActionId = request.BankActionId,
                    BankPersonCodeId = request.BankPersonCodeId,
                    BankPersonCodeName = request.BankPersonCodeName,
                };
                await _bankPersonCodeRepository.Create(bankPersonCode);

                var results = await GetAll(request.EmployerCodeFilter);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });

            }
        }

        [HttpGet]
        public async Task<IActionResult> EditPersonCode(int id)
        {
            var bankPersonCode = await _bankPersonCodeRepository.GetByKey(id);
            var bankActionCode = await _bankActionCodeRepository.GetByKey(bankPersonCode.BankActionId);

            var viewModel = new BankPersonCodeViewModel
            {
                BankPersonId= bankPersonCode.BankPersonId,
                BankPersonCodeId = bankPersonCode.BankPersonCodeId,
                BankPersonCodeName = bankPersonCode.BankPersonCodeName,
                BankActionId = bankPersonCode.BankActionId,
                BankActionCodeId = bankActionCode.BankActionCodeId,
                BankActionCodeName = $"{bankActionCode.BankActionCodeId}:{bankActionCode.BankActionCodeName}"
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> EditPersonCode([FromBody] BankPersonCodeRequest request)
        {
            try
            {
                var bankPersonCode = new BankPersonCode
                {
                    BankActionId = request.BankActionId,
                    BankPersonId = request.BankPersonId,
                    BankPersonCodeId = request.BankPersonCodeId,
                    BankPersonCodeName = request.BankPersonCodeName
                };

                await _bankPersonCodeRepository.Update(bankPersonCode);

                var results = await GetAll(request.EmployerCodeFilter);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });

            }
        }

        private async Task<List<BankActionCodeViewModel>> GetAll(string employerCode)
        {
            var bankActions = await _bankActionCodeRepository.GetByEmployer(employerCode);
            foreach (var bankAction in bankActions)
            {
                var bankPersonCodes = await _bankPersonCodeRepository.GetAll( bankAction.BankActionId);
                bankAction.BankPersonCodes = bankPersonCodes;
            }
            return bankActions;
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

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _bankActionCodeRepository.GetByKey(id);

            var employers = await _employerRepository.GetAll();
            List<SelectListItem> selects = new();
            foreach (var item in employers)
            {
                selects.Add(new SelectListItem
                {
                    Selected = item.EmployerCode == model.EmployerCode,
                    Text = item.EmployerName.ToString(),
                    Value = item.EmployerCode.ToString(),
                });
            }
            ViewBag.Employers = selects;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,BankActionCode model)
        {
            var oldEntity = await _bankActionCodeRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                await _bankActionCodeRepository.Update(model);
                _notify.Success($"{model.BankActionCodeName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            var employers = await _employerRepository.GetAll();
            List<SelectListItem> selects = new();
            foreach (var item in employers)
            {
                selects.Add(new SelectListItem
                {
                    Selected = item.EmployerCode == oldEntity.EmployerCode,
                    Text = item.EmployerName.ToString(),
                    Value = item.EmployerCode.ToString(),
                });
            }
            ViewBag.Employers = selects;
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                //var isHaveRelation = _loaneeRemarkRepository.BankActionCodeIsExist(id);
                //if (isHaveRelation)
                //{
                //    throw new Exception("ไม่สามารถลบข้อมูล เนื่องจากมีการใช้ใน รายงานการติดตาม");
                //}
                await _bankActionCodeRepository.Delete(id);
                var results = await GetAll("");
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ViewTable", results);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersonCode([FromBody] BankPersonCodeRequest request)
        {
            try
            {
                var isExist = _loaneeRemarkRepository.BankPersonCodeIsExist(request.BankPersonId);
                if (isExist)
                {
                    throw new Exception("ไม่สามารถลบข้อมูลได้ เนื่องจากมีการใช้งาน ที่รายงานการติดตาม");
                };
                await _bankPersonCodeRepository.Delete(request.BankPersonId);

                var results = await GetAll(request.EmployerCodeFilter);
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

