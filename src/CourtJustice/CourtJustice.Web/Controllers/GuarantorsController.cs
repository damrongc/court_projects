using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class GuarantorsController : BaseController<GuarantorsController>
    {
        private readonly IGuarantorRepository _guarantorRepository;

        public GuarantorsController(IGuarantorRepository guarantorRepository)
        {
            _guarantorRepository = guarantorRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        private async Task<List<Guarantor>> GetAll()
        {
            var results = await _guarantorRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new Guarantor());
        }


        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id =0)
        {
            if (id == 0)
            {
                return View(new Guarantor());
            }
            else
            {
                var guarantor = await _guarantorRepository.GetByKey(id);
                return View(guarantor);
            }
        }


        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] Guarantor model)
        {
            var isExisting = _guarantorRepository.IsExisting(model.GuarantorCode);
            if (isExisting)
            {
                await _guarantorRepository.Update(model.GuarantorCode, model);
            }
            else
            {
                await _guarantorRepository.Create(model);
            }
            var guarantor = await _guarantorRepository.GetByCusId(model.CusId);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_GuarantorCard", guarantor);
            return new JsonResult(new { isValid = true, html });
        }


        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, string cusId)
        {
            try
            {
                await _guarantorRepository.Delete(id);
                var guarantor = await _guarantorRepository.GetByCusId(cusId);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_GuarantorCard", guarantor);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetByCusId(string id)
        {
            var guarantor = await _guarantorRepository.GetByCusId(id);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_GuarantorCard", guarantor);
            return new JsonResult(new { isValid = true, message = "", html });
        }


    }
}

