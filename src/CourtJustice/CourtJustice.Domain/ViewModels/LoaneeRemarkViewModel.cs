using System;
using CourtJustice.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
namespace CourtJustice.Domain.ViewModels
{
	public class LoaneeRemarkViewModel
	{
        
        public int LoaneeRemarkId { get; set; }
        public string Remark { get; set; }
        [Display(Name = "ID-CUST")]
        public string CusId { get; set; }
    }
}

