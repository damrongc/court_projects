using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourtJustice.Web.Controllers
{
    public class BankPersonCodesController : BaseController<BankPersonCodesController>
    {
        private readonly IBankPersonCodeRepository _bankPersonCodeRepository;
        

        public BankPersonCodesController(IBankPersonCodeRepository bankPersonCodeRepository)
        {
            _bankPersonCodeRepository = bankPersonCodeRepository;
        }

        [HttpGet]
        public async Task<JsonResult> GetBankPersonCodes(int id)
        {
            //var bankAction = await _bankActionCodeRepository.GetByKey(id);
            var bankPersonCodes = await _bankPersonCodeRepository.GetByBankActionId(id);
            bankPersonCodes.Insert(0, new BankPersonCodeViewModel { BankPersonId = 0, BankPersonCodeName = "==กรุณาเเลือก=="});
            List<SelectListItem> selects = new();

            foreach (var item in bankPersonCodes)
            {
                selects.Add(new SelectListItem
                {
                    Text = $"{item.BankPersonCodeId}:{item.BankPersonCodeName}",
                    Value = item.BankPersonId.ToString(),
                });
            }
            return Json(selects);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
