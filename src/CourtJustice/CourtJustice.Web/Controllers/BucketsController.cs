using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
    
    public class BucketsController : BaseController<BucketsController>
    {
        private readonly IBucketRepository _bucketRepository ;

        public BucketsController(IBucketRepository bucketRepository)
        {
            _bucketRepository = bucketRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
        }



        private async Task<List<Bucket>> GetAll()
        {
            var results = await _bucketRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new Bucket());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bucket model)
        {
            if (ModelState.IsValid)
            {
                await _bucketRepository.Create(model);
                _notify.Success($"{model.BucketName} is Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _bucketRepository.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bucket model)
        {
            var oldEntity = await _bucketRepository.GetByKey(id);

            if (oldEntity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bucketRepository.Update(id, model);
                _notify.Success($"{model.BucketName} is Updated");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {


                await _bucketRepository.Delete(id);
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

