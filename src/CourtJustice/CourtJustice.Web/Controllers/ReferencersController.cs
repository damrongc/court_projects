using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
 
    public class ReferencersController : BaseController<ReferencersController>
    {
        private readonly IReferencerRepository _referencerRepository;

        public ReferencersController(IReferencerRepository referencerRepository)
        {
            _referencerRepository = referencerRepository;
        }

        public IActionResult Index()
        {
            return View();
        }



        private async Task<List<Referencer>> GetAll()
        {
            var results = await _referencerRepository.GetAll();
            return results.ToList();
        }

        public IActionResult Create()
        {
            return View(new Referencer());
        }


        [HttpGet]
        public async Task<IActionResult> AddOrEdit(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new Referencer());
            }
            else
            {
                var referencer = await _referencerRepository.GetByKey(id);
                return View(referencer);
            }
        }


        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] Referencer model)
        {
            var isExisting = _referencerRepository.IsExisting(model.ReferencerCode);
            if (isExisting)
            {
                await _referencerRepository.Update(model.ReferencerCode, model);
            }
            else
            {
                await _referencerRepository.Create(model);
            }
            var referencer = await _referencerRepository.GetByCusId(model.CusId);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ReferencerCard", referencer);
            return new JsonResult(new { isValid = true, html });
        }



        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id, string cusId)
        {
            try
            {


                await _referencerRepository.Delete(id);

                var referencer = await _referencerRepository.GetByCusId(cusId);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ReferencerCard", referencer);
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
            var referencer = await _referencerRepository.GetByCusId(id);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_ReferencerCard", referencer);
            return new JsonResult(new { isValid = true, message = "", html });
        }

      
    }
}

