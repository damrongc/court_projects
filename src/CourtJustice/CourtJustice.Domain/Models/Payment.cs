using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("payment")]
    public class Payment : BaseEntity
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
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }
        public DateOnly BookingDate { get; set; }
        public string StartOverdueStatus { get; set; }
        public string EndOverdueStatus { get; set; }

        [Required]
        public string EmployerCode { get; set; }
        public decimal WOBalance { get; set; }

    }
}
