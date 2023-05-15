﻿using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{

    public class AssetCarsController : BaseController<AssetCarsController>
    {
        private readonly IAssetCarRepository _assetCarRepository;
        private readonly ICarTypeRepository _carTypeRepository;
       

        public AssetCarsController(IAssetCarRepository assetCarRepository, ICarTypeRepository carTypeRepository)
        {
            _assetCarRepository = assetCarRepository;
            _carTypeRepository = carTypeRepository;
        }



        public IActionResult Index()
        {
            return View();
        }



        private async Task<List<AssetCar>> GetAll()
        {
            var results = await _assetCarRepository.GetAll();
            return results.ToList();
        }


        public async Task<IActionResult> AddOrEdit(string id = "")
        {
            var cartype = await _carTypeRepository.GetAll();
            List<SelectListItem> SelectCarType = new();


            if (string.IsNullOrEmpty(id))
            {
                foreach (var item in cartype)
                {
                    SelectCarType.Add(new SelectListItem
                    {
                        Text = item.CarTypeName.ToString(),
                        Value = item.CarTypeCode.ToString(),
                    });
                }
                ViewBag.CarTypes = SelectCarType;
                return View(new AssetCar());

            }
            else
            {
                var assetCar = await _assetCarRepository.GetByKey(id);

                foreach (var item in cartype)
                {
                    SelectCarType.Add(new SelectListItem
                    {
                        Selected = _carTypeRepository.IsExisting(item.CarTypeCode),
                        Text = item.CarTypeName.ToString(),
                        Value = item.CarTypeCode.ToString(),
                    });
                }

                ViewBag.CarTypes = SelectCarType;
                return View(assetCar);
            }
        }


        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] AssetCar model)
        {

        
            var isExisting = _assetCarRepository.IsExisting(model.ChassisNumber);
            if (isExisting)
            {
                await _assetCarRepository.Update(model.ChassisNumber, model);
            }
            else
            {
                await _assetCarRepository.Create(model);
            }
            var assetCars = await _assetCarRepository.GetByCusId(model.CusId);

            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetCarCard", assetCars);
            return new JsonResult(new { isValid = true, html });
        }


        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id, string cusId)
        {
         
                try
                {
                    await _assetCarRepository.Delete(id);
                    var assetCars = await _assetCarRepository.GetByCusId(cusId);
                    var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetCarCard", assetCars);
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
            var assetCars = await _assetCarRepository.GetByCusId(id);

            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetCarCard", assetCars);
            return new JsonResult(new { isValid = true, message = "", html });
        }



        /* [HttpPost]
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
                 int recordsTotal = await _assetCarRepository.GetRecordCount(searchValue);

                 var data = await _assetCarRepository.GetPaging(skip, pageSize, searchValue);
                 var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                 return Ok(jsonData);
             }
             catch
             {
                 throw;
             }
         }*/
    }
}

