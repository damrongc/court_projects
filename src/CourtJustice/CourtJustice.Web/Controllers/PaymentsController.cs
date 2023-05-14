using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
  
    public class PaymentsController : BaseController<PaymentsController>
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentsController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
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
            //return PartialView("~/Views/AssetLands/_AssetLandCard.cshtml", assetLands);
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

        [HttpGet]
        public async Task<JsonResult> GetByCusId(string id = "")
        {
            var payment = await _paymentRepository.GetByCusId(id);

            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_PaymentCard", payment);
            return new JsonResult(new { isValid = true, message = "", html });
        }

        //[HttpPost]
        //public async Task<IActionResult> GetWithPaging()
        //{
        //    try
        //    {
        //        //var productGroupCode = Request.Form["productGroupCode"].FirstOrDefault();
        //        var draw = Request.Form["draw"].FirstOrDefault();
        //        var start = Request.Form["start"].FirstOrDefault();
        //        var length = Request.Form["length"].FirstOrDefault();
        //        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;
        //        int recordsTotal = await _paymentRepository.GetRecordCount(searchValue);

        //        var data = await _paymentRepository.GetPaging(skip, pageSize, searchValue);
        //        var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
        //        return Ok(jsonData);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}

