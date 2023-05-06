using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("guarantor")]
    public class Guarantor
    {
        [Key]
        [Display(Name = "รหัสผู้ค้ำประกัน")]
        public string GuarantorCode { get; set; } =string.Empty;
        [Display(Name = "ชื่อ")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "นามสกุล")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ปัจจุบัน")]
        public string Address { get; set; } = string.Empty;
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual AddressSet? AddressSet { set; get; }

        [Display(Name = "ที่อยู่จัดส่งเอกสาร")]
        public string Address1 { get; set; } = string.Empty;
        public int Address1Id { get; set; }
        [ForeignKey(nameof(Address1Id))]
        public virtual AddressSet? AddressSet1 { set; get; }

    }
}
