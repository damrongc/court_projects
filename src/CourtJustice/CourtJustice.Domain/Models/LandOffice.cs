using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("land_office")]
    public class LandOffice : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสสำนักงานที่ดิน")]
        public string LandOfficeCode { get; set; }=string.Empty;
        [Required]
        [Display(Name = "สำนักงานที่ดิน")]
        public string LandOfficeName { get; set; } = string.Empty;
        
    }
}
