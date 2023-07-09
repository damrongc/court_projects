using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class AppUserViewModel
    {
        [Display(Name = "รหัสผู้ใช้")]
        public string UserId { get; set; }
        [Display(Name = "ชื่อผู้ใช้")]
        public string UserName { get; set;}
        public int  GroupId { get; set; }
        [Display(Name = "กลุ่มผู้ใช้งาน")]
        public string GroupName { get; set; }
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "สถานะ")]
        public bool IsActive { get; set; }
        [Display(Name = "ผู้ว่าจ้าง/ธนาคาร")]
        public string? EmployerCode { get; set; }
        [Display(Name = "หัวหน้า")]
        public string? ManagerId { get; set; }
        [Display(Name = "หัวหน้า")]
        public string? ManagerName { get; set; }
        [Display(Name = "เป้าหมาย")]
        public decimal Target { get; set; } = 0;
        [Required]
        [Display(Name = "รหัสผ่าน")]
        public string Password { get; set; }

        [Display(Name = "รหัสผ่านใหม่")]
        public string NewPassword { get; set; }

        [Display(Name = "ยืนยันรหัสผ่าน")]
        public string ConfirmPassword { get; set; }

        public bool SitePermission { get; set; }
    }
}
