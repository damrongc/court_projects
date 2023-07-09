using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class EmployeeViewModel
    {
        [Display(Name = "รหัสพนักงาน")]
        public string EmployeeCode { get; set; } = string.Empty;
        [Required]
        [Display(Name = "พนักงาน")]
        public string EmployeeName { get; set; } = string.Empty;
        [Display(Name = "อีเมล์ล")]
        public string? Email { get; set; } = string.Empty;
        [Display(Name = "เบอร์ติดต่อ")]
        public string? PhoneNumber { get; set; } = string.Empty;
        //[Display(Name = "วันที่จ้าง")]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        //public DateTime HireDate { get; set; }
        [Display(Name = "เป้าหมาย")]
        [DisplayFormat(DataFormatString = "{0:###,###.00}")]
        public decimal Target { get; set; } = 0;
        [Display(Name = "ที่อยู่")]
        public string? Address { get; set; } = string.Empty;
        [Display(Name = "รายละเอียด(ตำบล,อำเภอ,จังหวัด,รหัสไปรษณีย์)")]
        public string? AddressDetail { get; set; } = string.Empty;
        [Display(Name = "ผู้จัดการ")]
        public string ManagerCode { get; set; }
        public string ManagerName { get; set; }
        [Display(Name = "สถานะ")]
        public bool IsActive { get; set; }
    }
}
