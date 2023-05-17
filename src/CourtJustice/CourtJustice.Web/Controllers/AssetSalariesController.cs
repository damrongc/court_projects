using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class AssetSalariesController : BaseController<AssetSalariesController>
    {
        private readonly IAssetSalaryRepository _assetSalaryRepository;


        public AssetSalariesController(IAssetSalaryRepository assetSalaryRepository)
        {
            _assetSalaryRepository = assetSalaryRepository;

        }

        public IActionResult Index()
        {
            return View();
        }


        private async Task<List<AssetSalary>> GetAll()
        {
            var results = await _assetSalaryRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new AssetSalary() );
        }

      

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, string cusId) 
        {
            try
            {
                await _assetSalaryRepository.Delete(id);
                var assetSalaries = await _assetSalaryRepository.GetByCusId(cusId);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetSalaryCard", assetSalaries);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> GetWithPaging()
        {
            try
            {
                //var productGroupCode = Request.Form["productGroupCode"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = await _assetSalaryRepository.GetRecordCount(searchValue);

                var data = await _assetSalaryRepository.GetPaging(skip, pageSize, searchValue);
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            if (id == 0)
            {
                return View(new AssetSalary { SalaryDate = DateOnly.FromDateTime(DateTime.Today) });
            }
            else
            {
                var assetSalary = await _assetSalaryRepository.GetByKey(id);
                return View(assetSalary);
            }
        }

        

        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] AssetSalary model)
        {
            var isExisting = _assetSalaryRepository.IsExisting(model.AssetId);
            if (isExisting)
            {
                await _assetSalaryRepository.Update(model.AssetId, model);
            }
            else
            {
                await _assetSalaryRepository.Create(model);
            }
            var assetSalary = await _assetSalaryRepository.GetByCusId(model.CusId);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetSalaryCard", assetSalary);
            return new JsonResult(new { isValid = true, html });
        }


        [HttpGet]
        public async Task<JsonResult> GetByCusId(string id = "")
        {
            var assetSalary = await _assetSalaryRepository.GetByCusId(id);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetSalaryCard", assetSalary);
            return new JsonResult(new { isValid = true, message = "", html });
        }
    }
}

