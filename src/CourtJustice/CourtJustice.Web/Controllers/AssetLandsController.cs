using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CourtJustice.Web.Controllers
{

    public class AssetLandsController : BaseController<AssetLandsController>
    {
        const string ASSET_PATH = "/images/assetlands/";
        private readonly IAssetLandRepository _assetLandRepository;
        private readonly ILandOfficeRepository _landOfficeRepository;
        private readonly IAssetImageRepository _assetImageRepository;

        public AssetLandsController(IAssetLandRepository assetLandRepository,
            ILandOfficeRepository landOfficeRepository,
            IAssetImageRepository assetImageRepository)
        {
            _assetLandRepository = assetLandRepository;
            _landOfficeRepository = landOfficeRepository;
            _assetImageRepository = assetImageRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddOrEdit(string id="")
        {
            var landOffices = await _landOfficeRepository.GetAll();
            List<SelectListItem> SelectLandOffice = new();
 

            if (string.IsNullOrEmpty(id))
            {
                foreach (var item in landOffices)
                {
                    SelectLandOffice.Add(new SelectListItem
                    {
                        Text = item.LandOfficeName.ToString(),
                        Value = item.LandOfficeCode.ToString(),
                    });
                }
                ViewBag.LandOffices = SelectLandOffice;
                return View(new AssetLand());

            }
            else
            {
                var assetLand = await _assetLandRepository.GetByKey(id); 
                foreach (var item in landOffices)
                {
                    SelectLandOffice.Add(new SelectListItem
                    {
                        Selected = _landOfficeRepository.IsExisting(item.LandOfficeCode),
                        Text = item.LandOfficeName.ToString(),
                        Value = item.LandOfficeCode.ToString(),
                    });
                }
                ViewBag.LandOffices = SelectLandOffice;
                return View(assetLand);
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddOrEdit([FromBody] AssetLand model)
        {
            var isExisting = _assetLandRepository.IsExisting(model.AssetLandId);
            if(isExisting)
            {
                await _assetLandRepository.Update(model.AssetLandId, model);
            }
            else
            {
                await _assetLandRepository.Create(model);
            }

            var assetLands = await GetAssetLands(model.CusId);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetLandCard", assetLands);
            return new JsonResult(new { isValid = true, html });
        }


        public IActionResult UploadImage(string id = "")
        {
            var assetImage = new AssetImageViewModel { AssetId= id };
            return View(assetImage);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] AssetImageViewModel vm)
        {
            try
            {
                
                var assetId = vm.AssetId;
                string wwwRootPath = _hostEnvironment.WebRootPath;
                foreach (var file in vm.Files)
                {
                    string extension = Path.GetExtension(file.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName) + extension;
                    string path = Path.Combine(wwwRootPath + ASSET_PATH, fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    var assetImage = new AssetImage
                    {
                        AssetId = assetId,
                        FileName= fileName,
                        ImagePath = ASSET_PATH + fileName,
                    };
                    await _assetImageRepository.Create(assetImage);
                }

                var assetLands = await GetAssetLands(vm.CusId);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetLandCard", assetLands);
                return new JsonResult(new { isValid = true,  html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });
            }
        }


        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id, string cusId)
        {
            try
            {
                await _assetLandRepository.Delete(id);

                var assetLands = await GetAssetLands(id);
                var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetLandCard", assetLands);
                return new JsonResult(new { isValid = true, html });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isValid = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetByCusId(string id="")
        {

            var assetLands = await GetAssetLands(id);
            var html = RenderRazorViewHelper.RenderRazorViewToString(this, "_AssetLandCard", assetLands);
            return new JsonResult(new { isValid = true,  html });
        }


        private async Task<List<AssetLandViewModel>> GetAssetLands(string id)
        {
            var assetLands = await _assetLandRepository.GetByCusId(id);
            foreach (var asset in assetLands)
            {
                asset.AssetImages = _assetImageRepository.GetByAssetId(asset.AssetLandId);
            }
            return assetLands;
        }

    }
}

