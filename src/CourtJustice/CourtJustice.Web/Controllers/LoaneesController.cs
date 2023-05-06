using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourtJustice.Web.Controllers
{
    public class LoaneesController : BaseController<AddressSetsController>
    {
        private readonly ILoaneeRepository _loaneeRepository;

        public LoaneesController(ILoaneeRepository loaneeRepository)
        {
            _loaneeRepository = loaneeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new Loanee());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Loanee model)
        {
            if (ModelState.IsValid)
            {
                await _loaneeRepository.Create(model);
                _notify.Success($"{model.Name} is Created.");
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
                int recordsTotal = await _loaneeRepository.GetRecordCount(searchValue);

                var data = await _loaneeRepository.GetPaging(skip, pageSize, searchValue);
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch
            {
                throw;
            }
        }
    }
}
