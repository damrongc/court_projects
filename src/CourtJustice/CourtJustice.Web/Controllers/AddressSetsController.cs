using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourtJustice.Web.Controllers
{
    public class AddressSetsController : BaseController<AddressSetsController>
    {
        private readonly IAddressSetRepository _addressSetRepository;

        public AddressSetsController(IAddressSetRepository addressSetRepository)
        {
            _addressSetRepository = addressSetRepository;
        }

        public IActionResult IndexAddressSetLookup()
        {
            return View();
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
                int recordsTotal = await _addressSetRepository.GetRecordCount( searchValue);

                var data = await _addressSetRepository.GetPaging(skip, pageSize, searchValue);
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IActionResult> GetAddressById(int id)
        {
            var address = await _addressSetRepository.GetById(id);
            return Ok(address);
        }

    }
}
