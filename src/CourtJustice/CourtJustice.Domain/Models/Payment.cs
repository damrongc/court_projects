using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Domain.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
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
