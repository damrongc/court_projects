using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class AssetSalaryViewModel
	{

        [Display(Name = "รหัสสินทรัพย์")]
        public int AssetId { get; set; }
        [Display(Name = "บริษัท")]
        public string Company { get; set; } = string.Empty;
        [Display(Name = "เงินเดือน")]
        [DisplayFormat(DataFormatString = "{0:###,###.00}")]
        public decimal Salary { get; set; } = 0;
        [Display(Name = "วันที่เงินเดือนออก")]
        public DateTime SalaryDate { get; set; }
        [Display(Name = "ที่อยู่บริษัท")]
        public string Address { get; set; } = string.Empty;
        [Display(Name = "รายละเอียด(ตำบล,อำเภอ,จังหวัด,รหัสไปรษณีย์)")]
        public string AddressDetail { get; set; } = string.Empty;
        public string CusId { get; set; }
     

    }
}

