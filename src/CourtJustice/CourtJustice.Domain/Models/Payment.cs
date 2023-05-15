using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("payment")]
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Display(Name = "ครั้งที่ชำระ")]
        public int PaymentSeq { get; set; }
        [Display(Name = "วันที่ชำระ")]
        public DateOnly PaymentDate { get; set; }
        [Display(Name = "ค่างวด")]
        public decimal Amount { get; set; }
        [Display(Name = "ค่าปรับ")]
        public decimal Fee { get; set; }
        [Required]
        public string CusId { get; set; }
    }
}
