using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourtJustice.Web.Controllers
{
    public class BankPersonCodesController : BaseController<BankPersonCodesController>
    {
        private readonly IBankPersonCodeRepository _bankPersonCodeRepository;
        private readonly IBankActionCodeRepository _bankActionCodeRepository;

        public BankPersonCodesController(IBankPersonCodeRepository bankPersonCodeRepository, IBankActionCodeRepository bankActionCodeRepository)
        {
            _bankPersonCodeRepository = bankPersonCodeRepository;
            _bankActionCodeRepository = bankActionCodeRepository;
        }

        [HttpGet]
        public async Task<JsonResult> GetBankPersonCodes(int id)
        {
            //var bankAction = await _bankActionCodeRepository.GetByKey(id);
            var bankPersonCodes = await _bankPersonCodeRepository.GetAll(id);
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
