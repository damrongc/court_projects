using CourtJustice.Domain;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourtJustice.Web.Controllers
{
  
    public class LoaneeRemarkController : BaseController<LoaneeRemarkController>
    {
        private readonly ILoaneeRemarkRepository _loaneeRemarkRepository;
       

        public LoaneeRemarkController(ILoaneeRemarkRepository loaneeRemarkRepository)
        {
            _loaneeRemarkRepository = loaneeRemarkRepository;
          
        }

        public IActionResult Index()
        {
            return View();
        }


        private async Task<List<LoaneeRemark>> GetAll()
        {
            var results = await _loaneeRemarkRepository.GetAll();
            return results.ToList();
        }
    }
}

