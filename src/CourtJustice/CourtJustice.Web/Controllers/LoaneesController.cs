using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourtJustice.Web.Controllers
{
    public class LoaneesController : BaseController<LoaneesController>
    {
        private readonly ILoaneeRepository _loaneeRepository;
        private readonly IOccupationRepository _occupationRepository;

        public LoaneesController(ILoaneeRepository loaneeRepository, 
            IOccupationRepository occupationRepository)
        {
            _loaneeRepository = loaneeRepository;
            _occupationRepository = occupationRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var occupations = await _occupationRepository.GetAll();
            List<SelectListItem> SelectOccupations = new();
            foreach (var item in occupations)
            {
                SelectOccupations.Add(new SelectListItem
                {
                    Text = item.OccupationName.ToString(),
                    Value = item.OccupationId.ToString(),
                });
            }
            ViewBag.Occupations = SelectOccupations;
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
