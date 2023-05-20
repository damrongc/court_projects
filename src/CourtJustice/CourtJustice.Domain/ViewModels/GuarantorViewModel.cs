using System.ComponentModel.DataAnnotations;
namespace CourtJustice.Domain.ViewModels
{
    public class GuarantorViewModel
	{

        [Display(Name = "รหัสผู้ค้ำประกัน")]
        public int GuarantorCode { get; set; }
        [Display(Name = "ผู้ค้ำ")]
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

