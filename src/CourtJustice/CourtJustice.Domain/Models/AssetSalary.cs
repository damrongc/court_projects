using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("asset_salary")]
    public class AssetSalary
    {
        [Key]
        public int AssetId { get; set; }
        [Display(Name = "บริษัท")] public string Company { get; set; } = string.Empty;
        [Display(Name = "เงินเดือน")] public decimal Salary { get; set; } = 0;
        [Display(Name = "วันที่เงินเดือนออก")] public DateOnly SalaryDate { get; set; }
        [Display(Name = "ที่อยู่บริษัท")] public string Address { get; set; } = string.Empty;
        [Display(Name = "รายละเอียด(ตำบล,อำเภอ,จังหวัด,รหัสไปรษณีย์)")]public string AddressDetail { get; set; } = string.Empty;
        public string CusId { get; set; }
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }

    }
}
