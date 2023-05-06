using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
    
    public class CourtsController : BaseController<CourtsController>
    {
        private readonly ICourtRepository _courtRepository;

        public CourtsController(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }

        public IActionResult Create()
        {
            return View(new Court());
        }

        private async Task<List<Court>> GetAll()
        {
            var results = await _courtRepository.GetAll();
            return results.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Court model)
        {
            if (ModelState.IsValid)
            {
                await _courtRepository.Create(model);
                _notify.Success($"{model.CourtName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
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
                int recordsTotal = await _courtRepository.GetRecordCount(searchValue);

                var data = await _courtRepository.GetPaging(skip, pageSize, searchValue);
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch
            {
                throw;
            }
        }

       

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _courtRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Court model)
        {
            var oldEntity = await _courtRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _courtRepository.Update(id, model);
                _notify.Success($"{model.CourtName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {


                await _courtRepository.Delete(id);
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

