using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class ReferencerViewModel
	{
        [Display(Name = "รหัสบุคคลอ้างอิง")]
        public int ReferencerCode { get; set; }
        [Display(Name = "บุคคลอ้างอิง")]
        public string FullName { get; set; } = string.Empty;
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่")]
        public string Address { get; set; } = string.Empty;
        [Display(Name = "รายละเอียด(ตำบล,อำเภอ,จังหวัด,รหัสไปรษณีย์)")]
        public string AddressDetail { get; set; } = string.Empty;
        public string CusId { get; set; }
     
    }
}

