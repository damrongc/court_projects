using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("law_office")]
    public class LawOffice : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสสำนักทนายความ")]
        public string LawOfficeCode { get; set; } = string.Empty;
        [Required]
        [Display(Name = "สำนักทนายความ")]
        public string LawOfficeName { get; set; } = string.Empty;
        //public string? Email { get; set; }
        //[Display(Name = "เบอร์ติดต่อ")]
        //public string? PhoneNumber { get; set; }
        //[Display(Name = "ที่อยู่")]
        //public string? Address { get; set; }
        //public int AddressId { get; set; }
        //[ForeignKey(nameof(AddressId))]
        //public virtual AddressSet? AddressSet { set; get; }

    }
}
