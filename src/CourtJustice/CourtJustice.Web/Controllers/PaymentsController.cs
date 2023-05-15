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

        [HttpGet]
        public async Task<JsonResult> GetByCusId(string id = "")
        {
            var payment = await _paymentRepository.GetByCusId(id);

            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_PaymentCard", payment);
            return new JsonResult(new { isValid = true, message = "", html });
        }
        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id)
        {
            if (id == 0)
            {            
                return View(new Payment { PaymentDate=DateOnly.FromDateTime(DateTime.Today)});
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
                var seq = await _paymentRepository.GetPaymentSeq(model.CusId);
                model.PaymentSeq = seq;
                await _paymentRepository.Create(model);
            }
            var payments = await _paymentRepository.GetByCusId(model.CusId);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_PaymentCard", payments);
            return new JsonResult(new { isValid = true, html });
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

