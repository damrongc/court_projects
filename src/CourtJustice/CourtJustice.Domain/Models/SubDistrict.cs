using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("sub_district")]
    public class SubDistrict
    {
        [Key]
        public int SubDistrictId { get; set; }
        public string SubDistrictName { get; set; } = string.Empty;
        public int PostalCode { get; set; }
        public int DistrictId { get; set; }
    }
}
