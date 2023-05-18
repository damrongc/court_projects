using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("guarantor")]
    public class Guarantor
    {
        [Key]
        [Display(Name = "เลขบัตร ปชช")]
        public string GuarantorCode { get; set; } =string.Empty;
        [Display(Name = "ผู้ค้ำ")]
        public string FullName { get; set; } = string.Empty;
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่")]
        public string Address { get; set; } = string.Empty;
        public string AddressDetail { get; set; } = string.Empty;
        //[Display(Name = "ที่อยู่ปัจจุบัน")]
        //public string CurrentAddress { get; set; } = string.Empty;
        //[Display(Name = "ที่อยู่จัดส่งเอกสาร")]
        //public string PostAddress { get; set; } = string.Empty;
        [Required]
        public string CusId { get; set; }
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }

    }
}
