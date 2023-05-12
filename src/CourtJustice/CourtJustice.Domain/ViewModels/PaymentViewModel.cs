using System;
using CourtJustice.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CourtJustice.Domain.ViewModels
{
	public class PaymentViewModel
	{
        [Display(Name = "เลขที่การชำระ")]
        public int PaymentId { get; set; }
      
        [Display(Name = "เลขที่สัญญา")]
        public string LoanNumber { get; set; } = string.Empty;
        [Display(Name = "ครั้งที่ชำระ")]
        public int PaymentSeq { get; set; }
        [Display(Name = "วันที่ชำระ")]
        public DateOnly PaymentDate { get; set; }
        [Display(Name = "ค่างวด")]
        public decimal Amount { get; set; }
        [Display(Name = "ค่าปรับ")]
        public decimal Fee { get; set; }
    }
}

