using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class GroupUserViewModel
    {
        [Display(Name = "รหัส")]
        public int GroupId { get; set; }
        [Required]
        [Display(Name = "กลุ่มผู้ใช้")]
        public string GroupName { get; set; }
        public int GroupLevel { get; set; }
        //public string? EmployerCode { get; set; }
        //[Display(Name = "ผู้ว่าจ้าง/ธนาคาร")]
        //public string? EmployerName { get; set; }
        [Display(Name = "สถานะ")]
        public bool IsActive { get; set; } = true;
        [Display(Name = "ผู้บันทึก")]
        public string UserCreated { get; set; } = string.Empty;

        [Display(Name = "วันเวลาที่บันทึก")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime CreatedDateTime { get; set; } 
        [Display(Name = "ผู้แก้ไข")]
        public string? UserUpdated { get; set; }
        [Display(Name = "วันเวลาที่แก้ไข")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime? UpdatedDateTime { get; set; }
    }
}
