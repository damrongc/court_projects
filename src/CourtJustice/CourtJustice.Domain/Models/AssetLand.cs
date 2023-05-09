using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("asset_land")]
    public class AssetLand
    {
        [Key]
        [Display(Name = "โฉนดเลขที่")]
        public string AssetLandId { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ระวาง")]
        public string Position { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ที่ตั้ง")]
        public string Address { get; set; } = string.Empty;
        [Display(Name = "รายละเอียด(ตำบล,อำเภอ,จังหวัด,รหัสไปรษณีย์)")]
        public string AddressDetail { get; set; } = string.Empty;
        public string Gps { get; set; } = string.Empty;
        [Required]
        [Display(Name = "สำนักงานที่ดิน")]
        public string LandOfficeCode { get; set; } = string.Empty;
        [Display(Name = "ราคาประเมิน")]
        public decimal EstimatePrice { get; set; } = 0;

        //public int AddressId { get; set; }
        //[ForeignKey(nameof(AddressId))]
        //public virtual AddressSet? AddressSet { set; get; }

        [ForeignKey(nameof(LandOfficeCode))]
        public virtual LandOffice? LandOffice { set; get; }

        public string CusId { get; set; }
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }

    }
}
