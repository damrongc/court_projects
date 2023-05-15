﻿using CourtJustice.Domain.Models;
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
                var seq = await _paymentRepository.GetPaymentSeq(model.CusId);
                model.PaymentSeq = seq;
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

