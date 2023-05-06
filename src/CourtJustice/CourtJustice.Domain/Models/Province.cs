using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("province")]
    public class Province
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; } = string.Empty;
        //[ForeignKey(nameof(DistrictId))]
        //public virtual District District { get; private set; }
    }
}
