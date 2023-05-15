using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourtJustice.Web.Controllers
{
    public class PaymentsController : BaseController<PaymentsController>
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentsController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }


        public IActionResult Index()
        {
            return View();
        }



        private async Task<List<Payment>> GetAll()
        {
            var results = await _paymentRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new Payment());
        }


        /*     [HttpPost]
             [ValidateAntiForgeryToken]
             public async Task<IActionResult> Create(Payment model)
             {
                 if (ModelState.IsValid)
                 {
                     await _paymentRepository.Create(model);
                     _notify.Success($"{model.PaymentId} is Created.");
                     return RedirectToAction(nameof(Index));
                 }
                 return View(model);
             }


             public async Task<IActionResult> Edit(int id)
             {
                 var model = await _paymentRepository.GetByKey(id);
                 return View(model);
             }


             [HttpPost]
             [ValidateAntiForgeryToken]
             public async Task<IActionResult> Edit(int id, Payment model)
             {
                 var oldEntity = await _paymentRepository.GetByKey(id);

                 if (oldEntity == null)
                 {
                     return NotFound();
                 }

                 if (ModelState.IsValid)
                 {
                     await _paymentRepository.Update(id, model);
                     _notify.Success($"{model.PaymentId} is Updated");

                     return RedirectToAction(nameof(Index));
                 }
                 return View(model);
             }*/

        public async Task<IActionResult> AddOrEdit(int id)
        {
           

            if (id == 0)
            {            
                return View(new Payment());

            }
            else
            {
                var payment= await _paymentRepository.GetByKey(id);
               
                return View(payment);
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] Payment model)
        {
            var isExisting = _paymentRepository.IsExisting(model.PaymentId);
            if (isExisting)
            {
                await _paymentRepository.Update(model.PaymentId, model);
            }
            else
            {
                await _paymentRepository.Create(model);
            }
            var payments = await _paymentRepository.GetByCusId(model.CusId);
            //return PartialView("~/Views/AssetLands/_AssetLandCard.cshtml", assetLands);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_PaymentCard", payments);
            return new JsonResult(new { isValid = true, html });
        }


        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {


                await _paymentRepository.Delete(id);
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

      


        [HttpGet]
        public async Task<JsonResult> GetByCusId(string id = "")
        {
            var payment = await _paymentRepository.GetByCusId(id);

            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_PaymentCard", payment);
            return new JsonResult(new { isValid = true, message = "", html });
        }
     

        [HttpDelete, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirmed(int id,string cusId)
        {
            try
            {
                await _paymentRepository.Delete(id);
                var payments = await _paymentRepository.GetByCusId(cusId);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_PaymentCard", payments);
                return new JsonResult(new { isValid = true, message = "", html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });

            }
        }


    }
}

