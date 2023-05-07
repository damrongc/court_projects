using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("car_type")]
    public class CarType
    {
        [Key]
        [Display(Name = "รหัสประเภทรถ")]
        public int CarTypeCode { get; set; }
        [Required]
        [Display(Name = "ประเภทรถ")]
        public string CarTypeName { get; set; } = string.Empty;
    }
}
