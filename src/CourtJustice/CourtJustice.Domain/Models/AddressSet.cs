using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("address_set")]
    public class AddressSet 
    {
        [Key]
        public int AddressId { get; set; }
        public int ProvinceId { get; set; }
        [Display(Name = "จังหวัด")]
        public string ProvinceName { get; set; } = string.Empty;
        public int DistrictId { get; set; }
        [Display(Name = "อำเภอ")]
        public string DistrictName { get; set; } = string.Empty;
        public int SubDistrictId { get; set; }
        [Display(Name = "ตำบล")]
        public string SubDistrictName { get; set; } = string.Empty;
        [Display(Name = "รหัสไปรษณย์")]
        public int PostalCode { get; set; }
    }
}
