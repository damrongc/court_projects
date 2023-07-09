using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("lawyer")]
    public class Lawyer : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสทนายความ")]
        public string LawyerCode { get; set; }
        [Required]
        [Display(Name = "ชื่อทนายความ")]
        public string LawyerName { get; set; }
        [Display(Name = "อีเมลล์")]
        public string? Email { get; set; }
        [Display(Name = "เบอร์ติดต่อ")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "ที่อยู่")]
        public string? Address { get; set; }
        [Display(Name = "รายละเอียด(ตำบล,อำเภอ,จังหวัด,รหัสไปรษณีย์)")]
        public string? AddressDetail { get; set; }

       /* public string? AddressNo { get; set; }
        public string? Village { get; set; }
        public string? Building { get; set; }
        public string? Floor { get; set; }
        public string? Substreet { get; set; }
        public string? Street { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int SubDistrictId { get; set; }*/

    }
}
