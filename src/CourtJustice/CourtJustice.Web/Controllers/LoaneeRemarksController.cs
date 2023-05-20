using CourtJustice.Domain;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class LoaneeRemarksController : BaseController<LoaneeRemarksController>
    {
        private readonly ILoaneeRemarkRepository _loaneeRemarkRepository;

        public LoaneeRemarksController(ILoaneeRemarkRepository loaneeRemarkRepository)
        {
            _loaneeRemarkRepository = loaneeRemarkRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        private async Task<List<LoaneeRemark>> GetAll()
        {
            var results = await _loaneeRemarkRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new LoaneeRemark());
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, string cusId)
        {
            try
            {
                await _loaneeRemarkRepository.Delete(id);
                var loaneeRemark = await _loaneeRemarkRepository.GetByCusId(cusId);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_LoaneeRemarkCard", loaneeRemark);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            if (id == 0)
            {
                return View(new LoaneeRemark());
            }
            else
            {
                var loaneeRemark = await _loaneeRemarkRepository.GetByKey(id);
                return View(loaneeRemark);
            }
        }



        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] LoaneeRemark model)
        {
            var isExisting = _loaneeRemarkRepository.IsExisting(model.LoaneeRemarkId);
            if (isExisting)
            {
                await _loaneeRemarkRepository.Update(model.LoaneeRemarkId, model);
            }
            else
            {
                await _loaneeRemarkRepository.Create(model);
            }
            var loaneeRemark = await _loaneeRemarkRepository.GetByCusId(model.CusId);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_LoaneeRemarkCard", loaneeRemark);
            return new JsonResult(new { isValid = true, html });
        }


        [HttpGet]
        public async Task<JsonResult> GetByCusId(string id = "")
        {
            var loaneeRemark = await _loaneeRemarkRepository.GetByCusId(id);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_LoaneeRemarkCard", loaneeRemark);
            return new JsonResult(new { isValid = true, message = "", html });
        }

    }
}

