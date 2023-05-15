using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class PaymentViewModel
	{
        [Display(Name = "เลขที่การชำระ")]
        public int PaymentId { get; set; }
        [Display(Name = "ครั้งที่ชำระ")]
        public int PaymentSeq { get; set; }
        [Display(Name = "วันที่ชำระ")]
        public DateTime PaymentDate { get; set; }
        [Display(Name = "ค่างวด")]
        [DisplayFormat(DataFormatString = "{0:###,###.00}")]
        public decimal Amount { get; set; }
        [Display(Name = "ค่าปรับ")]
        [DisplayFormat(DataFormatString = "{0:###,###.00}")]
        public decimal Fee { get; set; }
        public string CusId { get; set; }
    }
}

